namespace SupportDesk.App.Models.Common;

public class BaseResponse
{
    public BaseResponse()
    {
        Success = true;
    }
    public BaseResponse(string message)
    {
        Success = true;
        Message = message;
    }

    public BaseResponse(string message, bool success)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? ValidationErrors { get; set; }


    public string GetErrorMessage()
    {
        if (Success)
        {
            return string.Empty;
        }

        string message = Message;

        if (ValidationErrors is not null && ValidationErrors.Count > 0)
        {

            ValidationErrors.ForEach(ValitionError =>
            {
                message += $" {ValitionError} ";
            });
        }

        return message;
    }
}
