namespace Accounting.Domain.Models;

public class CurrencyExchangeRate : Entity<CurrencyExchangeRateId>
{
    public string CurrencyCode { get; private set; } // "USD", "EUR"
    public DateOnly Date { get; private set; }
    public decimal BuyRate { get; private set; }
    public decimal SellRate { get; private set; }

    public static CurrencyExchangeRate Create(CurrencyExchangeRateId id, string currencyCode, DateOnly date,
        decimal buyRate, decimal sellRate)
    {
        var currencyExchangeRate = new CurrencyExchangeRate
        {
            Id = id,
            CurrencyCode = currencyCode,
            Date = date,
            BuyRate = buyRate,
            SellRate = sellRate
        };

        return currencyExchangeRate;
    }
}