using Microsoft.Toolkit.Uwp.UI.Animations;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Uno.Extensions;
using Uno.Extensions.ValueType;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

#if NETFX_CORE
using Popup = Windows.UI.Xaml.Controls.Primitives.Popup;
#else
using Popup = Windows.UI.Xaml.Controls.Popup;
#endif


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace P42.Uno.Controls
{
    
    [TemplatePart(Name = ContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = BorderElementName, Type = typeof(BubbleBorder))]
    [TemplatePart(Name = PopupElementName, Type = typeof(Windows.UI.Xaml.Controls.Primitives.Popup))]
    public partial class TargetedPopup : ContentControl, ITargetedPopup
    {
        #region Properties

        #region Overridden Properties
        
        #region HorizontalAlignment Property
        public static readonly new DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(TargetedPopup),
            new PropertyMetadata(DefaultHorizontalAlignment, new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnHorizontalAlignmentChanged(e)))
        );
        protected virtual void OnHorizontalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_border is null)
                return;
            UpdateMarginAndAlignment(true);
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
            typeof(TargetedPopup),
            new PropertyMetadata(DefaultVerticalAlignment, new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnVerticalAlignmentChanged(e)))
        );
        protected virtual void OnVerticalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_border is null)
                return;
            UpdateMarginAndAlignment(true);
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
            typeof(TargetedPopup),
            new PropertyMetadata(default(Thickness), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnMarginChanged(e)))
        );
        protected virtual void OnMarginChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateMarginAndAlignment();
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
            typeof(TargetedPopup),
            new PropertyMetadata(default(bool))
        );
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        #endregion

        #region PopAfter Property
        public static readonly DependencyProperty PopAfterProperty = DependencyProperty.Register(
            nameof(PopAfter),
            typeof(TimeSpan),
            typeof(TargetedPopup),
            new PropertyMetadata(default(TimeSpan))
        );
        public TimeSpan PopAfter
        {
            get => (TimeSpan)GetValue(PopAfterProperty);
            set => SetValue(PopAfterProperty, value);
        }
        #endregion PopAfter Property

        #region Target Properties

        #region Target Property
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(UIElement),
            typeof(TargetedPopup),
            new PropertyMetadata(default(UIElement), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnTargetChanged(e)))
        );
        protected virtual void OnTargetChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateMarginAndAlignment();
        }
        public UIElement Target
        {
            get => (UIElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        #endregion Target Property

        #region TargetPoint Property
        public static readonly DependencyProperty TargetPointProperty = DependencyProperty.Register(
            nameof(TargetPoint),
            typeof(Point),
            typeof(TargetedPopup),
            new PropertyMetadata(default(Point), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnTargetPointChanged(e)))
        );
        protected virtual void OnTargetPointChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateMarginAndAlignment();
        }
        public Point TargetPoint
        {
            get => (Point)GetValue(TargetPointProperty);
            set => SetValue(TargetPointProperty, value);
        }
        #endregion TargetPoint Property

        #endregion

        #region Pointer Properties

        #region PointerBias Property
        public static readonly DependencyProperty PointerBiasProperty = DependencyProperty.Register(
            nameof(PointerBias),
            typeof(double),
            typeof(TargetedPopup),
            new PropertyMetadata(0.5, new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnPointerBiasChanged(e)))
        );
        protected virtual void OnPointerBiasChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }
        /// <summary>
        /// Gets or sets the bias (0.0 is start; 0.5 is center;  1.0 is end; greater than 1.0 is pixels from start; less than 0.0 is pixels from end)of the pointer relative to the chosen face on the target.
        /// </summary>
        /// <value>The target bias.</value>
        public double PointerBias
        {
            get => (double)GetValue(PointerBiasProperty);
            set => SetValue(PointerBiasProperty, value);
        }
        #endregion PointerBias Property

        #region PointerCornerRadius Property
        public static readonly DependencyProperty PointerCornerRadiusProperty = DependencyProperty.Register(
            nameof(PointerCornerRadius),
            typeof(double),
            typeof(TargetedPopup),
            new PropertyMetadata(default(double))
        );
        public double PointerCornerRadius
        {
            get => (double)GetValue(PointerCornerRadiusProperty);
            set => SetValue(PointerCornerRadiusProperty, value);
        }
        #endregion PointerCornerRadius Property

        #region Pointer Directions

        public PointerDirection ActualPointerDirection { get; private set; }

        #region PreferredPointerDirection Property
        public static readonly DependencyProperty PreferredPointerDirectionProperty = DependencyProperty.Register(
            nameof(PreferredPointerDirection),
            typeof(PointerDirection),
            typeof(TargetedPopup),
            new PropertyMetadata(PointerDirection.Any, new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnPreferredPointerDirectionChanged(e)))
        );
        protected virtual void OnPreferredPointerDirectionChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateMarginAndAlignment();
        }
        public PointerDirection PreferredPointerDirection
        {
            get => (PointerDirection)GetValue(PreferredPointerDirectionProperty);
            set => SetValue(PreferredPointerDirectionProperty, value);
        }
        #endregion PreferredPointerDirection Property

        #region FallbackPointerDirection Property
        public static readonly DependencyProperty FallbackPointerDirectionProperty = DependencyProperty.Register(
            nameof(FallbackPointerDirection),
            typeof(PointerDirection),
            typeof(TargetedPopup),
            new PropertyMetadata(default(PointerDirection), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnFallbackPointerDirectionChanged(e)))
        );
        protected virtual void OnFallbackPointerDirectionChanged(DependencyPropertyChangedEventArgs e)
        {
            if (ActualPointerDirection == PointerDirection.None && PreferredPointerDirection != PointerDirection.None)
                UpdateMarginAndAlignment();
        }
        public PointerDirection FallbackPointerDirection
        {
            get => (PointerDirection)GetValue(FallbackPointerDirectionProperty);
            set => SetValue(FallbackPointerDirectionProperty, value);
        }
        #endregion FallbackPointerDirection Property

        #endregion

        #region PointerLength Property
        public static readonly DependencyProperty PointerLengthProperty = DependencyProperty.Register(
            nameof(PointerLength),
            typeof(double),
            typeof(TargetedPopup),
            new PropertyMetadata(10.0, new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnPointerLengthChanged(e)))
        );
        protected virtual void OnPointerLengthChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateMarginAndAlignment();
        }
        /// <summary>
        /// Gets or sets the length of the bubble layout's pointer.
        /// </summary>
        /// <value>The length of the pointer.</value>
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
            typeof(TargetedPopup),
            new PropertyMetadata(default(double), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnPointerTipRadiusChanged(e)))
        );
        protected virtual void OnPointerTipRadiusChanged(DependencyPropertyChangedEventArgs e)
        {
            // if done correctly, BubbleBorder would calculate the difference between sharp and rounded tip and adjust margins
        }
        /// <summary>
        /// Gets or sets the radius of the bubble's pointer tip.
        /// </summary>
        /// <value>The pointer tip radius.</value>
        public double PointerTipRadius
        {
            get => (double)GetValue(PointerTipRadiusProperty);
            set => SetValue(PointerTipRadiusProperty, value);
        }
        #endregion PointerTipRadius Property

        #region PointToOffScreenElements Property
        public static readonly DependencyProperty PointToOffScreenElementsProperty = DependencyProperty.Register(
            nameof(PointToOffScreenElements),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnPointToOffScreenElementsChanged(e)))
        );
        protected virtual void OnPointToOffScreenElementsChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        public bool PointToOffScreenElements
        {
            get => (bool)GetValue(PointToOffScreenElementsProperty);
            set => SetValue(PointToOffScreenElementsProperty, value);
        }
        #endregion PointToOffScreenElements Property

        #region PointerMargin Property
        public static readonly DependencyProperty PointerMarginProperty = DependencyProperty.Register(
            nameof(PointerMargin),
            typeof(double),
            typeof(TargetedPopup),
            new PropertyMetadata(5.0)
        );
        public double PointerMargin
        {
            get => (double)GetValue(PointerMarginProperty);
            set => SetValue(PointerMarginProperty, value);
        }
        #endregion PointerMargin Property

        #endregion

        #region LightDismiss Properties

        #region IsLightDismissEnabled Property
        public static readonly DependencyProperty IsLightDismissEnabledProperty = DependencyProperty.Register(
            nameof(IsLightDismissEnabled),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnIsLightDismissEnabledChanged(e)))
        );
        protected virtual void OnIsLightDismissEnabledChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_popup != null)
                _popup.IsLightDismissEnabled = IsLightDismissEnabled;
        }
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
            typeof(TargetedPopup),
            new PropertyMetadata(default(LightDismissOverlayMode), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnLightDismissOverlayModeChanged(e)))
        );
        protected virtual void OnLightDismissOverlayModeChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_popup != null)
                _popup.LightDismissOverlayMode = LightDismissOverlayMode;
        }
        public LightDismissOverlayMode LightDismissOverlayMode
        {
            get => (LightDismissOverlayMode)GetValue(LightDismissOverlayModeProperty);
            set => SetValue(LightDismissOverlayModeProperty, value);
        }
        #endregion LightDismissOverlayMode Property


        #endregion

        public PopupPoppedCause PoppedCause { get; private set; }

        public object PoppedTrigger { get; private set; }

        public PushPopState PushPopState { get; private set; }

        public bool IsEmpty
        {
            get
            {
                var contentPresenter = _contentPresenter;
                while (contentPresenter?.Content is ContentPresenter cp)
                    contentPresenter = cp;
                return contentPresenter.Content is null;
            }
        }
        #endregion


        #region Fields
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Center;
        const string ContentPresenterName = "TargetedPopup_Popup_Border_ContentPresenter";
        const string BorderElementName = "TargetedPopup_Popup_Border";
        const string PopupElementName = "TargetedPopup_Popup";
        ContentPresenter _contentPresenter;
        BubbleBorder _border;
        Popup _popup;
        #endregion


        #region Events
        public event EventHandler Pushed;
        /// <summary>
        /// Occurs when popup has been cancelled.
        /// </summary>
        public event EventHandler<PopupPoppedEventArgs> Popped;

        #endregion


        #region Construction / Initialization
        public TargetedPopup()
        {
            //this.InitializeComponent();
            ActualPointerDirection = PointerDirection.None;
            this.DefaultStyleKey = typeof(TargetedPopup);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _contentPresenter = GetTemplateChild(ContentPresenterName) as ContentPresenter;
            _border = GetTemplateChild(BorderElementName) as BubbleBorder;
            _border.HorizontalAlignment = HorizontalAlignment;
            _border.VerticalAlignment = VerticalAlignment;
            _border.PointerLength = PointerLength;

            OnIsLightDismissEnabledChanged(null);
            OnLightDismissOverlayModeChanged(null);

            UpdateMarginAndAlignment();
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
        public virtual async Task PushAsync()
        {
            if (PushPopState == PushPopState.Pushed || PushPopState == PushPopState.Pushing)
                return;

            if (PushPopState == PushPopState.Popping)
            {
                if (_popCompletionSource is null)
                    await WaitForPop();
                else
                    return;
            }

            PushPopState = PushPopState.Pushing;
            _popCompletionSource = null;

            if (Parent is Grid grid)
                grid.Children.Remove(this);

            _border.SizeChanged += OnBorderSizeChanged;
            _popup.Closed += OnPopupClosed;
            PoppedCause = PopupPoppedCause.BackgroundTouch;
            PoppedTrigger = null;

            await OnPushBeginAsync();

            _popup.Opacity = 0.0;
            _popup.IsOpen = true;

            var storyboard = new Storyboard();
            var opacityAnimation = new DoubleAnimation
            {
                Duration = TimeSpan.FromMilliseconds(400),
                EnableDependentAnimation = true,
                To = 1
            };
            Storyboard.SetTargetProperty(opacityAnimation, nameof(UIElement.Opacity));
            Storyboard.SetTarget(opacityAnimation, _popup);
            storyboard.Children.Add(opacityAnimation);
            await storyboard.BeginAsync();

            UpdateMarginAndAlignment();

            if (PopAfter > default(TimeSpan))
            {
                Device.StartTimer(PopAfter, () =>
                {
                    PopAsync(PopupPoppedCause.Timeout, "Timeout");
                    return false;
                });
            }

            await OnPushEndAsync();
            PushPopState = PushPopState.Pushed;
            Pushed?.Invoke(this, EventArgs.Empty);
            _pushCompletionSource?.SetResult(true);
        }

        protected virtual void OnPopupClosed(object sender, object e)
        {
            PushPopState = PushPopState.Popping;
            _border.SizeChanged -= OnBorderSizeChanged;
            _popup.Closed -= OnPopupClosed;
            var result = new PopupPoppedEventArgs(PoppedCause, PoppedTrigger);
            PushPopState = PushPopState.Popped;
            _popCompletionSource?.SetResult(result);
            Popped?.Invoke(this, result);
            _popCompletionSource = null;
        }

        public virtual async Task PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, [CallerMemberName] object trigger = null)
        {
            if (PushPopState == PushPopState.Popping || PushPopState == PushPopState.Popped)
                return;
            
            if (PushPopState == PushPopState.Pushing)
            {
                if (_pushCompletionSource is null)
                    await WaitForPush();
                else
                    return;
            }

            PushPopState = PushPopState.Popping;
            _pushCompletionSource = null;

            PoppedCause = cause;
            PoppedTrigger = trigger;
            await OnPopBeginAsync();

            var storyboard = new Storyboard();
            var animation = new DoubleAnimation
            {
                Duration = TimeSpan.FromMilliseconds(400),
                EnableDependentAnimation = true,
                To = 0
            };
            Storyboard.SetTargetProperty(animation, nameof(UIElement.Opacity));
            Storyboard.SetTarget(animation, _popup);
            storyboard.Children.Add(animation);
            await storyboard.BeginAsync();

            _popup.IsOpen = false;
            await OnPopEndAsync();
        
        }

        TaskCompletionSource<PopupPoppedEventArgs> _popCompletionSource;
        public async Task<PopupPoppedEventArgs> WaitForPop()
        {
            _popCompletionSource = _popCompletionSource ?? new TaskCompletionSource<PopupPoppedEventArgs>();
            return await _popCompletionSource.Task;
        }

        TaskCompletionSource<bool> _pushCompletionSource;
        async Task<bool> WaitForPush()
        {
            _pushCompletionSource = _pushCompletionSource ?? new TaskCompletionSource<bool>();
            return await _pushCompletionSource.Task;
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
            UpdateMarginAndAlignment();
        }

        #endregion


        #region Layout

        protected override Size MeasureOverride(Size availableSize)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride(" + availableSize + ") ================================================  margin: "+ Margin);
            if (IsEmpty)
                return new Size(50 + Padding.Horizontal(), 50 + Padding.Vertical());

            availableSize.Width -= Margin.Horizontal();
            availableSize.Height -= Margin.Vertical();

            _border.Measure(availableSize);
            var result = _border.DesiredSize;

            //System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride result: "+ result);
            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".ArrangeOverride("+finalSize+")");
            return base.ArrangeOverride(finalSize);
        }
        


        void UpdateMarginAndAlignment(bool isAlignmentOnlyChange = false)
        {
            if (_border is null)
                return;
            _border.Margin = Margin;

            var windowSize = AppWindow.Size();
            if (windowSize.Width < 1 || windowSize.Height < 1)
                return;
            var windowWidth = windowSize.Width - Margin.Horizontal();
            var windowHeight = windowSize.Height - Margin.Vertical();
            var cleanSize = RectangleBorderSize(new Size(windowWidth, windowHeight));

            if (PreferredPointerDirection == PointerDirection.None)
            {
                System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateMarginAndAlignment PreferredPointerDirection == PointerDirection.None");
                CleanMarginAndAlignment(HorizontalAlignment,VerticalAlignment, cleanSize);
                return;
            }

            var targetBounds = TargetBounds();


            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateBorderMarginAndAlignment targetBounds:["+targetBounds+"]");
            var availableSpace = AvailableSpace(targetBounds);
            var stats = BestFit(availableSpace, cleanSize);

            if (stats.PointerDirection == PointerDirection.None)
            {
                System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateMarginAndAlignment BestFit = None");
                CleanMarginAndAlignment(HorizontalAlignment.Center, VerticalAlignment.Center, cleanSize);
                return;
            }

            _popup.HorizontalOffset = 0;
            _popup.VerticalOffset = 0;
            ActualPointerDirection = stats.PointerDirection;
            var baseMargin = Margin;

            if (stats.PointerDirection.IsHorizontal())
            {
                if (stats.PointerDirection == PointerDirection.Left)
                {
                    baseMargin.Left = targetBounds.Right;
                    base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment == HorizontalAlignment.Stretch ? HorizontalAlignment.Stretch : HorizontalAlignment.Left;
                    System.Diagnostics.Debug.WriteLine("\t stats.PointerDirection == PointerDirection.Left");
                    System.Diagnostics.Debug.WriteLine("\t targetBounds:[" + targetBounds + "]");
                    System.Diagnostics.Debug.WriteLine("\t stats.BorderSize:[" + stats.BorderSize + "]");
                    System.Diagnostics.Debug.WriteLine("\t baseMargin:[" + baseMargin + "]");
                }
                else
                {
                    if (HorizontalAlignment == HorizontalAlignment.Stretch)
                    {
                        base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment;
                    }
                    else
                    {
                        baseMargin.Left = targetBounds.Left - stats.BorderSize.Width - PointerLength;
                        base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment.Left;
                        System.Diagnostics.Debug.WriteLine("\t targetBounds.Left:[" + targetBounds.Left + "]");
                        System.Diagnostics.Debug.WriteLine("\t stats.BorderSize.Width:[" + stats.BorderSize.Width + "]");
                        System.Diagnostics.Debug.WriteLine("\t PointerLength:[" + PointerLength + "]");
                        System.Diagnostics.Debug.WriteLine("\t baseMargin:[" + baseMargin + "]");
                    }
                }

                if (VerticalAlignment == VerticalAlignment.Top)
                {
                    baseMargin.Top = Math.Max(Margin.Top, targetBounds.Top);
                    base.VerticalAlignment = _border.VerticalAlignment = VerticalAlignment.Top;
                }
                else if (VerticalAlignment == VerticalAlignment.Center)
                {
                    baseMargin.Top = Math.Max(Margin.Top, (targetBounds.Top + targetBounds.Bottom) / 2.0 - stats.BorderSize.Height / 2.0);
                    base.VerticalAlignment = _border.VerticalAlignment = VerticalAlignment.Top;
                }
                else if (VerticalAlignment == VerticalAlignment.Bottom)
                {
                    baseMargin.Top = Math.Max(Margin.Top, targetBounds.Bottom - stats.BorderSize.Height);
                    base.VerticalAlignment = _border.VerticalAlignment = VerticalAlignment.Top;
                }
                else
                {
                    base.VerticalAlignment = _border.VerticalAlignment = VerticalAlignment.Stretch;
                }
                if (baseMargin.Top + stats.BorderSize.Height > windowSize.Height - Margin.Bottom)
                    baseMargin.Top = windowSize.Height - Margin.Bottom - stats.BorderSize.Height;


                _border.PointerAxialPosition = (targetBounds.Top - baseMargin.Top) + targetBounds.Bottom - (targetBounds.Top + targetBounds.Bottom) / 2.0;
            }
            else
            {
                if (stats.PointerDirection == PointerDirection.Up)
                {
                    baseMargin.Top = targetBounds.Bottom;
                    base.VerticalAlignment = _border.VerticalAlignment = VerticalAlignment == VerticalAlignment.Stretch ? VerticalAlignment.Stretch : VerticalAlignment.Top;
                }
                else
                {
                    if (VerticalAlignment == VerticalAlignment.Stretch)
                    {
                        base.VerticalAlignment = _border.VerticalAlignment = VerticalAlignment;
                    }
                    else
                    {
                        baseMargin.Top = targetBounds.Top - stats.BorderSize.Height - PointerLength;
                        base.VerticalAlignment = _border.VerticalAlignment = VerticalAlignment.Top;
                    }
                }

                if (HorizontalAlignment == HorizontalAlignment.Left)
                {
                    baseMargin.Left = Math.Max(Margin.Left, targetBounds.Left);
                    base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment.Left;

                }
                else if (HorizontalAlignment == HorizontalAlignment.Center)
                {
                    baseMargin.Left = Math.Max(Margin.Left, (targetBounds.Left + targetBounds.Right) / 2.0 - stats.BorderSize.Width / 2.0);
                    base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment.Left;
                }
                else if (HorizontalAlignment == HorizontalAlignment.Right)
                {
                    baseMargin.Left = Math.Max(Margin.Left, targetBounds.Right - stats.BorderSize.Width);
                    base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment.Left;
                }
                else
                {
                    base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment.Stretch;
                }
                if (baseMargin.Left + stats.BorderSize.Width > windowSize.Width - Margin.Right)
                    baseMargin.Left = windowSize.Width - Margin.Right - stats.BorderSize.Width;

                _border.PointerAxialPosition = (targetBounds.Left - baseMargin.Left) + targetBounds.Right - (targetBounds.Left + targetBounds.Right) / 2.0;
            }
#if __WASM__ || NETSTANDARD
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateMarginAndAlignment WASM || NETSTANDARD");
            GetFirstRenderSize();
            _popup.HorizontalOffset = -(_firstRenderSize.Width) / 2.0;
            _popup.VerticalOffset = -(_firstRenderSize.Height) / 2.0;
            System.Diagnostics.Debug.WriteLine(GetType() + ".CleanMarginAndAlignment hOffset:" + _popup.HorizontalOffset + " vOffset:" + _popup.VerticalOffset);
#endif

            _border.PointerDirection = stats.PointerDirection;
            base.Margin = _border.Margin = baseMargin;
        }

        void CleanMarginAndAlignment(HorizontalAlignment hzAlign, VerticalAlignment vtAlign, Size cleanSize)
        {             //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment");

            ActualPointerDirection = PointerDirection.None;

            if (_border is null || _popup is null)
                return;

            base.Margin = _border.Margin = Margin;

            var windowSize = AppWindow.Size();
            if (windowSize.Width < 1 || windowSize.Height < 1)
                return;

            base.HorizontalAlignment = _border.HorizontalAlignment = hzAlign;
            base.VerticalAlignment = _border.VerticalAlignment = vtAlign;
            _border.PointerDirection = PointerDirection.None;

            var windowWidth = windowSize.Width - Margin.Horizontal();
            var windowHeight = windowSize.Height - Margin.Vertical();

            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment: window("+windowWidth+","+windowHeight+")   content("+ _lastMeasuredSize + ")");

            double hOffset = 0.0;
            if (hzAlign == HorizontalAlignment.Center)
                hOffset = (windowWidth - cleanSize.Width) / 2;
            else if (hzAlign == HorizontalAlignment.Right)
                hOffset = (windowWidth - cleanSize.Width);

            double vOffset = 0.0;
            if (vtAlign == VerticalAlignment.Center)
                vOffset = (windowHeight - cleanSize.Height) / 2;
            else if (vtAlign == VerticalAlignment.Bottom)
                vOffset = (windowHeight - cleanSize.Height);


#if __WASM__ || NETSTANDARD
            System.Diagnostics.Debug.WriteLine(GetType() + ".CleanMarginAndAlignment WASM || NETSTANDARD");
            GetFirstRenderSize();
            hOffset -= (_firstRenderSize.Width) / 2.0;
            vOffset -= (_firstRenderSize.Height) / 2.0;
            System.Diagnostics.Debug.WriteLine(GetType() + ".CleanMarginAndAlignment hOffset:" + hOffset + " vOffset:"+vOffset);
#endif
            _popup.HorizontalOffset = hOffset;
            _popup.VerticalOffset = vOffset;
        }

        DirectionStats BestFit(Thickness availableSpace, Size cleanSize)
        {
            //var stats = new List<DirectionStats>();
            // given the amount of free space, determine if the border will fit 
            var windowSpace = new Size(AppWindow.Size().Width - Margin.Horizontal(), AppWindow.Size().Height - Margin.Vertical());
            var cleanStat = new DirectionStats
            {
                PointerDirection = PointerDirection.None,
                BorderSize = cleanSize,
                FreeSpace = new Size(0,0)
            };

            #region Check if clean border fits in preferred pointer quadrants
            // see if the existing measurement data works
            var prefStats = GetRectangleBorderStatsForDirection(PreferredPointerDirection, cleanStat, availableSpace);
            // At this point in time, only valid fits are in the stats list
            if (prefStats.Count > 0)
            {
                (DirectionStats stat, double minFree) = prefStats.MaxBy(s => s.MinFree);
                //if (minFree >=0)  // At this point in time, only valid fits are in the stats list
                    return stat;
            }
            #endregion

            #region Check if border + content could fit in any of the preferred pointer quadrants
            // at this point in time valid and invalid fits are in the stats list
            prefStats = GetMeasuredStatsForDirection(PreferredPointerDirection, cleanStat, availableSpace, windowSpace);

            if (prefStats.Count > 0)
            {
                (DirectionStats stat, double minFree) = prefStats.MaxBy(s => s.MinFree);
                if (minFree >= 0)
                    return stat;
            }
            #endregion

            // the stats list only contains invalid fallback fits ... but perhaps not all fallback fits have yet been tried
            var uncheckedFallbackPointerDirection = (FallbackPointerDirection ^ PreferredPointerDirection) | FallbackPointerDirection;

            #region Check if clean border fits in unchecked fallback pointer quadrants
            var fallbackStats = GetRectangleBorderStatsForDirection(uncheckedFallbackPointerDirection, cleanStat, availableSpace);
            if (fallbackStats.Count > 0)
            {
                (DirectionStats stat, double minFree) = fallbackStats.MaxBy(s => s.MinFree);
                //if (minFree >=0)  // At this point in time, only valid fits are in the stats list
                return stat;
            }
            #endregion

            #region Check if border + content could fit in any of the unchecked fallback pointer quadrants
            fallbackStats = GetMeasuredStatsForDirection(uncheckedFallbackPointerDirection, cleanStat, availableSpace, windowSpace);

            if (fallbackStats.Count > 0)
            {
                (DirectionStats stat, double minFree) = fallbackStats.MaxBy(s => s.MinFree);
                if (minFree >= 0)
                    return stat;
            }
            #endregion

            return cleanStat;
        }

        Rect TargetBounds()
        {
            var targetBounds = Target is null ? Rect.Empty : Target.GetBounds();

            double targetLeft = (Target is null ? TargetPoint.X : targetBounds.Left) - PointerMargin;
            double targetRight = (Target is null ? TargetPoint.X : targetBounds.Right) + PointerMargin;
            double targetTop = (Target is null ? TargetPoint.Y : targetBounds.Top) - PointerMargin;
            double targetBottom = (Target is null ? TargetPoint.Y : targetBounds.Bottom) + PointerMargin;


            return new Rect(targetLeft, targetTop, targetRight - targetLeft, targetBottom - targetTop);
        }

        Thickness AvailableSpace(Rect target)
        {
            var windowBounds = AppWindow.Size();
            if (Target != null || (TargetPoint.X > 0 || TargetPoint.Y > 0))
            {

                if (target.Right > 0 && target.Left < windowBounds.Width && target.Bottom > 0 && target.Top < windowBounds.Height)
                {
                    var availL = target.Left - Margin.Left;
                    var availR = windowBounds.Width - Margin.Right - target.Right;
                    var availT = target.Top - Margin.Top;
                    var availB = windowBounds.Height - target.Bottom - Margin.Bottom;

                    var maxWidth = MaxWidth;
                    if (Width > 0 && Width < maxWidth)
                        maxWidth = Width;
                    if (maxWidth > 0 && HorizontalAlignment != HorizontalAlignment.Stretch)
                    {
                        availL = Math.Min(availL, maxWidth);
                        availR = Math.Min(availR, maxWidth);
                    }

                    var maxHeight = MaxHeight;
                    if (Height > 0 && Height < maxHeight)
                        maxHeight = Height;
                    if (maxHeight > 0 && VerticalAlignment != VerticalAlignment.Stretch)
                    {
                        availT = Math.Min(availT, maxHeight);
                        availB = Math.Min(availB, maxHeight);
                    }

                    return new Thickness(availL, availT, availR, availB);
                }
            }
            if (PointToOffScreenElements)
                return new Thickness(windowBounds.Width - Margin.Horizontal(), windowBounds.Height - Margin.Vertical(), windowBounds.Width - Margin.Horizontal(), windowBounds.Height - Margin.Vertical());
            return new Thickness(-1, -1, -1, -1);

        }

        List<DirectionStats> GetRectangleBorderStatsForDirection(PointerDirection pointerDirection, DirectionStats cleanStat, Thickness availableSpace)
        {
            System.Diagnostics.Debug.WriteLine(GetType() + ".GetRectangleBorderStatsForDirection cleanStat:["+cleanStat+"]");
            var stats = new List<DirectionStats>();
            if (pointerDirection.LeftAllowed() && cleanStat.FreeSpace.Width >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Left;
                stat.BorderSize.Width += PointerLength;
                var free = availableSpace.Right - stat.BorderSize.Width;
                if (free >= 0)
                {
                    stat.FreeSpace.Width = free;
                    stats.Add(stat);
                }
            }
            if (pointerDirection.RightAllowed() && cleanStat.FreeSpace.Width >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Right;
                stat.BorderSize.Width += PointerLength;
                var free = availableSpace.Left - stat.BorderSize.Width;
                if (free >= 0)
                {
                    stat.FreeSpace.Width = free;
                    stats.Add(stat);
                }
            }
            if (pointerDirection.UpAllowed() && cleanStat.FreeSpace.Height >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Up;
                stat.BorderSize.Height += PointerLength;
                var free = availableSpace.Bottom - stat.BorderSize.Height;
                if (free >= 0)
                {
                    stat.FreeSpace.Width = free;
                    stats.Add(stat);
                }
            }
            if (pointerDirection.DownAllowed() && cleanStat.FreeSpace.Height >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Down;
                stat.BorderSize.Height += PointerLength;
                var free = availableSpace.Top - stat.BorderSize.Height;
                if (free >= 0)
                {
                    stat.FreeSpace.Width = free;
                    stats.Add(stat);
                }
            }
            return stats;
        }

        List<DirectionStats> GetMeasuredStatsForDirection(PointerDirection pointerDirection, DirectionStats cleanStat, Thickness availableSpace, Size windowSpace)
        {
            var stats = new List<DirectionStats>();
            if (pointerDirection.LeftAllowed())
            {
                if (availableSpace.Right > 0)
                {
                    var size = new Size(availableSpace.Right, windowSpace.Height);
                    var border = RectangleBorderSize(size, cleanStat.BorderSize);
                    var stat = cleanStat;
                    stat.PointerDirection = PointerDirection.Left;
                    stat.BorderSize = border;
                    var free = availableSpace.Right - border.Width;
                    if (free >= 0)
                    {
                        stat.FreeSpace.Width = free;
                        stats.Add(stat);
                    }
                }
            }
            if (pointerDirection.RightAllowed())
            {
                if (availableSpace.Left > 0)
                {
                    var size = new Size(availableSpace.Left, windowSpace.Height);
                    var border = RectangleBorderSize(size, cleanStat.BorderSize);
                    var stat = cleanStat;
                    stat.PointerDirection = PointerDirection.Right;
                    stat.BorderSize = border;
                    var free = availableSpace.Left - border.Width;
                    if (free >= 0)
                    {
                        stat.FreeSpace.Width = free;
                        stats.Add(stat);
                    }
                }
            }
            if (pointerDirection.UpAllowed())
            {
                if (availableSpace.Bottom > 0)
                {
                    var size = new Size(windowSpace.Width, availableSpace.Bottom);
                    var border = RectangleBorderSize(size, cleanStat.BorderSize);
                    var stat = cleanStat;
                    stat.PointerDirection = PointerDirection.Up;
                    stat.BorderSize = border;
                    var free = availableSpace.Bottom - border.Height;
                    if (free >= 0)
                    {
                        stat.FreeSpace.Width = free;
                        stats.Add(stat);
                    }
                }
            }
            if (pointerDirection.DownAllowed())
            {
                if (availableSpace.Top > 0)
                {
                    var size = new Size(windowSpace.Width, availableSpace.Top);
                    var border = RectangleBorderSize(size, cleanStat.BorderSize);
                    var stat = cleanStat;
                    stat.PointerDirection = PointerDirection.Down;
                    stat.BorderSize = border;
                    var free = availableSpace.Top - border.Height;
                    if (free >= 0)
                    {
                        stat.FreeSpace.Width = free;
                        stats.Add(stat);
                    }
                }
            }
            return stats;
        }


        Size RectangleBorderSize(Size available, Size failSize = default)
        {
            if (IsEmpty)
                return new Size(50 + Padding.Horizontal(), 50 + Padding.Vertical());
            var availableWidth = available.Width;
            var availableHeight = available.Height;
            var hasBorder = (BorderThickness.Average() > 0) && BorderBrush is SolidColorBrush brush && brush.Color.A > 0;
            var border = BorderThickness.Average() * (hasBorder ? 1 : 0) * 2;
            availableWidth -= Padding.Horizontal() - border;
            availableHeight -= Padding.Vertical() - border;
            System.Diagnostics.Debug.WriteLine(GetType() + ".RectangleBorderSize availableWidth:["+availableWidth+"] availableHeight:["+availableHeight+"]");
            if (availableWidth > 0 && availableHeight > 0)
            {
                _contentPresenter.Measure(new Size(availableWidth, availableHeight));
                var result = _contentPresenter.DesiredSize;
                System.Diagnostics.Debug.WriteLine("\t  _contentPresenter.DesiredSize:[" + _contentPresenter.DesiredSize + "]");
                result.Width += Padding.Horizontal();
                result.Height += Padding.Vertical();
                return result;
            }
            return failSize;
        }

#if __WASM__ || NETSTANDARD
        Size _firstRenderSize;
        void GetFirstRenderSize()
        {
            if (_firstRenderSize.Width < 1 || _firstRenderSize.Height < 1)
            {
                double height = AppWindow.Size().Height;
                double width = AppWindow.Size().Width;
                if (Windows.UI.Xaml.Window.Current.Content is Frame frame)
                {
                    if (frame.Content is Page currentPage)
                    {
                        if (currentPage.Content is Grid contentGrid)
                        {
                            if (contentGrid.RowDefinitions.Count > 0 && contentGrid.RowDefinitions[0] is RowDefinition rowDefinition)
                                height = rowDefinition.ActualHeight;

                            if (contentGrid.ColumnDefinitions.Count > 0 && contentGrid.ColumnDefinitions[0] is ColumnDefinition colDef)
                                width = colDef.ActualWidth;
                            System.Diagnostics.Debug.WriteLine(GetType() + ".PushAsync w:" + width + "   h:" + height);
                        }
                        else
                            throw new Exception("Page content needs to be a Grid in order for Popup to work in WASM");
                    }
                }
                _firstRenderSize = new Size(width, height);
            }

        }
#endif

        #endregion

    }
}
