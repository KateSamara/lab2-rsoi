FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/LibrarySystem ./LibrarySystem

WORKDIR /app

ENTRYPOINT ["dotnet", "./LibrarySystem/LibrarySystem.Web.Api/bin/Release/net8.0/LibrarySystem.Web.Api.dll"]