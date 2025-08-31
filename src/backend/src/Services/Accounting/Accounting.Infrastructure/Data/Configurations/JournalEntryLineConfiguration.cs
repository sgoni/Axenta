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
                val => JournalEntryLineId.Of(val))
            .IsRequired();

        builder.Property(l => l.JournalEntryId)
            .HasConversion(
                jeId => jeId.Value,
                val => JournalEntryId.Of(val))
            .IsRequired()
            .HasColumnName("JournalEntryId");

        builder.Property(l => l.AccountId)
            .HasConversion(
                accId => accId.Value,
                val => AccountId.Of(val))
            .IsRequired()
            .HasColumnName("AccountId");

        // Mapeo de Debit como Money (decimal + currency)
        builder.OwnsOne(l => l.Debit, debit =>
        {
            debit.Property(d => d.Amount)
                 .HasColumnName("DebitAmount")
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();

            debit.Property(d => d.CurrencyCode)
                 .HasColumnName("DebitCurrency")
                 .HasMaxLength(3)
                 .IsRequired();
        });

        // Mapeo de Credit como Money (decimal + currency)
        builder.OwnsOne(l => l.Credit, credit =>
        {
            credit.Property(c => c.Amount)
                  .HasColumnName("CreditAmount")
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();

            credit.Property(c => c.CurrencyCode)
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
