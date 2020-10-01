using P42.Utils.Uno;
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

#if NETFX_CORE
using Popup = Windows.UI.Xaml.Controls.Primitives.Popup;
#else
using Popup = Windows.UI.Xaml.Controls.Popup;
#endif


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace P42.Uno.Popups
{
    
    [TemplatePart(Name = ContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = BorderElementName, Type = typeof(BubbleBorder))]
    [TemplatePart(Name = PopupElementName, Type = typeof(Windows.UI.Xaml.Controls.Primitives.Popup))]
    public partial class ModalPopup : ContentControl
    {
        #region Properties

        #region Overridden Properties
        
        #region HorizontalAlignment Property
        public static readonly new DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(ModalPopup),
            new PropertyMetadata(DefaultHorizontalAlignment, new PropertyChangedCallback((d, e) => ((ModalPopup)d).OnHorizontalAlignmentChanged(e)))
        );
        protected virtual void OnHorizontalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_border != null)
            {
                //base.HorizontalAlignment = HorizontalAlignment.Stretch;
                if ((HorizontalAlignment)e.NewValue == HorizontalAlignment.Stretch)
                {
                    _border.HorizontalAlignment = HorizontalAlignment.Stretch;
                }
                else
                    _border.HorizontalAlignment = HorizontalAlignment;
            }
            if ((HorizontalAlignment)e.OldValue == HorizontalAlignment.Stretch || (HorizontalAlignment)e.NewValue == HorizontalAlignment.Stretch)
                InvalidateMeasure();
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
            typeof(ModalPopup),
            new PropertyMetadata(DefaultVerticalAlignment, new PropertyChangedCallback((d, e) => ((ModalPopup)d).OnVerticalAlignmentChanged(e)))
        );
        protected virtual void OnVerticalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_border != null)
            {
                //base.VerticalAlignment = VerticalAlignment.Stretch;
                if ((VerticalAlignment)e.NewValue == VerticalAlignment.Stretch)
                    _border.VerticalAlignment = VerticalAlignment.Stretch;
                else
                    _border.VerticalAlignment = VerticalAlignment;
            }
            if ((VerticalAlignment)e.OldValue == VerticalAlignment.Stretch || (VerticalAlignment)e.NewValue == VerticalAlignment.Stretch)
                InvalidateMeasure();
            UpdateAlignment();
        }
        public new VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }
        #endregion VerticalAlignment Property
        
        #region Margin Property
        public static readonly new DependencyProperty MarginProperty = DependencyProperty.Register(
            nameof(Margin),
            typeof(Thickness),
            typeof(ModalPopup),
            new PropertyMetadata(default(Thickness), new PropertyChangedCallback((d, e) => ((ModalPopup)d).OnMarginChanged(e)))
        );
        protected virtual void OnMarginChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_border!=null)
                _border.Margin = Margin;
        }
        public new Thickness Margin
        {
            get => (Thickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }
        #endregion Margin Property

        #endregion

        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(ModalPopup),
            new PropertyMetadata(default(bool))
        );
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        #endregion

        #endregion


        #region Fields
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Center;
        const string ContentPresenterName = "ModalPopup_Popup_Border_ContentPresenter";
        const string BorderElementName = "ModalPopup_Popup_Border";
        const string PopupElementName = "ModalPopup_Popup";
        ContentPresenter _contentPresenter;
        BubbleBorder _border;
        Popup _popup;
        #endregion


        public ModalPopup()
        {
            //base.HorizontalAlignment = HorizontalAlignment.Stretch;
            //base.VerticalAlignment = VerticalAlignment.Stretch;
            //this.InitializeComponent();
            this.DefaultStyleKey = typeof(ModalPopup);
            /*
            var startStyle = Style;
            var defaultStyleObject = Application.Current.Resources["BaseModalPopupStyle"];
            if (defaultStyleObject is Style defaultStyle)
                Style = defaultStyle;
            */
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _contentPresenter = GetTemplateChild(ContentPresenterName) as ContentPresenter;
            _border = GetTemplateChild(BorderElementName) as BubbleBorder;
            _border.HorizontalAlignment = HorizontalAlignment;
            _border.VerticalAlignment = VerticalAlignment;
            var popupChild = GetTemplateChild(PopupElementName);
            
            _popup = popupChild as Popup;

            if (_popup is null)
            {
                var borderParent = _border.Parent;
                _popup = borderParent as Popup;
            }

            /*
            //BindingOperations.SetBinding(this, ModalPopup.BorderSizeProperty, new Binding { Source=_border, Path=, Mode=BindingMode.OneWay});
            SetBinding(BorderSizeProperty, new Binding 
            {
                Mode=BindingMode.TwoWay,
                Source = _border.Size
            });

            UpdateAlignment();
            */
        }

