# 📦 Product Management API

API para gerenciamento de produtos, desenvolvida como parte de um desafio técnico, com arquitetura limpa, integração com banco de dados PostgreSQL e armazenamento de imagens simulando o serviço AWS S3 via LocalStack.

---

## 📐 Arquitetura do Projeto

Este projeto adota Clean Architecture com princípios de Domain-Driven Design (DDD). A separação entre as camadas garante um código limpo, desacoplado e de fácil manutenção:

```text
[Controller]
   ↓
[UseCase Handler]
   ↓
[Repository Interface]
   ↓
[Infra Repository + Services]
   ↓
[PostgreSQL / S3 (via LocalStack)]
```

---

## 🧠 Princípios SOLID Aplicados

Este projeto aplica os 5 princípios do SOLID:

- 💡 **SRP**: Camadas com responsabilidade única (Controller, UseCase, DTO, Repository).
- 💡 **OCP**: Componentes como ApiResult<T> e DTOs extensíveis sem modificar lógica.
- 💡 **LSP**: DTOs substituíveis por suas interfaces sem impactar os consumidores.
- 💡 **ISP**: Interfaces enxutas como IStorageService, IRequestValidator.
- 💡 **DIP**: Application depende de abstrações; injeção de dependência nas controllers.

## 📏 Padrões e Boas Práticas

### ✅ Command-Query Separation (CQS)

- **Comandos**: Alteram o estado da aplicação (ex: CreateProductCommand).
- **Consultas**: São apenas leitura (ex: GetAllProductsQuery).
- **Handlers**: Cada operação tem seu próprio handler.
  > ℹ️ O projeto não implementa CQRS completo, apenas a separação entre leitura e escrita (CQS).

---

### ✅ DTOs Específicos da Camada de API

- DTOs como CreateProductWithImageRequest encapsulam os dados de entrada.
- Possuem métodos ToCommand() e Validate(), facilitando a conversão para a camada de aplicação e a validação de dados.

---

### ✅ Record Types para Imutabilidade

- Utiliza record class em DTOs e Commands, promovendo imutabilidade e legibilidade.

---

### ✅ UseCases como Camada de Aplicação

- Cada operação é tratada em um UseCaseHandler, centralizando a lógica e desacoplando das controllers.

---

### ✅ Organização por Feature

- Diretórios separados por domínio: Product/Commands, Category/Queries, etc.
  Isso facilita a escalabilidade e o entendimento por contexto.

---

### ✅ ApiResult<T> como Wrapper de Resposta

- Todas as respostas seguem o mesmo padrão: sucesso, falha, mensagens e payloads consistentes.

---

### ✅ Upload de Arquivos Isolado

- Upload de imagens é tratado por um serviço externo (IStorageService), mantendo os Commands limpos.

---

## 📁 Estrutura de diretórios

```txt
src/ → Raiz
├─ ProductManagement.API → Web API (controllers, DI, configuração)
├─ ProductManagement.Application → UseCases e contratos de serviço
├─ ProductManagement.Domain → Entidades e lógica de negócio
└─ ProductManagement.Infrastructure → Repositórios  e serviços externos
```

---

## 🧰 Tecnologias Utilizadas

- ⚙️ [.NET 8](https://dotnet.microsoft.com/)
- 🐘 **PostgreSQL** via Docker
- ⚙️ **Entity Framework Core** (ORM e Migrations)
- ☁️ **Amazon S3** (simulado via [LocalStack](https://github.com/localstack/localstack))
- 🐳 **Docker & Docker Compose** (ambiente de desenvolvimento completo)
- 📘 **Swagger (Swashbuckle)** para documentação da API

---

## 🐳 Como rodar o projeto com Docker

### ✅ Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

> ℹ️ **Dependências para o script `localstack-init.sh`:**  
> Para rodar o script que inicializa o ambiente LocalStack (como a criação do bucket `product-images`), é necessário ter:
>
> - [AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html) instalado
> - [awslocal](https://github.com/localstack/awscli-local) instalado (`pip install awscli-local`)
>
> O script `localstack-init.sh` executa comandos como `awslocal s3 mb s3://product-images`, portanto certifique-se de que essas ferramentas estão disponíveis no ambiente.

## 🐘 Inicialização do banco de dados

O script `database-init.sh` é executado automaticamente no container da API e é responsável por aplicar as migrations no banco PostgreSQL ao subir o ambiente com Docker.

⚠️ **Importante**:  
Esse script só roda corretamente se o banco de dados (`postgres`) estiver acessível na porta 5432. Ele usa o comando `nc` (netcat) para aguardar até que a conexão esteja disponível. Esse script é executado automaticamente via Dockerfile. Ele aplica as migrations e inicia a API logo em seguida.

### 📦Serviços via Docker

| Serviço           | Porta                    | Descrição                        |
| ----------------- | ------------------------ | -------------------------------- |
| ProductManagement | Http:8080 <br> Https:443 | API REST com suporte a Swagger   |
| PostgreSQL        | 5432                     | Banco de dados relacional        |
| LocalStack        | 4566                     | Simulador local dos serviços AWS |

> 💡 **Observação:**  
> A aplicação está rodando com **HTTPS** na porta **443** (com um certificado já incluído no projeto).  
> Redireciona automaticamente requisições HTTP (porta 8080) para HTTPS (porta 443).
> Você pode ter que aceitar o certificado na primeira vez que acessar via navegador.

### 🪣 Upload de Arquivos

- As imagens de produtos são armazenadas no bucket S3 **product-images** simulado no LocalStack.
- Durante a inicialização, o bucket é criado automaticamente via script.

### ▶️ Subindo os containers

1. Clone este repositório:

   ```bash
   git clone https://github.com/MarcosNasc/product-management-api.git
   cd product-management-api\src
   ```

2. Suba os containers com:

   ```bash
   docker-compose up --build
   ```

3. Acesse o Swagger da API em:

   ```bash
   http://localhost/swagger
   ```

## 📌 Autor

Desenvolvido por [Marcos Nascimento](https://github.com/marcosnasc)  
Desafio técnico para fins de avaliação e estudo.
