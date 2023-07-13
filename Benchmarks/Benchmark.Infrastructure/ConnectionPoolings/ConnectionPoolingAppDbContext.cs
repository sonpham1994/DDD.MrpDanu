using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Benchmark.Infrastructure.ConnectionPoolings;

public class ConnectionPoolingAppDbContext : DbContext
{
    private readonly string _connectionString;
    
    public DbSet<Material> Materials { get; set; }
    
    public ConnectionPoolingAppDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_connectionString);
    }
}