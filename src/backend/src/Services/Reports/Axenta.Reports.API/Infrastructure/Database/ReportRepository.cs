namespace Axenta.Reports.API.Infrastructure.Database;

public class ReportRepository : IReportRepository
{
    private readonly DapperContext _dapperContext;

    public ReportRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<BalanceSheetDto>> GetBalanceSheetAsync(Guid periodId, Guid companyId)
    {
        var nameSpace = string.Concat(SqlLoader.ProjectName(), ".", "Infrastructure.Database.Sql.GetBalanceSheet.sql");
        var sql = SqlLoader.LoadSql(nameSpace);
        using var connection = _dapperContext.CreateConnection();
        var result =
            await connection.QueryAsync<dynamic>(sql, param: new { PeriodId = periodId, CompanyId = companyId });
        return result
            .Select(x => ((IDictionary<string, object>)x).Adapt<BalanceSheetDto>())
            .ToList();
    }
}