using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accounting.Infrastructure.Data.Configurations
{
    public class CostCenterConfiguration : IEntityTypeConfiguration<CostCenter>
    {
        public void Configure(EntityTypeBuilder<CostCenter> builder)
        {
            // Tabla
            builder.ToTable("CostCenters");

            // ValueConverters para los VOs
            var costCenterIdConverter = new ValueConverter<CostCenterId, Guid>(
                v => v.Value,
                v => CostCenterId.Of(v));

            var companyIdConverter = new ValueConverter<CompanyId, Guid>(
                v => v.Value,
                v => CompanyId.Of(v));

            //var nullableCostCenterIdConverter = new ValueConverter<CostCenterId?, Guid?>(
            //    v => v.HasValue ? v.Value.Value : null,
            //    v => v.HasValue ? CostCenterId.Of(v.Value) : null);

            // Clave primaria
            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Id)
                .HasConversion(costCenterIdConverter)
                .ValueGeneratedNever();

            // Propiedades
            builder.Property(cc => cc.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cc => cc.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(cc => cc.Description)
                .HasMaxLength(1000);

            builder.Property(cc => cc.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Foreign key CompanyId
            builder.Property(cc => cc.CompanyId)
                .HasConversion(companyIdConverter)
                .IsRequired();

            builder.HasOne(cc => cc.Company)
                .WithMany()
                .HasForeignKey(cc => cc.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones jerárquicas (ParentCostCenter)
            //builder.Property(cc => cc.ParentCostCenterId)
            //       .HasConversion(nullableCostCenterIdConverter);

            builder.Property(cc => cc.ParentCostCenterId)
                .HasConversion(
                    pid => pid!.Value,
                    val => CostCenterId.FromNullable(val)
                )
                .IsRequired();

            builder.HasOne(cc => cc.ParentCostCenter)
                .WithMany(cc => cc.Children)
                .HasForeignKey(cc => cc.ParentCostCenterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Acceso al campo privado _children
            builder.Metadata
                .FindNavigation(nameof(CostCenter.Children))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Índices opcionales
            builder.HasIndex(cc => cc.Code);
            builder.HasIndex(cc => cc.CompanyId);
        }
    }
}