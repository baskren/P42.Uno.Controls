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
            .Active(false)
            .RowSpan(2)
            .Width(40)
            .Height(40)
            .Margin(10);
                
#if MACCATALYST
#elif __IOS__
            if (Runtime.Arch == Arch.SIMULATOR)
                return;
#endif
            
        ProgressRing.WBind(ProgressRing.IsActiveProperty, this, IsPushedProperty);
        _bubbleContentGrid.Children.Add(ProgressRing);
    }
}
