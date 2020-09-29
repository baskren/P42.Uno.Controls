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
            UpdateAlignment();
        }
        public new VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }
        #endregion VerticalAlignment Property




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

        /* Neither of the following are called */
        protected override Size MeasureOverride(Size availableSize)
        {
            System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride("+availableSize+")");
            if (double.IsInfinity(availableSize.Width))
                availableSize.Width = ((Frame)Window.Current.Content).ActualWidth;
            if (double.IsInfinity(availableSize.Height))
                availableSize.Height = ((Frame)Window.Current.Content).ActualHeight;

            var result = base.MeasureOverride(availableSize);
            if (HorizontalAlignment == HorizontalAlignment.Stretch)
                result.Width = availableSize.Width;
            if (VerticalAlignment == VerticalAlignment.Stretch)
                result.Height = availableSize.Height;
            //RegeneratePath(result);
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
            _bubbleBorder.Width = double.NaN;
            if (HorizontalAlignment == HorizontalAlignment.Center)
                hOffset = (Window.Current.Bounds.Width - _bubbleBorder.ActualWidth) / 2;
            else if (HorizontalAlignment == HorizontalAlignment.Right)
                hOffset = (Window.Current.Bounds.Width - _bubbleBorder.ActualWidth) - Margin.Right;
            else if (HorizontalAlignment == HorizontalAlignment.Stretch)
                _bubbleBorder.Width = ((Frame)Window.Current.Content).ActualWidth - Margin.Left - Margin.Right;
            hOffset = Math.Max(Margin.Left, hOffset);

            double vOffset = 0.0;
            _bubbleBorder.Height = double.NaN;
            if (VerticalAlignment == VerticalAlignment.Center)
                vOffset = (Window.Current.Bounds.Height - _bubbleBorder.ActualHeight) / 2;
            else if (VerticalAlignment == VerticalAlignment.Bottom)
                vOffset = (Window.Current.Bounds.Height - _bubbleBorder.ActualHeight) - Margin.Bottom;
            else if (VerticalAlignment == VerticalAlignment.Stretch)
                _bubbleBorder.Height = ((Frame)Window.Current.Content).ActualHeight - Margin.Top - Margin.Bottom;
            vOffset = Math.Max(Margin.Top, vOffset);

            // works on UWP and WASM - not Android
            ParentPopup.HorizontalOffset = hOffset;
            ParentPopup.VerticalOffset = vOffset;
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
