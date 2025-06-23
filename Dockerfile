# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore

# Instala o EF CLI
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet build -c Release

# Roda as migrações (gera o app.db no /app)
RUN dotnet ef database update

# Publica a aplicação
RUN dotnet publish -c Release -o out

# Copia o banco SQLite para a pasta de publicação
RUN cp person.sqlite out/person.sqlite

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "WebAPI_Person.dll"]
