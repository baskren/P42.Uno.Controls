using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Microsoft.UI.Xaml;

namespace P42.Uno.Controls
{
    interface ITargetedPopup
    {
        bool HasShadow { get; set; }

        TimeSpan PopAfter { get; set; }

        UIElement Target { get; set; }

        Point TargetPoint { get; set; }

        double PointerBias { get; set; }

        double PointerCornerRadius { get; set; }

        PointerDirection ActualPointerDirection { get; }

        PointerDirection PreferredPointerDirection { get; set; }

        PointerDirection FallbackPointerDirection { get; set; }

        double PointerLength { get; set; }

        double PointerTipRadius { get; set; }

        PopupPoppedCause PoppedCause { get; }

        object PoppedTrigger { get; }

        event EventHandler<PopupPoppedEventArgs> Popped;

        Task PushAsync(bool animated = true);

        Task PopAsync(PopupPoppedCause cause, bool animated = true, object trigger = null);
    }
}
