FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Scoreboard.csproj", "."]
RUN dotnet restore "./Scoreboard.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Scoreboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scoreboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scoreboard.dll"]