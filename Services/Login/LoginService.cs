using SupportDesk.App.Extensions;
using SupportDesk.App.Models.Auth;
using SupportDesk.App.Services.RequestProvider;

namespace SupportDesk.App.Services.Login;

public class LoginService : ILoginService
{
    private readonly IRequestProvider _requestProvider;

    public LoginService(IRequestProvider requestProvider)
    {
        _requestProvider = requestProvider;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        LoginResponse response = await _requestProvider
            .PostAsync<LoginResponse>(GlobalSetting.Instance.LoginEndpoint, request)
            .ConfigureAwait(false);

        if (response is null)
        {
            return new LoginResponse()
            {
                Success = false,
                Message = "Ocurrió un error, inténtelo de nuevo."
            };
        }

        return response;
    }

    public async Task<VerifyTwoFactorResponse> VerifyTwoFactorCode(VerifyTwoFactorRequest request)
    {
        VerifyTwoFactorResponse response = await _requestProvider
            .PostAsync<VerifyTwoFactorResponse>(GlobalSetting.Instance.VerifyTwoFactorEndpoint, request)
            .ConfigureAwait(false);

        if (response is null)
        {
            return new VerifyTwoFactorResponse()
            {
                Success = false,
                Message = "Ocurrió un error al realizar la verificación, inténtelo de nuevo."
            };
        }

        return response;
    }
}
