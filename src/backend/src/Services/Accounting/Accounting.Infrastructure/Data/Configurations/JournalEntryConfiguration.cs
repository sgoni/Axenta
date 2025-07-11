namespace Accounting.Infrastructure.Data.Configurations;

public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        builder.ToTable("JournalEntry");
        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id).HasConversion(
            journalEntryLineId => journalEntryLineId.Value,
            dbId => JournalEntryId.Of(dbId));

        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.Description);
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

        builder.HasOne<JournalEntry>()
            .WithMany()
            .HasForeignKey(e => e.PeriodId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(j => j.JournalEntryLines)
            .WithOne()
            .HasForeignKey(jl => jl.JournalEntryId);
    }
}