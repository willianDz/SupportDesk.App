namespace SupportDesk.App.Services.User;

public class UserMockService : IUserService
{
    public Task<GetUserInfoResponse> GetUserInfo()
    {
        throw new NotImplementedException();
    }

    public Task<GetUserSummaryResponse> GetUserSummary()
    {
        throw new NotImplementedException();
    }
}
