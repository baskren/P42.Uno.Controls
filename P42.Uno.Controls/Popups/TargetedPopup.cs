using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Uno.Extensions;
using Uno.Extensions.ValueType;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;
#if NETFX_CORE
using Popup = Windows.UI.Xaml.Controls.Primitives.Popup;
#else
using Popup = Windows.UI.Xaml.Controls.Popup;
#endif


namespace P42.Uno.Controls
{
    [Windows.UI.Xaml.Data.Bindable]
    [System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    [ContentProperty(Name = nameof(PopupContent))]
    public partial class TargetedPopup : UserControl, ITargetedPopup
    {
        #region Properties

        #region PopupContent Property
        public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
            nameof(PopupContent),
            typeof(object),
            typeof(TargetedPopup),
            new PropertyMetadata(null, OnPopupContentChanged)
        );

        private static void OnPopupContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is TargetedPopup popup)
                popup._contentPresenter.Content = args.NewValue;
        }

        public object PopupContent
        {
            get => GetValue(PopupContentProperty);
            set => SetValue(PopupContentProperty, value);
        }
        #endregion 

        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(default(bool), OnHasShadowChanged)
        );

        private static void OnHasShadowChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is TargetedPopup popup)
                popup._border.HasShadow = (bool)args.NewValue;
        }

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
            if (_border != null)
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
            if (_border != null)
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
            if (_border != null)
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
            if (_border != null && ActualPointerDirection == PointerDirection.None && PreferredPointerDirection != PointerDirection.None)
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
            if (_border != null)
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
            new PropertyMetadata(3.0)
        );
        public double PointerMargin
        {
            get => (double)GetValue(PointerMarginProperty);
            set => SetValue(PointerMarginProperty, value);
        }
        #endregion PointerMargin Property

        #endregion

        #region DismissOnPointerMove Property
        public static readonly DependencyProperty DismissOnPointerMoveProperty = DependencyProperty.Register(
            nameof(DismissOnPointerMove),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(default(bool))
        );
        /// <summary>
        /// Causes the popup to be dismissed (popped) when the Pointer (mouse) moves outside of the Target
        /// </summary>
        public bool DismissOnPointerMove
        {
            get => (bool)GetValue(DismissOnPointerMoveProperty);
            set => SetValue(DismissOnPointerMoveProperty, value);
        }
        #endregion DismissOnPointerMove Property


        #region LightDismiss Properties

        #region IsLightDismissEnabled Property
        public static readonly DependencyProperty IsLightDismissEnabledProperty = DependencyProperty.Register(
            nameof(IsLightDismissEnabled),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(true, new PropertyChangedCallback(OnIsLightDismissOverlayEnabledChanged))
        );
        private static void OnIsLightDismissOverlayEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TargetedPopup popup && popup._popup != null)
            {
                popup._popup.IsLightDismissEnabled = popup.IsLightDismissEnabled;
            }
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
            new PropertyMetadata(LightDismissOverlayMode.On, new PropertyChangedCallback(OnLightDismissOverlayModeChanged))
        );
        private static void OnLightDismissOverlayModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TargetedPopup popup && popup._popup != null)
            {
                popup._popup.LightDismissOverlayMode = popup.LightDismissOverlayMode;
            }
        }
        public LightDismissOverlayMode LightDismissOverlayMode
        {
            get => (LightDismissOverlayMode)GetValue(LightDismissOverlayModeProperty);
            set => SetValue(LightDismissOverlayModeProperty, value);
        }
        #endregion LightDismissOverlayMode Property

        #region AnimationDuration Property
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
            nameof(AnimationDuration),
            typeof(int),
            typeof(TargetedPopup),
            new PropertyMetadata(200)
        );
        public int AnimationDuration
        {
            get => (int)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }
        #endregion AnimationDuration Property

        #endregion

        public PopupPoppedCause PoppedCause { get; private set; }

        public object PoppedTrigger { get; private set; }

        #region PushPopState Property
        public static readonly DependencyProperty PushPopStateProperty = DependencyProperty.Register(
            nameof(PushPopState),
            typeof(PushPopState),
            typeof(TargetedPopup),
            new PropertyMetadata(default(PushPopState), new PropertyChangedCallback((d,e)=>((TargetedPopup)d).OnPushPopStateChanged(e)))
        );

        protected virtual void OnPushPopStateChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }

        public PushPopState PushPopState
        {
            get => (PushPopState)GetValue(PushPopStateProperty);
            internal set => SetValue(PushPopStateProperty, value);
        }
        #endregion PushPopState Property


        public bool IsEmpty
        {
            get
            {
                var contentPresenter = _contentPresenter;
                while (contentPresenter?.Content is ContentPresenter cp)
                    contentPresenter = cp;
                //System.Diagnostics.Debug.WriteLine("TargetedPopup.IsEmpty: " + (contentPresenter.Content is null ? "TRUE" : "FALSE") );
                return contentPresenter?.Content is null;
            }
        }

        #region IsAnimated Property
        public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register(
            nameof(IsAnimated),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(true)
        );
        public bool IsAnimated
        {
            get => (bool)GetValue(IsAnimatedProperty);
            set => SetValue(IsAnimatedProperty, value);
        }
        #endregion IsAnimated Property

        #endregion


        #region Events
        public event EventHandler Pushed;
        /// <summary>
        /// Occurs when popup has been cancelled.
        /// </summary>
        public event EventHandler<PopupPoppedEventArgs> Popped;

        public event EventHandler<DismissPointerPressedEventArgs> DismissPointerPressed;
        #endregion


        #region Construction / Initialization
        public static async Task<TargetedPopup> CreateAsync(UIElement target, UIElement bubbleContent)
        {
            var result = new TargetedPopup
            {
                Target = target,
                PopupContent = bubbleContent
            };
            await result.PushAsync();
            return result;
        }

        public TargetedPopup()
        {
            Build();
        }

        #endregion


        #region Event Handlers
        async void OnDismissPointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (IsLightDismissEnabled)
            {
                var dismissEventArgs = new DismissPointerPressedEventArgs();
                DismissPointerPressed?.Invoke(this, dismissEventArgs);
                if (!dismissEventArgs.CancelDismiss)
                    await PopAsync(PopupPoppedCause.BackgroundTouch);
            }
        }

        Point _enteredPoint = new Point(-1,-1);
        private void OnPointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _enteredPoint = e.GetCurrentPoint(Windows.UI.Xaml.Window.Current.Content).Position;
            //System.Diagnostics.Debug.WriteLine("TargetedPopup.OnPointerEntered e: [" + _enteredPoint.X + ", " + _enteredPoint.Y + "]");
        }

        async void OnPointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (DismissOnPointerMove)
            {
                var position = e.GetCurrentPoint(Windows.UI.Xaml.Window.Current.Content).Position;
                System.Diagnostics.Debug.WriteLine("TargetedPopup.OnPointerMoved e: [" + position.X + ", " + position.Y + "]");

                if (Target != null)
                {

                    var zone = new Rect(position.X - 5, position.Y - 5, 10, 10);

                    var targetBounts = Target.GetBounds();
                    var borderBounds = _border.GetBounds();

                    if (Windows.UI.Xaml.RectHelper.Intersect(targetBounts, zone) is Rect intersect0
                        && intersect0.Width > 0
                        && intersect0.Height > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("\t\t RectHelper.Intersect(targetBounts, zone) = " + RectHelper.Intersect(targetBounts, zone));
                        System.Diagnostics.Debug.WriteLine("\t\t in Target ["+ targetBounts + "]");
                        return;
                    }
                    else if (Windows.UI.Xaml.RectHelper.Intersect(borderBounds, zone) is Rect intersect1
                        && intersect1.Width > 0
                        && intersect1.Height > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("\t\t RectHelper.Intersect(borderBounds, zone) = " + RectHelper.Intersect(borderBounds, zone));
                        System.Diagnostics.Debug.WriteLine("\t\t in Border");
                        return;
                    }
                    else if (Windows.UI.Xaml.RectHelper.Intersect(borderBounds, targetBounts) is Rect intersect2
                        && intersect2.Width <= 0
                        && intersect2.Height <= 0)
                    {
                        System.Diagnostics.Debug.WriteLine("\t\t RectHelper.Intersect(borderBounds, targetBounts) = " + RectHelper.Intersect(borderBounds, targetBounts));
                        System.Diagnostics.Debug.WriteLine("\t\t testing Bridge");

                        var bridge = new Rect();
                        if (targetBounts.Left > borderBounds.Right)
                        {
                            bridge.X = borderBounds.Right;
                            bridge.Width = targetBounts.Left - borderBounds.Right;
                        }
                        else if (targetBounts.Right < borderBounds.Left)
                        {
                            bridge.X = targetBounts.Right;
                            bridge.Width = borderBounds.Left - targetBounts.Right;
                        }
                        else
                        {
                            bridge.X = Math.Max(targetBounts.X, borderBounds.X);
                            bridge.Width = Math.Min(targetBounts.Width, borderBounds.Width);
                        }
                        if (targetBounts.Top > borderBounds.Bottom)
                        {
                            bridge.Y = borderBounds.Bottom;
                            bridge.Height = targetBounts.Top - borderBounds.Bottom;
                        }
                        else if (targetBounts.Bottom < borderBounds.Top)
                        {
                            bridge.Y = targetBounts.Bottom;
                            bridge.Height = borderBounds.Top - targetBounts.Bottom;
                        }
                        else
                        {
                            bridge.Y = Math.Max(targetBounts.Y, borderBounds.Y);
                            bridge.Width = Math.Min(targetBounts.Height, borderBounds.Height);
                        }

                        bridge.X -= 5;
                        bridge.Y -= 5;
                        bridge.Width += 10;
                        bridge.Height += 10;

                        if (Windows.UI.Xaml.RectHelper.Intersect(bridge, zone) is Rect intersect3
                            && intersect3.Width > 0
                            && intersect3.Height > 0)
                        {
                            System.Diagnostics.Debug.WriteLine("\t\t in Bridge");
                            return;
                        }
                        
                    }
                    System.Diagnostics.Debug.WriteLine("\t\t Popping");
                    await PopAsync(PopupPoppedCause.PointerMoved);

                }
                else
                {
                    if (_enteredPoint.X == -1 && _enteredPoint.Y == -1)
                        _enteredPoint = e.GetCurrentPoint(Windows.UI.Xaml.Window.Current.Content).Position;

                    var dx = position.X - _enteredPoint.X;
                    var dy = position.Y - _enteredPoint.Y;
                    var dist = Math.Sqrt(dx * dx + dy * dy);
                    if (dist > 10)
                        await PopAsync(PopupPoppedCause.PointerMoved);
                }
            }
        }
        #endregion


        #region Push / Pop
        TaskCompletionSource<bool> _popupClosedCompletionSource;
        private void OnPopupClosed(object sender, object e)
        {
            _popupClosedCompletionSource?.SetResult(true);
        }

        TaskCompletionSource<bool> _popupOpenedCompletionSource;
        private void OnPopupOpened(object sender, object e)
        {
            _popupOpenedCompletionSource?.SetResult(true);
        }


        bool _firstPush = true;
        public virtual async Task PushAsync()
        {
            if (PushPopState == PushPopState.Pushed || PushPopState == PushPopState.Pushing)
                return;

            if (PushPopState == PushPopState.Popping)
            {
                if (_popCompletionSource is null)
                {
                    await WaitForPoppedAsync();
                    _popCompletionSource = null;
                }
                else
                    return;
            }

            //AssureGraft();

            PushPopState = PushPopState.Pushing;
            _popCompletionSource = null;

            PoppedCause = PopupPoppedCause.BackgroundTouch;
            PoppedTrigger = null;

            await OnPushBeginAsync();

            Opacity = 0.0;
            if (_firstPush)
            {
                // requiired to render popup the first time.
                _border.Opacity = 0.0;
                _popup.IsOpen = true;
                await Task.Delay(50);
                _popup.IsOpen = false;
                _firstPush = false;
            }
            _border.Opacity = 1.0;
            UpdateMarginAndAlignment();
            _popupOpenedCompletionSource = new TaskCompletionSource<bool>();
            _popup.IsOpen = true;


#if NETFX_CORE
            if (IsAnimated)
            {
                var storyboard = new Storyboard();
                var opacityAnimation = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(AnimationDuration),
                    EnableDependentAnimation = true,
                    To = 1
                };
                Storyboard.SetTargetProperty(opacityAnimation, nameof(UIElement.Opacity));
                Storyboard.SetTarget(opacityAnimation, this);
                storyboard.Children.Add(opacityAnimation);
                await storyboard.BeginAsync();
            }
