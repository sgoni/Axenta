namespace Axenta.Reports.API.Application.Abstractions;

public interface IReportRepository
{
    Task<IEnumerable<BalanceSheetDto>> GetBalanceSheetAsync(Guid periodId, Guid companyId);
}