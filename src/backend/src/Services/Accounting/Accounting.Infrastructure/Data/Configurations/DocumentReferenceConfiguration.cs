namespace Accounting.Infrastructure.Data.Configurations;

public class DocumentReferenceConfiguration : IEntityTypeConfiguration<DocumentReference>
{
    public void Configure(EntityTypeBuilder<DocumentReference> builder)
    {
        builder.ToTable("DocumentReferences");

        builder.HasKey(dr => dr.Id);

        builder.Property(dr => dr.Id)
            .HasConversion(
                id => id.Value,
                value => DocumentReferenceId.Of(value))
            .IsRequired();

        builder.Property(dr => dr.JournalEntryId)
            .HasConversion(
                id => id.Value,
                value => JournalEntryId.Of(value))
            .IsRequired();

        builder.Property(dr => dr.SourceType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(dr => dr.SourceId)
            .HasConversion(
                id => id.Value,
                value => SourceId.Of(value))
            .IsRequired();

        builder.Property(dr => dr.ReferenceNumber)
            .HasConversion(
                vo => vo.Value,
                value => DocumentReferenceNumber.Of(value))
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(dr => dr.Description)
            .HasMaxLength(500);

        builder.HasOne<JournalEntry>()
            .WithMany(j => j.DocumentReferences)
            .HasForeignKey(dr => dr.JournalEntryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(dr => new { dr.JournalEntryId, dr.SourceType, dr.SourceId })
            .IsUnique();
    }
}