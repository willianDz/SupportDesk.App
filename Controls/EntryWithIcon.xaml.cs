namespace SupportDesk.App.Controls;

public partial class EntryWithIcon : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(EntryWithIcon),
        string.Empty,
        BindingMode.TwoWay,
        null,
        SetText);

    public string Text
    {
        get => (string)this.GetValue(TextProperty);
        set => this.SetValue(TextProperty, value);
    }

    private static void SetText(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as EntryWithIcon).entryTextBox.Text = (string)newValue;

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
      nameof(Placeholder),
      typeof(string),
      typeof(EntryWithIcon),
      string.Empty,
      BindingMode.OneWay,
      null,
      SetPlaceholder);

    public string Placeholder
    {
        get => (string)this.GetValue(PlaceholderProperty);
        set => this.SetValue(PlaceholderProperty, value);
    }

    private static void SetPlaceholder(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as EntryWithIcon).entryTextBox.Placeholder = (string)newValue;


    public static readonly BindableProperty IconProperty = BindableProperty.Create(
       nameof(Icon),
       typeof(ImageSource),
       typeof(EntryWithIcon),
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
        (bindable as EntryWithIcon).imgIcon.Source = (ImageSource)newValue;

    public EntryWithIcon()
    {
        InitializeComponent();
    }

    private void entryTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        Text = e.NewTextValue;
    }
}