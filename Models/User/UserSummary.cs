namespace SupportDesk.App.Models.User;

public class UserSummary
{
    public int TotalRequests { get; set; }
    public int PendingRequests { get; set; }
    public int RejectedRequests { get; set; }
    public int ApprovedRequests { get; set; }
}
