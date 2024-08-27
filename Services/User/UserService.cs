using SupportDesk.App.Models.User;
using SupportDesk.App.Services.RequestProvider;
using SupportDesk.App.Services.Settings;
namespace SupportDesk.App.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ISettingsService _settingsService;

        public UserService(IRequestProvider requestProvider, ISettingsService settingsService)
        {
            _requestProvider = requestProvider;
            _settingsService = settingsService;
        }

        public async Task<GetUserInfoResponse> GetUserInfo()
        {
            GetUserInfoResponse response = await _requestProvider
                .GetAsync<GetUserInfoResponse>(GlobalSetting.Instance.GetUserInformationEndpoint, token: _settingsService.AuthAccessToken)
                .ConfigureAwait(false);

            if (response is null)
            {
                return new GetUserInfoResponse()
                {
                    Success = false,
                    Message = "Ocurrió un error al obtener la informacion del usuario, inténtelo de nuevo."
                };
            }

            return response;
        }

        public async Task<GetUserSummaryResponse> GetUserSummary()
        {
            GetUserSummaryResponse response = await _requestProvider
                .GetAsync<GetUserSummaryResponse>(GlobalSetting.Instance.GetUserSummaryEndpoint, token: _settingsService.AuthAccessToken)
                .ConfigureAwait(false);

            if (response is null)
            {
                return new GetUserSummaryResponse()
                {
                    Success = false,
                    Message = "Ocurrió un error al obtener el resumen de solicitudes del usuario, inténtelo de nuevo."
                };
            }

            return response;
        }
    }
}
