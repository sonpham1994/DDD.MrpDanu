# DDD.MrpDanu



Architecture:
  - Clean Architecture

Domain-driven design

Design patterns:
 - Mediator design pattern
 - CQRS design pattern
 - Factory method design pattern
 - Factory design pattern
 - Interceptor design pattern
 - Chain of Responsibility design pattern
 - Repository design pattern
 - Unit of Work design pattern

Patterns:
  - Enumeration pattern
  - Options pattern

Framework and Libraries:
  - Entity Framework 8
  - EntityFrameworkCore.SqlServer
  - EntityFrameworkCore.Proxies
  - Serilog
  - MediatR
  - FluentValidation
  - FluentAssertions
  - Dapper
  - NetArchTest.Rules
  - xUnit

## Apply Migration
stand at DDD.MrpDanu folder and run the command lines below

Install Microsoft.EntityFrameworkCore.Design
AppDbContext:
```zsh
dotnet ef migrations add Initial --project src/External/Infrastructure --startup-project src/External/Api --context AppDbContext -o Persistence/Migrations
dotnet ef database update --project src/External/Infrastructure --startup-project src/External/Api --context AppDbContext
dotnet ef migrations remove --project src/External/Infrastructure --startup-project src/External/Api --context AppDbContext
```


ExternalDbContext
```zsh
dotnet ef migrations add InititAuditTable --project src/External/Infrastructure --startup-project src/External/Api --context ExternalDbContext -o Persistence/Externals/Migrations
dotnet ef database update --project src/External/Infrastructure --startup-project src/External/Api --context ExternalDbContext
dotnet ef migrations remove --project src/External/Infrastructure --startup-project src/External/Api --context ExternalDbContext
```


Install Microsoft.EntityFrameworkCore.Tools
AppDbContext:
```zsh
Add-Migration Initial -StartupProject src/External/Api -Context AppDbContext -Project src/External/Infrastructure -o Persistence/Migrations
Remove-Migration -StartupProject src/External/Api -Context AppDbContext -Project src/External/Infrastructure
Update-Database -project src/External/Infrastructure -startupProject src/External/Api -context AppDbContext
```

ExternalDbContext:
```zsh
Add-Migration InititAuditTable -Project src/External/Infrastructure -StartupProject src/External/Api -Context ExternalDbContext -o Persistence/Externals/Migrations
Update-Database -project src/External/Infrastructure -StartupProject src/External/Api -Context ExternalDbContext
Remove-Migration -Project src/External/Infrastructure -StartupProject src/External/Api -Context ExternalDbContext
```


## Run project
At the root of project run docker compose:
```
docker compose -p mrp_infra up -d
dotnet run --project src/External/UI
dotnet run --project src/External/Api
```

url:
```
http://localhost:5288/MaterialManagement/Materials
http://localhost:5288/MaterialManagement/TransactionalPartners
```

Seq:
```
http://localhost:8081
```