#else
            if (IsAnimated)
            {
                await AnimateAppearing();
            }
#endif

            Opacity = 1.0;
            if (PopAfter > default(TimeSpan))
            {
                P42.Utils.Timer.StartTimer(PopAfter, async () =>
                {
                    await PopAsync(PopupPoppedCause.Timeout, "Timeout");
                    return false;
                });
            }

            await OnPushEndAsync();

            PushPopState = PushPopState.Pushed;
            Pushed?.Invoke(this, EventArgs.Empty);
            _pushCompletionSource?.SetResult(true);
            
        }

        async Task AnimateAppearing()
        {
            var start = DateTime.Now;
            TimeSpan elapsed;
            while ((elapsed = DateTime.Now - start) < TimeSpan.FromMilliseconds(AnimationDuration))
            {
                Opacity = elapsed.TotalMilliseconds / AnimationDuration;
                await Task.Delay(50);
            }
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
            _pushCompletionSource = null;

            PushPopState = PushPopState.Popping;
            _pushCompletionSource = null;

            _border.SizeChanged -= OnBorderSizeChanged;

            PoppedCause = cause;
            PoppedTrigger = trigger;
            await OnPopBeginAsync();

#if NETFX_CORE
            if (IsAnimated)
            {
                var storyboard = new Storyboard();
                var opacityAnimation = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(AnimationDuration),
                    EnableDependentAnimation = true,
                    From = 1.0,
                    To = 0.0
                };
                Storyboard.SetTargetProperty(opacityAnimation, nameof(UIElement.Opacity));
                Storyboard.SetTarget(opacityAnimation, this);
                storyboard.Children.Add(opacityAnimation);
                await storyboard.BeginAsync();
            }
