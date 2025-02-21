# FastWiki
## 介绍

本项目是一个高性能、基于最新技术栈的知识库系统，专为大规模信息检索和智能搜索设计。利用微软Semantic Kernel进行深度学习和自然语言处理，结合.NET 8和MasaBlazor于react并存框架，后台采用MasaFramework，实现了一个高效、易用、可扩展的智能向量搜索平台。我们的目标是提供一个能够理解和处理复杂查询的智能搜索解决方案，帮助用户快速准确地获取所需信息。

## 技术栈

- 前端框架：MasaBlazor于react并存
- 后端框架：MasaFramework 基于 .NET 8
- 向量搜索引擎：使用 PostgreSQL 的向量插件，优化搜索性能
- 深度学习与NLP：微软Semantic Kernel，提升搜索的语义理解能力
- 许可证：Apache-2.0，鼓励社区贡献和使用

## 特点

- 智能搜索：借助Semantic Kernel的深度学习和自然语言处理技术，能够理解复杂查询，提供精准的搜索结果。
- 高性能：通过pgsql的向量插件优化向量搜索性能，确保即使在大数据量下也能快速响应。
- 现代化前端：使用MasaBlazor前端框架，提供响应式设计和用户友好的界面。
- 强大的后端：基于最新的.NET 8和MasaFramework，确保了代码的高效性和可维护性。
- 开源和社区驱动：采用Apache-2.0许可证，鼓励开发者和企业使用和贡献。

## 快速开始

### 先决条件

确保你已经安装了.NET 8 SDK和PostgreSQL数据库，并且配置了相应的环境。

### 安装

1. 克隆仓库：

```
git clone https://github.com/239573049/fast-wiki.git
```

2. 安装依赖项：

在项目根目录下执行：

```
dotnet restore
```

3. 数据库配置：

确保你的PostgreSQL数据库运行正常，并且创建了必要的数据库。根据你的配置修改`appsettings.json`中的数据库连接字符串。

### 运行

在项目根目录下执行：

```
dotnet run
```

这将启动后端服务。对于前端部分，请参照MasaBlazor文档，进行相应的构建和启动步骤。

默认账号密码：admin Aa123456

## 环境变量参数

FastWikiService环境变量参数：
- QUANTIZE_MAX_TASK：量化任务的最大并发数，默认为3
- OPENAI_CHAT_ENDPOINT：OpenAI API的地址
- OPENAI_CHAT_EMBEDDING_ENDPOINT： Embedding API的地址
- OPENAI_CHAT_TOKEN： OpenAI API的Token
- OPENAI_CHAT_MODEL： 对话的模型，默认gpt-3.5-turbo
- OPENAI_EMBEDDING_MODEL： Embedding的模型，默认text-embedding-3-small
- OPENAI_EMBEDDING_TOKEN: Embedding的Token, 默认为空，为空则使用对话的Token

FastWiki.Web.Server环境变量参数：
- FAST_WIKI_SERVICE：FastWikiService的地址，默认为http://localhost:5124

## 技术交流
![输入图片说明](img/wechat.png)

## 贡献指南

我们欢迎所有形式的贡献，无论是功能请求、bug报告、代码提交、文档或是其他类型的支持。请参阅`CONTRIBUTING.md`了解如何开始。

## 许可证

本项目采用Apache-2.0许可证。详情请见[LICENSE](LICENSE)文件。
