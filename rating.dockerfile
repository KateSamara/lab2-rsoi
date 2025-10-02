FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/RatingSystem ./RatingSystem

WORKDIR /app

ENTRYPOINT ["dotnet", "./RatingSystem/RatingSystem.Web.Api/bin/Release/net8.0/RatingSystem.Web.Api.dll"]