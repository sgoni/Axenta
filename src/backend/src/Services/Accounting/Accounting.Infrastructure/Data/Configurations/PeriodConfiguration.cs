namespace Accounting.Infrastructure.Data.Configurations;

public class PeriodConfiguration : IEntityTypeConfiguration<Period>
{
    public void Configure(EntityTypeBuilder<Period> builder)
    {
        builder.ToTable("Period");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            periodId => periodId.Value,
            dbId => PeriodId.Of(dbId));

        builder.Property(e => e.Year).IsRequired();
        builder.Property(e => e.Month).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();
        builder.Property(e => e.IsClosed).HasDefaultValue(false);
    }
}