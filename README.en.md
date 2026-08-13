# Dist Desktop Launcher

[中文说明](./README.cn.md)

> Package any frontend dist folder into a self-contained Windows desktop launcher.  

This repository is packaged to be easy to **star, fork, run, remix, and contribute to**. It keeps a dedicated English version for global GitHub discovery, with a separate Chinese version linked above.

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




