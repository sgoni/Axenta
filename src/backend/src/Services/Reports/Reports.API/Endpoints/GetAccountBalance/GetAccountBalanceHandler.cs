namespace Reports.API.Endpoints.GetAccountBalance;

public record GetAccountBalanceQuery(Guid AccountId) : IQuery<GetAccountBalanceResult>;

public record GetAccountBalanceResult(decimal? Balance);

public class GetAccountBalanceHandler(IReportRepository repository)
    : IQueryHandler<GetAccountBalanceQuery, GetAccountBalanceResult>
{
    public async Task<GetAccountBalanceResult> Handle(GetAccountBalanceQuery query,
        CancellationToken cancellationToken)
    {
        var balance = await repository.GetAccountBalanceAsync(query.AccountId);
        return new GetAccountBalanceResult(balance);
    }
}