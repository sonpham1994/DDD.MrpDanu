using System.Data;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Benchmark.Infrastructure.ConnectionPoolings;

[MemoryDiagnoser()]
public class ConnectionPoolingBenchmark
{
    const string ConnectionPooling =
    "Server=localhost,1433;Database=DDD.MrpDanu;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=true";
    const string NoConnectionPooling =
    "Server=localhost,1433;Database=DDD.MrpDanu;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=true;Pooling=false;";

    public static readonly Guid MaterialId = new Guid("e3b6f3e8-862e-4f62-be71-08db6e6e4db3");
    
    [Params(100)]
    public int Length { get; set; }
    
    [Benchmark]
    public void PerformConnectionPooling()
    {
        for (int i = 0; i < Length; i++)
        {
            using IDbConnection connection = new SqlConnection(ConnectionPooling);
            connection.Open();
            connection.Close();
        }
    }

    [Benchmark]
    public void PerformNoConnectionPooling()
    {
        for (int i = 0; i < Length; i++)
        {
            using IDbConnection connection = new SqlConnection(NoConnectionPooling);
            connection.Open();
            connection.Close();
        }
    }

    [Benchmark]
    public void PerformConnectionPoolingWithDbContext()
    {
        for (int i = 0; i < Length; i++)
        {
            using var appDbContext = new ConnectionPoolingAppDbContext(ConnectionPooling);
            appDbContext.Database.OpenConnection();
            appDbContext.Database.CloseConnection();
        }
    }

    [Benchmark]
    public void PerformNoConnectionPoolingWithDbContext()
    {
        for (int i = 0; i < Length; i++)
        {
            using var appDbContext = new ConnectionPoolingAppDbContext(NoConnectionPooling);
            appDbContext.Database.OpenConnection();
            appDbContext.Database.CloseConnection();
        }
    }
    
    [Benchmark]
    public void PerformQueryConnectionPoolingWithDbContext()
    {
        for (int i = 0; i < Length; i++)
        {
            using var appDbContext = new ConnectionPoolingAppDbContext(ConnectionPooling);
            appDbContext.Materials.ToList();
        }
    }

    [Benchmark]
    public void PerformQueryNoConnectionPoolingWithDbContext()
    {
        for (int i = 0; i < Length; i++)
        {
            using var appDbContext = new ConnectionPoolingAppDbContext(NoConnectionPooling);
            appDbContext.Materials.ToList();
        }
    }
    
    [Benchmark]
    public void PerformQueryConnectionPoolingWithDapper()
    {
        for (int i = 0; i < Length; i++)
        {
            using IDbConnection connection = new SqlConnection(ConnectionPooling);
            connection.Query<Material>(@"SELECT * FROM Materials");
        }
    }

    [Benchmark]
    public void PerformQueryNoConnectionPoolingWithDapper()
    {
        for (int i = 0; i < Length; i++)
        {
            using IDbConnection connection = new SqlConnection(NoConnectionPooling);
            connection.Query<Material>(@"SELECT * FROM Materials");
        }
    }
}