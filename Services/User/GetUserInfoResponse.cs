using SupportDesk.App.Models.Common;

namespace SupportDesk.App.Services.User;

public class GetUserInfoResponse : BaseResponse
{
    public SupportDesk.App.Models.User.User User { get; set; } = null!;
}
