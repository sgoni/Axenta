namespace Accounting.Application.Exceptions;

public class AuditlogNotFoundException : NotFoundException
{
    public AuditlogNotFoundException(Guid id) : base($"Audit-log with id {id} not found")
    {
    }
}