namespace Accounting.Domain.Enums;

public class Action : Enumeration
{
    public static SourceType CREATE = new(0, nameof(CREATE));
    public static SourceType UPDATE = new(1, nameof(UPDATE));
    public static SourceType DELETE = new(2, nameof(DELETE));
    public static SourceType INSERT = new(3, nameof(INSERT));

    public Action(int id, string name) : base(id, name)
    {
    }
}