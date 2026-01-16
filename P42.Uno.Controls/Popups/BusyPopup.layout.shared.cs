#if __IOS__
using ObjCRuntime;
#endif

namespace P42.Uno.Controls;

public partial class BusyPopup : Toast
{
    protected readonly ProgressRing ProgressRing = new();

    private void Build()
    {
        _iconPresenter.Collapsed();
            
        ProgressRing
            .IsActive(false)
            .RowSpan(2)
            .Width(40)
            .Height(40)
            .Margin(10);
                            
        ProgressRing.AltBind(ProgressRing.IsActiveProperty, this, IsPushedProperty);
        _bubbleContentGrid.Children.Add(ProgressRing);
    }
}
