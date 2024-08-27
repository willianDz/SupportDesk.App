using SupportDesk.App.Services.User;

namespace SupportDesk.App.Services.AppEnvironment;

public interface IAppEnvironmentService
{
    IUserService UserService { get; }

    void UpdateDependencies(bool useMockServices);
}
