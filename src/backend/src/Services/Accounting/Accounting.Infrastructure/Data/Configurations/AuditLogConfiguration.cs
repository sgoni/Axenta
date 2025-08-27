namespace Accounting.Infrastructure.Data.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(al => al.Id);

        builder.Property(al => al.Id)
            .HasConversion(
                pid => pid!.Value,
                val => AuditLogId.Of(val)
            )
            .IsRequired();

        builder.Property(al => al.Entity)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(al => al.Action)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(al => al.PerformedAt)
            .IsRequired();

        builder.Property(al => al.Details)
            .HasMaxLength(5000);

        builder.Property(al => al.EntityId)
            .HasConversion(
                eid => eid.Value,
                val => EntityId.Of(val)
            )
            .IsRequired()
            .HasColumnName("EntityId");

        builder.Property(al => al.PerformedBy)
            .HasConversion(
                pb => pb.Value,
                val => PerformedBy.Of(val)
            )
            .IsRequired()
            .HasColumnName("PerformedBy")
            .HasMaxLength(100);
    }
}