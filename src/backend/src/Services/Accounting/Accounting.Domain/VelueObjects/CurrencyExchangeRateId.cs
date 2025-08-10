namespace Accounting.Domain.VelueObjects;

public class CurrencyExchangeRateId
{
    private CurrencyExchangeRateId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static CurrencyExchangeRateId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("AccountId cannot be empty");

        return new CurrencyExchangeRateId(value);
    }
}