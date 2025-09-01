namespace Accounting.Infrastructure.Data.Configurations;

public class JournalEntryLineConfiguration : IEntityTypeConfiguration<JournalEntryLine>
{
    public void Configure(EntityTypeBuilder<JournalEntryLine> builder)
    {
        builder.ToTable("JournalEntryLines");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .HasConversion(
                id => id.Value,
                val => JournalEntryLineId.Of(val))
            .IsRequired();

        builder.Property(l => l.JournalEntryId)
            .HasConversion(
                jeId => jeId.Value,
                val => JournalEntryId.Of(val))
            .IsRequired();

        builder.Property(l => l.AccountId)
            .HasConversion(
                accId => accId.Value,
                val => AccountId.Of(val))
            .IsRequired();
        // Debit (Amount + Currency)
        builder.OwnsOne(l => l.Debit, debit =>
        {
            debit.Property(p => p.Amount)
                .HasColumnName("DebitAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            debit.Property(p => p.CurrencyCode)
                .HasColumnName("DebitCurrency")
                .HasMaxLength(3)
                .IsRequired();
        });

        // Credit (Amount + Currency)
        builder.OwnsOne(l => l.Credit, credit =>
        {
            credit.Property(p => p.Amount)
                .HasColumnName("CreditAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            credit.Property(p => p.CurrencyCode)
                .HasColumnName("CreditCurrency")
                .HasMaxLength(3)
                .IsRequired();
        });

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