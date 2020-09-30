using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    [TemplatePart(Name = PopupElementName, Type = typeof(Windows.UI.Xaml.Controls.Primitives.Popup))]
    [TemplatePart(Name = BorderElementName, Type = typeof(BubbleBorder))]
    [TemplatePart(Name = ContentPresenterElementName, Type = typeof(ContentPresenter))]
    public partial class BubblePopup
        : ContentControl
    {
        #region Properties

        #region Override Properties

        #region HorizontalAlignment Property
        public static readonly new DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(BubblePopup),
            new PropertyMetadata(default(HorizontalAlignment), new PropertyChangedCallback((d, e) => ((BubblePopup)d).OnHorizontalAlignmentChanged(e)))
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
            typeof(BubblePopup),
            new PropertyMetadata(default(VerticalAlignment), new PropertyChangedCallback((d, e) => ((BubblePopup)d).OnVerticalAlignmentChanged(e)))
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

        #endregion

        #region Popup Emumlated Properties

        #region IsLightDismissEnabled Property
        public static readonly DependencyProperty IsLightDismissEnabledProperty = DependencyProperty.Register(
            nameof(IsLightDismissEnabled),
            typeof(bool),
            typeof(BubblePopup),
            new PropertyMetadata(default(bool))
        );
        public bool IsLightDismissEnabled
        {
            get => (bool)GetValue(IsLightDismissEnabledProperty);
            set => SetValue(IsLightDismissEnabledProperty, value);
        }
        #endregion IsLightDismissEnabled Property

        #region LightDismissOverlayMode Property
        public static readonly DependencyProperty LightDismissOverlayModeProperty = DependencyProperty.Register(
            nameof(LightDismissOverlayMode),
            typeof(LightDismissOverlayMode),
            typeof(BubblePopup),
            new PropertyMetadata(LightDismissOverlayMode.Auto)
        );
        public LightDismissOverlayMode LightDismissOverlayMode
        {
            get => (LightDismissOverlayMode)GetValue(LightDismissOverlayModeProperty);
            set => SetValue(LightDismissOverlayModeProperty, value);
        }
        #endregion LightDismissOverlayMode Property

        #endregion

        #region Popup Extendend Properties

        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(BubblePopup),
            new PropertyMetadata(default(bool))
        );
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }
        #endregion HasShadow Property

        #region PopAfter Property
        public static readonly DependencyProperty PopAfterProperty = DependencyProperty.Register(
            nameof(PopAfter),
            typeof(TimeSpan),
            typeof(BubblePopup),
            new PropertyMetadata(default(TimeSpan))
        );
        public TimeSpan PopAfter
        {
            get => (TimeSpan)GetValue(PopAfterProperty);
            set => SetValue(PopAfterProperty, value);
        }
        #endregion PopAfter Property

        public PopupPoppedCause PoppedCause { get; private set; }

        public object PoppedTrigger { get; private set; }

        #endregion

        #region BubbleUnique Properties

        #region TargetBias Property
        public static readonly DependencyProperty TargetBiasProperty = DependencyProperty.Register(
            nameof(TargetBias),
            typeof(double),
            typeof(BubblePopup),
            new PropertyMetadata(0.5)
        );
        public double TargetBias
        {
            get => (double)GetValue(TargetBiasProperty);
            set => SetValue(TargetBiasProperty, value);
        }
        #endregion TargetBias Property

        #region PointerLength Property
        public static readonly DependencyProperty PointerLengthProperty = DependencyProperty.Register(
            nameof(PointerLength),
            typeof(double),
            typeof(BubblePopup),
            new PropertyMetadata(10.0)
        );
        public double PointerLength
        {
            get => (double)GetValue(PointerLengthProperty);
            set => SetValue(PointerLengthProperty, value);
        }
        #endregion PointerLength Property

        #region PointerTipRadius Property
        public static readonly DependencyProperty PointerTipRadiusProperty = DependencyProperty.Register(
            nameof(PointerTipRadius),
            typeof(double),
            typeof(BubblePopup),
            new PropertyMetadata(1.0)
        );
        public double PointerTipRadius
        {
            get => (double)GetValue(PointerTipRadiusProperty);
            set => SetValue(PointerTipRadiusProperty, value);
        }
        #endregion PointerTipRadius Property

        #region PointerAxialPosition Property
        public static readonly DependencyProperty PointerAxialPositionProperty = DependencyProperty.Register(
            nameof(PointerAxialPosition),
            typeof(double),
            typeof(BubblePopup),
            new PropertyMetadata(0.5)
        );
        public double PointerAxialPosition
        {
            get => (double)GetValue(PointerAxialPositionProperty);
            set => SetValue(PointerAxialPositionProperty, value);
        }
        #endregion PointerAxialPosition Property

        #region PointerDirection Property
        public static readonly DependencyProperty PointerDirectionProperty = DependencyProperty.Register(
            nameof(PointerDirection),
            typeof(PointerDirection),
            typeof(BubblePopup),
            new PropertyMetadata(PointerDirection.None)
        );
        public PointerDirection PointerDirection
        {
            get => (PointerDirection)GetValue(PointerDirectionProperty);
            set => SetValue(PointerDirectionProperty, value);
        }
        #endregion PointerDirection Property

        #region PointerCornerRadius Property
        public static readonly DependencyProperty PointerCornerRadiusProperty = DependencyProperty.Register(
            nameof(PointerCornerRadius),
            typeof(double),
            typeof(BubblePopup),
            new PropertyMetadata(default(double))
        );
        public double PointerCornerRadius
        {
            get => (double)GetValue(PointerCornerRadiusProperty);
            set => SetValue(PointerCornerRadiusProperty, value);
        }
        #endregion PointerCornerRadius Property

        #endregion

        #endregion


        #region Fields
        const string ContentPresenterElementName = "_contentPresenter";
        const string BorderElementName = "_border";
        const string PopupElementName = "_popup";

        ContentPresenter _contentPresenter;
        BubbleBorder _border;
        Windows.UI.Xaml.Controls.Primitives.Popup _popup;
        #endregion


        #region Construction / Initialization
        static BubblePopup()
        {

        }

        public BubblePopup()
        {
            DefaultStyleKey = typeof(BubblePopup);

            var xstyle = Application.Current.Resources["BubblePopupStyle"] as Style;
            Style = xstyle;
            bool rebuilt = ApplyTemplate();

        }


        protected override void OnBringIntoViewRequested(BringIntoViewRequestedEventArgs e)
        {
            base.OnBringIntoViewRequested(e);
        }
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
        }

        protected override void OnContentTemplateChanged(DataTemplate oldContentTemplate, DataTemplate newContentTemplate)
        {
            base.OnContentTemplateChanged(oldContentTemplate, newContentTemplate);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _popup = (Windows.UI.Xaml.Controls.Primitives.Popup)GetTemplateChild(PopupElementName);
            _border = (BubbleBorder)GetTemplateChild(BorderElementName);
            _contentPresenter = (ContentPresenter)GetTemplateChild(ContentPresenterElementName);
        }

        #endregion


        #region Push / Pop
        bool _hasAppeared;
        public async Task PushAsync()
        {
            var ystyle = Style;
            var background = ((SolidColorBrush)Background).Color;
            var template = Template;

            PoppedCause = PopupPoppedCause.BackgroundTouch;
            PoppedTrigger = null;

            await OnPushBeginAsync();
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
            await OnPushEndAsync();
            if (PopAfter != default)
            {
                P42.Utils.Uno.Device.StartTimer(PopAfter, () =>
                {
                    PopAsync(PopupPoppedCause.Timeout);
                    return false;
                });
            }

        }

        public async Task PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, [CallerMemberName] object trigger = null)
        {
            PoppedCause = cause;
            PoppedTrigger = trigger;

            await OnPopBeginAsync();
            _popup.IsOpen = false;
            await OnPopEndAsync();
        }

        /// <summary>
        /// Invoked at start on appearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnPushBeginAsync()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Invoked at end of appearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnPushEndAsync()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Invoked at start of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnPopBeginAsync()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Invoked at end of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnPopEndAsync()
        {
            return Task.FromResult(0);
        }

        #endregion


        #region Layout

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

        #endregion
    }
}
