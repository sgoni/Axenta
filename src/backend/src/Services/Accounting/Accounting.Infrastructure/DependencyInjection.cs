using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var server = configuration["DatabaseConfig:server"];
        var port = configuration["DatabaseConfig:port"];
        var database = configuration["DatabaseConfig:database"];

        // Assemble the connection string
        //var  connectionString =
        //    $"Server={server};Port={port};Database={database};User Id={usernamePasswordCredentials.Result.Username};Password={usernamePasswordCredentials.Result.Password};Include Error Detail=true";

        var connectionString =
            $"Server={server};Port={port};Database={database};User Id=postgres;Password=postgres;Include Error Detail=true";

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        return services;
    }
}