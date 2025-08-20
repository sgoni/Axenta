namespace Reports.API.Endpoints.IncomeStatement;

public record GetIncomeStatementrQuery(Guid periodId, Guid companyId) : IQuery<GetIncomeStatementrResult>;

public record GetIncomeStatementrResult(IEnumerable<IncomeStatementDto> incomeStatementDto);

public class GetIncomeStatementHandler(IReportRepository repository)
    : IQueryHandler<GetIncomeStatementrQuery, GetIncomeStatementrResult>
{
    public async Task<GetIncomeStatementrResult> Handle(GetIncomeStatementrQuery query,
        CancellationToken cancellationToken)
    {
        var incomeStatement = await repository.GetIncomeStatementAsync(query.periodId, query.companyId);
        return new GetIncomeStatementrResult(incomeStatement);
    }
}