namespace Accounting.Infrastructure.Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Account");
        builder.HasKey(ac => ac.Id);
        builder.Property(ac => ac.Id).HasConversion(
            accountId => accountId.Value,
            dbId => AccountId.Of(dbId));

        builder.Property(ac => ac.Code).HasMaxLength(20).IsRequired();
        builder.Property(ac => ac.Name).HasMaxLength(100).IsRequired();
        builder.Property(ac => ac.IsActive).HasDefaultValue(true);

        builder.HasOne<AccountType>()
            .WithMany()
            .HasForeignKey(ac => ac.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(ac => ac.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}