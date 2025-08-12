namespace Accounting.Infrastructure.Data.Configurations;

public class CurrencyExchangeRateConfiguration : IEntityTypeConfiguration<CurrencyExchangeRate>
{
    public void Configure(EntityTypeBuilder<CurrencyExchangeRate> builder)
    {
        builder.ToTable("CurrencyExchangeRates");

        builder.HasKey(al => al.Id);

        builder.Property(al => al.Id)
            .HasConversion(
                pid => pid!.Value,
                val => CurrencyExchangeRateId.Of(val)
            )
            .IsRequired();

        builder.Property(al => al.CurrencyCode)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(j => j.Date)
            .IsRequired();

        builder.Property(l => l.BuyRate)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(l => l.SellRate)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}