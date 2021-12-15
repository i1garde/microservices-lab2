FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . unhealthy-api/
RUN dotnet restore "unhealthy-api/unhealthy-api.csproj"
COPY . .
WORKDIR "/src/unhealthy-api"
RUN dotnet build "unhealthy-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "unhealthy-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "unhealthy-api.dll"]
