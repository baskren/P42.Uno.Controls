namespace P42.Uno.Controls.AnimateBar;

public partial class Right : Left
{
    // ReSharper disable once MemberCanBeProtected.Global
    public Right()
    {
        //Height = 30;
        VerticalAlignment = VerticalAlignment.Stretch;
        Margin = new Thickness(0, 5, 0, 5);
        HorizontalAlignment = HorizontalAlignment.Right;

        Dir = -1;
        //StaticRect.HorizontalAlignment = 
        DynamicRect.HorizontalAlignment = HorizontalAlignment.Right;

    }
}
