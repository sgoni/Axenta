namespace Accounting.Infrastructure.Data.Configurations;

public class DocumentReferenceConfiguration : IEntityTypeConfiguration<DocumentReference>
{
    public void Configure(EntityTypeBuilder<DocumentReference> builder)
    {
        builder.ToTable("DocumentReferences");

        builder.HasKey(dr => dr.Id);

        builder.Property(dr => dr.Id)
            .HasConversion(
                jeId => jeId.Value,
                val => DocumentReferenceId.Of(val)
            )
            .IsRequired();

        builder.Property(dr => dr.JournalEntryId)
            .HasConversion(
                jeId => jeId.Value,
                val => JournalEntryId.Of(val)
            )
            .IsRequired()
            .HasColumnName("JournalEntryId");

        builder.Property(dr => dr.SourceType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(dr => dr.SourceId)
            .HasConversion(
                src => src.Value,
                val => SourceId.Of(val)
            )
            .IsRequired()
            .HasColumnName("SourceId");

        builder.Property(dr => dr.ReferenceNumber)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(dr => dr.Description)
            .HasMaxLength(200);

        builder.HasOne<JournalEntry>()
            .WithMany(j => j.DocumentReferences)
            .HasForeignKey(dr => dr.JournalEntryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(dr => new { dr.JournalEntryId, dr.SourceType, dr.SourceId })
            .IsUnique();
    }
}