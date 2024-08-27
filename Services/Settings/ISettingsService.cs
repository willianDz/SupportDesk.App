namespace SupportDesk.App.Services.Settings;

public interface ISettingsService
{
    string AuthAccessToken { get; set; }
    Guid UserId { get; set; }
    int ApplicationId { get; set; }
    bool UseMocks { get; set; }
    NetworkAccess NetworkAccess { get; set; }
}
