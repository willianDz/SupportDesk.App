namespace SupportDesk.App.Services.Dialog;

public class DialogAppService : IDialogAppService
{
    public Task ShowAlertAsync(string message, string title, string buttonLabel)
    {
        if (Application.Current is null || Application.Current.MainPage is null)
        {
            return Task.CompletedTask;
        }

        return Application.Current.MainPage.DisplayAlert(title, message, buttonLabel);
    }

    public Task<bool> ShowQuestionAsync(string message, string title, string accept, string cancel)
    {
        if (Application.Current is null || Application.Current.MainPage is null)
        {
            return Task.FromResult(false);
        }

        return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }
}
