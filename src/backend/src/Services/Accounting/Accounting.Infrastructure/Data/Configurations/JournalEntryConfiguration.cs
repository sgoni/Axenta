namespace Accounting.Infrastructure.Data.Configurations;

public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        builder.ToTable("JournalEntries");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Id)
            .HasConversion(
                id => id.Value,
                val => JournalEntryId.Of(val))
            .IsRequired();

        builder.Property(j => j.PeriodId)
            .HasConversion(
                id => id.Value,
                val => PeriodId.Of(val))
            .HasColumnName("PeriodId");

        builder.Property(j => j.CompanyId)
            .HasConversion(
                id => id.Value,
                val => CompanyId.Of(val))
            .IsRequired()
            .HasColumnName("CompanyId");

        builder.Property(j => j.Date).IsRequired();

        builder.Property(j => j.Description)
            .HasMaxLength(500);

        builder.Property(j => j.CurrencyCode)
            .HasMaxLength(3);

        builder.Property(j => j.ExchangeRate)
            .HasColumnType("decimal(18,6)");

        builder.Property(j => j.ExchangeRateDate);

        builder.Property(j => j.JournalEntryType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(j => j.ReversalJournalEntryId)
            .HasConversion(
                id => id!.Value,
                val => JournalEntryId.Of(val))
            .HasColumnName("ReversalJournalEntryId");

        // relación con líneas
        builder.HasMany(j => j.JournalEntryLines)
            .WithOne()
            .HasForeignKey(l => l.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        // relación con referencias de documentos
        builder.HasMany(j => j.DocumentReferences)
            .WithOne()
            .HasForeignKey(dr => dr.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}