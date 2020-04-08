# This image is deprecated. New images at: https://hub.docker.com//microsoft-dotnet-core-sdk/
# Need alpine since it has apk installed, we should probably create our own images..
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
# Install OpenJDK
RUN apk add --no-cache --update openjdk8-jre nss
# Install the sonarscanner tool
RUN dotnet tool install -g dotnet-sonarscanner

# Need to specify the tools path in a container
ENV PATH="${PATH}:/root/.dotnet/tools"

ARG PROJAPI="Geo.Service"
ARG PROJAPITEST="$PROJAPI.Test"
ARG PROJMODEL="Geo.Service.Model"
ARG PROJMODELTEST="$PROJMODEL.Test"
ARG CLOUTPUTFORMAT="opencover"
ARG BUILDCONFIG="Release"
ARG PROGETFEED="http://proget.inflection.net/nuget/Inflection/"
ARG TESTRESULTFILE="results.trx"
# These should not be changed on input
ARG TESTRESULTPATH="TestResults/$TESTRESULTFILE"
ARG CLRESULTFILE="coverage.opencover.xml"

WORKDIR /src
COPY $PROJAPI $PROJAPI
COPY $PROJAPITEST $PROJAPITEST
COPY $PROJMODEL $PROJMODEL
COPY $PROJMODELTEST $PROJMODELTEST

# Restore all the nuget packages
RUN dotnet restore "./$PROJMODELTEST/$PROJMODELTEST.csproj" -s $PROGETFEED
RUN dotnet restore "./$PROJAPITEST/$PROJAPITEST.csproj" -s $PROGETFEED

# Build the test projects here so they aren't included in SQ analysis
RUN dotnet build "./$PROJMODELTEST/$PROJMODELTEST.csproj" -c $BUILDCONFIG
RUN dotnet build "./$PROJAPITEST/$PROJAPITEST.csproj" -c $BUILDCONFIG

# Shutdown any build activities to prevent locking of files
RUN dotnet build-server shutdown

# Start the sonarqube scanner
# Unfortunately, we need to define exclusions both for SQ AND Coverlet - SQ won't accept the file listing from the SQ coverage report
# Build to run analysis
# Run the tests against the build
# https://github.com/Microsoft/msbuild/issues/2999
CMD sleep 10s \
	&& dotnet sonarscanner begin /d:sonar.login=${SQTOKEN} /d:sonar.host.url=${SQURL} \
	/k:organization_api--${BRANCH} \
	/v:${BUILDNUM} \
	/d:sonar.coverage.exclusions="**/Startup.cs,**/Program.cs,Geo.Service.Model/Hierarchy/InvalidNodeIdException.cs,Geo.Service/Controllers/*,Geo.Service/Data/GoodHire/*,Geo.Service/Data/Support/*,Geo.Service/HostedServices/*" \
	/d:sonar.cs.opencover.reportsPaths="Geo.Service.Model.Test/coverage.opencover.xml,Geo.Service.Test/coverage.opencover.xml" \
	/d:sonar.cs.vstest.reportsPaths="Geo.Service.Model.Test/TestResults/results.trx,Geo.Service.Test/TestResults/results.trx" \
	&& dotnet build "Geo.Service/Geo.Service.csproj" -c Release \
	&& dotnet test "Geo.Service.Model.Test/Geo.Service.Model.Test.csproj" -c Release --no-build --no-restore --logger "trx;LogFileName=results.trx" \
	-p:CollectCoverage=true -p:CoverletOutputFormat=opencover \
	-p:Exclude=[Inflection.Organization.Model]Inflection.Organization.Model.Hierarchy.InvalidNodeIdException \
	&& dotnet test "./Geo.Service.Test/Geo.Service.Test.csproj" -c Release --no-build --no-restore --logger "trx;LogFileName=results.trx" \
	-p:CollectCoverage=true -p:CoverletOutputFormat=opencover \
	-p:Exclude=[Geo.Service]Geo.Service.Program%2c[Geo.Service]Geo.Service.Startup%2c[Geo.Service]Geo.Service.Controllers.*%2c[Geo.Service]Geo.Service.Data.GoodHire.*%2c[Geo.Service]Geo.Service.Data.Support.*%2c[Geo.Service]Geo.Service.HostedServices.* \
	&& dotnet sonarscanner end /d:sonar.login=${SQTOKEN}
