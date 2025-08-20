using Axenta.Reports.API.Infrastructure;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration, assembly);

var app = builder.Build();

ConfigureMiddleware(app);
app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, Assembly assembly)
{
    string? AccountingDb;

    // Add MediatR
    services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(assembly);
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        cfg.AddOpenBehavior(typeof(LogginBehavior<,>));
    });

    // Add Validators
    services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

    // Add Carter
    services.AddCarter();

    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var configProvider = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        // Get base configuration
        var server = configProvider["DatabaseConfig:server"];
        var port = configProvider["DatabaseConfig:Port"];
        var database = configProvider["DatabaseConfig:Database"];

        // Assemble the connection string
        AccountingDb =
            $"Server={server};Port={port};Database={database};User Id=postgres;Password=postgres;Include Error Detail=true";
    }

    // Add Exception Handler
    services.AddExceptionHandler<CustomExceptionHandler>();

    // Add Serilog
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    builder.Host.UseSerilog();

    // Add Health Checks
    services
        .AddHealthChecks()
        .AddApplicationStatus("api_status", tags: new[] { "api" })
        .AddNpgSql(AccountingDb!,
            name: "sql",
            failureStatus: HealthStatus.Degraded,
            tags: new[] { "db", "sql", "Npgsql" });

    // Add Controllers
    services.AddControllers();

    // Add Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // Add Dapper (connection string desde appsettings.json)
    services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(AccountingDb));

    services.AddScoped<IReportRepository, ReportRepository>();

    // Add Mapping
    MapsterConfig.RegisterBalanceSheetMaping();
}

void ConfigureMiddleware(WebApplication app)
{
    // Map Carter Endpoints
    app.MapCarter();

    // Configure Swagger for Development
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Add Middleware
    app.UseHttpsRedirection();
    app.UseAuthorization();

    // Map Controllers
    app.MapControllers();

    // Use Exception Handler
    app.UseExceptionHandler(options => { });

    // Add Health Checks
    app.UseHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
}