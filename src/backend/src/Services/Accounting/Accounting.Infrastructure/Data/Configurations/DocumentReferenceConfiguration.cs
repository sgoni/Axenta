namespace Accounting.Infrastructure.Data.Configurations;

public class DocumentReferenceConfiguration : IEntityTypeConfiguration<DocumentReference>
{
    public void Configure(EntityTypeBuilder<DocumentReference> builder)
    {
        builder.ToTable("DocumentReferences");
        builder.HasKey(dr => dr.Id);

        builder.Property(dr => dr.Id)
            .HasConversion(
                drId => drId.Value,
                value => DocumentReferenceId.Of(value))
            .IsRequired();

        builder.Property(dr => dr.JournalEntryId)
            .HasConversion(
                jeId => jeId.Value,
                value => JournalEntryId.Of(value))
            .IsRequired();

        builder.Property(dr => dr.SourceType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(dr => dr.SourceId)
            .HasConversion(
                src => src.Value,
                val => SourceId.Of(val))
            .IsRequired();

        builder.Property(dr => dr.ReferenceNumber)
            .HasConversion(
                jeId => jeId.Value,
                val => DocumentReferenceNumber.Of(val))
            .HasMaxLength(150)
            .IsRequired();

        //   builder.Property(dr => dr.ReferenceNumber).HasMaxLength(150).IsRequired();
        builder.Property(dr => dr.Description)
            .HasMaxLength(500);

        builder.HasOne<JournalEntry>()
            .WithMany(j => j.DocumentReferences)
            .HasForeignKey(dr => dr.JournalEntryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(dr => new { dr.JournalEntryId, dr.SourceType, dr.SourceId }).IsUnique();
    }
}