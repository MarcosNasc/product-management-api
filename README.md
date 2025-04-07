# 🛠️ Product Management API

API para gerenciamento de produtos, desenvolvida como parte de um desafio técnico, com arquitetura limpa, integração com banco de dados PostgreSQL e armazenamento de imagens simulando o serviço AWS S3 via LocalStack.

---

## 🧱 Arquitetura do Projeto - Padrões e Boas Práticas

Este documento descreve os principais padrões e boas práticas utilizados na arquitetura do projeto, com o objetivo de manter o código limpo, organizado, escalável e de fácil manutenção. O projeto segue os princípios da **Clean Architecture** e **Domain Drive Design**, com separação clara de responsabilidades entre as camadas:

- **Domain**: Entidades e regras de negócio puras, sem dependências.
- **Application**: Casos de uso (UseCases), interfaces e contratos.
- **Infrastructure**: Implementações concretas de serviços externos (banco de dados, armazenamento, etc).
- **API**: Interface pública da aplicação (Web API), configuração de middlewares, DI, controllers, etc.

---

## ⚖️ Princípios Adotados

### 1. Command-Query Separation (CQS)

- **Comandos**: Responsáveis por alterar o estado da aplicação (ex: `CreateProductCommand`).
- **Consultas**: Responsáveis apenas por leitura (ex: `GetAllProductsQuery`).
- Handlers separados para cada operação.
  > ❗ Este projeto **não utiliza CQRS**, apenas o princípio CQS (separação entre comandos e consultas), mantendo uma estrutura mais simples e coesa.

---

### 2. DTOs Específicos da Camada de API

- Objetos de transporte como `CreateProductWithImageRequest` e `EditProductWithImageRequest`.
- Usados para receber dados dos endpoints .
- Possuem métodos `ToCommand()` para conversão em comandos da camada de aplicação.
- Possuem métodos `Validate()` para validar a entrada de dados

---

### 3. Record Types para Imutabilidade

- Utiliza `record class` em DTOs e Commands.
- Favorece comparação estrutural e imutabilidade.

---

### 4. Responsabilidade Única (SRP - SOLID)

- Controllers só orquestram.
- DTOs recebem entrada de dados e faz a validação.
- Commands representam intenções/Ações.
- UseCases executam lógica de negócio.

---

### 5. UseCases como Camada de Aplicação

- Cada caso de uso é representado por uma classe dedicada (ex: `CreateProductUseCaseHandler`).
- Isola a lógica da aplicação das controllers.

---

### 7. Organização por Feature

- Diretórios por contexto de negócio: `Product/Commands`, `Category/Queries`, etc.

---

### 9. Interfaces para Abstração

- `IProductImageRequest` para padronizar acesso a `Image` entre diferentes DTOs.
- `IRequestValidator` para padronizar a validação entre diferentes DTOs.

---

### 10. ApiResult<T> como Wrapper de Resposta

- Retornos da API padronizados com `ApiResult<T>`.
- Sucesso, falha, mensagens e payload sempre consistentes.

---

### 11. Injeção de Dependência

- UseCases e services injetados nas controllers.
- Favorece teste e desacoplamento.

---

### 12. Upload de Arquivos Isolado

- Upload de imagens é tratado por um serviço (`_storageService`) fora da lógica do Command.
- A imagem é processada, a URL é enviada ao `Command`.

---

## 📁 Estrutura de diretórios

```txt
src/ → Raiz
├─ ProductManagement.API** → Projeto da WebAPI
├─ ProductManagement.Application** → UseCases e contratos de serviço
├─ ProductManagement.Domain** → Entidades e lógica de negócio
└─ ProductManagement.Infrastructure** → Acesso a dados e serviços externos
```

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- **PostgreSQL** via Docker
- **Entity Framework Core** (ORM e Migrations)
- **Amazon S3** (simulado via [LocalStack](https://github.com/localstack/localstack))
- **Docker & Docker Compose** (ambiente de desenvolvimento completo)
- **Swagger (Swashbuckle)** para documentação da API

---

## 🐳 Como rodar o projeto com Docker

### ✅ Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### 📦Serviços via Docker

| Serviço           | Porta | Descrição                        |
| ----------------- | ----- | -------------------------------- |
| ProductManagement | 5000  | API REST com suporte a Swagger   |
| PostgreSQL        | 5432  | Banco de dados relacional        |
| LocalStack        | 4566  | Simulador local dos serviços AWS |

### 🖼️ Upload de Imagens

- As imagens de produtos são armazenadas no bucket S3 **product-images** simulado no LocalStack.
- Durante a inicialização, o bucket é criado automaticamente via script.

### ▶️ Subindo os containers

1. Clone este repositório:

   ```bash
   git clone https://github.com/MarcosNasc/product-management-api.git
   cd prova-pratica
   ```

2. Suba os containers com:

   ```bash
   docker-compose up --build
   ```

3. Acesse o Swagger da API em:

   ```bash
   http://localhost:5000/swagger
   ```

## 📌 Autor

Desenvolvido por [Marcos Nascimento](https://github.com/marcosnasc)  
Desafio técnico para fins de avaliação e estudo.
