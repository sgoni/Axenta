namespace Accounting.Domain.Models;

public class CostCenter : Entity<CostCenterId>
{
    public string Code { get; set; } = default!; // Unique Code, Ex: "CC-001"
    public string Name { get; set; } = default!; // Descriptive name
    public string? Description { get; set; } // Free text
    public bool IsActive { get; set; } = true; // Activate/deactivate

    // Relationship with company (multi company)
    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; } = default!;

    // Optional: hierarchy of cost centers
    public CostCenterId? ParentCostCenterId { get; set; }
    public CostCenter? ParentCostCenter { get; set; }
}