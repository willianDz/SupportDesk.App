namespace SupportDesk.App.Models.Requests;

public class RequestDocument
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public string DocumentUrl { get; set; } = string.Empty!;
    public bool IsActive { get; set; }
}
