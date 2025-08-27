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

    public static CurrencyExchangeRateDto ToCurrencyExchangeRateDto(this CurrencyExchangeRate currencyExchangeRate)
    {
        return DtoFromCurrencyExchangeRate(currencyExchangeRate);
    }

    public static CurrencyExchangeRateDto DtoFromCurrencyExchangeRate(CurrencyExchangeRate currencyExchangeRate)
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