namespace SupportDesk.App;

public class GlobalSetting
{
    public const string SupportDeskApiUrl = "https://supportdesk-dev-fvceetcmc7d9gjdx.eastus-01.azurewebsites.net";

    public const string SupportDeskEndpointBase = $"{SupportDeskApiUrl}/api";
    public const string DisclaimerTitle = "Limitación de Responsabilidad";
    public const string Disclaimer = "La información disponible en esta aplicación tiene el único propósito de servir como demostración para la prueba técnica en Grupo Corinsa.";
    public const string ShareAppMessage = "Te invito a descargar la app de Support Desk";
    public const string AppStoreUrlAndroid = "";
    public const string AppStoreUrlIos = "";

    public static string AppStoreUrl
    {
        get
        {
            if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
            {
                return AppStoreUrlIos;
            }

            return AppStoreUrlAndroid;
        }
    }

    public static GlobalSetting Instance { get; } = new GlobalSetting();

    public GlobalSetting()
    {
        AuthToken = "";
        LoginEndpoint = $"{SupportDeskEndpointBase}/auth/login";
        VerifyTwoFactorEndpoint = $"{SupportDeskEndpointBase}/auth/verifytwofactor";

        CreateRequestEndpoint = $"{SupportDeskEndpointBase}/requests";
        UpdateRequestEndpoint = $"{SupportDeskEndpointBase}/requests";
        InactiveRequestEndpoint = $"{SupportDeskEndpointBase}/requests";
        GetRequestsEndpoint = $"{SupportDeskEndpointBase}/users/me/requests";
        GetRequestByIdEndpoint = SupportDeskEndpointBase + "/requests/{0}";
        UpdateProfileInformationEndpoint = $"{SupportDeskEndpointBase}/users/me/profile";
        GetUserInformationEndpoint = $"{SupportDeskEndpointBase}/users/me/profile/GetUserInformation";
        GetUserSummaryEndpoint = $"{SupportDeskEndpointBase}/users/me/summary";
    }

    public string AuthToken { get; }
    public string LoginEndpoint { get; }
    public string VerifyTwoFactorEndpoint { get; set; }
    public string CreateRequestEndpoint { get; }
    public string UpdateRequestEndpoint { get; }
    public string InactiveRequestEndpoint { get; }
    public string GetRequestsEndpoint { get; }
    public string GetRequestByIdEndpoint { get; }
    public string UpdateProfileInformationEndpoint { get; }
    public string GetUserInformationEndpoint { get; }
    public string GetUserSummaryEndpoint { get; }
}