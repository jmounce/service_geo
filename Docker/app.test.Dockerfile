# This image is deprecated. New images at: https://hub.docker.com//microsoft-dotnet-core-sdk/
# Need alpine since it has apk installed, we should probably create our own images..
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build

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

# Build to run analysis
RUN	dotnet build "$PROJAPI/$PROJAPI.csproj" -c $BUILDCONFIG

# Run the tests against the build
# https://github.com/Microsoft/msbuild/issues/2999
CMD sleep 10s \
	&& dotnet test "Geo.Service.Model.Test/Geo.Service.Model.Test.csproj" -c Release --no-build --no-restore --logger "trx;LogFileName=results.trx"\
		-p:CollectCoverage=true -p:CoverletOutputFormat=opencover \
		-p:Exclude=[Inflection.Organization.Model]Inflection.Organization.Model.Hierarchy.InvalidNodeIdException \
	&& dotnet test "./Geo.Service.Test/Geo.Service.Test.csproj" -c Release --no-build --no-restore --logger "trx;LogFileName=result.trx" \
		-p:CollectCoverage=true -p:CoverletOutputFormat=opencover \
		-p:Exclude=[Geo.Service]Geo.Service.Program%2c[Geo.Service]Geo.Service.Startup%2c[Geo.Service]Geo.Service.Controllers.*%2c[Geo.Service]Geo.Service.Data.GoodHire.*%2c[Geo.Service]Geo.Service.Data.Support.*%2c[Geo.Service]Geo.Service.HostedServices.*
# CMD sleep 10s \
# 	&& dotnet test "./$PROJMODELTEST/$PROJMODELTEST.csproj" -c $BUILDCONFIG --no-build --no-restore --logger "trx;LogFileName=$TESTRESULTFILE"\
# 		-p:CollectCoverage=true -p:CoverletOutputFormat=$CLOUTPUTFORMAT \
# 		-p:Exclude=[Inflection.Organization.Model]Inflection.Organization.Model.Hierarchy.InvalidNodeIdException \
# 	&& dotnet test "./$PROJAPITEST/$PROJAPITEST.csproj" -c $BUILDCONFIG --no-build --no-restore --logger "trx;LogFileName=$TESTRESULTFILE" \
# 		-p:CollectCoverage=true -p:CoverletOutputFormat=$CLOUTPUTFORMAT \
# 		-p:Exclude=[Geo.Service]Geo.Service.Program%2c[Geo.Service]Geo.Service.Startup%2c[Geo.Service]Geo.Service.Controllers.*%2c[Geo.Service]Geo.Service.Data.GoodHire.*%2c[Geo.Service]Geo.Service.Data.Support.*%2c[Geo.Service]Geo.Service.HostedServices.*
