FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/GatewayService ./GatewayService

WORKDIR /app

ENTRYPOINT ["dotnet", "./GatewayService/GatewayService.Web.Api/bin/Release/net8.0/GatewayService.Web.Api.dll"]