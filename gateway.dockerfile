FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/GatewayService ./GatewayService

WORKDIR /app/GatewayService

RUN dotnet publish --configuration Release --runtime linux-x64 --self-contained true --output /app

WORKDIR /app

ENTRYPOINT ["GatewayService.Web.Api"]