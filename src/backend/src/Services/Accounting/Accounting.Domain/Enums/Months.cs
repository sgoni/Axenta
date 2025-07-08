namespace Accounting.Domain.Enums;

public class Months : Enumeration
{
    public static SourceType January = new(0, nameof(January));
    public static SourceType February = new(1, nameof(February));
    public static SourceType March = new(2, nameof(March));
    public static SourceType April = new(3, nameof(April));
    public static SourceType May = new(4, nameof(May));
    public static SourceType June = new(5, nameof(June));
    public static SourceType July = new(6, nameof(July));
    public static SourceType August = new(7, nameof(August));
    public static SourceType September = new(8, nameof(September));
    public static SourceType Octuber = new(9, nameof(Octuber));
    public static SourceType November = new(10, nameof(November));
    public static SourceType December = new(11, nameof(December));

    public Months(int id, string name) : base(id, name)
    {
    }
}