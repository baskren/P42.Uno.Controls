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

        #region Properties

        #region Override / Workaround Properties

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

        #region Child Property
        // Can't "new" Content on Uno platforms.  
        public static readonly DependencyProperty ChildProperty = DependencyProperty.Register(
            nameof(Child),
            typeof(FrameworkElement),
            typeof(UwpPopup),
            new PropertyMetadata(default(FrameworkElement), new PropertyChangedCallback((d, e) => ((UwpPopup)d).OnChildChanged(e)))
        );
        protected virtual void OnChildChanged(DependencyPropertyChangedEventArgs e)
        {
            _border.Content = Child;
        }
        public FrameworkElement Child
        {
            get => (FrameworkElement)GetValue(ChildProperty);
            set => SetValue(ChildProperty, value);
        }
        #endregion Content Property
        
        #region Padding Property
        // Binding to userControl.Padding isn't working.  I have no idea why.
        public static readonly new DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(UwpPopup),
            new PropertyMetadata(default(Thickness), new PropertyChangedCallback((d, e) => ((UwpPopup)d).OnPaddingChanged(e)))
        );
        protected virtual void OnPaddingChanged(DependencyPropertyChangedEventArgs e)
        {
            _border.Padding = Padding;
        }
        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        #endregion Padding Property

        #region Background Property
        public static readonly new DependencyProperty BackgroundProperty = DependencyProperty.Register(
            nameof(Background),
            typeof(Brush),
            typeof(UwpPopup),
            new PropertyMetadata(default(Brush), new PropertyChangedCallback((d, e) => ((UwpPopup)d).OnBackgroundChanged(e)))
        );
        protected virtual void OnBackgroundChanged(DependencyPropertyChangedEventArgs e)
        {
            _border.Background = Background;
        }
        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
        #endregion Background Property


        #region BorderBrush Property
        public static readonly new DependencyProperty BorderBrushProperty = DependencyProperty.Register(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(UwpPopup),
            new PropertyMetadata(default(Brush), new PropertyChangedCallback((d, e) => ((UwpPopup)d).OnBorderBrushChanged(e)))
        );
        protected virtual void OnBorderBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            _border.BorderBrush = BorderBrush;
        }
        public new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }
        #endregion BorderBrush Property


        #region BorderThickness Property
        public static readonly new DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
            nameof(BorderThickness),
            typeof(double),
            typeof(UwpPopup),
            new PropertyMetadata(default(double), new PropertyChangedCallback((d, e) => ((UwpPopup)d).OnBorderThicknessChanged(e)))
        );
        protected virtual void OnBorderThicknessChanged(DependencyPropertyChangedEventArgs e)
        {
            _border.BorderThickness = new Thickness(BorderThickness);
        }
        public new double BorderThickness
        {
            get => (double)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        #endregion BorderThickness Property

        #region CornerRadius Property
        public static readonly new DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(double),
            typeof(UwpPopup),
            new PropertyMetadata(default(double), new PropertyChangedCallback((d, e) => ((UwpPopup)d).OnCornerRadiusChanged(e)))
        );
        protected virtual void OnCornerRadiusChanged(DependencyPropertyChangedEventArgs e)
        {
            _border.CornerRadius = new Windows.UI.Xaml.CornerRadius(CornerRadius);
        }
        public new double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        #endregion CornerRadius Property



        #endregion

        #region Unique Properties

        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(UwpPopup),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((UwpPopup)d).OnHasShadowChanged(e)))
        );
        protected virtual void OnHasShadowChanged(DependencyPropertyChangedEventArgs e)
        {
            _border.HasShadow = HasShadow;
        }
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }
        #endregion HasShadow Property


        #endregion

        #endregion

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
