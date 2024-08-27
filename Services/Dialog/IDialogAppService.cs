namespace SupportDesk.App.Services.Dialog;

public interface IDialogAppService
{
    Task ShowAlertAsync(string message, string title, string buttonLabel);

    Task<bool> ShowQuestionAsync(string message, string title, string accept, string cancel);
}
