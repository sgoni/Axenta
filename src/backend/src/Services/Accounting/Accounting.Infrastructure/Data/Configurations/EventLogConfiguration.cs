namespace Accounting.Infrastructure.Data.Configurations;

public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
{
    public void Configure(EntityTypeBuilder<EventLog> builder)
    {
        builder.ToTable("EventLogs");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => EventLogId.Of(value))
            .IsRequired();

        builder.Property(e => e.MessageId)
            .IsRequired();

        builder.Property(e => e.ProcessedAt)
            .IsRequired();
    }
}