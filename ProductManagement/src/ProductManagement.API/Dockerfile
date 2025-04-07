# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Imagem para build e instalação do EF CLI
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instala EF CLI para build
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

ARG BUILD_CONFIGURATION=Release

# Copia csproj
COPY ["ProductManagement.API/ProductManagement.API.csproj", "ProductManagement.API/"]
COPY ["ProductManagement.Infrastructure/ProductManagement.Infrastructure.csproj", "ProductManagement.Infrastructure/"]
COPY ["ProductManagement.Application/ProductManagement.Application.csproj", "ProductManagement.Application/"]
COPY ["ProductManagement.Domain/ProductManagement.Domain.csproj", "ProductManagement.Domain/"]

# Restaura dependências
RUN dotnet restore "ProductManagement.API/ProductManagement.API.csproj"

# Copia tudo
COPY . .

# Build do projeto
WORKDIR "ProductManagement.API"
RUN dotnet build "ProductManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publica a aplicação
FROM build AS publish
RUN dotnet publish "ProductManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final (com SDK + runtime + EF CLI)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app

# Instala EF CLI novamente no container final
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Instala netcat pra esperar o banco
RUN apt-get update && apt-get install -y netcat-openbsd

# Copia arquivos publicados
COPY --from=publish /app/publish .

# Copia os sources pro EF funcionar
COPY --from=build /src /src

# Copia o script de entrypoint e dá permissão
COPY database-init.sh .
RUN chmod +x ./database-init.sh

ENTRYPOINT ["./database-init.sh"]
