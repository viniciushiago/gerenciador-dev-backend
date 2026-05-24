# Backend — Gerenciador de Desenvolvedores

## Tecnologias
- .NET 8 / C# 12
- Entity Framework Core
- SQL Server
- MediatR (CQRS)
- FluentValidation
- JWT Authentication
- BCrypt

## Como executar

### Pré-requisitos
- .NET 8 SDK
- SQL Server

### Configuração

Configure o `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=.;Database=GerenciadorDevDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "chave-secreta-minimo-32-caracteres-aqui",
    "Issuer": "GerenciadorDev",
    "Audience": "GerenciadorDev",
    "ExpiresInHours": 8
  }
}
```

### Executando

```bash
cd src/Api
dotnet run
```

O banco será criado e populado automaticamente.
Acesse o Swagger em `https://localhost:7032/swagger`

### Seed inicial
E-mail: admin@sistema.com
Senha:  Admin@123

## Estrutura do projeto
```txt
src/
├── Api/
│   ├── Controllers/
│   ├── Middlewares/
│   └── Program.cs
├── Application/
│   ├── Behaviors/
│   ├── Commands/
│   ├── DTOs/
│   ├── Interfaces/
│   └── Queries/
├── Domain/
│   ├── Common/
│   ├── Entities/
│   ├── Enums/
│   └── Interfaces/
└── Infrastructure/
├── Persistence/
├── Repositories/
└── Services/
```
## Decisões técnicas

**Clean Architecture** — separação em 4 camadas com dependências apontando sempre para o Domain, garantindo que regras de negócio não dependam de infraestrutura.

**CQRS com MediatR** — commands e queries separados para deixar claro o que modifica estado e o que apenas lê dados.

**Result Pattern** — retorno explícito de sucesso ou falha em vez de exceções para controle de fluxo, tornando o código mais previsível.

**Soft Delete com Global Query Filter** — registros deletados nunca aparecem nas consultas sem configuração adicional em cada repository.

**Senioridade como enum** — garante consistência dos dados e evita valores inválidos no banco.

**Usuários sem listagem no frontend** — a gestão de usuários foi mantida no backend mas não exposta no dashboard. Em produção seria necessário um perfil de administrador para esse acesso.

## Melhorias futuras

- Paginação server-side
- Multi-tenancy
- Perfil de administrador
- Testes unitários e de integração
- Docker
