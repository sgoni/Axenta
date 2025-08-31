namespace Accounting.Domain.VelueObjects;

public record CurrencyExchangeRateId : GuidValueObject
{
    public CurrencyExchangeRateId(Guid value) : base(value)
    {
    }

    public static CurrencyExchangeRateId Of(Guid value)
    {
        return new CurrencyExchangeRateId(value);
    }
}