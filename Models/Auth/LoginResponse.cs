using SupportDesk.App.Models.Common;

namespace SupportDesk.App.Models.Auth;

public class LoginResponse : BaseResponse
{
    public string TwoFactorCode { get; set; } = string.Empty!;
    public bool RequiresTwoFactor { get; set; } = false;
}
