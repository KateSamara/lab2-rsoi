FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy

WORKDIR /app

COPY ./src/LibrarySystem ./LibrarySystem

WORKDIR /app/LibrarySystem

RUN dotnet publish --configuration Release --runtime linux-x64 --self-contained true --output /app

WORKDIR /app

ENTRYPOINT ["/app/LibrarySystem.Web.Api"]