#else
            if (IsAnimated)
            {
                await AnimateDisappearing();
            }
#endif
            Visibility = Visibility.Collapsed;
            if (this.Parent is Grid grid)
                grid.Children.Remove(this);

            _popupClosedCompletionSource = new TaskCompletionSource<bool>();
            _popup.IsOpen = false;
            await OnPopEndAsync();

            var result = new PopupPoppedEventArgs(PoppedCause, PoppedTrigger);
            PushPopState = PushPopState.Popped;
            _popCompletionSource?.SetResult(result);
            Popped?.Invoke(this, result);
        }

        async Task AnimateDisappearing()
        {
            var start = DateTime.Now;
            TimeSpan elapsed;
            while ((elapsed = DateTime.Now - start) < TimeSpan.FromMilliseconds(AnimationDuration))
            {
                Opacity = 1 - (elapsed.TotalMilliseconds / AnimationDuration);
                await Task.Delay(50);
            }
        }


        TaskCompletionSource<PopupPoppedEventArgs> _popCompletionSource;
        public async Task<PopupPoppedEventArgs> WaitForPoppedAsync()
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
        protected virtual async Task OnPushBeginAsync()
        {
            await (_popupOpenedCompletionSource?.Task ?? Task.CompletedTask);
        }

        /// <summary>
        /// Invoked at end of appearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnPushEndAsync()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Invoked at start of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnPopBeginAsync()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Invoked at end of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnPopEndAsync()
        {
            await (_popupClosedCompletionSource?.Task ?? Task.CompletedTask);
        }
        #endregion


        #region Event Handlers
        protected virtual void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
        {
            if (args.NewSize.Width < 1 || args.NewSize.Height < 1)
                return;
            UpdateMarginAndAlignment();
        }
        #endregion


        #region Layout
        /*
        protected override Size MeasureOverride(Size availableSize)
        {
            if (IsEmpty)
                return AppWindow.Size();

            _border.Measure(AppWindow.Size());
            return AppWindow.Size();
        }
        */
        void UpdateMarginAndAlignment()
        {
            if (_border is null)
                return;
            //await AssureGraft();

            _popup.Margin = Margin;

            var windowSize = AppWindow.Size();
            if (windowSize.Width < 1 || windowSize.Height < 1)
                return;
            var windowWidth = windowSize.Width - Margin.Horizontal();
            var windowHeight = windowSize.Height - Margin.Vertical();
            var cleanSize = MeasureBorder(new Size(windowWidth, windowHeight));

            if (PreferredPointerDirection == PointerDirection.None || Target is null)
            {
                //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateMarginAndAlignment PreferredPointerDirection == PointerDirection.None");
                CleanMarginAndAlignment(HorizontalAlignment,VerticalAlignment, windowSize, cleanSize);
                return;
            }

            var targetBounds = TargetBounds();

            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateBorderMarginAndAlignment targetBounds:["+targetBounds+"]");
            var availableSpace = AvailableSpace(targetBounds);
            var stats = BestFit(availableSpace, cleanSize);

            if (stats.PointerDirection == PointerDirection.None)
            {
                CleanMarginAndAlignment(HorizontalAlignment.Center, VerticalAlignment.Center, windowSize, cleanSize);
                return;
            }

            ActualPointerDirection = stats.PointerDirection;
            var borderMargin = Margin;

            if (stats.PointerDirection.IsHorizontal())
            {
                if (stats.PointerDirection == PointerDirection.Left)
                {
                    borderMargin.Left = targetBounds.Right;
                    base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment == HorizontalAlignment.Stretch ? HorizontalAlignment.Stretch : HorizontalAlignment.Left;
                }
                else
                {
                    if (HorizontalAlignment == HorizontalAlignment.Stretch)
                    {
                        base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment;
                    }
                    else
                    {
                        borderMargin.Left = targetBounds.Left - stats.BorderSize.Width;
                        base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment.Left;
                    }
                }

                if (VerticalAlignment == VerticalAlignment.Top)
                {
                    borderMargin.Top = Math.Max(Margin.Top, targetBounds.Top);
                    _border.VerticalAlignment = VerticalAlignment.Top;
                }
                else if (VerticalAlignment == VerticalAlignment.Center)
                {
                    borderMargin.Top = Math.Max(Margin.Top, (targetBounds.Top + targetBounds.Bottom) / 2.0 - stats.BorderSize.Height / 2.0);
                    _border.VerticalAlignment = VerticalAlignment.Top;
                }
                else if (VerticalAlignment == VerticalAlignment.Bottom)
                {
                    borderMargin.Top = Math.Max(Margin.Top, targetBounds.Bottom - stats.BorderSize.Height);
                    _border.VerticalAlignment = VerticalAlignment.Top;
                }
                else
                {
                    _border.VerticalAlignment = VerticalAlignment.Stretch;
                }
                if (borderMargin.Top + stats.BorderSize.Height > windowSize.Height - Margin.Bottom)
                    borderMargin.Top = windowSize.Height - Margin.Bottom - stats.BorderSize.Height;


                _border.PointerAxialPosition = (targetBounds.Top - borderMargin.Top) + targetBounds.Bottom - (targetBounds.Top + targetBounds.Bottom) / 2.0;
            }
            else
            {
                if (stats.PointerDirection == PointerDirection.Up)
                {
                    borderMargin.Top = targetBounds.Bottom;
                    _border.VerticalAlignment = VerticalAlignment == VerticalAlignment.Stretch ? VerticalAlignment.Stretch : VerticalAlignment.Top;
                }
                else
                {
                    if (VerticalAlignment == VerticalAlignment.Stretch)
                    {
                        _border.VerticalAlignment = VerticalAlignment;
                    }
                    else
                    {
                        borderMargin.Top = targetBounds.Top - stats.BorderSize.Height;
                        _border.VerticalAlignment = VerticalAlignment.Top;
                    }
                }

                if (HorizontalAlignment == HorizontalAlignment.Left)
                {
                    borderMargin.Left = Math.Max(Margin.Left, targetBounds.Left);
                    _border.HorizontalAlignment = HorizontalAlignment.Left;

                }
                else if (HorizontalAlignment == HorizontalAlignment.Center)
                {
                    borderMargin.Left = Math.Max(Margin.Left, (targetBounds.Left + targetBounds.Right) / 2.0 - stats.BorderSize.Width / 2.0);
                    _border.HorizontalAlignment = HorizontalAlignment.Left;
                }
                else if (HorizontalAlignment == HorizontalAlignment.Right)
                {
                    borderMargin.Left = Math.Max(Margin.Left, targetBounds.Right - stats.BorderSize.Width);
                    _border.HorizontalAlignment = HorizontalAlignment.Left;
                }
                else
                {
                    _border.HorizontalAlignment = HorizontalAlignment.Stretch;
                }
                if (borderMargin.Left + stats.BorderSize.Width > windowSize.Width - Margin.Right)
                    borderMargin.Left = windowSize.Width - Margin.Right - stats.BorderSize.Width;

                _border.PointerAxialPosition = (targetBounds.Left - borderMargin.Left) + targetBounds.Right - (targetBounds.Left + targetBounds.Right) / 2.0;
            }

            _border.PointerDirection = stats.PointerDirection;
            _popup.Margin = borderMargin;

#if __ANDROID__
            /*
            _grid.Measure(windowSize);
            _grid.Arrange(new Rect(0, 0, windowSize.Width, windowSize.Height));
            */
#endif

        }

        Rect CalculateFrame(HorizontalAlignment hzAlign, VerticalAlignment vtAlign, Size borderSize, Size windowSize)
        {
            var left = Margin.Left;
            var top = Margin.Top;
            var right = Math.Min(left + borderSize.Width, windowSize.Width - Margin.Right);
            var bottom = Math.Min(top + borderSize.Height, windowSize.Height - Margin.Bottom);

            if (hzAlign == HorizontalAlignment.Center)
            {
                left = Math.Max((windowSize.Width - borderSize.Width) / 2.0, left);
                right = Math.Min(left + borderSize.Width, windowSize.Width - Margin.Right);
            }
            else if (hzAlign == HorizontalAlignment.Right)
            {
                left = Math.Max(windowSize.Width - Margin.Right - borderSize.Width, left);
                right = windowSize.Width - Margin.Right;
            }
            else if (hzAlign == HorizontalAlignment.Stretch)
            {
                right = windowSize.Width - Margin.Right;
            }

            if (vtAlign == VerticalAlignment.Center)
            {
                top = Math.Max((windowSize.Height - borderSize.Height) / 2.0, top);
                bottom = Math.Min(top + borderSize.Height, windowSize.Height - Margin.Bottom);
            }
            else if (vtAlign == VerticalAlignment.Bottom)
            {
                top = Math.Max(windowSize.Height - Margin.Bottom - borderSize.Height, top);
                bottom = windowSize.Height - Margin.Bottom;
            }
            else if (vtAlign == VerticalAlignment.Stretch)
            {
                bottom = windowSize.Height - Margin.Bottom;
            }

            return new Rect(left, top, right - left, bottom - top);
        }

        void CleanMarginAndAlignment(HorizontalAlignment hzAlign, VerticalAlignment vtAlign, Size windowSize, Size cleanSize)
        {
            ActualPointerDirection = PointerDirection.None;

            if (_border is null)
                return;

            _border.PointerDirection = ActualPointerDirection;

            var borderSize = cleanSize;
            var frame = CalculateFrame(hzAlign, vtAlign, borderSize, windowSize);

            _popup.Margin = new Thickness(0);
            _popup.HorizontalOffset = frame.Left;
            _popup.VerticalOffset = frame.Top;

            _border.Margin = new Thickness(0);
            _border.Width = frame.Width;
            _border.Height = frame.Height;

            _border.HorizontalAlignment = hzAlign == HorizontalAlignment.Stretch
                ? HorizontalAlignment.Stretch
                : HorizontalAlignment.Left;
            _border.VerticalAlignment = vtAlign == VerticalAlignment.Stretch
                ? VerticalAlignment.Stretch
                : VerticalAlignment.Top;

            System.Diagnostics.Debug.WriteLine("TargetedPopup.CleanMarginAndAlignment frame: " + frame);
#if __ANDROID__
            /*
            _grid.Measure(windowSize);
            _grid.Arrange(new Rect(0, 0, windowSize.Width, windowSize.Height));
            */
#endif
        }

        DirectionStats BestFit(Thickness availableSpace, Size cleanSize)
        {
            // given the amount of free space, determine if the border will fit 
            var windowSize = AppWindow.Size();
            var windowSpaceW = Math.Max(0, windowSize.Width - Margin.Horizontal());
            var windowSpaceH = Math.Max(0, windowSize.Height - Margin.Vertical());
            var windowSpace = new Size(windowSpaceW, windowSpaceH);

            var freeSpaceW = Math.Max(0, windowSpace.Width - cleanSize.Width);
            var freeSpaceH = Math.Max(0, windowSpace.Height - cleanSize.Height);

            var cleanStat = new DirectionStats
            {
                PointerDirection = PointerDirection.None,
                BorderSize = cleanSize,
                FreeSpace = new Size(freeSpaceW, freeSpaceH)
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
            //System.Diagnostics.Debug.WriteLine(GetType() + ".GetRectangleBorderStatsForDirection cleanStat:["+cleanStat+"]");
            var stats = new List<DirectionStats>();
            if (pointerDirection.LeftAllowed() && (availableSpace.Right - cleanStat.BorderSize.Width) >= PointerLength)
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

            if (pointerDirection.RightAllowed() && (availableSpace.Left - cleanStat.BorderSize.Width) >= PointerLength)
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

            if (pointerDirection.UpAllowed() && (availableSpace.Bottom - cleanStat.BorderSize.Height) >= PointerLength)
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

            if (pointerDirection.DownAllowed() && (availableSpace.Top - cleanStat.BorderSize.Height) >= PointerLength)
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
                    var borderSize = MeasureBorder(size, cleanStat.BorderSize);
                    var stat = cleanStat;
                    stat.PointerDirection = PointerDirection.Left;
                    stat.BorderSize = borderSize;
                    var free = availableSpace.Right - borderSize.Width;
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
                    var border = MeasureBorder(size, cleanStat.BorderSize);
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
                    var border = MeasureBorder(size, cleanStat.BorderSize);
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
                    var border = MeasureBorder(size, cleanStat.BorderSize);
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

        Size MeasureBorder(Size available, Size failSize = default)
        {
            if (IsEmpty)
                return new Size(50 + Padding.Horizontal(), 50 + Padding.Vertical());
            var availableWidth = available.Width;
            var availableHeight = available.Height;
            var hasBorder = (BorderThickness.Average() > 0) && BorderBrush is SolidColorBrush brush && brush.Color.A > 0;
            var border = BorderThickness.Average() * (hasBorder ? 1 : 0) * 2;
            availableWidth -= Padding.Horizontal() + border;
            availableHeight -= Padding.Vertical() + border;
            //System.Diagnostics.Debug.WriteLine(GetType() + ".RectangleBorderSize availableWidth:["+availableWidth+"] availableHeight:["+availableHeight+"]");
            if (availableWidth > 0 && availableHeight > 0 && _contentPresenter.Content != null)
            {
                _contentPresenter.Measure(new Size(availableWidth, availableHeight));
                var result = _contentPresenter.DesiredSize;
                //System.Diagnostics.Debug.WriteLine("\t  _contentPresenter.DesiredSize:[" + _contentPresenter.DesiredSize + "]");
                result.Width += Padding.Horizontal() + border;
                result.Height += Padding.Vertical() + border;
                return result;
            }

            return failSize;
        }

        #endregion

    }
}
