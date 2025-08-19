namespace Axenta.Reports.API.Infrastructure.Database;

public class DapperContext
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;
    private readonly DatabaseConfig _databaseConfig;

    public DapperContext(IOptions<DatabaseConfig> options)
    {
        _databaseConfig = options.Value;

        _connectionString = FiddleHelper.GetConnectionStringSqlServer(_databaseConfig.DataSource,
            _databaseConfig.Catalog,
            _databaseConfig.Id, _databaseConfig.Password);
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}