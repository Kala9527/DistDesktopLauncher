using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

var appName = Environment.GetEnvironmentVariable("DIST_LAUNCHER_APP_NAME") ?? "Dist Desktop Launcher";
var server = new StaticDistServer(5123, 80);
var url = await server.StartAsync(CancellationToken.None);
var shouldOpenBrowser = !args.Any(arg => arg.Equals("--no-browser", StringComparison.OrdinalIgnoreCase));
var shouldRunSelfTest = args.Any(arg => arg.Equals("--self-test", StringComparison.OrdinalIgnoreCase));

if (shouldRunSelfTest)
{
    var passed = await RunSelfTestAsync(url);
    server.Stop();
    return passed ? 0 : 1;
}

Console.Title = appName;
Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine($"{appName} started.");
Console.WriteLine($"Local URL: {url}");
Console.WriteLine("Keep this window open while using the app. Closing it stops the local server.");
Console.WriteLine();

if (shouldOpenBrowser)
{
    OpenBrowser(url);
}

var lifetime = new TaskCompletionSource();
Console.CancelKeyPress += (_, eventArgs) =>
{
    eventArgs.Cancel = true;
    lifetime.TrySetResult();
};

await lifetime.Task;
server.Stop();
return 0;

static async Task<bool> RunSelfTestAsync(string baseUrl)
{
    using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
    var home = await client.GetStringAsync(baseUrl);
    var fallback = await client.GetStringAsync(new Uri(new Uri(baseUrl), "some/deep/link"));

    var assetPath = MatchAssetPath(home);
    var assetLength = 0;
    if (!string.IsNullOrWhiteSpace(assetPath))
    {
        var asset = await client.GetStringAsync(new Uri(new Uri(baseUrl), assetPath));
        assetLength = asset.Length;
    }

    var passed = home.Length > 0 && fallback.Length > 0;

    Console.WriteLine(passed ? "Self-test passed." : "Self-test failed.");
    Console.WriteLine($"Home: {home.Length} chars");
    Console.WriteLine(string.IsNullOrWhiteSpace(assetPath)
        ? "Asset: no linked asset found in index.html"
        : $"Asset: {assetPath} ({assetLength} chars)");
    Console.WriteLine($"SPA fallback: {fallback.Length} chars");

    return passed;
}

static string? MatchAssetPath(string html)
{
    var match = Regex.Match(html, """(?:src|href)=["']\.?/?(?<path>assets/[^"']+)["']""", RegexOptions.IgnoreCase);
    return match.Success ? match.Groups["path"].Value : null;
}

static void OpenBrowser(string url)
{
    try
    {
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Browser could not be opened automatically. Please visit: {url}");
        Console.WriteLine($"Reason: {ex.Message}");
    }
}

internal sealed class StaticDistServer
{
    private readonly int _preferredPort;
    private readonly int _portAttempts;
    private readonly Assembly _assembly = Assembly.GetExecutingAssembly();
    private readonly Dictionary<string, string> _resources;
    private TcpListener? _listener;
    private CancellationTokenSource? _stopSignal;

    public StaticDistServer(int preferredPort, int portAttempts)
    {
        _preferredPort = preferredPort;
        _portAttempts = portAttempts;
        _resources = _assembly
            .GetManifestResourceNames()
            .Where(name => name.StartsWith("dist/", StringComparison.OrdinalIgnoreCase) ||
                           name.StartsWith("dist\\", StringComparison.OrdinalIgnoreCase))
            .ToDictionary(NormalizeResourceName, name => name, StringComparer.OrdinalIgnoreCase);
    }

    public async Task<string> StartAsync(CancellationToken cancellationToken)
    {
        _listener = StartListener();
        _stopSignal = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _ = Task.Run(() => AcceptLoopAsync(_stopSignal.Token));
        await Task.Yield();

        var port = ((IPEndPoint)_listener.LocalEndpoint).Port;
        return $"http://127.0.0.1:{port}/";
    }

    public void Stop()
    {
        _stopSignal?.Cancel();
        _listener?.Stop();
    }

    private TcpListener StartListener()
    {
        for (var port = _preferredPort; port < _preferredPort + _portAttempts; port++)
        {
            try
            {
                var listener = new TcpListener(IPAddress.Loopback, port);
                listener.Start();
                return listener;
            }
            catch (SocketException)
            {
                // Try the next port when another local app already owns this one.
            }
        }

        var fallback = new TcpListener(IPAddress.Loopback, 0);
        fallback.Start();
        return fallback;
    }

