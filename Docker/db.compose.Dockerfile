FROM mcr.microsoft.com/mssql/server:2017-latest AS base
WORKDIR /root
# "Install" sqlpackage from MS
# https://docs.microsoft.com/en-us/sql/tools/sqlpackage-download?view=sql-server-2017
RUN wget -O sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=2108814 \
	&& mkdir sqlpackage \
	&& python -m zipfile -e sqlpackage.zip ./sqlpackage \
	&& chmod a+x ./sqlpackage/sqlpackage
# Fetch the latest DB bacpac
RUN ls
RUN wget -O GEO.bacpac http://proget.inflection.net/endpoints/SqlServer-GEO-BacPac/content/GEO.bacpac 
# SQL Server Setup
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@ssw0rd
#ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=1433
EXPOSE 1433
# Run SQL Server
#RUN /opt/mssql/bin/sqlservr --accept-eula
RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
	&& /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q 'ALTER SERVICE MASTER KEY REGENERATE' \
	&& ./sqlpackage/sqlpackage /a:Import /sf:./GEO.bacpac /tsn:localhost /tdn:GEO /tu:sa /tp:$SA_PASSWORD \
	&& /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q "ALTER LOGIN geo_svc WITH PASSWORD = N'P@ssw0rd'"