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

namespace P42.Uno.Popups
{

    public partial class UwpPopup : UserControl
    {

        #region HorizontalAlignment Property
        public static readonly new DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(UwpPopup),
            new PropertyMetadata(default(HorizontalAlignment), new PropertyChangedCallback((d, e) => ((UwpPopup)d).OnHorizontalAlignmentChanged(e)))
        );
        protected virtual void OnHorizontalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((HorizontalAlignment)e.NewValue == HorizontalAlignment.Stretch)
                _border.HorizontalAlignment = HorizontalAlignment.Stretch;
            else
                _border.HorizontalAlignment = HorizontalAlignment.Left;

            UpdateAlignment();
        }
        public new HorizontalAlignment HorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalAlignmentProperty);
            set => SetValue(HorizontalAlignmentProperty, value);
        }
        #endregion HorizontalAlignment Property


        #region VerticalAlignment Property
        public static readonly new DependencyProperty VerticalAlignmentProperty = DependencyProperty.Register(
            nameof(VerticalAlignment),
            typeof(VerticalAlignment),
            typeof(UwpPopup),
            new PropertyMetadata(default(VerticalAlignment), new PropertyChangedCallback((d, e) => ((UwpPopup)d).OnVerticalAlignmentChanged(e)))
        );
        protected virtual void OnVerticalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((VerticalAlignment)e.NewValue == VerticalAlignment.Stretch)
                _border.VerticalAlignment = VerticalAlignment.Stretch;
            else
                _border.VerticalAlignment = VerticalAlignment.Top;
            UpdateAlignment();

        }
        public new VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }
        #endregion VerticalAlignment Property



        #region Fields
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Left;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Top;
        #endregion


        public UwpPopup()
        {
            this.InitializeComponent();
        }

#if __WASM__
        bool _hasAppeared;
#endif
        public async Task PushAsync()
        {
            _popup.IsOpen = true;
#if __WASM__
            if (!_hasAppeared)
            {
                _hasAppeared = true;
                await Task.Delay(5);
                _popup.IsOpen = false;
                await Task.Delay(5);
                _popup.IsOpen = true;
            }
#endif
        }

        public void PopAsync()
        {
            _popup.IsOpen = false;
        }

        /* Neither of the following are called */
        protected override Size MeasureOverride(Size availableSize)
        {
            System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride("+availableSize+")");
            if (double.IsInfinity(availableSize.Width))
                availableSize.Width = ((Frame)Windows.UI.Xaml.Window.Current.Content).ActualWidth;
            if (double.IsInfinity(availableSize.Height))
                availableSize.Height = ((Frame)Windows.UI.Xaml.Window.Current.Content).ActualHeight;

            var result = base.MeasureOverride(availableSize);
            if (HorizontalAlignment == HorizontalAlignment.Stretch)
                result.Width = availableSize.Width;
            if (VerticalAlignment == VerticalAlignment.Stretch)
                result.Height = availableSize.Height;
            //RegeneratePath(result);

            _border.Measure(result);

            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Diagnostics.Debug.WriteLine(GetType() + ".ArrangeOverride("+finalSize+")");
            return base.ArrangeOverride(finalSize);
        }
        

        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            UpdateAlignment();
        }

        void UpdateAlignment()
        {
            double hOffset = 0.0;
            _border.Width = double.NaN;
            if (HorizontalAlignment == HorizontalAlignment.Center)
                hOffset = (Windows.UI.Xaml.Window.Current.Bounds.Width - _border.ActualWidth) / 2;
            else if (HorizontalAlignment == HorizontalAlignment.Right)
                hOffset = (Windows.UI.Xaml.Window.Current.Bounds.Width - _border.ActualWidth) - Margin.Right;
            else if (HorizontalAlignment == HorizontalAlignment.Stretch)
                _border.Width = ((Frame)Windows.UI.Xaml.Window.Current.Content).ActualWidth - Margin.Left - Margin.Right;
            hOffset = Math.Max(Margin.Left, hOffset);

            double vOffset = 0.0;
            _border.Height = double.NaN;
            if (VerticalAlignment == VerticalAlignment.Center)
                vOffset = (Windows.UI.Xaml.Window.Current.Bounds.Height - _border.ActualHeight) / 2;
            else if (VerticalAlignment == VerticalAlignment.Bottom)
                vOffset = (Windows.UI.Xaml.Window.Current.Bounds.Height - _border.ActualHeight) - Margin.Bottom;
            else if (VerticalAlignment == VerticalAlignment.Stretch)
                _border.Height = ((Frame)Windows.UI.Xaml.Window.Current.Content).ActualHeight - Margin.Top - Margin.Bottom;
            vOffset = Math.Max(Margin.Top, vOffset);

            // works on UWP and WASM - not Android
            _popup.HorizontalOffset = hOffset;
            _popup.VerticalOffset = vOffset;
        }


        
    }
}
