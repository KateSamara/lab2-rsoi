FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/GatewayService ./GatewayService

RUN dotnet restore ./GatewayService/GatewayService.sln

RUN dotnet build ./GatewayService/GatewayService.sln -c Release

WORKDIR /app