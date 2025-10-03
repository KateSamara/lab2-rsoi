FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/ReservationSystem ./ReservationSystem

WORKDIR /app/ReservationSystem

RUN dotnet publish --configuration Release --runtime linux-x64 --self-contained true --output /app

WORKDIR /app

ENTRYPOINT ["/app/ReservationSystem.Web.Api"]