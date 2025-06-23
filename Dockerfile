# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Rodar migrações e depois iniciar a app
ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet WebAPI_Person.dll"]