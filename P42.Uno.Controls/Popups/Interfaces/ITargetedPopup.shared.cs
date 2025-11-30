using Windows.Foundation;

namespace P42.Uno.Controls;

internal interface ITargetedPopup
{

    Thickness Margin { get; set; }

    Thickness Padding { get; set; }

    object Content { get; set; }

    #region Pointer Properties
    UIElement Target { get; set; }

    Rect TargetRect { get; set; }

    double PointerBias { get; set; }

    double PointerCornerRadius { get; set; }

    PointerDirection ActualPointerDirection { get; }

    PointerDirection PreferredPointerDirection { get; set; }

    PointerDirection FallbackPointerDirection { get; set; }

    double PointerLength { get; set; }

    double PointerTipRadius { get; set; }

    bool PointToOffScreenElements { get; set; }

    double PointerMargin { get; set; }

    #endregion

    Brush PageOverlayBrush { get; set; }

    #region Push / Pop Properties
    TimeSpan PopAfter { get; set; }

    bool PopOnPointerMove { get; set; }

    bool PopOnPageOverlayTouch { get; set; }

    bool PopOnBackButtonClick { get; set; }

    object Parameter { get; set; }

    Effect PushEffect { get; set; }
        
    PopupPoppedCause PoppedCause { get; }

    object PoppedTrigger { get; }

    PushPopState PushPopState { get; }

    TimeSpan AnimationDuration { get; set; }

    #endregion



    event EventHandler Pushed;

    event EventHandler<PopupPoppedEventArgs> Popped;

    Task PushAsync(bool animated = true);

    Task PopAsync(PopupPoppedCause cause, bool animated = true, object trigger = null);
}
