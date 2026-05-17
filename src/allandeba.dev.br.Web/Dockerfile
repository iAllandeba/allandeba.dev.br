# docker buildx build --platform linux/arm64 -o type=docker -t web_local . --debug
# docker run -p 5279:8080 web_local

ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_VERSION AS base
WORKDIR /app
EXPOSE 8080 8081

FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_VERSION AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

RUN apt-get update && apt-get install -y python3 --no-install-recommends && rm -rf /var/lib/apt/lists/*
RUN dotnet workload install wasm-tools

COPY ["src/allandeba.dev.br.Web/allandeba.dev.br.Web.csproj", "allandeba.dev.br.Web/"]
COPY ["src/allandeba.dev.br.Core/allandeba.dev.br.Core.csproj", "allandeba.dev.br.Core/"]
RUN dotnet restore "allandeba.dev.br.Web/allandeba.dev.br.Web.csproj"

COPY ["src/allandeba.dev.br.Web", "allandeba.dev.br.Web/"]
COPY ["src/allandeba.dev.br.Core", "allandeba.dev.br.Core/"]

# Substitui a url do backend conforme o ambiente através do docker build --build-arg
ARG ACTUAL_BACKEND_URL
ARG ENVIRONMENT_BACKEND_URL
WORKDIR /app/allandeba.dev.br.Web
RUN sed -i "s#${ACTUAL_BACKEND_URL}#${ENVIRONMENT_BACKEND_URL}#g" ./wwwroot/appsettings.json
RUN dotnet build "allandeba.dev.br.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
WORKDIR /app/allandeba.dev.br.Web
RUN dotnet publish "allandeba.dev.br.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM nginx:alpine AS final
COPY ["src/allandeba.dev.br.Web/nginx.conf", "/etc/nginx/conf.d/default.conf"]
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot ./