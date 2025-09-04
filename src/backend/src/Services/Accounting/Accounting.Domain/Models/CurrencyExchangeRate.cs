namespace Accounting.Domain.Models;

public class CurrencyExchangeRate : Entity<CurrencyExchangeRateId>
{
    private CurrencyExchangeRate()
    {
    } // EF

    public string CurrencyCode { get; private set; } = default!;
    public DateOnly Date { get; private set; }
    public decimal BuyRate { get; private set; }
    public decimal SellRate { get; private set; }

    public static CurrencyExchangeRate Create(CurrencyExchangeRateId id, string currencyCode, DateOnly date,
        decimal buyRate, decimal sellRate)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
            throw new DomainException("CurrencyCode is required");

        if (currencyCode.Length > 3)
            throw new DomainException("CurrencyCode must be a 3-letter ISO code");

        if (buyRate <= 0)
            throw new DomainException("BuyRate must be greater than zero");

        if (sellRate <= 0)
            throw new DomainException("SellRate must be greater than zero");

        return new CurrencyExchangeRate
        {
            Id = id,
            CurrencyCode = currencyCode.ToUpperInvariant(),
            Date = date,
            BuyRate = buyRate,
            SellRate = sellRate
        };
    }
}