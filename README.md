# Dist Desktop Launcher

> Package any frontend dist folder into a self-contained Windows desktop launcher.  
> 中文：把任意前端 dist 静态目录打包成可双击运行的 Windows 桌面启动器。

This repository is packaged to be easy to **star, fork, run, remix, and contribute to**. It keeps the first screen English-first for global GitHub discovery, while preserving a Chinese guide below.

## Why Star This

- Practical project idea with a clear real-world use case.
- Small enough to fork, study, and customize quickly.
- English-first bilingual README for both global and Chinese-speaking developers.
- Clean setup instructions, project structure, roadmap, and contribution entry points.
- Built around popular GitHub themes such as AI tools, TypeScript, developer tools, local-first apps, automation, and indie-friendly workflows when relevant.

## What It Does

Package any frontend dist folder into a self-contained Windows desktop launcher.

## Highlights

- Embeds static dist assets into a Windows executable
- Starts a lightweight local web server automatically
- Opens the browser to the packaged app
- SPA fallback support for frontend routes
- Useful for Vue, React, Svelte, and Vite tools

## Tech Stack

`	ext
C#, .NET, Windows, static web hosting
`

## Quick Start

`ash
dotnet restore`n# Put your frontend build into ./dist first`nbuild-exe.bat
`

## Project Structure

`	ext
.
|-- src/ or app/          Main source code
|-- public/ or assets/    Static assets when available
|-- docs/                 Notes, specs, or deployment docs when available
|-- README.md             English-first bilingual project guide
-- package / project files
`

## Deployment / Packaging

- Do not commit generated builds, local databases, API keys, private logs, or large media files.
- For frontend projects, deploy the production dist/ folder to GitHub Pages, Vercel, Netlify, Nginx, or package it with DistDesktopLauncher.
- For desktop/mobile projects, publish only release artifacts from a clean build environment.
- Keep configuration examples public and real credentials private.

## Roadmap

- [ ] Tray icon mode
- [ ] Custom icon and app metadata wizard
- [ ] Release zip generation
- [ ] macOS/Linux equivalents

## Contributing

Issues and pull requests are welcome. Useful contributions include better screenshots, demos, docs, templates, presets, provider guides, compatibility fixes, tests, and translations.

If this project helps you, a star and fork make it easier for more people to discover it.

---

# 中文说明

> 把任意前端 dist 静态目录打包成可双击运行的 Windows 桌面启动器。

这个仓库已经改成 **英文优先、中文在后** 的双语 README，方便 GitHub 全球用户第一眼理解项目，同时保留中文开发者阅读体验。

## 为什么值得 Star / Fork

- 目标场景清晰，不是空壳项目。
- 项目规模适合学习、二次开发和快速改造。
- README、路线图、贡献入口和部署说明更完整。
- topics 会尽量贴近当前 GitHub 热门方向，例如 AI、LLM、OpenAI-compatible、TypeScript、developer-tools、automation、local-first、gamedev 等。

## 功能亮点

- Embeds static dist assets into a Windows executable
- Starts a lightweight local web server automatically
- Opens the browser to the packaged app
- SPA fallback support for frontend routes
- Useful for Vue, React, Svelte, and Vite tools

## 快速开始

`ash
dotnet restore`n# Put your frontend build into ./dist first`nbuild-exe.bat
`

## 部署与安全

- 不要提交 .env、API Key、生成媒体、大型文件、数据库、日志和构建产物。
- 前端项目可以部署 dist/ 到 GitHub Pages、Vercel、Netlify 或 Nginx。
- 桌面/移动端项目建议只发布干净环境构建出来的 release 文件。

## 后续计划

- [ ] Tray icon mode
- [ ] Custom icon and app metadata wizard
- [ ] Release zip generation
- [ ] macOS/Linux equivalents
