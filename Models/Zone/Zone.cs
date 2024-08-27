namespace SupportDesk.App.Models.Zone;

public class Zone
{
    public int ZoneId { get; set; }
    public string Description { get; set; } = string.Empty!;
    public string Abbreviation { get; set; } = string.Empty!;
}
