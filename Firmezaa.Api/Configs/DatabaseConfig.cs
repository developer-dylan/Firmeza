namespace Firmezaa.Api.Configs;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {

        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var name = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var pass = Environment.GetEnvironmentVariable("DB_PASS");

        var connectionString = $"Host={host};Port={port};Database={name};Username={user};Password={pass}";

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}