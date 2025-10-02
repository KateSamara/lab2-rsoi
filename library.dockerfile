FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./src/LibrarySystem ./LibrarySystem

RUN dotnet restore ./LibrarySystem/LibrarySystem.sln

RUN dotnet build ./LibrarySystem/LibrarySystem.sln -c Release

WORKDIR /app