# Dist Desktop Launcher

[中文说明](./README.cn.md)

> Tiny Windows launcher that serves a packaged static dist folder with SPA fallback and self-test support.

![csharp](https://img.shields.io/badge/csharp-111827?style=flat-square) ![dotnet](https://img.shields.io/badge/dotnet-111827?style=flat-square) ![desktop-app](https://img.shields.io/badge/desktop-app-111827?style=flat-square) ![static-server](https://img.shields.io/badge/static-server-111827?style=flat-square) ![packaging](https://img.shields.io/badge/packaging-111827?style=flat-square)

## Showcase

![Dist Desktop Launcher showcase](./docs/images/github-showcase.png)

## Highlights

- csharp
- dotnet
- desktop app
- static server
- packaging
- Practical project structure for learning, demos, and remixing.
- Local-first setup where secrets, generated files, and build output stay out of Git.

## Quick Start

```bash
dotnet build
dotnet run -- --self-test
```

## Project Structure

```text
.
|-- src/ or app/          Main source code
|-- public/ or assets/    Static assets when available
|-- docs/                 Screenshots, notes, or deployment docs
|-- README.md             GitHub landing README
|-- README.en.md          English documentation
`-- README.cn.md          Chinese documentation
```

## Roadmap

- [ ] Add more real-world examples and screenshots.
- [ ] Expand tests or smoke checks for the primary workflow.
- [ ] Publish clean release artifacts where the project type supports it.
- [ ] Keep documentation friendly for new contributors.

## Contributing

Issues and pull requests are welcome. Useful contributions include screenshots, demos, docs, templates, presets, compatibility fixes, tests, and translations.

If this project helps you, a star and fork make it easier for more people to discover it.
