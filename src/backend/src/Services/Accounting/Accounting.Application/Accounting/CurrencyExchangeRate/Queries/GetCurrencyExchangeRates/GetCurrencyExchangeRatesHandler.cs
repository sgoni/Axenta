namespace Accounting.Application.Accounting.CurrencyExchangeRate.Queries.GetCurrencyExchangeRates;

public class GetCurrencyExchangeRatesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCurrencyExchangeRatesQuery, GetCurrencyExchangeRatesResult>
{
    public async Task<GetCurrencyExchangeRatesResult> Handle(GetCurrencyExchangeRatesQuery query,
        CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.CurrencyExchangeRates.LongCountAsync(cancellationToken);

        var currentExchangeRates = await dbContext.CurrencyExchangeRates
            .AsNoTracking()
            .OrderBy(cx => cx.Date)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetCurrencyExchangeRatesResult(
            new PaginatedResult<CurrencyExchangeRateDto>(
                pageIndex,
                pageSize,
                totalCount,
                currentExchangeRates.ToCurrencyExchangeRateDtoList()));
    }
}