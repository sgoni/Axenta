namespace Accounting.Infrastructure.Data.Configurations;

public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
{
    public void Configure(EntityTypeBuilder<EventLog> builder)
    {
        builder.ToTable("EventLogs");

        builder.HasKey(dr => dr.Id);

        builder.Property(dr => dr.Id)
            .HasConversion(
                jeId => jeId.Value,
                val => EventLogId.Of(val)
            )
            .IsRequired();

        builder.Property(ev => ev.MessageId)
            .IsRequired();

        builder.Property(ev => ev.ProcessedAt)
            .IsRequired();
    }
}