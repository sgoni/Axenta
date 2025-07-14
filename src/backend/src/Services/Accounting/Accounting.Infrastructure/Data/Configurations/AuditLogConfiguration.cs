namespace Accounting.Infrastructure.Data.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLog");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(
            audilogId => audilogId.Value,
            dbId => AuditLogId.Of(dbId));

        builder.Property(a => a.EntityId).HasConversion(
            audilogId => audilogId.Value,
            dbId => EntityId.Of(dbId));

        builder.Property(e => e.Entity).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Action).HasMaxLength(20).IsRequired();
        builder.Property(e => e.PerformedAt).HasDefaultValueSql("now()");
        builder.Property(e => e.Details).HasColumnType("jsonb");
    }
}