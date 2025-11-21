# ===== build =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet publish Api/Api.csproj -c Release -o /app

# ===== runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80

# Swagger[../Cyberia-Storage/server/Api/Program.cs]
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 80

COPY --from=build /app .

ENTRYPOINT ["dotnet", "Api.dll"]
