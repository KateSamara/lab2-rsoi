FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/LibrarySystem ./LibrarySystem

WORKDIR /app