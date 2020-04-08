FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Geo.Service/Geo.Service.csproj", "Geo.Service/"]

RUN dotnet restore "Geo.Service/Geo.Service.csproj"
COPY . .
WORKDIR "/src/Geo.Service"
RUN dotnet build "Geo.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Geo.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Geo.Service.dll"]