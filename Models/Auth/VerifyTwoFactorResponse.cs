using SupportDesk.App.Models.Common;

namespace SupportDesk.App.Models.Auth
{
    public class VerifyTwoFactorResponse : BaseResponse
    {
        public string JwtToken { get; set; } = string.Empty!;
    }
}
