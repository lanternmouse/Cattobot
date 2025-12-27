FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cattobot/Cattobot.csproj", "Cattobot/"]
COPY ["Kinopoisk.Gateway/Kinopoisk.Gateway.csproj", "Kinopoisk.Gateway/"]
COPY ["Cattobot.Db/Cattobot.Db.csproj", "Cattobot.Db/"]
RUN dotnet restore "Cattobot/Cattobot.csproj"
COPY . .
WORKDIR "/src/Cattobot"
RUN dotnet build "./Cattobot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cattobot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cattobot.dll"]
