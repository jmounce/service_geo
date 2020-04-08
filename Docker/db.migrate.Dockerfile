FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY Geo.Service/Geo.Service Geo.Service/
COPY Geo.Service.Model/Geo.Service.Model/ Geo.Service.Model/
COPY Geo.Service.sln Geo.Service.sln

RUN dotnet restore Geo.Service/Geo.Service.csproj -s https://api.nuget.org/v3/index.json -s http://proget.inflection.net/nuget/Inflection/
RUN dotnet restore Geo.Service.Model/Geo.Service.Model.csproj -s https://api.nuget.org/v3/index.json -s http://proget.inflection.net/nuget/Inflection/

WORKDIR /src/Geo.Service
RUN dotnet build Geo.Service.csproj -c Release -o /app

ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ARG vault_url
ENV vault_url=$vault_url
ARG vault_token
ENV vault_token=$vault_token

RUN dotnet ef database update
