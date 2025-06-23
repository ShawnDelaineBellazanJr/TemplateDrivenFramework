# Multi-stage build for StrangeLoopPlatform API
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["StrangeLoopPlatform.sln", "./"]
COPY ["StrangeLoopPlatform.Api/StrangeLoopPlatform.Api.csproj", "StrangeLoopPlatform.Api/"]
COPY ["StrangeLoopPlatform.Core/StrangeLoopPlatform.Core.csproj", "StrangeLoopPlatform.Core/"]
COPY ["StrangeLoopPlatform.Agents/StrangeLoopPlatform.Agents.csproj", "StrangeLoopPlatform.Agents/"]
COPY ["StrangeLoopPlatform.Infrastructure/StrangeLoopPlatform.Infrastructure.csproj", "StrangeLoopPlatform.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "StrangeLoopPlatform.Api/StrangeLoopPlatform.Api.csproj"

# Copy source code
COPY . .

# Build the application
WORKDIR "/src/StrangeLoopPlatform.Api"
RUN dotnet build "StrangeLoopPlatform.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "StrangeLoopPlatform.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM base AS final
WORKDIR /app

# Copy published application
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Create non-root user for security
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost/health || exit 1

ENTRYPOINT ["dotnet", "StrangeLoopPlatform.Api.dll"] 