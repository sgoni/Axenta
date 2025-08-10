namespace Accounting.Application.Accounting.CurrencyExchangeRate.Queries.GetCurrencyExchangeRates;

public record GetCurrencyExchangeRatesQuery(PaginationRequest PaginationRequest)
    : IQuery<GetCurrencyExchangeRatesResult>;

public record GetCurrencyExchangeRatesResult(PaginatedResult<CurrencyExchangeRateDto> currencyExchangeRates);