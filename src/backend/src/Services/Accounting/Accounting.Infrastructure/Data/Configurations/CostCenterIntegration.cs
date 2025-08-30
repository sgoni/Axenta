namespace Accounting.Infrastructure.Data.Configurations;

public class CostCenterIntegration : IEntityTypeConfiguration<CostCenter>
{
    public void Configure(EntityTypeBuilder<CostCenter> builder)
    {
        builder.ToTable("CostCenters");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                pid => pid!.Value,
                val => CostCenterId.Of(val)
            )
            .IsRequired();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(55);

        builder.Property(a => a.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(a => a.CompanyId)
            .HasConversion(
                pid => pid!.Value,
                val => CompanyId.Of(val)
            )
            .IsRequired();

        builder.Property(a => a.ParentCostCenterId)
            .HasConversion(
                pid => pid!.Value,
                val => CostCenterId.Of(val)
            )
            .IsRequired();
    }
}