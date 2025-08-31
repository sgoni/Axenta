namespace Accounting.Infrastructure.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                pid => pid!.Value,
                val => CompanyId.Of(val)
            )
            .IsRequired();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.TaxId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.TaxId).IsUnique();

        builder.Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.CurrencyCode)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(a => a.IsActive)
            .HasDefaultValue(true)
            .IsRequired();
    }
}