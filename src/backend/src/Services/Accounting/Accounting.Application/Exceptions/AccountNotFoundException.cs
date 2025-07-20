namespace Accounting.Application.Exceptions;

public class AccountNotFoundException : NotFoundException
{
    public AccountNotFoundException(Guid id) : base($"Account with id {id} not found")
    {
    }
}