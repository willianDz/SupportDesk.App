namespace SupportDesk.App.Models.Common;

public class MonthName
{
    public MonthName(int month, string name)
    {
        Month = month;
        Name = name;
    }

    public int Month { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
