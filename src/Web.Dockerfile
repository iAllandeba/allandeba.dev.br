# docker buildx build --platform linux/arm64 -o type=docker -f Web.Dockerfile -t web_local . --debug
# docker run -p 5279:8080 web_local

ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_VERSION AS base
WORKDIR /app
EXPOSE 8080 8081

FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_VERSION AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY ./allandeba.dev.br.Web/*.csproj ./allandeba.dev.br.Web/
COPY ./allandeba.dev.br.Core/*.csproj ./allandeba.dev.br.Core/
RUN dotnet restore "allandeba.dev.br.Web/allandeba.dev.br.Web.csproj"

COPY ./allandeba.dev.br.Web/ ./allandeba.dev.br.Web/
COPY ./allandeba.dev.br.Core/ ./allandeba.dev.br.Core/
RUN dotnet build "allandeba.dev.br.Web/allandeba.dev.br.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
WORKDIR /app
RUN dotnet publish "allandeba.dev.br.Web/allandeba.dev.br.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM nginx:alpine AS final
COPY ./allandeba.dev.br.Web/nginx.conf /etc/nginx/conf.d/default.conf
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot ./