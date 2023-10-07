FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BattleBot/BattleBot.csproj", "BattleBot/"]
RUN dotnet restore "BattleBot/BattleBot.csproj"
COPY . .
WORKDIR "/src/BattleBot"
RUN dotnet build "BattleBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BattleBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BattleBot.dll"]
