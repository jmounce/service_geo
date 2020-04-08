#---------------
# Build Image
#---------------
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

ARG PROJAPI="Geo.Service"
ARG PROJMODEL="Geo.Service.Model"
ARG BUILDCONFIG="Release"
ARG PROGETFEED="http://proget.inflection.net/nuget/Inflection/"

WORKDIR /src
COPY $PROJAPI $PROJAPI
COPY $PROJMODEL $PROJMODEL

# Restore Packages
RUN dotnet restore "$PROJAPI/$PROJAPI.csproj" -s $PROGETFEED

# Build App
RUN dotnet build "$PROJAPI/$PROJAPI.csproj" -c $BUILDCONFIG -o /app

WORKDIR /src/$PROJAPI

# Build/Migrate DB
ARG ASPNETCORE_ENVIRONMENT="Development"
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ARG vault_url
ENV vault_url=$vault_url

CMD export vault_token="$(cat /run/vault/vault_token)" \
	&& dotnet ef database update
#CMD /bin/bash
