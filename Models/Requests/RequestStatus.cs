namespace SupportDesk.App.Models.Requests;

public class RequestStatus
{
    public int RequestStatusId { get; set; }
    public string Description { get; set; } = string.Empty!;
    public string Abbreviation { get; set; } = string.Empty!;
}
