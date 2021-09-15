#
# 1. DotNet Build
#

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
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

#
# 2. Angular Build
#

FROM 311303914270.dkr.ecr.us-west-2.amazonaws.com/node:latest-lts AS angular_build
WORKDIR /build/src/WebSpa/ClientApp/
COPY /src/WebSpa/ClientApp/package*.json ./
COPY /src/WebSpa/ClientApp/.npmrc ./
# use single RUN command to install node modules and then remove the .npmrc which has the auth token for the private package feed
RUN npm ci && \
    rm -f .npmrc

### spa build
WORKDIR /build/src/WebSpa/ClientApp
COPY /src/WebSpa/ClientApp ./
RUN rm -f .npmrc* && \
    npm run build-prod

#
# 3. merge angular and dotnet build output and create the final container of the published app with the dotnet runtime and none of the temporary build and test files
#
FROM base AS final
WORKDIR /app
COPY --chown=appuser --from=builder /app .
COPY --chown=appuser --from=angular_build /build/src/WebSpa/ClientApp/dist ./ClientApp/dist

ENTRYPOINT ["./NasaMarsImagesApp.WebSpa"]
CMD []
