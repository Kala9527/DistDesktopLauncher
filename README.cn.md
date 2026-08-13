# Dist 桌面启动器

[English README](./README.en.md)

> 把静态 dist 目录打包成本地桌面启动器，支持 SPA 回退、自动端口和自检。

![csharp](https://img.shields.io/badge/csharp-111827?style=flat-square) ![dotnet](https://img.shields.io/badge/dotnet-111827?style=flat-square) ![desktop-app](https://img.shields.io/badge/desktop-app-111827?style=flat-square) ![static-server](https://img.shields.io/badge/static-server-111827?style=flat-square) ![packaging](https://img.shields.io/badge/packaging-111827?style=flat-square)

## 项目展示

![Dist 桌面启动器 展示图](./docs/images/github-showcase.png)

## 为什么值得 Star / Fork

- 项目目标清晰，不是空壳仓库。
- README 首屏有真实截图或基于真实功能的产品展示图，访客能快速理解项目。
- 代码规模适合学习、二次开发和快速改造。
- 同时维护英文与中文说明，方便 GitHub 全球用户和中文开发者阅读。

## 功能亮点

- csharp
- dotnet
- desktop app
- static server
- packaging
- 保持本地优先：密钥、生成文件、构建产物和本机缓存不进入 Git。

## 快速开始

```bash
dotnet build
dotnet run -- --self-test
```

## 项目结构

```text
.
|-- src/ 或 app/          主要源码
|-- public/ 或 assets/    静态资源
|-- docs/                 截图、说明或部署文档
|-- README.md             GitHub 首屏入口
|-- README.en.md          英文说明
`-- README.cn.md          中文说明
```

## 后续计划

- [ ] 补充更多真实使用示例和截图。
- [ ] 为核心工作流增加测试或 smoke check。
- [ ] 在适合的项目中发布干净的 release 成品。
- [ ] 持续优化文档，让新贡献者更容易上手。

欢迎提交 Issue 和 PR。如果这个项目帮到了你，Star 和 Fork 能让更多人更容易发现它。
