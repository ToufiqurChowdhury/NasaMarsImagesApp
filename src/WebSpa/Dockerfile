#
# 1. DotNet Build
#

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /build
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebSpa/WebSpa.csproj", "WebSpa/"]
RUN dotnet restore "WebSpa/WebSpa.csproj"
COPY . .
WORKDIR "/src/WebSpa"
RUN dotnet build "WebSpa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebSpa.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "angular-dotnet.dll"]

#
# 2. Angular Build
#
FROM node:10.15-alpine AS client
WORKDIR /ClientApp
COPY /ClientApp ./
RUN npm install
RUN npm run-script build 