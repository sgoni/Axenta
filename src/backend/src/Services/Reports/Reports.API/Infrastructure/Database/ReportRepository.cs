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
            await _db.QueryAsync<dynamic>(sql, new { PeriodId = periodId, CompanyId = companyId });
        return result
            .Select(x => ((IDictionary<string, object>)x).Adapt<BalanceSheetDto>())
            .ToList();
    }

    public async Task<IEnumerable<IncomeStatementDto>> GetIncomeStatementAsync(Guid periodId, Guid companyId)
    {
        var nameSpace = string.Concat(SqlLoader.ProjectName(), ".",
            "Infrastructure.Database.Sql.GetIncomeStatement.sql");
        var sql = SqlLoader.LoadSql(nameSpace);
        var result =
            await _db.QueryAsync<dynamic>(sql, new { PeriodId = periodId, CompanyId = companyId });
        return result
            .Select(x => ((IDictionary<string, object>)x).Adapt<IncomeStatementDto>())
            .ToList();
    }

    public async Task<IEnumerable<GeneralLedgerDto>> GetGeneralLedgerAsync(Guid periodId, Guid companyId,
        Guid accountId)
    {
        var nameSpace = string.Concat(SqlLoader.ProjectName(), ".", "Infrastructure.Database.Sql.GetGeneralLedger.sql");
        var sql = SqlLoader.LoadSql(nameSpace);
        var result =
            await _db.QueryAsync<dynamic>(sql,
                new { PeriodId = periodId, CompanyId = companyId, AccountId = accountId });
        return result
            .Select(x => ((IDictionary<string, object>)x).Adapt<GeneralLedgerDto>())
            .ToList();
    }
}