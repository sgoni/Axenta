namespace Accounting.Infrastructure.Data.Configurations;

public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        builder.ToTable("JournalEntries");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Id)
            .HasConversion(
                pid => pid!.Value,
                val => JournalEntryId.Of(val)
            )
            .IsRequired();

        builder.Property(j => j.Date)
            .IsRequired();

        builder.Property(j => j.Description)
            .HasMaxLength(500);

        builder.Property(j => j.PeriodId)
            .HasConversion(
                pid => pid!.Value,
                val => PeriodId.Of(val)
            )
            .HasColumnName("PeriodId");

        builder.Property(j => j.CompanyId)
            .HasConversion(
                pid => pid!.Value,
                val => CompanyId.Of(val)
            )
            .HasColumnName("CompanyId");

        builder.Property(j => j.CurrencyCode)
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(j => j.ExchangeRate)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(j => j.ExchangeRateDate)
            .IsRequired(false);

        builder.Property(j => j.JournalEntryType)
            .HasMaxLength(55)
            .IsRequired();

        builder.Property(j => j.ReversalJournalEntryId)
            .HasConversion(
                pid => pid!.Value,
                val => JournalEntryId.Of(val)
            )
            .IsRequired(false);

        builder.HasMany(j => j.DocumentReferences)
            .WithOne()
            .HasForeignKey(dr => dr.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(j => j.JournalEntryLines)
            .WithOne()
            .HasForeignKey(el => el.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}