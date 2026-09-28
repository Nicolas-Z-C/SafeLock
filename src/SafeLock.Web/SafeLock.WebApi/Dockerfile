# syntax=docker/dockerfile:1

# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Safelock.sln", "."]
COPY ["src/Safelock.Domain/Safelock.Domain.csproj", "src/Safelock.Domain/"]
COPY ["src/Safelock.Application/Safelock.Application.csproj", "src/Safelock.Application/"]
COPY ["src/Safelock.Infrastructure/Safelock.Infrastructure.csproj", "src/Safelock.Infrastructure/"]
COPY ["src/Safelock.Shared/Safelock.Shared.csproj", "src/Safelock.Shared/"]
COPY ["src/Safelock.Web/Safelock.WebApi/Safelock.WebApi.csproj", "src/Safelock.Web/Safelock.WebApi/"]
COPY ["Directory.Packages.props", "."]

RUN dotnet restore "src/Safelock.Web/Safelock.WebApi/Safelock.WebApi.csproj"

COPY . .
WORKDIR "/src/src/Safelock.Web/Safelock.WebApi"
RUN dotnet publish -c Release -o /app/publish --no-restore

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

RUN adduser --disabled-password --gecos "" appuser
USER appuser

EXPOSE 8080
ENTRYPOINT ["dotnet", "Safelock.WebApi.dll"]