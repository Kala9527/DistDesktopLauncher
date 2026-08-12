# Dist Desktop Launcher

A small Windows desktop launcher that packages a frontend `dist` folder into a self-contained .NET executable. It embeds static files, starts a lightweight local server, and opens the app in the browser.

## What It Is For

- Ship Vite, Vue, React, Svelte, or other static frontend builds to non-technical users.
- Give a pure frontend tool a simple Windows launch experience.
- Run static web apps on machines without Node.js.

## Structure

```text
.
├─ DistLauncher.csproj
├─ Program.cs
├─ build-exe.bat
├─ start-dist-launcher.bat
└─ dist/                     # Local build input, ignored by Git
```

## Build And Deploy

1. Build your frontend app:

```bash
npm install
npm run build
```

2. Copy the generated `dist` folder into this project root.
3. Install .NET SDK 8.0 or later.
4. Run on Windows:

```bat
build-exe.bat
```

The packaged files are written to `release/`:

```text
release/
├─ DistDesktopLauncher.exe
└─ start-dist-launcher.bat
```

Send the `release` folder to users. They can double-click `start-dist-launcher.bat` to launch the app.

## Notes

- `dist/`, `bin/`, `obj/`, and `release/` are generated files and are not committed.
- The launcher starts at port `5123` and automatically tries later ports if needed.
- SPA deep links fall back to `dist/index.html`.
- Set `DIST_LAUNCHER_APP_NAME` to customize the console title.

## Thanks

Thank you for checking out this project. It was made to help frontend work feel easier to share and nicer to open. If it helps you, a Star, Fork, issue, or kind suggestion would mean a lot and will encourage me to keep improving it.
