namespace Accounting.Application.Exceptions;

public class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(Guid id) : base($"CompanyId with id {id} not found")
    {
    }
}