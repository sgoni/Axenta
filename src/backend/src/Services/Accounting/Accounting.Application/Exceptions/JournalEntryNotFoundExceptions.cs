namespace Accounting.Application.Exceptions;

public class JournalEntryNotFoundExceptions : NotFoundException
{
    public JournalEntryNotFoundExceptions(Guid id) : base($"JournalEntry with id {id} not found")
    {
    }
}