#if __WASM__
        bool _hasAppeared;
#endif
        public async Task PushAsync()
        {
            _border.SizeChanged += OnBorderSizeChanged;
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

        private void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
        {
            if (args.NewSize.Width < 1 || args.NewSize.Height < 1)
                return;
            _lastMeasuredSize = args.NewSize;
            UpdateAlignment();
        }

        public void PopAsync()
        {
            _border.SizeChanged -= OnBorderSizeChanged;
            _popup.IsOpen = false;
        }


        Size _lastMeasuredSize;
        protected override Size MeasureOverride(Size availableSize)
        {

            /*
            System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride("+availableSize+")");
            var windowWidth = ((Frame)Windows.UI.Xaml.Window.Current.Content).ActualWidth;
            if (double.IsInfinity(availableSize.Width) || availableSize.Width > windowWidth)
                availableSize.Width = windowWidth;

            var windowHeight = ((Frame)Windows.UI.Xaml.Window.Current.Content).ActualHeight;
            if (double.IsInfinity(availableSize.Height) || availableSize.Height > windowHeight)
                availableSize.Height = windowHeight;


            //var result = base.MeasureOverride(availableSize);

            availableSize.Width -= Margin.Horizontal();
            availableSize.Height -= Margin.Vertical();

            _border.Measure(availableSize);
            var result = _border.DesiredSize;
            if (HorizontalAlignment == HorizontalAlignment.Stretch)
                result.Width = availableSize.Width;
            if (VerticalAlignment == VerticalAlignment.Stretch)
                result.Height = availableSize.Height;
            //RegeneratePath(result);

            //_border.Measure(result);

            return result;
            
            //return new Size(windowWidth, windowHeight);
            */

            System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride(" + availableSize + ") ================================================  margin: "+ Margin);

            availableSize.Width -= Margin.Horizontal();
            availableSize.Height -= Margin.Vertical();

            _border.Measure(availableSize);
            var result = _border.DesiredSize;
            _lastMeasuredSize = result;
            //UpdateAlignment();

            System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride result: "+ result);
            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Diagnostics.Debug.WriteLine(GetType() + ".ArrangeOverride("+finalSize+")");
            return base.ArrangeOverride(finalSize);
        }
        

        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(GetType() + ".OnSizeChanged");
            UpdateAlignment();
        }

        void UpdateAlignment()
        {
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment");
            var windowWidth = AppWindow.Size().Width - Margin.Horizontal();
            var windowHeight = AppWindow.Size().Height - Margin.Vertical();

            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment: window("+windowWidth+","+windowHeight+")   content("+ _lastMeasuredSize + ")");

            if (_border is null)
                return;
            double hOffset = 0.0;
            if (HorizontalAlignment == HorizontalAlignment.Center)
                hOffset = (windowWidth - _lastMeasuredSize.Width) / 2;
            else if (HorizontalAlignment == HorizontalAlignment.Right)
                hOffset = (windowWidth - _lastMeasuredSize.Width);

            double vOffset = 0.0;
            if (VerticalAlignment == VerticalAlignment.Center)
                vOffset = (windowHeight - _lastMeasuredSize.Height) / 2;
            else if (VerticalAlignment == VerticalAlignment.Bottom)
                vOffset = (windowHeight - _lastMeasuredSize.Height);

            // works on UWP and WASM - not Android
            _popup.HorizontalOffset = hOffset;
            _popup.VerticalOffset = vOffset;
            
        }



    }
}
