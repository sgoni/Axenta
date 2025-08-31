namespace Accounting.Domain.Models;

/// <summary>
///     Company
/// </summary>
public class Company : Entity<CompanyId>
{
    public string Name { get; private set; } = default!;
    public string TaxId { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string CurrencyCode { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;

    private Company() { } // EF

    public static Company Create(CompanyId id, string name, string taxId, string country, string currencyCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(taxId);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        ArgumentException.ThrowIfNullOrWhiteSpace(currencyCode);

        return new Company
        {
            Id = id,
            Name = name,
            TaxId = taxId,
            Country = country,
            CurrencyCode = currencyCode,
            IsActive = true
        };
    }

    public void Update(string name, string taxId, string country, string currencyCode, bool isActive)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(taxId);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        ArgumentException.ThrowIfNullOrWhiteSpace(currencyCode);

        Name = name;
        TaxId = taxId;
        Country = country;
        CurrencyCode = currencyCode;
        IsActive = isActive;
    }
}