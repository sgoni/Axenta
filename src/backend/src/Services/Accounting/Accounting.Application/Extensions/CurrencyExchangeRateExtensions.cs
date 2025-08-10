namespace Accounting.Application.Extensions;

public static class CurrencyExchangeRateExtensions
{
    public static IEnumerable<CurrencyExchangeRateDto> ToCurrencyExchangeRateDtoList(
        this IEnumerable<CurrencyExchangeRate> currencyExchangeRates)
    {
        return currencyExchangeRates.Select(currencyExchangeRate => new CurrencyExchangeRateDto(
            currencyExchangeRate.Id.Value,
            currencyExchangeRate.CurrencyCode,
            currencyExchangeRate.Date,
            currencyExchangeRate.BuyRate,
            currencyExchangeRate.SellRate
        ));
    }

    public static CurrencyExchangeRateDto DtoFromCurrencyExchangeRate(this CurrencyExchangeRate currencyExchangeRate)
    {
        return new CurrencyExchangeRateDto(
            currencyExchangeRate.Id.Value,
            currencyExchangeRate.CurrencyCode,
            currencyExchangeRate.Date,
            currencyExchangeRate.BuyRate,
            currencyExchangeRate.SellRate
        );
    }
}