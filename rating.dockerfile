FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./src/RatingSystem ./RatingSystem

RUN dotnet restore ./RatingSystem/RatingSystem.sln

RUN dotnet build ./RatingSystem/RatingSystem.sln -c Release

WORKDIR /app