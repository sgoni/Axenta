namespace Accounting.Application.Accounting.Periods.Queries.GetPeriodById;

public class GetPeriodByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPeriodByIdQuery, GetPeriodByIdQueryResult>
{
    public async Task<GetPeriodByIdQueryResult> Handle(GetPeriodByIdQuery query, CancellationToken cancellationToken)
    {
        // get periods by Id using dbContext
        // return result

        var periodId = PeriodId.Of(query.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period == null) throw EntityNotFoundException.For<Period>(query.PeriodId);

        return new GetPeriodByIdQueryResult(period.ToPeriodDto());
    }
}