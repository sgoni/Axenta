namespace Accounting.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();

        // Add Health Checks
        services
            .AddHealthChecks()
            .AddApplicationStatus("api_status", tags: new[] { "api" })
            .AddNpgSql(configuration.GetConnectionString("Database")!,
                name: "sql",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "db", "sql", "Npgsql" });

        // Add Controllers
        services.AddControllers();

        // Add Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        // Add Middleware
        app.UseHttpsRedirection();
        //app.UseAuthorization();

        // Middleware para Prometheus
        //app.UseMetricServer();
        //app.MapMetrics(); // Exponer las métricas en "/metrics"
        //app.UseHttpMetrics(); // Mide las solicitudes HTTP automáticamente

        // Map Controllers
        app.MapControllers();

        // Use Exception Handler
        app.UseExceptionHandler(options => { });

        // Add Health Checks
        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        // Map the /metrics endpoint
        //app.UseOpenTelemetryPrometheusScrapingEndpoint();
        return app;
    }
}