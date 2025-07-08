FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/backend/src/Services/Accounting/Accounting.API/Accounting.API.csproj", "src/backend/src/Services/Accounting/Accounting.API/"]
RUN dotnet restore "src/backend/src/Services/Accounting/Accounting.API/Accounting.API.csproj"
COPY . .
WORKDIR "/src/src/backend/src/Services/Accounting/Accounting.API"
RUN dotnet build "./Accounting.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Accounting.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounting.API.dll"]
