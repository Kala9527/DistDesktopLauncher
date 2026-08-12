# Dist Desktop Launcher

一个把前端 `dist` 静态构建目录打包成 Windows 桌面启动器的小工具。它会把 `dist/` 中的文件嵌入到一个 .NET 单文件程序里，启动后在本机开启轻量静态服务，并自动打开浏览器访问应用。

## 适合用来做什么

- 把 Vite、Vue、React、Svelte 等前端项目的 `dist` 交给非技术用户双击运行。
- 给纯前端工具做一个简单的 Windows 启动壳。
- 在没有 Node.js 环境的电脑上分发静态 Web 应用。

## 项目结构

```text
.
├─ DistLauncher.csproj
├─ Program.cs
├─ build-exe.bat
├─ start-dist-launcher.bat
└─ dist/                     # 待打包的前端构建产物，本仓库默认不提交
```

## 使用方式

1. 先在你的前端项目里构建：

```bash
npm install
npm run build
```

2. 把生成的 `dist` 文件夹复制到本项目根目录。
3. 安装 .NET SDK 8.0 或更高版本。
4. 在 Windows 上运行：

```bat
build-exe.bat
```

5. 发布结果会出现在 `release/`：

```text
release/
├─ DistDesktopLauncher.exe
└─ start-dist-launcher.bat
```

把 `release` 文件夹发给用户即可。用户双击 `start-dist-launcher.bat`，浏览器会自动打开本地地址。

## 部署说明

本项目面向 Windows 桌面分发，不需要服务器部署。构建出的 `DistDesktopLauncher.exe` 是自包含单文件程序，目标机器不需要安装 Node.js，也不需要保留源码。

如果你要换一个前端应用，只需要替换根目录的 `dist/` 后重新运行 `build-exe.bat`。

## 注意事项

- `dist/`、`bin/`、`obj/`、`release/` 都是构建或打包产物，不提交到 GitHub。
- 默认从 `5123` 端口开始寻找可用端口，如果端口被占用会自动尝试后续端口。
- 支持 SPA 深链接回退：找不到静态文件时会返回 `dist/index.html`。
- 可设置环境变量 `DIST_LAUNCHER_APP_NAME` 改变控制台窗口名称。

## 感谢与支持

感谢你愿意看到这里。这个小工具来自一次次把前端作品交到别人手里时的真实需求：希望作品不只是能跑，还能更体面、更容易被打开。如果它帮到了你，欢迎点一个 Star、Fork 或提出建议，你的支持会让我更有动力继续把这些小工具打磨好。
