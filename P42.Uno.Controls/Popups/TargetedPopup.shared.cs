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
        static int _pushingCount = 0;
        public static bool IsPushing
        {
            get => _pushingCount > 0;
            private set
            {
                _pushingCount += value ? 1 : -1;
            }

        }

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

        //public event EventHandler<DismissPointerPressedEventArgs> DismissPointerPressed;
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

#if __ANDROID__
        protected override void OnNativeUnloaded()
        {
            System.Diagnostics.Debug.WriteLine("TargetedPopup.OnNativeUnload ENTER");
            base.OnNativeUnloaded();
            P42.Utils.Uno.GC.Collect();
            System.Diagnostics.Debug.WriteLine("TargetedPopup.OnNativeUnload EXIT");
        }
#endif

        #endregion


        #region Event Handlers
        /*
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
        */

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
        private void OnPopupClosed(object sender, object e)
        {
            if (PushPopState == PushPopState.Pushed || PushPopState == PushPopState.Popping)
            {
                CompletePop(PopupPoppedCause.BackgroundTouch, null);
            }
        }

        TaskCompletionSource<bool> _popupOpenedCompletionSource;
        private void OnPopupOpened(object sender, object e)
        {
            _popupOpenedCompletionSource?.TrySetResult(true);
        }

        public virtual async Task PushAsync()
        {
            if (PushPopState == PushPopState.Pushed || PushPopState == PushPopState.Pushing)
                return;

            if (PushPopState == PushPopState.Popping)
            {
                if (_popCompletionSource is null)
                {
                    await WaitForPoppedAsync();
                }
                else
                    return;
            }

            await InnerPushAsyc();
        }

        async Task InnerPushAsyc()
        { 
            PushPopState = PushPopState.Pushing;
            _popCompletionSource = null;

            PoppedCause = PopupPoppedCause.BackgroundTouch;
            PoppedTrigger = null;

            await OnPushBeginAsync();

            // requiired to render popup the first time.
            _border.Opacity = 0.0;
            _popupOpenedCompletionSource = new TaskCompletionSource<bool>();

            System.Diagnostics.Debug.WriteLine("TargetedPopup.InnerPushAsyn t0: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);
            // WHAT IF WE PUT _popup.IsOpen AFTER UpdateMarginAndAlignment?
            _popup.IsOpen = true;
            await Task.Delay(5);
            System.Diagnostics.Debug.WriteLine("TargetedPopup.InnerPushAsyn t1: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);
            UpdateMarginAndAlignment();
            System.Diagnostics.Debug.WriteLine("TargetedPopup.InnerPushAsyn t2: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);

            // IS THIS NECESSARY?!?!
            _popup.InvalidateMeasure();
            System.Diagnostics.Debug.WriteLine("TargetedPopup.InnerPushAsyn t3: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);


            if (IsAnimated)
            {
                Action<double> action = percent => _border.Opacity = Opacity * percent;
                var animator = new P42.Utils.Uno.ActionAnimator(0.11, 0.95, TimeSpan.FromMilliseconds(300), action);
                await animator.RunAsync();
            }
            System.Diagnostics.Debug.WriteLine("TargetedPopup.InnerPushAsyn t4: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);

            _border.Bind(BubbleBorder.OpacityProperty, this, nameof(Opacity));

            if (PopAfter > default(TimeSpan))
            {
                P42.Utils.Timer.StartTimer(PopAfter, async () =>
                {
                    await PopAsync(PopupPoppedCause.Timeout, "Timeout");
                    return false;
                });
            }

            System.Diagnostics.Debug.WriteLine("TargetedPopup.InnerPushAsyn t5: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);
            await OnPushEndAsync();
            System.Diagnostics.Debug.WriteLine("TargetedPopup.InnerPushAsyn t6: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);

            PushPopState = PushPopState.Pushed;
            Pushed?.Invoke(this, EventArgs.Empty);
            _pushCompletionSource?.TrySetResult(true);
            System.Diagnostics.Debug.WriteLine("TargetedPopup.InnerPushAsyn t7: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);

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

            if (IsAnimated)
            {
                Action<double> action = percent => _border.Opacity = Opacity * percent;
                var animator = new P42.Utils.Uno.ActionAnimator(0.95, 0.11, TimeSpan.FromMilliseconds(300), action);
                await animator.RunAsync();
            }

            _popup.IsOpen = false;

            CompletePop(PoppedCause, PoppedTrigger);
        }

        void CompletePop(PopupPoppedCause poppedCause, object poppedTrigger)
        {
            var result = new PopupPoppedEventArgs(PoppedCause, PoppedTrigger);
            PushPopState = PushPopState.Popped;
            _border.Bind(BubbleBorder.OpacityProperty, this, nameof(Opacity));
            _popCompletionSource?.TrySetResult(result);
            Popped?.Invoke(this, result);
            P42.Utils.Uno.GC.Collect();
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
            await Task.CompletedTask;
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

            var windowSize = AppWindow.Size(this);
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

            var target = TargetBounds();

            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateBorderMarginAndAlignment targetBounds:["+targetBounds+"]");
            var availableSpace = AvailableSpace(target);
            var stats = BestFit(availableSpace, cleanSize);

            if (stats.PointerDirection == PointerDirection.None)
            {
                CleanMarginAndAlignment(HorizontalAlignment.Center, VerticalAlignment.Center, windowSize, cleanSize);
                return;
            }

            ActualPointerDirection = stats.PointerDirection;
            var margin = Margin;
            var hzAlign = HorizontalAlignment;
            var vtAlign = VerticalAlignment;

            if (stats.PointerDirection.IsHorizontal())
            {
                if (stats.PointerDirection == PointerDirection.Left)
                {
                    margin.Left = target.Right;
                    if (HorizontalAlignment != HorizontalAlignment.Stretch)
                        hzAlign = HorizontalAlignment.Left;
                }
                else if (stats.PointerDirection == PointerDirection.Right)
                {
                    margin.Right = windowSize.Width - target.Left;
                    if (HorizontalAlignment != HorizontalAlignment.Stretch)
                        hzAlign = HorizontalAlignment.Right;
                }

                if (VerticalAlignment == VerticalAlignment.Top)
                {
                    margin.Top = Math.Max(Margin.Top, target.Top);
                }
                else if (VerticalAlignment == VerticalAlignment.Center)
                {
                    margin.Top = Math.Max(Margin.Top, (target.Top + target.Bottom) / 2.0 - stats.BorderSize.Height / 2.0);
                    vtAlign = VerticalAlignment.Top;
                }
                else if (VerticalAlignment == VerticalAlignment.Bottom)
                {
                    margin.Bottom = Math.Max(Margin.Bottom, windowSize.Height - target.Bottom);
                }

                if (margin.Top + stats.BorderSize.Height > windowSize.Height - Margin.Bottom)
                    margin.Top = windowSize.Height - Margin.Bottom - stats.BorderSize.Height;

                if (VerticalAlignment == VerticalAlignment.Bottom)
                    _border.PointerAxialPosition = (target.Top - (windowSize.Height - margin.Bottom - cleanSize.Height)) + target.Bottom - (target.Top + target.Bottom) / 2.0;
                else
                    _border.PointerAxialPosition = (target.Top - margin.Top) + target.Bottom - (target.Top + target.Bottom) / 2.0;
            }
            else
            {
                if (stats.PointerDirection == PointerDirection.Up)
                {
                    margin.Top = target.Bottom;
                    if (VerticalAlignment != VerticalAlignment.Stretch)
                        vtAlign = VerticalAlignment.Top;
                }
                else if (stats.PointerDirection == PointerDirection.Down)
                {
                    margin.Bottom = windowSize.Height - target.Top;
                    if (VerticalAlignment != VerticalAlignment.Stretch)
                        vtAlign = VerticalAlignment.Bottom;
                }

                if (HorizontalAlignment == HorizontalAlignment.Left)
                    margin.Left = Math.Max(Margin.Left, target.Left);
                else if (HorizontalAlignment == HorizontalAlignment.Center)
                    margin.Left = Math.Max(Margin.Left, (target.Left + target.Right) / 2.0 - stats.BorderSize.Width / 2.0);
                else if (HorizontalAlignment == HorizontalAlignment.Right)
                    margin.Right = Math.Max(Margin.Right, windowSize.Width - target.Right);

                if (margin.Left + stats.BorderSize.Width > windowSize.Width - Margin.Right)
                    margin.Left = windowSize.Width - Margin.Right - stats.BorderSize.Width;

                if (HorizontalAlignment == HorizontalAlignment.Right)
                    _border.PointerAxialPosition = (target.Left - (windowSize.Width - margin.Right - cleanSize.Width)) + (target.Right - (target.Left + target.Right) / 2.0);
                else
                    _border.PointerAxialPosition = (target.Left - margin.Left) + (target.Right - (target.Left + target.Right) / 2.0);
            }

            ActualPointerDirection = _border.PointerDirection = stats.PointerDirection;
            SetMarginAndAlignment(margin, hzAlign, vtAlign, windowSize, cleanSize);
        }

        void CleanMarginAndAlignment(HorizontalAlignment hzAlign, VerticalAlignment vtAlign, Size windowSize, Size cleanSize)
        {
            ActualPointerDirection = PointerDirection.None;

            if (_border is null)
                return;

            _border.PointerDirection = ActualPointerDirection;
            SetMarginAndAlignment(Margin, hzAlign, vtAlign, windowSize, cleanSize);
        }

        void SetMarginAndAlignment(Thickness margin, HorizontalAlignment hzAlign, VerticalAlignment vtAlign, Size windowSize, Size cleanSize)
        { 
            var frame = CalculateFrame(margin, hzAlign, vtAlign, windowSize, cleanSize);

            _popup.Margin = new Thickness(0);
            _popup.HorizontalOffset = frame.Left;
            _popup.VerticalOffset = frame.Top;
#if !__ANDROID__
            _popup.VerticalOffset += P42.Utils.Uno.AppWindow.StatusBarHeight(this);
#endif

            _border.Margin = new Thickness(0);
            _border.Width = frame.Width;
            _border.Height = frame.Height;

            _border.HorizontalAlignment = hzAlign == HorizontalAlignment.Stretch && !this.HasPrescribedWidth()
                ? HorizontalAlignment.Stretch
                : HorizontalAlignment.Left;
            _border.VerticalAlignment = vtAlign == VerticalAlignment.Stretch && !this.HasPrescribedHeight()
                ? VerticalAlignment.Stretch
                : VerticalAlignment.Top;

            System.Diagnostics.Debug.WriteLine("TargetedPopup.CleanMarginAndAlignment frame: " + frame);
        }

        Rect CalculateFrame(Thickness margin, HorizontalAlignment hzAlign, VerticalAlignment vtAlign, Size windowSize, Size borderSize)
        {
            var hzPointer = ActualPointerDirection.IsHorizontal() ? PointerLength : 0;
            var vtPointer = ActualPointerDirection.IsVertical() ? PointerLength : 0;
            var left = margin.Left;
            var top = margin.Top;
            if (this.HasPrescribedWidth())
                borderSize.Width = Width;
            if (this.HasPrescribedHeight())
                borderSize.Height = Height;
            var right = Math.Min(left + borderSize.Width + hzPointer, windowSize.Width - margin.Right);
            var bottom = Math.Min(top + borderSize.Height + vtPointer, windowSize.Height - margin.Bottom);

            if (hzAlign == HorizontalAlignment.Center)
            {
                left = Math.Max((windowSize.Width - borderSize.Width) / 2.0, left);
                right = Math.Min(left + borderSize.Width, windowSize.Width - margin.Right);
            }
            else if (hzAlign == HorizontalAlignment.Right)
            {
                left = Math.Max(windowSize.Width - margin.Right - hzPointer - borderSize.Width, left);
                right = windowSize.Width - margin.Right;
            }
            else if (hzAlign == HorizontalAlignment.Stretch && !this.HasPrescribedWidth())
            {
                right = windowSize.Width - margin.Right;
            }

            if (vtAlign == VerticalAlignment.Center)
            {
                top = Math.Max((windowSize.Height - borderSize.Height) / 2.0, top);
                bottom = Math.Min(top + borderSize.Height, windowSize.Height - margin.Bottom);
            }
            else if (vtAlign == VerticalAlignment.Bottom)
            {
                top = Math.Max(windowSize.Height - margin.Bottom - vtPointer - borderSize.Height, top);
                bottom = windowSize.Height - margin.Bottom;
            }
            else if (vtAlign == VerticalAlignment.Stretch && !this.HasPrescribedHeight())
            {
                bottom = windowSize.Height - margin.Bottom;
            }

            return new Rect(left, top, right - left, bottom - top);
        }

        DirectionStats BestFit(Thickness availableSpace, Size cleanSize)
        {
            // given the amount of free space, determine if the border will fit 
            var windowSize = AppWindow.Size(this);
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
            if (GetBestDirectionStat(GetRectangleBorderStatsForDirection(PreferredPointerDirection, cleanStat, availableSpace)) is DirectionStats stats0)
                return stats0;
            #endregion

            #region Check if border + content could fit in any of the preferred pointer quadrants
            // at this point in time valid and invalid fits are in the stats list
            if (GetBestDirectionStat(GetMeasuredStatsForDirection(PreferredPointerDirection, cleanStat, availableSpace, windowSpace)) is DirectionStats stats1)
                return stats1;
            #endregion

            // the stats list only contains invalid fallback fits ... but perhaps not all fallback fits have yet been tried
            var uncheckedFallbackPointerDirection = (FallbackPointerDirection ^ PreferredPointerDirection) | FallbackPointerDirection;

            #region Check if clean border fits in unchecked fallback pointer quadrants
            if (GetBestDirectionStat(GetRectangleBorderStatsForDirection(uncheckedFallbackPointerDirection, cleanStat, availableSpace)) is DirectionStats stats2)
                return stats2;
            #endregion

            #region Check if border + content could fit in any of the unchecked fallback pointer quadrants
            if (GetBestDirectionStat(GetMeasuredStatsForDirection(uncheckedFallbackPointerDirection, cleanStat, availableSpace, windowSpace)) is DirectionStats stats3)
                return stats3;
            #endregion

            return cleanStat;
        }

        DirectionStats? GetBestDirectionStat(List<DirectionStats> stats)
        {
            if (stats.Count == 1)
                return stats[0];
            if (stats.Count > 1)
            {
                var max = stats[0];
                foreach (var s in stats)
                {
                    if (s.MinAvail == max.MinAvail && s.MaxAvail > max.MaxAvail)
                        max = s;
                    else if (s.MinAvail > max.MinAvail)
                        max = s;
                }
                return max;
            }
            return null;
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
            var windowBounds = AppWindow.Size(this);
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
                    stat.FreeSpace.Height = free;
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
                    stat.FreeSpace.Height = free;
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
                        stat.FreeSpace.Height = free;
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
                        stat.FreeSpace.Height = free;
                        stats.Add(stat);
                    }
                }
            }

            return stats;
        }

        Size _lastSizeAvailable = Size.Empty;
        Size _lastResultSize = Size.Empty;
        bool _lastWasFixedWidth;
        Size MeasureBorder(Size available, Size failSize = default)
        {
            System.Diagnostics.Debug.WriteLine("\n");
            System.Diagnostics.Debug.WriteLine($"TargetedPopup.MeasureBorder({available})");

            var width = available.Width;
            var height = available.Height;
            if (this.HasPrescribedWidth())
                width = Math.Min(Width, width);
            if (this.HasPrescribedHeight())
                height = Math.Min(Height, height);

            if (this.HasPrescribedWidth() && this.HasPrescribedHeight())
            {
                _lastSizeAvailable = Size.Empty;
                _lastResultSize = Size.Empty;
                _lastWasFixedWidth = false;
                return new Size(width, height);
            }

            if (_lastWasFixedWidth && this.HasPrescribedWidth() &&
                _lastSizeAvailable.Width == width)
            {
                if (VerticalAlignment == VerticalAlignment.Stretch || _lastResultSize.Height > height)
                    return new Size(_lastSizeAvailable.Width, height);
                return _lastResultSize;
            }


            if (IsEmpty)
                return new Size(
                    this.HasPrescribedWidth()
                        ? width : 50 + Padding.Horizontal(),
                    this.HasPrescribedHeight()
                        ? height : 50 + Padding.Vertical()
                    );

            if (HorizontalAlignment == HorizontalAlignment.Stretch && VerticalAlignment == VerticalAlignment.Stretch)
            {
                _lastSizeAvailable = Size.Empty;
                _lastResultSize = Size.Empty;
                _lastWasFixedWidth = false;
                return new Size(width, height);
            }

            var hasBorder = (BorderThickness.Average() > 0) && BorderBrush is SolidColorBrush brush && brush.Color.A > 0;
            var border = BorderThickness.Average() * (hasBorder ? 1 : 0) * 2;
            var availableWidth = width - Padding.Horizontal() - border -1;
            var availableHeight = height - Padding.Vertical() - border -1;
            System.Diagnostics.Debug.WriteLine($"TargetedPopup.MeasureBorder border:[{border}] Padding:[{Padding}]  availableWidth:[" + availableWidth+"] availableHeight:["+availableHeight+"]");
            if (availableWidth > 0 && availableHeight > 0 && _contentPresenter.Content != null)
            {
                _contentPresenter.Measure(new Size(availableWidth, availableHeight));
                var result = _contentPresenter.DesiredSize;
                System.Diagnostics.Debug.WriteLine("TargetedPopup.MeasureBorder  _contentPresenter.DesiredSize:[" + _contentPresenter.DesiredSize + "]");
                result.Width += Padding.Horizontal() + border;
                result.Height += Padding.Vertical() + border;



                var resultSize = new Size(
                    this.HasPrescribedWidth()
                        ? width : result.Width,
                    this.HasPrescribedHeight()
                        ? height : result.Height
                    );

                System.Diagnostics.Debug.WriteLine($"TargetedPopup.MeasureBorder resultSize: {resultSize}");

                _lastSizeAvailable = available;
                _lastResultSize = resultSize;
                _lastWasFixedWidth = this.HasPrescribedWidth();

                return resultSize;
            }

            _lastSizeAvailable = Size.Empty;
            _lastResultSize = Size.Empty;
            _lastWasFixedWidth = false;
            return failSize;
        }

        #endregion

    }
}
