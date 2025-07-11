namespace Accounting.Infrastructure.Data.Configurations;

public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        builder.ToTable("AccountType");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            accountTypeId => accountTypeId.Value,
            dbId => TypeId.Of(dbId));

        builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(100);
    }
}