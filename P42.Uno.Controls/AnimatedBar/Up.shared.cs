namespace P42.Uno.Controls.AnimateBar;

public partial class Up : Down
{
    public Up()
    {
        Width = 30;
        Margin = new Microsoft.UI.Xaml.Thickness(5, 0, 5, 0);
        VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Bottom;

        dir = -1;
        //StaticRect.VerticalAlignment = 
        DynamicRect.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Bottom;
    }
}