    private async Task AcceptLoopAsync(CancellationToken cancellationToken)
    {
        if (_listener is null)
        {
            return;
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var client = await _listener.AcceptTcpClientAsync(cancellationToken);
                _ = Task.Run(() => HandleClientAsync(client, cancellationToken), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (SocketException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (ObjectDisposedException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to accept request: {ex.Message}");
            }
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        using var clientToDispose = client;
        client.ReceiveTimeout = 5000;
        client.SendTimeout = 5000;

        using var stream = client.GetStream();
        using var reader = new StreamReader(stream, Encoding.ASCII, detectEncodingFromByteOrderMarks: false, leaveOpen: true);

        var requestLine = await reader.ReadLineAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(requestLine))
        {
            return;
        }

        while (!string.IsNullOrEmpty(await reader.ReadLineAsync(cancellationToken)))
        {
        }

        var parts = requestLine.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
            await WriteTextAsync(stream, HttpStatusCode.BadRequest, "Bad Request", cancellationToken);
            return;
        }

        var method = parts[0].ToUpperInvariant();
        if (method is not ("GET" or "HEAD"))
        {
            await WriteTextAsync(stream, HttpStatusCode.MethodNotAllowed, "Method Not Allowed", cancellationToken);
            return;
        }

        var path = ResolvePath(parts[1]);
        if (!_resources.TryGetValue(path, out var resourceName))
        {
            resourceName = _resources.GetValueOrDefault("dist/index.html");
        }

        if (resourceName is null)
        {
            await WriteTextAsync(stream, HttpStatusCode.NotFound, "dist/index.html not found", cancellationToken);
            return;
        }

        await using var resourceStream = _assembly.GetManifestResourceStream(resourceName);
        if (resourceStream is null)
        {
            await WriteTextAsync(stream, HttpStatusCode.NotFound, "Resource not found", cancellationToken);
            return;
        }

        var contentType = GetContentType(path);
        await WriteHeaderAsync(stream, HttpStatusCode.OK, contentType, resourceStream.Length, cancellationToken);

        if (method == "GET")
        {
            await resourceStream.CopyToAsync(stream, cancellationToken);
        }
    }

    private static string ResolvePath(string rawTarget)
    {
        var path = rawTarget.Split('?', 2)[0].Split('#', 2)[0];
        path = Uri.UnescapeDataString(path).Replace('\\', '/').TrimStart('/');

        if (string.IsNullOrWhiteSpace(path))
        {
            return "dist/index.html";
        }

        if (path.Contains("..", StringComparison.Ordinal))
        {
            return "dist/index.html";
        }

        return $"dist/{path}";
    }

    private static string NormalizeResourceName(string resourceName)
    {
        return resourceName.Replace('\\', '/');
    }

    private static async Task WriteTextAsync(Stream stream, HttpStatusCode statusCode, string message, CancellationToken cancellationToken)
    {
        var bytes = Encoding.UTF8.GetBytes(message);
        await WriteHeaderAsync(stream, statusCode, "text/plain; charset=utf-8", bytes.Length, cancellationToken);
        await stream.WriteAsync(bytes, cancellationToken);
    }

    private static async Task WriteHeaderAsync(Stream stream, HttpStatusCode statusCode, string contentType, long contentLength, CancellationToken cancellationToken)
    {
        var reason = statusCode switch
        {
            HttpStatusCode.OK => "OK",
            HttpStatusCode.BadRequest => "Bad Request",
            HttpStatusCode.NotFound => "Not Found",
            HttpStatusCode.MethodNotAllowed => "Method Not Allowed",
            _ => statusCode.ToString()
        };
        var header = new StringBuilder()
            .Append("HTTP/1.1 ")
            .Append((int)statusCode)
            .Append(' ')
            .Append(reason)
            .Append("\r\nContent-Type: ")
            .Append(contentType)
            .Append("\r\nContent-Length: ")
            .Append(contentLength)
            .Append("\r\nCache-Control: no-cache")
            .Append("\r\nConnection: close")
            .Append("\r\n\r\n")
            .ToString();

        var bytes = Encoding.ASCII.GetBytes(header);
        await stream.WriteAsync(bytes, cancellationToken);
    }

    private static string GetContentType(string path)
    {
        return Path.GetExtension(path).ToLowerInvariant() switch
        {
            ".html" => "text/html; charset=utf-8",
            ".js" => "text/javascript; charset=utf-8",
            ".css" => "text/css; charset=utf-8",
            ".json" => "application/json; charset=utf-8",
            ".svg" => "image/svg+xml",
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".ico" => "image/x-icon",
            ".woff" => "font/woff",
            ".woff2" => "font/woff2",
            ".ttf" => "font/ttf",
            ".wasm" => "application/wasm",
            _ => "application/octet-stream"
        };
    }
}
