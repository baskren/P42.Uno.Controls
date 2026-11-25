using Microsoft.UI.Xaml.Controls;
using P42.Uno.Markup;
#if __IOS__
using ObjCRuntime;
using UIKit;
#endif

namespace P42.Uno.Controls;

public partial class BusyPopup : Toast
{
    protected readonly ProgressRing ProgressRing = new();

    void Build()
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
            
        // Sole driver of IsActive (IsPushed == PushPopState is Pushed). A local IsActive set in
        // OnPushPopStateChanged used to duplicate this and, on first use, silently replaced the binding.
        ProgressRing.WBind(ProgressRing.IsActiveProperty, this, IsPushedProperty);
        _bubbleContentGrid.Children.Add(ProgressRing);
    }
}