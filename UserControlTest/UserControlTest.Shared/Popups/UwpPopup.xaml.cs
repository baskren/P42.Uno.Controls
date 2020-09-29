using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UserControlTest.Popups
{
    public sealed partial class UwpPopup : UserControl
    {
        public UwpPopup()
        {
            this.InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(UwpPopup),
                        new PropertyMetadata("", OnTextChanged));


        bool _hasAppeared;
        public async Task OpenPopupAsync()
        {
            this.ParentPopup.IsOpen = true;
#if __WASM__
            if (!_hasAppeared)
            {
                _hasAppeared = true;
                await Task.Delay(5);
                this.ParentPopup.IsOpen = false;
                await Task.Delay(5);
                this.ParentPopup.IsOpen = true;
            }
#endif
        }

        public void ClosePopup()
        {
            this.ParentPopup.IsOpen = false;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            
            var transform = Window.Current.Content.TransformToVisual(_bubbleBorder);
            Point point = transform.TransformPoint(new Point(0, 0)); // gets the window's (0,0) coordinate relative to the popup

            double hOffset = 0.0;
            if (HorizontalAlignment == HorizontalAlignment.Center)
                hOffset = (Window.Current.Bounds.Width - _bubbleBorder.ActualWidth) / 2;
            else if (HorizontalAlignment == HorizontalAlignment.Right)
                hOffset = (Window.Current.Bounds.Width - _bubbleBorder.ActualWidth) - Margin.Right;
            hOffset = Math.Max(Margin.Left, hOffset);

            double vOffset = 0.0;
            if (VerticalAlignment == VerticalAlignment.Center)
                vOffset = (Window.Current.Bounds.Height - _bubbleBorder.ActualHeight) / 2;
            else if (VerticalAlignment == VerticalAlignment.Bottom)
                vOffset = (Window.Current.Bounds.Height - _bubbleBorder.ActualHeight) - Margin.Bottom;
            vOffset = Math.Max(Margin.Top, vOffset);

            // works on UWP and WASM - not Android
            ParentPopup.HorizontalOffset = point.X + hOffset;
            ParentPopup.VerticalOffset = point.Y + vOffset;
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as UwpPopup;
            var newValue = e.NewValue as string;
            if (instance != null && newValue != null)
            {
                instance.CustomTextBlock.Text = newValue;
            }
        }

        private void OnPopupLoaded(object sender, RoutedEventArgs e)
        {
        }

        
    }
}
