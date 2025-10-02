FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/GatewayService ./GatewayService

WORKDIR /app