# syntax=docker/dockerfile:1

# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Safelock.sln", "."]
COPY ["src/Safelock.Shared/Safelock.Shared.csproj", "src/Safelock.Shared/"]
COPY ["src/Safelock.Web/Safelock.WebAssembly/Safelock.WebAssembly.csproj", "src/Safelock.Web/Safelock.WebAssembly/"]
COPY ["Directory.Packages.props", "."]

RUN dotnet restore "src/Safelock.Web/Safelock.WebAssembly/Safelock.WebAssembly.csproj"

COPY . .
WORKDIR "/src/src/Safelock.Web/Safelock.WebAssembly"
RUN dotnet publish -c Release -o /app/publish --no-restore

# Etapa 2: servir estático con nginx
FROM nginx:alpine AS final
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
EXPOSE 80