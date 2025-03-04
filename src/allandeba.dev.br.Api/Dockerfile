# docker buildx build --platform linux/amd64 -o type=docker -t webapi_local . --debug
# docker run -p 5025:8080 webapi_local

ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_VERSION AS base
WORKDIR /app
EXPOSE 8080 8081

FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_VERSION AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY ["src/allandeba.dev.br.Api/allandeba.dev.br.Api.csproj", "allandeba.dev.br.Api/"]
COPY ["src/allandeba.dev.br.Core/allandeba.dev.br.Core.csproj", "allandeba.dev.br.Core/"]
RUN dotnet restore "allandeba.dev.br.Api/allandeba.dev.br.Api.csproj"

COPY ["src/allandeba.dev.br.Api", "allandeba.dev.br.Api/"]
COPY ["src/allandeba.dev.br.Core", "allandeba.dev.br.Core/"]
WORKDIR /app/allandeba.dev.br.Api
RUN dotnet build "allandeba.dev.br.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
WORKDIR /app/allandeba.dev.br.Api
RUN dotnet publish "allandeba.dev.br.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "allandeba.dev.br.Api.dll"]
