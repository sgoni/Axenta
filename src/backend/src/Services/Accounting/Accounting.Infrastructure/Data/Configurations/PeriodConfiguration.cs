namespace Accounting.Infrastructure.Data.Configurations;

public class PeriodConfiguration : IEntityTypeConfiguration<Period>
{
    public void Configure(EntityTypeBuilder<Period> builder)
    {
        builder.ToTable("Periods");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                pid => pid.Value,
                val => PeriodId.Of(val)
            )
            .IsRequired();

        builder.Property(p => p.Year)
            .IsRequired();

        builder.Property(p => p.Month)
            .IsRequired();

        builder.Property(p => p.StartDate)
            .IsRequired();

        builder.Property(p => p.EndDate)
            .IsRequired();

        builder.Property(p => p.CompanyId)
            .HasConversion(
                pid => pid.Value,
                val => CompanyId.Of(val)
            )
            .IsRequired();

        builder.Property(p => p.IsClosed)
            .IsRequired();

        builder.HasIndex(p => new { p.Year, p.Month })
            .IsUnique();
    }
}