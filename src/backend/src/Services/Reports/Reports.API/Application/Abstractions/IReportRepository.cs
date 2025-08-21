namespace Axenta.Reports.API.Application.Abstractions;

public interface IReportRepository
{
    Task<decimal?> GetAccountBalanceAsync(Guid accountId);
    Task<decimal?> GetAccountBalanceByPeriodAsync(Guid accountId, Guid periodId);
    Task<IEnumerable<BalanceSheetDto>> GetBalanceSheetAsync(Guid periodId, Guid companyId);
    Task<IEnumerable<IncomeStatementDto>> GetIncomeStatementAsync(Guid periodId, Guid companyId);
    Task<IEnumerable<GeneralLedgerDto>> GetGeneralLedgerAsync(Guid periodId, Guid companyId, Guid accountId);
    Task<IEnumerable<TrialBalanceDto>> GetTrialBalanceAsync(Guid periodId, Guid companyId);
}