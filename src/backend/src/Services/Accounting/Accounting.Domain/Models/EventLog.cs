namespace Accounting.Domain.Models;

public class EventLog : Entity<EventLogId>
{
    public Guid MessageId { get; set; } // Viene del broker (context.MessageId)
    public DateTime ProcessedAt { get; set; }
}