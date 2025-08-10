namespace Accounting.Application.Exceptions;

public class CurrencyExchangeRateException : NotFoundException
{
    public CurrencyExchangeRateException(Guid id) : base($"CurrencyExchangeRate with id {id} not found")
    {
    }
}