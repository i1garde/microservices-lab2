FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . Giraffe.API/
RUN dotnet restore "Giraffe.API/Giraffe.API.fsproj"
COPY . .
WORKDIR "/src/Giraffe.API"
RUN dotnet build "Giraffe.API.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Giraffe.API.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Giraffe.API.App.dll"]
