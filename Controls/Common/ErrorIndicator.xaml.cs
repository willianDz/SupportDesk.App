using CommunityToolkit.Mvvm.Input;

namespace SupportDesk.App.Controls.Common;

public partial class ErrorIndicator : VerticalStackLayout
{
    //Bindable Properties

    public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(
        "IsError",
        typeof(bool),
        typeof(ErrorIndicator),
        false,
        BindingMode.OneWay,
        null,
        SetIsError);

    public bool IsError
    {
        get => (bool)GetValue(IsErrorProperty);
        set => SetValue(IsErrorProperty, value);
    }

    private static void SetIsError(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as ErrorIndicator).IsVisible = (bool)newValue;


    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        "ErrorText",
        typeof(string),
        typeof(ErrorIndicator),
        string.Empty,
        BindingMode.OneWay,
        null,
        SetErrorText);

    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    private static void SetErrorText(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as ErrorIndicator).lblErrorText.Text = (string)newValue;


    public static readonly BindableProperty ErrorImageProperty = BindableProperty.Create(
        "ErrorImage",
        typeof(ImageSource),
        typeof(ErrorIndicator),
        null,
        BindingMode.OneWay,
        null,
        SetErrorImage);

    public ImageSource ErrorImage
    {
        get => (ImageSource)GetValue(ErrorImageProperty);
        set => SetValue(ErrorImageProperty, value);
    }

    private static void SetErrorImage(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as ErrorIndicator).imgError.Source = (ImageSource)newValue;

    public IRelayCommand RetryInitializeCommand
    {
        get => (IRelayCommand)GetValue(RetryInitializeCommandProperty);
        set => SetValue(RetryInitializeCommandProperty, value);
    }

    public static readonly BindableProperty RetryInitializeCommandProperty =
        BindableProperty.Create(
        "RetryInitializeCommand",
        typeof(IRelayCommand),
        typeof(ErrorIndicator),
        null,
        BindingMode.OneWay,
        null,
        null);

    public ErrorIndicator()
    {
        InitializeComponent();
    }

    private void RetryIntialize_Clicked(object sender, EventArgs e)
    {
        if (RetryInitializeCommand != null && RetryInitializeCommand.CanExecute(null))
        {
            RetryInitializeCommand.Execute(null);
        }
    }
}
