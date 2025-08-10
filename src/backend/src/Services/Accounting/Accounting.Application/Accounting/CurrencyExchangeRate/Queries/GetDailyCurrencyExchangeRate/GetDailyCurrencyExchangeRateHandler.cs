namespace Accounting.Application.Accounting.CurrencyExchangeRate.Queries.GetCurrencyExchangeRateCurrent;

public class GetDailyCurrencyExchangeRateHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetDailyCurrencyExchangeRateQuery, GetDailyCurrencyExchangeRatResult>
{
    public async Task<GetDailyCurrencyExchangeRatResult> Handle(GetDailyCurrencyExchangeRateQuery query,
        CancellationToken cancellationToken)
    {
        // get urrencyExchangeRate by Id using dbContext
        // return result

        var currencyExchangeRate = await dbContext.CurrencyExchangeRates
            .AsNoTracking()
            .OrderBy(cx => cx.Date)
            .LastOrDefaultAsync(cancellationToken);

        return new GetDailyCurrencyExchangeRatResult(currencyExchangeRate.DtoFromCurrencyExchangeRate());
    }
}