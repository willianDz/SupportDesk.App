namespace SupportDesk.App.Models.Auth;

public class UserToken
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
