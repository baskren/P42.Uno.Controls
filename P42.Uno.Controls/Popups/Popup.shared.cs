using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    public static class Popups
    {
        static List<TargetedPopup> Stack = new List<TargetedPopup>();

        internal static event SizeChangedEventHandler FrameSizeChanged;

        public static Visibility Visibility
        {
            get => RootFrame.Grid.Visibility;
            set => RootFrame.Grid.Visibility = Visibility;
        }

        public static void Show()
            => Visibility = Visibility.Visible;

        public static void Hide()
            => Visibility = Visibility.Collapsed;

        internal static void Add(TargetedPopup popup)
        {

            if (!RootFrame.Grid.Children.Contains(popup.ContentBorder))
            {
                Stack.Add(popup);
                RootFrame.Grid.Children.Add(popup.PageOverlay);
                RootFrame.Grid.Children.Add(popup.ShadowBorder);
                RootFrame.Grid.Children.Add(popup.ContentBorder);
            }
        }

        internal static void Remove(TargetedPopup popup)
        {
            if (RootFrame.Grid.Children.Contains(popup.ContentBorder))
            {
                Stack.Remove(popup);
                RootFrame.Grid.Children.Remove(popup.PageOverlay);
                RootFrame.Grid.Children.Remove(popup.ShadowBorder);
                RootFrame.Grid.Children.Remove(popup.ContentBorder);
            }

        }

        internal static void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs args)
            => FrameSizeChanged?.Invoke(sender, args);

        public static async Task<bool> TryPopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled)
        {
            if (Stack.LastOrDefault() is TargetedPopup popup)
            {
                await popup.PopAsync(cause);
                return true;
            }
            return false;
        }

    }
}