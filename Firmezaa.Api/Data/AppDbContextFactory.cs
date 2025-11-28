using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
 
namespace Firmezaa.Api.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {

        DotNetEnv.Env.Load();

        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var name = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var pass = Environment.GetEnvironmentVariable("DB_PASS");

        var connectionString = $"Host={host};Port={port};Database={name};Username={user};Password={pass}";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        // Retorna una instancia funcional del DbContext
        return new AppDbContext(optionsBuilder.Options);
    }
}