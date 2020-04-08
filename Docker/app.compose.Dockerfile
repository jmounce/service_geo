#---------------
# Build Image
#---------------
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

ARG PROJAPI="Geo.Service"
ARG PROJMODEL="Geo.Service.Model"
ARG BUILDCONFIG="Debug"
ARG PROGETFEED="http://proget.inflection.net/nuget/Inflection/"

WORKDIR /src
COPY $PROJAPI $PROJAPI
COPY $PROJMODEL $PROJMODEL

# Restore Packages
RUN dotnet restore "$PROJAPI/$PROJAPI.csproj" -s $PROGETFEED

# Build App
RUN dotnet build "$PROJAPI/$PROJAPI.csproj" -c $BUILDCONFIG -o /app

# Publish App
RUN dotnet publish "$PROJAPI/$PROJAPI.csproj" -c $BUILDCONFIG -o /app

#---------------
# Runtime Image
#---------------
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
EXPOSE 80

# Install API
WORKDIR /app
COPY --from=build /app .

# Run API
ENV ASPNETCORE_ENVIRONMENT="DevelopmentDocker"

ENTRYPOINT ["dotnet", "Geo.Service.dll"]
