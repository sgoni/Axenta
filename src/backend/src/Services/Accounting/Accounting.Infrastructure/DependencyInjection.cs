using Accounting.Application.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Accounting.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        //var server = configuration["DatabaseConfig:server"];
        //var port = configuration["DatabaseConfig:port"];
        //var database = configuration["DatabaseConfig:database"];

        // Assemble the connection string
        //var  connectionString =
        //    $"Server={server};Port={port};Database={database};User Id={usernamePasswordCredentials.Result.Username};Password={usernamePasswordCredentials.Result.Password};Include Error Detail=true";

        //var connectionString =
        //    $"Server={server};Port={port};Database={database};User Id=postgres;Password=postgres;Include Error Detail=true";
//
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        return services;
    }
}