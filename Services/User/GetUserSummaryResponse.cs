using SupportDesk.App.Models.Common;
using SupportDesk.App.Models.User;

namespace SupportDesk.App.Services.User;

public class GetUserSummaryResponse : BaseResponse
{
    public int TotalRequests { get; set; }
    public int PendingRequests { get; set; }
    public int RejectedRequests { get; set; }
    public int ApprovedRequests { get; set; }
}
