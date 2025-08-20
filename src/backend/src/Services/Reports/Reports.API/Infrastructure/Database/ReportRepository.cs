namespace Axenta.Reports.API.Infrastructure.Database;

public class ReportRepository : IReportRepository
{
    private readonly IDbConnection _db;

    public ReportRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<BalanceSheetDto>> GetBalanceSheetAsync(Guid periodId, Guid companyId)
    {
        var nameSpace = string.Concat(SqlLoader.ProjectName(), ".", "Infrastructure.Database.Sql.GetBalanceSheet.sql");
        var sql = SqlLoader.LoadSql(nameSpace);
        var result =
            await _db.QueryAsync<dynamic>(sql, param: new { PeriodId = periodId, CompanyId = companyId });
        return result
            .Select(x => ((IDictionary<string, object>)x).Adapt<BalanceSheetDto>())
            .ToList();
    }
}