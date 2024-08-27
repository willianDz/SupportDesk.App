using Newtonsoft.Json;
using SupportDesk.App.Models.Auth;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SupportDesk.App.Extensions;

public static class StringExtensions
{
    public static T? GetPayload<T>(string token)
    {
        string payLoad = GetPayload(token);

        return JsonConvert.DeserializeObject<T>(payLoad);
    }

    public static UserToken ExtractUserInfoFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userid");

        if (userIdClaim == null)
        {
            throw new Exception("UserId not found in token");
        }

        var userInfo = new UserToken
        {
            UserId = Guid.Parse(userIdClaim.Value),
            Token = token
        };

        return userInfo;
    }

    public static string GetPayload(string token)
    {
        token = token
            .Replace("Bearer", "")
            .Replace("bearer", "")
            .Replace(" ", "");

        string payloadString = token.Split('.')[1];

        int mod4 = payloadString.Length % 4;

        if (mod4 > 0)
        {
            payloadString += new string('=', 4 - mod4);
        }

        payloadString = Encoding.UTF8
            .GetString(Convert.FromBase64String(payloadString));

        return payloadString;
    }

    public static string GetInitials(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        string[] textSplit = text.Split(
            new string[] { ",", " " },
            StringSplitOptions.RemoveEmptyEntries);

        StringBuilder initials = new StringBuilder();

        foreach (string item in textSplit)
        {
            initials.Append(item.Substring(0, 1).ToUpper());
        }

        return initials.ToString();
    }
}
