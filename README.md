# DDD.MrpDanu



Architecture:
  - Clean Architecture

Design patterns:
 - Domain-driven design
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
  - Entity Framework 7
  - EntityFrameworkCore.SqlServer
  - EntityFrameworkCore.Proxies
  - Serilog
  - MediatR
  - FluentValidation
  - FluentAssertions
  - Dapper
  - NetArchTest.Rules
  - xUnit

## Run project
At the root of project run docker compose:
```
# docker compose -p mrp_infra up -d
```

url:
```
http://localhost:5288/MaterialManagement/Materials
http://localhost:5288/MaterialManagement/TransactionalPartners
```