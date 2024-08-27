using SupportDesk.App.Services.User;

namespace SupportDesk.App.Services.AppEnvironment;

public class AppEnvironmentService : IAppEnvironmentService
{
    private readonly IUserService _mockUserService;
    private readonly IUserService _userService;

    public IUserService UserService { get; private set; }

    public AppEnvironmentService(
        IUserService mockUserService, IUserService userService
        )
    {
        _mockUserService = mockUserService;
        _userService = userService;
    }

    public void UpdateDependencies(bool useMockServices)
    {
        if (useMockServices)
        {
            UserService = _mockUserService;
        }
        else
        {
            UserService = _userService;
        }
    }
}
