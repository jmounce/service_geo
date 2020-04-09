#---------------
# Build Image
#---------------
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

ARG PROJAPI="Geo.Service"
ARG PROJMODEL="Geo.Service.Model"
ARG BUILDCONFIG="Release"
ARG PROGETFEED="https://inflection.jfrog.io/artifactory/api/nuget/inflectionnuget/"

COPY $PROJAPI $PROJAPI
COPY $PROJMODEL $PROJMODEL
COPY version.txt "$PROJAPI/wwwroot/"

# Restore Packages
RUN dotnet restore "$PROJAPI/$PROJAPI.csproj" -s $PROGETFEED -s "https://api.nuget.org/v3/index.json"
RUN dotnet restore "$PROJMODEL/$PROJMODEL.csproj" -s $PROGETFEED -s "https://api.nuget.org/v3/index.json"

# Build App
RUN dotnet build "$PROJAPI/$PROJAPI.csproj" -c $BUILDCONFIG -o /app

# Publish App
RUN dotnet publish "$PROJAPI/$PROJAPI.csproj" -c $BUILDCONFIG -o /app

#---------------
# Runtime Image
#---------------
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
EXPOSE 80

## Install AppD 
ARG PROGETAPPDFEED="https://proget.inflection.net/endpoints/Appd-libs/content"
ARG APPDTAR="Appdlib.tar"
ARG APPDINSTALLDIR="/opt/appdynamics/dotnet"

RUN mkdir -p $APPDINSTALLDIR \
	&& cd $APPDINSTALLDIR \
	&& curl -O $PROGETAPPDFEED/$APPDTAR \
	&& tar -xf $APPDTAR \
	&& rm $APPDTAR

# Install API
WORKDIR /app
COPY --from=build /app .

# Run API
ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ARG vault_url
ENV vault_url=$vault_url

CMD export vault_token="$(cat /run/vault/vault_token)" \
	&& dotnet $PROJAPI.dll
#CMD /bin/bash
