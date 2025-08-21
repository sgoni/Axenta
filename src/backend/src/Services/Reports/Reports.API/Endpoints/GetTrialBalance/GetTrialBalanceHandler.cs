namespace Reports.API.Endpoints.GetTrialBalance;

public record GetTrialBalanceQuery(Guid periodId, Guid companyId) : IQuery<GetTrialBalanceResult>;

public record GetTrialBalanceResult(IEnumerable<TrialBalanceDto> trialBalanceDto);

public class GetTrialBalanceHandler(IReportRepository repository)
    : IQueryHandler<GetTrialBalanceQuery, GetTrialBalanceResult>
{
    public async Task<GetTrialBalanceResult> Handle(GetTrialBalanceQuery query, CancellationToken cancellationToken)
    {
        var trialBalance = await repository.GetTrialBalanceAsync(query.periodId, query.companyId);
        return new GetTrialBalanceResult(trialBalance);
    }
}