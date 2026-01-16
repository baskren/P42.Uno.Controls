using AsyncAwaitBestPractices;

namespace P42.Uno.Controls;

public static class Popups
{
    private static List<TargetedPopup> Stack = [];

    private static WeakEventManager _frameSizeChangedManager = new();
    internal static event SizeChangedEventHandler FrameSizeChanged
    {
        add => _frameSizeChangedManager.AddEventHandler(value);
        remove => _frameSizeChangedManager.RemoveEventHandler(value);
    }

    private static Visibility _visibility = Visibility.Visible;
    public static Visibility Visibility
    {
        get => _visibility;
        set
        {
            if (_visibility != value)
            {
                _visibility = value;
                foreach (var popup in Stack)
                    popup.Visibility = _visibility;
            }
        }
    }

    public static void Show()
    {
        if (!RootFrame.Initiated)
            throw new Exception("P42.Uno.Controls popups require using P42.Uno.Controls.RootFrame as the application's window's Content");
        Visibility = Visibility.Visible;
    }

    public static void Hide()
    {
        if (!RootFrame.Initiated)
            throw new Exception("P42.Uno.Controls popups require using P42.Uno.Controls.RootFrame as the application's window's Content");
        Visibility = Visibility.Collapsed;
    }

    internal static async Task AddAsync(TargetedPopup popup)
    {
        if (!RootFrame.Initiated)
            throw new Exception("P42.Uno.Controls popups require using P42.Uno.Controls.RootFrame as the application's window's Content");

        if (await RootFrame.GetCurrentAsync() is RootFrame rootFrame && rootFrame.IsLoaded)
        {
            await InnerAddAsync(popup);
            Stack.Add(popup);
        }
    }

    private static async Task InnerAddAsync(TargetedPopup popup)
    {
        if (await RootFrame.GetPopupGridAsync() is not Grid grid)
            throw new Exception("P42.Uno.Controls popups require using P42.Uno.Controls.RootFrame as the application's window's Content");
        popup.Visibility = Visibility;
        if (!grid.Children.Contains(popup.PageOverlay))
            grid.Children.Add(popup.PageOverlay);
        if (!grid.Children.Contains(popup.ShadowBorder))
            grid.Children.Add(popup.ShadowBorder);
        if (!grid.Children.Contains(popup.ContentBorder))
            grid.Children.Add(popup.ContentBorder);
    }

    internal static async Task RemoveAsync(TargetedPopup popup)
    {
        if (!RootFrame.Initiated)
            throw new Exception("P42.Uno.Controls popups require using P42.Uno.Controls.RootFrame as the application's window's Content");

        if (await RootFrame.GetCurrentAsync() is RootFrame rootFrame && rootFrame.IsLoaded)
        {
            await InnerRemoveAsync(popup);
            Stack.Remove(popup);
        }
    }

    private static async Task InnerRemoveAsync(TargetedPopup popup)
    {
        if (await RootFrame.GetPopupGridAsync() is not Grid grid)
            throw new Exception("P42.Uno.Controls popups require using P42.Uno.Controls.RootFrame as the application's window's Content");
        if (grid.Children.Contains(popup.PageOverlay))
            grid.Children.Remove(popup.PageOverlay);
        if (grid.Children.Contains(popup.ShadowBorder))
            grid.Children.Remove(popup.ShadowBorder);
        if (grid.Children.Contains(popup.ContentBorder))
            grid.Children.Remove(popup.ContentBorder);
    }

    internal static void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs args)
        => _frameSizeChangedManager.RaiseEvent(sender, args, nameof(FrameSizeChanged)); //FrameSizeChanged?.Invoke(sender, args);

    public static async Task<bool> TryPopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled)
    {
        if (!RootFrame.Initiated)
            throw new Exception("P42.Uno.Controls popups require using P42.Uno.Controls.RootFrame as the application's window's Content");

        //System.Diagnostics.Debug.WriteLine($"Popups.TryPopAsync : Visibility [{Visibility}]");
        //System.Diagnostics.Debug.WriteLine($"Popups.TryPopAsync : last [{Stack.LastOrDefault()?.GetType()}]");
        if (Visibility == Visibility.Collapsed)
        {
            //System.Diagnostics.Debug.WriteLine($"Popups.TryPopAsync : return false");
            return false;
        }

        //System.Diagnostics.Debug.WriteLine($"Popups.TryPopAsync : Stack.Count [{Stack.Count}]");
        if (Stack.LastOrDefault() is { } last)
        {
            //System.Diagnostics.Debug.WriteLine($"Popups.TryPopAsync : last [{last.GetType()}]");
            await last.PopAsync(cause);
            //System.Diagnostics.Debug.WriteLine($"Popups.TryPopAsync : return TRUE");
            return true;
        }

        //System.Diagnostics.Debug.WriteLine($"Popups.TryPopAsync : return false");
        return false;
    }

}
