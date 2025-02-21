---
sidebar_position: 3
---

# 使用Docker-Compose快速构建

在项目根目录下创建`docker-compose.yml`文件，内容如下：

```yaml
version: '3.8'  # 可以根据需要使用不同的版本
services:
  fast-wiki-service:
    image: registry.cn-shenzhen.aliyuncs.com/fast-wiki/fast-wiki-service
    container_name: fast-wiki-service
    user: root
    restart: always
    ports:
      - "8080:8080"
    build: 
      context: .
      dockerfile: ./src/Service/FastWiki.Service/Dockerfile
    depends_on:
      - postgres
    volumes:
      - ./wwwroot/uploads:/app/wwwroot/uploads
    environment:
      - OPENAI_CHAT_ENDPOINT=https://api.openai.com
      - OPENAI_CHAT_EMBEDDING_ENDPOINT=https://api.openai.com
      - OPENAI_CHAT_TOKEN={您的TokenKey}
      - OPENAI_CHAT_MODEL=gpt-3.5-turbo
      - OPENAI_EMBEDDING_MODEL=text-embedding-3-small 
      - ASPNETCORE_ENVIRONMENT=Development

  postgres: # 当前compose服务名
    image: registry.cn-shenzhen.aliyuncs.com/fast-wiki/pgvector:v0.5.0 # 拉取的数据库镜像
    container_name: postgres  # 容器运行的容器名称
    restart: always  # 开机自启动
    environment:  # 环境变量
      POSTGRES_USER: token  # 默认账号
      POSTGRES_PASSWORD: dd666666 # 默认密码
      POSTGRES_DB: wiki # 默认数据库
      TZ: Asia/Shanghai  # 数据库时区
    volumes:
      - ./postgresql:/var/lib/postgresql/data # 将PostgreSql数据持久化
```

修改`docker-compose.yml`中的`{您的TokenKey}`为您的OpenAI Key，如果需要使用自己的代理则修改`OPENAI_CHAT_ENDPOINT`和`OPENAI_CHAT_EMBEDDING_ENDPOINT`为自己的代理地址

```shell
docker-compose up -d
```

即可快速构建并启动服务。
