using SupportDesk.App.Models.User;

namespace SupportDesk.App.Services.User
{
    public interface IUserService
    {
        Task<GetUserInfoResponse> GetUserInfo();

        Task<GetUserSummaryResponse> GetUserSummary();
    }
}
