namespace Accounting.Application.Accounting.CurrencyExchangeRate.Queries.GetCurrencyExchangeRateCurrent;

public class GetCurrencyExchangeRateCurrentHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCurrencyExchangeRateCurrentQuery, GetCurrencyExchangeRateCurrentResult>
{
    public async Task<GetCurrencyExchangeRateCurrentResult> Handle(GetCurrencyExchangeRateCurrentQuery query,
        CancellationToken cancellationToken)
    {
        // get urrencyExchangeRate by Id using dbContext
        // return result

        var currencyExchangeRate = await dbContext.CurrencyExchangeRates
            .AsNoTracking()
            .LastOrDefaultAsync(cancellationToken);

        return new GetCurrencyExchangeRateCurrentResult(currencyExchangeRate.DtoFromCurrencyExchangeRate());
    }
}