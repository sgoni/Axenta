namespace Reports.API.Endpoints.GetAccountBalanceByPeriod;

public record GetAccountBalanceByPeriodQuery(Guid periodId, Guid accountId) : IQuery<GetAccountBalanceByPeriodResult>;

public record GetAccountBalanceByPeriodResult(decimal? Balance);

public class GetAccountBalanceByPeriodHandler(IReportRepository repository)
    : IQueryHandler<GetAccountBalanceByPeriodQuery, GetAccountBalanceByPeriodResult>
{
    public async Task<GetAccountBalanceByPeriodResult> Handle(GetAccountBalanceByPeriodQuery query,
        CancellationToken cancellationToken)
    {
        var balance = await repository.GetAccountBalanceByPeriodAsync(query.accountId, query.periodId);
        return new GetAccountBalanceByPeriodResult(balance);
    }
}