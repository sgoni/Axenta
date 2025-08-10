namespace Accounting.Domain.Models;

public class Company : Aggregate<CompanyId>
{
    public string Name { get; private set; }
    public string TaxId { get; private set; }
    public string Country { get; private set; }
    public string CurrencyCode { get; private set; }
    public bool IsActive { get; private set; } = true;

    public static Company Create(CompanyId id, string name, string taxId, string country, string currencyCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(taxId);
        ArgumentException.ThrowIfNullOrWhiteSpace(currencyCode);

        var company = new Company
        {
            Id = id,
            Name = name,
            TaxId = taxId,
            Country = country,
            IsActive = true
        };

        return company;
    }

    public void Update(string name, string taxId, string country, string currencyCode, bool isActive)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(taxId);
        ArgumentException.ThrowIfNullOrWhiteSpace(currencyCode);

        Name = name;
        TaxId = taxId;
        Country = country;
        IsActive = isActive;
    }
}