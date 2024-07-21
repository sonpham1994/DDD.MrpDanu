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

## Run docker
```zsh
docker compose -p mrp_infra -f docker-compose.mrp-infra.yml up -d
docker compose -p mongodb-replica-set -f docker-compose.mongodb-replica-set.yml up -d
OR
docker compose -p mrp_infra -f docker-compose.mrp-infra.yml -f docker-compose.mongodb-replica-set.yml up -d
```

## Apply config replica set for mongoDb
References: https://www.youtube.com/watch?v=LGqTzPATSV4
Connect to mongo primary
```zsh
docker compose exec mongo-primary mongosh
OR
mongosh --port 27001
```
27001 is mongo primary port

Create config
```zsh
config = {
  "_id": "rs0",
  "version": 1,
  "members": [
      {
        "_id": 1,
        "host": "mongo-primary:27017",
        priority: 3
      },
      {
        "_id": 2,
        "host": "mongo-secondary-1:27017",
        priority: 2
      },
      {
        "_id": 3,
        "host": "mongo-secondary-2:27017",
        priority: 1
      },
      {
        "_id": 4,
        "host": "mongo-arbiter:27017",
        "arbiterOnly": true
      }
  ]
}
```
Apply config to replica set
```zsh
rs.initiate(config, {force: true})
```

Add some data
```zsh
db.createCollection("user")
db.user.insertOne({"name": "This is my name", "age": 18})
```

Check data
```zsh
db.user.find()
```

Check data on secondary
Run in a new window for the terminal on linux or command prompt on window. `27002` is port of secondary you define in 
docker compose
```zsh
mongosh --host localhost --port 27002
```
After connecting secondary, check data on secondary
```zsh
db.user.find()
```

## Update config replica set
For ex, we want to add arbiter for replica set from replica set we already configured
Connect to mongo primary
```zsh
mongosh --port 27001
```

Set the Default Read/Write Concern:
```zsh
db.adminCommand({
    setDefaultRWConcern: 1,
    defaultWriteConcern: { w: 1 }
});
```
setDefaultRWConcern Command:
This command sets the default read and write concern for the cluster, ensuring that the new replica set configuration 
does not change the implicit default write concern.

Get current config in replica set
```zsh
config = rs.conf()
```

Add arbiter to members
```zsh
config.members.push({
    _id: 4,
    host: "mongo-arbiter:27017",
    arbiterOnly: true
});
```

Re-apply replica set with new configuration
```zsh
rs.reconfig(config, { force: true })
```

Show nodes in replica set
```zsh
rs.status()
```

## Check the election process
Stop primary node server from docker, one of secondaries will become primary. If you restart the former primary node,
it can become current primary though it should be secondary node. It can be attributed to high-priority, the former 
primary node hah priority which is higher than others, hence it can become primary again.

If you want to prevent the former primary node to become primary again. You can:
Connect to current primary: 
```
mongosh --port <current primary port>
```
Get config
```zsh
config = rs.conf();
```
Modify priority
```zsh
// Set a lower priority for the original primary node
config.members[0].priority = 0.5; // Adjust the index as per your configuration

// Optional: Set higher priorities for the secondary nodes
config.members[1].priority = 1; // Adjust the index as per your configuration
config.members[2].priority = 1; // Adjust the index as per your configuration

// Apply the updated configuration
rs.reconfig(config, { force: true });
```

## Connect replica set via Mongo Compass
Connect primary mongo via command line
```zsh
mongo --port 27001
```
after that, get connection string
```zsh
db.getMongo()
```
After that, copy and paste connection string from command `db.getMong()` into Mongo Compass

## Run project
At the root of project run docker compose:
```
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