namespace SupportDesk.App.Models.Requests;

public class RequestType
{
    public int RequestTypeId { get; set; }
    public string Description { get; set; } = string.Empty!;
    public string Abbreviation { get; set; } = string.Empty!;
}
