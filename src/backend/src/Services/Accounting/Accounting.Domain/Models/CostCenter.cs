namespace Accounting.Domain.Models;

public class CostCenter : Entity<CostCenterId>
{
    private readonly List<CostCenter> _children = new();

    private CostCenter()
    {
    } // EF

    public IReadOnlyCollection<CostCenter> Children => _children.AsReadOnly();

    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; } = true;

    public CompanyId CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;

    public CostCenterId? ParentCostCenterId { get; private set; }
    public CostCenter? ParentCostCenter { get; private set; }

    public static CostCenter Create(CostCenterId id, string code, string name, string? description,
        CompanyId companyId, CostCenterId? parentCostCenterId, CostCenter? parentCostCenter = null)
    {
        ValidateInputs(name, description);

        if (parentCostCenter != null && parentCostCenter.GetLevel() >= 3)
            throw new DomainException("CostCenter cannot be created beyond level 3.");

        return new CostCenter
        {
            Id = id,
            Code = code,
            Name = name,
            Description = description,
            CompanyId = companyId,
            ParentCostCenterId = parentCostCenterId,
            ParentCostCenter = parentCostCenter
        };
    }

    public void Update(string name, string? description, bool isActive, CostCenterId? parentCostCenterId,
        CostCenter? parentCostCenter = null)
    {
        ValidateInputs(name, description);

        if (parentCostCenter != null && parentCostCenter.GetLevel() >= 3)
            throw new DomainException("CostCenter cannot be moved beyond level 3.");

        Name = name;
        Description = description;
        isActive = isActive;
        ParentCostCenterId = parentCostCenterId;
        ParentCostCenter = parentCostCenter;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    // 🔎 Auxiliar: Calcula el nivel del CostCenter en el árbol
    public int GetLevel()
    {
        var level = 1;
        var current = ParentCostCenter;
        while (current != null)
        {
            level++;
            current = current.ParentCostCenter;
        }

        return level;
    }

    private static void ValidateInputs(string name, string? description)
    {
        //ArgumentException.ThrowIfNullOrWhiteSpace(code);
        //if (code.Length > 50) throw new DomainException("Code must be <= 50 characters");

        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.Length > 200) throw new DomainException("Name must be <= 200 characters");

        if (description?.Length > 1000) throw new DomainException("Description must be <= 1000 characters");
    }
}