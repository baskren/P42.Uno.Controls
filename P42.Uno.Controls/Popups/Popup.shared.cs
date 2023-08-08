using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;

namespace P42.Uno.Controls
{
    public static class Popups
    {
        static List<TargetedPopup> Stack = new List<TargetedPopup>();

        internal static event SizeChangedEventHandler FrameSizeChanged;

        static Visibility _visibility = Visibility.Visible;
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

        internal static void Add(TargetedPopup popup)
        {
            if (!RootFrame.Initiated)
                throw new Exception("P42.Uno.Controls popups require using P42.Uno.Controls.RootFrame as the application's window's Content");

            if (RootFrame.Current?.IsLoaded ?? false    )
            {
                InnerAdd(popup);
                Stack.Add(popup);
            }
        }

        static void InnerAdd(TargetedPopup popup)
        {
            popup.Visibility = Visibility;
            if (!RootFrame.PopupGrid.Children.Contains(popup.PageOverlay))
                RootFrame.PopupGrid.Children.Add(popup.PageOverlay);
            if (!RootFrame.PopupGrid.Children.Contains(popup.ShadowBorder))
                RootFrame.PopupGrid.Children.Add(popup.ShadowBorder);
            if (!RootFrame.PopupGrid.Children.Contains(popup.ContentBorder))
                RootFrame.PopupGrid.Children.Add(popup.ContentBorder);
        }

        internal static void Remove(TargetedPopup popup)
        {
            if (!RootFrame.Initiated)
                throw new Exception("P42.Uno.Controls popups require using P42.Uno.Controls.RootFrame as the application's window's Content");

            if (RootFrame.Current?.IsLoaded ?? false)
            {
                InnerRemove(popup);
                Stack.Remove(popup);
            }
        }

        static void InnerRemove(TargetedPopup popup)
        {
            if (RootFrame.PopupGrid.Children.Contains(popup.PageOverlay))
                RootFrame.PopupGrid.Children.Remove(popup.PageOverlay);
            if (RootFrame.PopupGrid.Children.Contains(popup.ShadowBorder))
                RootFrame.PopupGrid.Children.Remove(popup.ShadowBorder);
            if (RootFrame.PopupGrid.Children.Contains(popup.ContentBorder))
                RootFrame.PopupGrid.Children.Remove(popup.ContentBorder);
        }

        internal static void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs args)
            => FrameSizeChanged?.Invoke(sender, args);

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
            if (Stack.LastOrDefault() is TargetedPopup last)
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
}