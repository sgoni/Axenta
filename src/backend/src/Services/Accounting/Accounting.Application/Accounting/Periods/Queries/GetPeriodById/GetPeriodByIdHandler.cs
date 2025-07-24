namespace Accounting.Application.Accounting.Periods.Queries.GetPeriodById;

public class GetPeriodByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPeriodByIdQuery, GetPeriodByIdQueryResult>
{
    public async Task<GetPeriodByIdQueryResult> Handle(GetPeriodByIdQuery query, CancellationToken cancellationToken)
    {
        // get periods by Id using dbContext
        // return result

        var period = await dbContext.Periods.FindAsync(query.PeriodId, cancellationToken);

        if (period == null) throw new PeriodNotFoundException(query.PeriodId);

        return new GetPeriodByIdQueryResult(period.DtoFromPeriod());
    }
}