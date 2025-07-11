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

        builder.HasOne<DocumentReference>()
            .WithMany()
            .HasForeignKey(jl => jl.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}