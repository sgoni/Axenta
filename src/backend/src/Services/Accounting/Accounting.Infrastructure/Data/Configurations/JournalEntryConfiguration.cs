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

        builder.Property(j => j.IsCanceled)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(j => j.PeriodId)
            .HasConversion(
                pid => pid!.Value,
                val => PeriodId.Of(val)
            )
            .HasColumnName("PeriodId");

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