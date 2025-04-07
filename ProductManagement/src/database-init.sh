#!/bin/sh

echo "Aguardando o banco de dados ficar disponível..."

# Espera o Postgres estar disponível (usa pg_isready ou espera a porta responder)
until nc -z postgres 5432; do
  sleep 1
done

echo "Banco de dados disponível. Rodando migrations..."

# Executa a migration
dotnet ef database update --project /src/ProductManagement.Infrastructure --startup-project /src/ProductManagement.API

# Inicia a API
echo "Iniciando a aplicação..."
dotnet ProductManagement.API.dll
