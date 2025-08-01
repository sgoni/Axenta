namespace Accounting.Infrastructure.Data.Configurations;

public class JournalEntryLineConfiguration : IEntityTypeConfiguration<JournalEntryLine>
{
    public void Configure(EntityTypeBuilder<JournalEntryLine> builder)
    {
        builder.ToTable("JournalEntryLines");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .HasConversion(
                lineId => lineId.Value,
                val => JournalEntryLineId.Of(val)
            )
            .IsRequired();

        builder.Property(l => l.JournalEntryId)
            .HasConversion(
                jeId => jeId.Value,
                val => JournalEntryId.Of(val)
            )
            .IsRequired()
            .HasColumnName("JournalEntryId");

        builder.Property(l => l.AccountId)
            .HasConversion(
                accId => accId.Value,
                val => AccountId.Of(val)
            )
            .IsRequired()
            .HasColumnName("AccountId");

        builder.Property(l => l.Debit)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(l => l.Credit)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(l => l.LineNumber)
            .IsRequired();

        builder.HasOne<JournalEntry>()
            .WithMany(j => j.JournalEntryLines)
            .HasForeignKey(l => l.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(l => new { l.JournalEntryId, l.Id, l.LineNumber })
            .IsUnique();
    }
}