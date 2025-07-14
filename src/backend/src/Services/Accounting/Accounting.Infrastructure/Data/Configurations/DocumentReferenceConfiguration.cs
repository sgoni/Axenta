namespace Accounting.Infrastructure.Data.Configurations;

public class DocumentReferenceConfiguration : IEntityTypeConfiguration<DocumentReference>
{
    public void Configure(EntityTypeBuilder<DocumentReference> builder)
    {
        builder.ToTable("DocumentReference");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasConversion(
            documentId => documentId.Value,
            dbId => DocumentReferenceId.Of(dbId));

        builder.Property(d => d.JournalEntryId).HasConversion(
            journalEntryId => journalEntryId.Value,
            dbId => JournalEntryId.Of(dbId));
        
        builder.Property(d => d.SourceId).HasConversion(
            sourceId => sourceId.Value,
            dbId => SourceId.Of(dbId));
        
        builder.Property(e => e.SourceId);
        builder.Property(e => e.SourceType).HasMaxLength(50).IsRequired();

        builder.HasOne<DocumentReference>()
            .WithMany()
            .HasForeignKey(jl => jl.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}