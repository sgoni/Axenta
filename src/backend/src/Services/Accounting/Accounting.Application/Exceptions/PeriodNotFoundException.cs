namespace Accounting.Application.Exceptions;

public class PeriodNotFoundException : NotFoundException
{
    public PeriodNotFoundException(Guid id) : base($"Period with id {id} not found")
    {
    }
}