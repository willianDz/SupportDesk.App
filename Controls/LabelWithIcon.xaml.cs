namespace SupportDesk.App.Controls;

public partial class LabelWithIcon : VerticalStackLayout
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(LabelWithIcon),
        string.Empty,
        BindingMode.OneWay,
        null,
        SetTitle);

    public string Title
    {
        get => (string)this.GetValue(TitleProperty);
        set => this.SetValue(TitleProperty, value);
    }

    private static void SetTitle(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as LabelWithIcon).lblTitle.Text = (string)newValue;

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
       nameof(Text),
       typeof(string),
       typeof(LabelWithIcon),
       string.Empty,
       BindingMode.OneWay,
       null,
       SetText);

    public string Text
    {
        get => (string)this.GetValue(TextProperty);
        set => this.SetValue(TextProperty, value);
    }

    private static void SetText(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as LabelWithIcon).lblText.Text = (string)newValue;

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
       nameof(Icon),
       typeof(ImageSource),
       typeof(LabelWithIcon),
       null,
       BindingMode.OneWay,
       null,
       SetIcon);

    public ImageSource Icon
    {
        get => (ImageSource)this.GetValue(IconProperty);
        set => this.SetValue(IconProperty, value);
    }

    private static void SetIcon(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as LabelWithIcon).imgIcon.Source = (ImageSource)newValue;


    public LabelWithIcon()
    {
        InitializeComponent();
    }
}