namespace SupportDesk.App.Services.Settings;

public class SettingsService : ISettingsService
{
    #region Setting Constants

    private const string AccessToken = "access_token";
    private readonly string AccessTokenDefault = string.Empty;

    private const string IdUserId = "id_user_logged";
    private readonly string IdUserIdDefault = Guid.Empty.ToString();

    private const string IdApplicationId = "id_application";
    private readonly int IdApplicationDefault = 25;

    private const string IdUseMocks = "use_mocks";
    private readonly bool UseMocksDefault = false;

    private const string IdNetworkAccess = "id_network_access";
    private readonly NetworkAccess NetworkAccessDefault = NetworkAccess.Internet;

    #endregion

    #region Settings Properties

    public string AuthAccessToken
    {
        get => Preferences.Get(AccessToken, AccessTokenDefault);
        set => Preferences.Set(AccessToken, value);
    }

    public Guid UserId
    {
        get
        {
            string userIdPreferences = Preferences.Get(IdUserId, IdUserIdDefault);

            if (userIdPreferences == null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(userIdPreferences);
        }

        set => Preferences.Set(IdUserId, value.ToString());
    }

    public int ApplicationId
    {
        get => Preferences.Get(IdApplicationId, IdApplicationDefault);
        set => Preferences.Set(IdApplicationId, value);
    }

    public bool UseMocks
    {
        get => Preferences.Get(IdUseMocks, UseMocksDefault);
        set => Preferences.Set(IdUseMocks, value);
    }

    public NetworkAccess NetworkAccess
    {
        get
        {
            var status = Preferences.Get(
                IdNetworkAccess,
                NetworkAccessDefault.ToString());

            return (NetworkAccess)Enum
                .Parse(typeof(NetworkAccess), status);
        }

        set => Preferences.Set(IdNetworkAccess, value.ToString());
    }

    #endregion
}
