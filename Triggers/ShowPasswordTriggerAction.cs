using System.ComponentModel;

namespace SupportDesk.App.Triggers;

public class ShowPasswordTriggerAction : TriggerAction<ImageButton>, INotifyPropertyChanged
{
    public string ShowIcon { get; set; } = string.Empty;
    public string HideIcon { get; set; } = string.Empty;

    bool _hidePassword = true;

    public bool HidePassword
    {
        set
        {
            if (_hidePassword != value)
            {
                _hidePassword = value;

                PropertyChanged?
                    .Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(HidePassword)));
            }
        }
        get => _hidePassword;
    }

    protected override void Invoke(ImageButton sender)
    {
        sender.Source = HidePassword ? ShowIcon : HideIcon;
        HidePassword = !HidePassword;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}

