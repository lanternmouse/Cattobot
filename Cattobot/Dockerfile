FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . /src
WORKDIR "/src/Cattobot"
RUN dotnet restore "./Cattobot.csproj"
RUN dotnet build "./Cattobot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cattobot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN apt update
RUN apt install -y --no-install-recommends libsodium libopus0 libopus-dev ffmpeg ca-certificates curl python3
RUN rm -rf /var/lib/apt/lists/*
RUN curl -L https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp -o /usr/local/bin/yt-dlp
RUN chmod +x /usr/local/bin/yt-dlp
RUN yt-dlp --version
USER $APP_UID
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cattobot.dll"]
