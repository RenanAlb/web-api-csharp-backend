# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Render espera que a app escute na porta da variável de ambiente PORT
ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "WebAPI_Person.dll"]
