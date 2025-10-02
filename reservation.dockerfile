FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/ReservationSystem ./ReservationSystem

WORKDIR /app

ENTRYPOINT ["dotnet", "./ReservationSystem/ReservationSystem.Web.Api/bin/Release/net8.0/ReservationSystem.Web.Api.dll"]