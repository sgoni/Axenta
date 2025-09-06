FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/backend/src/Services/Notifications/Axenta.Notifications/Axenta.Notifications.csproj", "src/backend/src/Services/Notifications/Axenta.Notifications/"]
COPY ["src/backend/src/shared/Axenta.BuildingBlocks.Email/Axenta.BuildingBlocks.Email.csproj", "src/backend/src/shared/Axenta.BuildingBlocks.Email/"]
COPY ["src/backend/src/shared/Axenta.BuildingBlocks.Messaging/Axenta.BuildingBlocks.Messaging.csproj", "src/backend/src/shared/Axenta.BuildingBlocks.Messaging/"]
RUN dotnet restore "src/backend/src/Services/Notifications/Axenta.Notifications/Axenta.Notifications.csproj"
COPY . .
WORKDIR "/src/src/backend/src/Services/Notifications/Axenta.Notifications"
RUN dotnet build "./Axenta.Notifications.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Axenta.Notifications.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Axenta.Notifications.dll"]
