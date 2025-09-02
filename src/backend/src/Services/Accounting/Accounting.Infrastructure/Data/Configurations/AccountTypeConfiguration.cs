namespace Accounting.Infrastructure.Data.Configurations;

public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        builder.ToTable("AccountTypes");

        builder.HasKey(at => at.Id);

        builder.Property(at => at.Id)
            .HasConversion(
                id => id.Value,
                val => AccountTypeId.Of(val)
            )
            .IsRequired();

        builder.Property(at => at.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(at => at.Description)
            .HasMaxLength(500);

        // Relación uno a muchos con Accounts
        builder.HasMany(at => at.Accounts)
            .WithOne(a => a.Type)
            .HasForeignKey(a => a.AccountTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}