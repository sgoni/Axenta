namespace Accounting.Application.Exceptions;

public class AccountNotFoundException : NotFoundException
{
    public AccountNotFoundException(Guid id) : base($"AccountDetail with id {id} not found")
    {
    }
}