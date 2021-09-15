#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Scoreboard/Scoreboard.csproj", "Scoreboard/"]
RUN dotnet restore "Scoreboard/Scoreboard.csproj"
COPY . .
WORKDIR "/src/Scoreboard"
RUN dotnet build "Scoreboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scoreboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scoreboard.dll"]