FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Firmezaa.Api/Firmezaa.Api.csproj", "Firmezaa.Api/"]
RUN dotnet restore "Firmezaa.Api/Firmezaa.Api.csproj"
COPY . .
WORKDIR "/src/Firmezaa.Api"
RUN dotnet build "./Firmezaa.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Firmezaa.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Firmezaa.Api.dll"]
