FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./src/ReservationSystem ./ReservationSystem

RUN dotnet restore ./ReservationSystem/ReservationSystem.sln

RUN dotnet build ./ReservationSystem/ReservationSystem.sln -c Release

WORKDIR /app