namespace Reports.API.Endpoints.GeneralLedger;

public record GetGeneralLedgerQuery(Guid periodId, Guid companyId, Guid AccountId) : IQuery<GetGeneralLedgerResult>;

public record GetGeneralLedgerResult(IEnumerable<GeneralLedgerDto> generalLedgerDto);

public class GetGeneralLedgerHandler(IReportRepository repository)
    : IQueryHandler<GetGeneralLedgerQuery, GetGeneralLedgerResult>
{
    public async Task<GetGeneralLedgerResult> Handle(GetGeneralLedgerQuery query, CancellationToken cancellationToken)
    {
        var generalLedger = await repository.GetGeneralLedgerAsync(query.periodId, query.companyId, query.AccountId);
        return new GetGeneralLedgerResult(generalLedger);
    }
}