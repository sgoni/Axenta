// Serilog consola
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddEmail(configuration);
builder.Services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
//builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();