FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./src/LibrarySystem ./LibrarySystem

WORKDIR /app