using SupportDesk.App.Models.Auth;

namespace SupportDesk.App.Services.Login;

public interface ILoginService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<VerifyTwoFactorResponse> VerifyTwoFactorCode(VerifyTwoFactorRequest request);
}
