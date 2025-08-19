namespace Axenta.Reports.API.Endpoints.GetBalanceSheet;

public record GetBalanceSheetQuery(Guid periodId, Guid companyId) : IQuery<GetBalanceSheetResult>;

public record GetBalanceSheetResult(IEnumerable<BalanceSheetDto> balanceSheet);

public class GetBalanceSheetHandler(IReportRepository repository)
    : IQueryHandler<GetBalanceSheetQuery, GetBalanceSheetResult>
{
    public async Task<GetBalanceSheetResult> Handle(GetBalanceSheetQuery query, CancellationToken cancellationToken)
    {
        var balanceSheet = await repository.GetBalanceSheetAsync(query.periodId, query.companyId);
        return new GetBalanceSheetResult(balanceSheet);
    }
}