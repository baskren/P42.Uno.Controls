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
    public partial class BasePopup : ContentControl
    {
        #region Properties

        #region Overridden Properties
        
        #region HorizontalAlignment Property
        public static readonly new DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(BasePopup),
            new PropertyMetadata(DefaultHorizontalAlignment, new PropertyChangedCallback((d, e) => ((BasePopup)d).OnHorizontalAlignmentChanged(e)))
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
            typeof(BasePopup),
            new PropertyMetadata(DefaultVerticalAlignment, new PropertyChangedCallback((d, e) => ((BasePopup)d).OnVerticalAlignmentChanged(e)))
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
            typeof(BasePopup),
            new PropertyMetadata(default(Thickness), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnMarginChanged(e)))
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
            typeof(BasePopup),
            new PropertyMetadata(default(bool))
        );
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        #endregion

        #region Target Property
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(UIElement),
            typeof(BasePopup),
            new PropertyMetadata(default(UIElement), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnTargetChanged(e)))
        );
        protected virtual void OnTargetChanged(DependencyPropertyChangedEventArgs e)
        {
            // where is the target?
            if (Target != null)
            {
                var frame = Target.GetFrame();
            }

        }
        public UIElement Target
        {
            get => (UIElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        #endregion Target Property

        #endregion


        #region Fields
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Center;
        const string ContentPresenterName = "BasePopup_Popup_Border_ContentPresenter";
        const string BorderElementName = "BasePopup_Popup_Border";
        const string PopupElementName = "BasePopup_Popup";
        ContentPresenter _contentPresenter;
        BubbleBorder _border;
        Popup _popup;
        #endregion


        #region Construction / Initialization
        public BasePopup()
        {
            //this.InitializeComponent();
            this.DefaultStyleKey = typeof(BasePopup);
            /*
            var startStyle = Style;
            var defaultStyleObject = Application.Current.Resources["BaseBasePopupStyle"];
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
        }
        #endregion


        #region Push / Pop
#if DEPRECATED // __WASM__ || NETSTANDARD
        bool _hasAppeared;
#endif
        public async Task PushAsync()
        {
            if (Parent is Grid grid)
                grid.Children.Remove(this);

            _border.SizeChanged += OnBorderSizeChanged;

            await OnPushBeginAsync();
            _popup.IsOpen = true;
#if DEPRECATED // __WASM__ || NETSTANDARD
            if (!_hasAppeared)
            {
                _hasAppeared = true;
                await Task.Delay(5);
                _popup.IsOpen = false;
                await Task.Delay(5);
                _popup.IsOpen = true;
            }
#endif
            await OnPushEndAsync();
        }

        public async Task PopAsync()
        {
            _border.SizeChanged -= OnBorderSizeChanged;
            await OnPopBeginAsync();
            _popup.IsOpen = false;
            await OnPopEndAsync();
        }
        #endregion


        #region Protected Push / Pop Methods
        /// <summary>
        /// Invoked at start on appearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnPushBeginAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Invoked at end of appearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnPushEndAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Invoked at start of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnPopBeginAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Invoked at end of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnPopEndAsync()
        {
            return Task.CompletedTask;
        }

        #endregion


        #region Event Handlers
        private void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
        {
            if (args.NewSize.Width < 1 || args.NewSize.Height < 1)
                return;
            _lastMeasuredSize = args.NewSize;
            UpdateAlignment();
        }

        #endregion


        #region Layout

        Size _lastMeasuredSize;
        protected override Size MeasureOverride(Size availableSize)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride(" + availableSize + ") ================================================  margin: "+ Margin);

            availableSize.Width -= Margin.Horizontal();
            availableSize.Height -= Margin.Vertical();

            _border.Measure(availableSize);
            var result = _border.DesiredSize;
            _lastMeasuredSize = result;

            //System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride result: "+ result);
            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".ArrangeOverride("+finalSize+")");
            return base.ArrangeOverride(finalSize);
        }
        

#if __WASM__ || NETSTANDARD
        Size _firstRenderSize;
#endif
        void UpdateAlignment()
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment");
            var windowSize = AppWindow.Size();
            if (windowSize.Width < 1 || windowSize.Height < 1)
                return;

            var windowWidth = windowSize.Width - Margin.Horizontal();
            var windowHeight = windowSize.Height - Margin.Vertical();

            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment: window("+windowWidth+","+windowHeight+")   content("+ _lastMeasuredSize + ")");

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

#if __WASM__ || NETSTANDARD
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment WASM || NETSTANDARD");

            if (_firstRenderSize.Width < 1 || _firstRenderSize.Height < 1)
                _firstRenderSize = windowSize;

            hOffset -= (_firstRenderSize.Width + Margin.Left) / 2.0 ; 
            //vOffset -= windowHeight / 2.0;
#elif __ANDROID__
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment ANDROID");
#elif __iOS__
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment iOS");
#elif __MACOS__
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment MACOS");
#elif NETFX_CORE
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment UWP");
#else
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment ????");
#endif
            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment Offset: " + hOffset + ", " + vOffset);

            _popup.HorizontalOffset = hOffset;
            _popup.VerticalOffset = vOffset;
            
        }

        #endregion

    }
}
