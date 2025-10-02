FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./src/GatewayService ./GatewayService

RUN dotnet restore ./GatewayService/GatewayService.sln

RUN dotnet build ./GatewayService/GatewayService.sln -c Release

WORKDIR /app