namespace Accounting.Infrastructure.Data.Configurations;

public class JournalEntryLineConfiguration : IEntityTypeConfiguration<JournalEntryLine>
{
    public void Configure(EntityTypeBuilder<JournalEntryLine> builder)
    {
        builder.ToTable("JournalEntryLine");
        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id).HasConversion(
            journalEntryLineId => journalEntryLineId.Value,
            dbId => JournalEntryLineId.Of(dbId));

        builder.Property(e => e.Debit).HasColumnType("numeric(18,2)").HasDefaultValue(0);
        builder.Property(e => e.Credit).HasColumnType("numeric(18,2)").HasDefaultValue(0);

        builder.HasCheckConstraint("CK_Debit_NonNegative", "\"Debit\" >= 0");
        builder.HasCheckConstraint("CK_Credit_NonNegative", "\"Credit\" >= 0");

        builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(j => j.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}