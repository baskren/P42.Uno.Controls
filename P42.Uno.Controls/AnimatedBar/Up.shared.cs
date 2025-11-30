namespace P42.Uno.Controls.AnimateBar;

public partial class Up : Down
{
    // ReSharper disable once MemberCanBeProtected.Global
    public Up()
    {
        Width = 30;
        Margin = new Thickness(5, 0, 5, 0);
        VerticalAlignment = VerticalAlignment.Bottom;

        Dir = -1;
        //StaticRect.VerticalAlignment = 
        DynamicRect.VerticalAlignment = VerticalAlignment.Bottom;
    }
}
