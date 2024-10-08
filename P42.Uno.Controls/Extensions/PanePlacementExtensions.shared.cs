namespace P42.Uno.Controls;

public static class PanePlacementExtensions
{
    public static bool IsHorizontal(this PanePlacement panePlacement)
        => panePlacement == PanePlacement.Left || panePlacement == PanePlacement.Right;

    public static bool IsVertical(this PanePlacement panePlacement)
        => panePlacement == PanePlacement.Top || panePlacement == PanePlacement.Bottom;
}
