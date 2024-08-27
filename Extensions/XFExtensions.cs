namespace SupportDesk.App.Extensions;

public static class XFExtensions
{
    public static void AddClicked(this View view, Action action)
    {
        var tapGestureRecognizer = new TapGestureRecognizer();

        tapGestureRecognizer.Tapped += (s, e) =>
        {
            action();
        };

        view.GestureRecognizers.Add(tapGestureRecognizer);
    }
}
