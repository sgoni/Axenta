namespace Accounting.Domain.Enums;

public class SourceType : Enumeration
{
    public static SourceType Invoice = new(0, nameof(Invoice));
    public static SourceType Payment = new(1, nameof(Payment));
    public static SourceType Payroll = new(2, nameof(Payroll));

    public SourceType(int id, string name) : base(id, name)
    {
    }
}