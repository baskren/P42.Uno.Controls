using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml.Input;
using Windows.Devices.Sensors;
using P42.Utils;

namespace P42.Uno.Controls
{
    [Microsoft.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    [ContentProperty(Name = "Content")]
    public partial class ContentAndDetailPresenter : Grid
    {

        #region Properties

        #region Content Property
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(Content),
            typeof(FrameworkElement),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(null, new PropertyChangedCallback(OnContentChanged))
        );
        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentAndDetailPresenter view)
            {
                if (e.OldValue is FrameworkElement oldElement)
                    view.Children.Remove(oldElement);
                if (e.NewValue is FrameworkElement newElement)
                    view.Children.Insert(0,newElement);
            }
        }
        public FrameworkElement Content
        {
            get => (FrameworkElement)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
        #endregion Content Property

        #region Footer Property
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(
            nameof(Footer),
            typeof(FrameworkElement),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(null, new PropertyChangedCallback(OnFooterChanged))
        );
        private static void OnFooterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentAndDetailPresenter view)
            {
                if (e.OldValue is FrameworkElement oldElement)
                    view.Children.Remove(oldElement);
                if (e.NewValue is FrameworkElement newElement)
                {
                    Grid.SetRow(newElement, 2);
                    view.Children.Insert(0,newElement);
                }
            }
        }
        public FrameworkElement Footer
        {
            get => (FrameworkElement)GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }
        #endregion Footer Property

        #region Detail Properties

        #region Detail Property
        public static readonly DependencyProperty DetailProperty = DependencyProperty.Register(
            nameof(Detail),
            typeof(FrameworkElement),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(null)
        );
        public FrameworkElement Detail
        {
            get => (FrameworkElement)GetValue(DetailProperty);
            set => SetValue(DetailProperty, value);
        }
        #endregion Detail Property

        #region DetailBackgroundColor Property
        public static readonly DependencyProperty DetailBackgroundColorProperty = DependencyProperty.Register(
            nameof(DetailBackgroundColor),
            typeof(Color),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(SkiaBubble.DefaultFillColor)
        );
        public Color DetailBackgroundColor
        {
            get => (Color)GetValue(DetailBackgroundColorProperty);
            set => SetValue(DetailBackgroundColorProperty, value);
        }
        #endregion DetailBackgroundColor Property

        #region DetailBorderWidth Property
        public static readonly DependencyProperty DetailBorderWidthProperty = DependencyProperty.Register(
            nameof(DetailBorderWidth),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(SkiaBubble.DefaultBorderWidth)
        );
        public double DetailBorderWidth
        {
            get => (double)GetValue(DetailBorderWidthProperty);
            set => SetValue(DetailBorderWidthProperty, value);
        }
        #endregion DetailBorderWidth Property

        #region DetailBorderColor Property
        public static readonly DependencyProperty DetailBorderColorProperty = DependencyProperty.Register(
            nameof(DetailBorderColor),
            typeof(Color),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(SkiaBubble.DefaultBorderColor)
        );
        public Color DetailBorderColor
        {
            get => (Color)GetValue(DetailBorderColorProperty);
            set => SetValue(DetailBorderColorProperty, value);
        }
        #endregion DetailBorderColor Property

        #region DetailCornerRadius Property
        public static readonly DependencyProperty DetailCornerRadiusProperty = DependencyProperty.Register(
            nameof(DetailCornerRadius),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(SkiaBubble.DefaultCornerRadius)
        );
        public double DetailCornerRadius
        {
            get => (double)GetValue(DetailCornerRadiusProperty);
            set => SetValue(DetailCornerRadiusProperty, value);
        }
        #endregion DetailCornerRadius Property

        #endregion

        #region Drawer Properties

        #region DrawerAspectRatio Property
        public static readonly DependencyProperty DrawerAspectRatioProperty = DependencyProperty.Register(
            nameof(DrawerAspectRatio),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(1.0, new PropertyChangedCallback((d, e) => ((ContentAndDetailPresenter)d).OnDetailAspectRatioChanged(e)))
        );
        protected virtual void OnDetailAspectRatioChanged(DependencyPropertyChangedEventArgs e)
        {
            if (DrawerAspectRatio < 2.0 / 3.0 || DrawerAspectRatio > 3.0 / 2.0)
                throw new Exception("Invalid DrawerAspectRatio [" + DrawerAspectRatio + "].  Must be between 2/3 and 3/2");
        }
        public double DrawerAspectRatio
        {
            get => (double)GetValue(DrawerAspectRatioProperty);
            set => SetValue(DrawerAspectRatioProperty, value);
        }
        #endregion DrawerAspectRatio Property

        #endregion

        #region PopupProperties

        #region Target Property
        /// <summary>
        /// The UIElement the popup will point at (no pointer if Target is null or not found)
        /// </summary>
        public UIElement Target
        {
            get
            {
                if (WeakTarget?.TryGetTarget(out var target) ?? false)
                    return target;
                return null;
            }
            set => WeakTarget = new WeakReference<UIElement>(value);
        }
        #endregion Target Property

        #region WeakTarget Property
        public static readonly DependencyProperty WeakTargetProperty = DependencyProperty.Register(
            nameof(WeakTarget),
            typeof(WeakReference<UIElement>),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(default(WeakReference<UIElement>))
        );
        public WeakReference<UIElement> WeakTarget
        {
            get => (WeakReference<UIElement>)GetValue(WeakTargetProperty);
            set => SetValue(WeakTargetProperty, value);
        }
        #endregion WeakTarget Property

        #region PopupMinWidth Property
        public static readonly DependencyProperty PopupMinWidthProperty = DependencyProperty.Register(
            nameof(PopupMinWidth),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(300.0)
        );
        public double PopupMinWidth
        {
            get => (double)GetValue(PopupMinWidthProperty);
            set => SetValue(PopupMinWidthProperty, value);
        }

        #endregion PopupMinWidth Property

        #region PopupMinHeight Property
        public static readonly DependencyProperty PopupMinHeightProperty = DependencyProperty.Register(
            nameof(PopupMinHeight),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(40)
        );
        public double PopupMinHeight
        {
            get => (double)GetValue(PopupMinHeightProperty);
            set => SetValue(PopupMinHeightProperty, value);
        }
        #endregion PopupMinHeight Property

        #region PopupHorizontalAlignment Property
        public static readonly DependencyProperty PopupHorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(PopupHorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(default(HorizontalAlignment))
        );
        public HorizontalAlignment PopupHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(PopupHorizontalAlignmentProperty);
            set => SetValue(PopupHorizontalAlignmentProperty, value);
        }
        #endregion PopupHorizontalAlignment Property

        #region PopupVerticalAlignment Property
        public static readonly DependencyProperty PopupVerticalAlignmentProperty = DependencyProperty.Register(
            nameof(PopupVerticalAlignment),
            typeof(VerticalAlignment),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(default(VerticalAlignment))
        );
        public VerticalAlignment PopupVerticalAlignment
        {
            get => (VerticalAlignment)GetValue(PopupVerticalAlignmentProperty);
            set => SetValue(PopupVerticalAlignmentProperty, value);
        }
        #endregion PopupVerticalAlignment Property

        #endregion

        #region IsInDrawerMode Property
        public static readonly DependencyProperty IsInDrawerModeProperty = DependencyProperty.Register(
            nameof(IsInDrawerMode),
            typeof(bool),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(default(bool))
        );
        public bool IsInDrawerMode
        {
            get => (bool)GetValue(IsInDrawerModeProperty);
            private set => SetValue(IsInDrawerModeProperty, value);
        }
        #endregion IsInDrawerMode Property

        Size ViewEstimatedSize
        {
            get
            {
                Size windowSize = Size.Empty;
                var width = ActualWidth;
                if (width <= 0)
                    width = DesiredSize.Width;
                if (width <= 0)
                {
                    if (windowSize == Size.Empty)
                        windowSize = AppWindow.Size(this);
                    width = windowSize.Width;
                }
                var height = ActualHeight;
                if (height <= 0)
                    height = DesiredSize.Height;
                if (height <= 0)
                {
                    if (windowSize == Size.Empty)
                        windowSize = AppWindow.Size(this);
                    height = windowSize.Height;
                }
                return new Size(width, height);
            }
        }

        public Orientation DrawerOrientation
            => ActualWidth > ActualHeight ? Orientation.Horizontal : Orientation.Vertical;

        public PushPopState DetailPushPopState { get; private set; } = PushPopState.Popped;

        #region PageOverlay Properties

        #region PopOnPageOverlayTouch Property
        public static readonly DependencyProperty PopOnPageOverlayTouchProperty = DependencyProperty.Register(
            nameof(PopOnPageOverlayTouch),
            typeof(bool),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(true)
        );
        public bool PopOnPageOverlayTouch
        {
            get => (bool)GetValue(PopOnPageOverlayTouchProperty);
            set => SetValue(PopOnPageOverlayTouchProperty, value);
        }
        #endregion PopOnPageOverlayTouch Property

        #region IsPageOverlayHitTestVisible Property
        public static readonly DependencyProperty IsPageOverlayHitTestVisibleProperty = DependencyProperty.Register(
            nameof(IsPageOverlayHitTestVisible),
            typeof(bool),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(true)
        );
        public bool IsPageOverlayHitTestVisible
        {
            get => (bool)GetValue(IsPageOverlayHitTestVisibleProperty);
            set => SetValue(IsPageOverlayHitTestVisibleProperty, value);
        }
        #endregion IsPageOverlayHitTestVisible Property

        #region PageOverlayBrush Property
        public static readonly DependencyProperty PageOverlayBrushProperty = DependencyProperty.Register(
            nameof(PageOverlayBrush),
            typeof(Brush),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(Colors.Black.WithAlpha(0.01).ToBrush())
        );
        public Brush PageOverlayBrush
        {
            get => (Brush)GetValue(PageOverlayBrushProperty);
            set => SetValue(PageOverlayBrushProperty, value);
        }
        #endregion PageOverlay Property

        #endregion

        #endregion


        #region Events
        public event EventHandler<DismissPointerPressedEventArgs> DismissPointerPressed;
        #endregion


        #region Construction / Initialization
        public ContentAndDetailPresenter()
        {
            Build();
            SizeChanged += OnSizeChanged;
        }
        #endregion


        #region Layout

        void OnSizeChanged(object sender, SizeChangedEventArgs args)
            => LayoutDetailAndOverlay(args.NewSize, DetailPushPopState == PushPopState.Pushed ? 1 : 0);

        void OpenDrawer(double percentOpen)
        {
            if (DrawerOrientation == Orientation.Horizontal)
            {
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.OpenDrawer : HORIZONTAL");
                _drawerColumnDefinition.Width = new GridLength(percentOpen * DrawerSize.Width);
                Grid.SetRow(_detailDrawer, 0);
                Grid.SetRowSpan(_detailDrawer, 2);
                Grid.SetColumn(_detailDrawer, 1);
                _detailDrawer.BorderThickness = new Thickness(DetailBorderWidth, 0, 0, 0);
                _detailDrawer.CornerRadius = new CornerRadius(DetailCornerRadius, 0, 0, DetailCornerRadius);
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.OpenDrawer : VERTICAL");
                _drawerRowDefinition.Height = new GridLength(percentOpen * DrawerSize.Height);
                Grid.SetRow(_detailDrawer, 2);
                Grid.SetRowSpan(_detailDrawer, 1);
                Grid.SetColumn(_detailDrawer, 0);
                _detailDrawer.BorderThickness = new Thickness(0, DetailBorderWidth, 0, 0);
                _detailDrawer.CornerRadius = new CornerRadius(DetailCornerRadius, DetailCornerRadius, 0, 0);
            }
        }

        static object popupToDrawerResizeTrigger = new();
        void LayoutDetailAndOverlay(Size size, double percentOpen, bool? knownDrawerMode = null)
        {
            if (size.IsZero())
                return;
            if (double.IsNaN(size.Width))
                return;
            if (double.IsNaN(size.Height))
                return;

            var drawerMode = knownDrawerMode ?? LocalUpdateIsInDrawerMode(size);

            if (drawerMode)
            {
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LayoutDetailAndOverlay : DRAWER");
                if (percentOpen > 0 && _targetedPopup.PushPopState == PushPopState.Pushed)
                    _targetedPopup.PopAsync(trigger: popupToDrawerResizeTrigger).Forget();
                _targetedPopup.Content = null;
                _detailDrawer.Child = Detail;
                _detailDrawer.Opacity = percentOpen;
                OpenDrawer(percentOpen);
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LayoutDetailAndOverlay : POPUP");
                _drawerRowDefinition.Height = GridLength.Auto;
                //_targetedPopup.Opacity = percentOpen;
                _detailDrawer.Child = null;
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LayoutDetailAndOverlay _targetedPopup.Size:[{_targetedPopup.Width},{_targetedPopup.Height}]");
                _targetedPopup.Content = Detail;

                if (percentOpen > 0 && _targetedPopup.PushPopState == PushPopState.Popped)
                    _targetedPopup.PushAsync().Forget();
                else if (percentOpen <=0 && _targetedPopup.PushPopState == PushPopState.Pushed)
                    _targetedPopup.PopAsync().Forget();
                
                
                //while (RowDefinitions.Count > 2)
                //    RowDefinitions.RemoveAt(RowDefinitions.Count - 1);
                //while (ColumnDefinitions.Count > 1)
                //    ColumnDefinitions.RemoveAt(ColumnDefinitions.Count - 1);
            }

            if (percentOpen > 0)
            {
                _overlay.Opacity = percentOpen;

                if (drawerMode && !Children.Contains(_detailDrawer))
                {
                    Children.Add(_overlay);
                    Children.Add(_detailDrawer);
                }
                if (!drawerMode && Children.Contains(_detailDrawer))
                {
                    Children.Remove(_detailDrawer);
                    Children.Remove(_overlay);
                }
            }
            else
            {
                _drawerColumnDefinition.Width = GridLength.Auto;
                _drawerRowDefinition.Height = GridLength.Auto;

                if (Children.Contains(_detailDrawer))
                    Children.Remove(_detailDrawer);
                if (Children.Contains(_overlay))
                    Children.Remove(_overlay);

            }

            //System.Diagnostics.Debug.WriteLine("ContentAndDetailPresenter.LayoutDetailAndOverlay percentOpen: " + percentOpen);
        }

        public Size DrawerSize
        {
            get
            {
                var size = new Size(ActualWidth, ActualHeight);
                if (size.Width / size.Height > 1)
                    size.Width = Math.Min(300, size.Height / DrawerAspectRatio);
                else
                    size.Height = size.Width * DrawerAspectRatio;

                return size;
            }
        }


        bool LocalUpdateIsInDrawerMode(Size availableSize)
        {
            if (Detail is null)
                return false;
            
            var minSide = Math.Min(availableSize.Width, availableSize.Height);
            IsInDrawerMode = minSide <= 600;
            return IsInDrawerMode;

            /*
            var maxSide = Math.Max(availableSize.Width, availableSize.Height);
            var aspect = maxSide / minSide;
            
            
            
            /*
            if (!isAndroid || )
            {
                //System.Diagnostics.Debug.WriteLine($" : ");
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LocalUpdateIsInDrawerMode({availableSize}) : ============= ENTER ================");
                var measurements = _targetedPopup.GetAlignmentMarginsAndPointerMeasurements(Detail);
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LocalUpdateIsInDrawerMode : Measurements: {measurements}");
                
                if (measurements.PointerDirection.IsVertical() || measurements.PointerDirection.IsHorizontal())
                {
                    //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LocalUpdateIsInDrawerMode : ============= EXIT [FALSE] A ================");
                    //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LocalUpdateIsInDrawerMode : false");
                    return false;
                }
            }
            */
            
            /*
            //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LocalUpdateIsInDrawerMode : aspect: {aspect}");
            
            if (aspect > 1.5 * DrawerAspectRatio)
            {
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LocalUpdateIsInDrawerMode : ============= EXIT [TRUE] B================");
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LocalUpdateIsInDrawerMode : true");
                return true;
            }

            //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LocalUpdateIsInDrawerMode : ============= EXIT [FALSE] C ================");
            //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LocalUpdateIsInDrawerMode : false");
            return false; 
            */
        }

        double AspectRatio(Size size)
        {
            if (size.Height > 0)
                return size.Width / size.Height;
            return 0;
        }

        #endregion


        #region Push / Pop
        public async Task PushDetailAsync(bool animated = false, Action<double> animation = null)
        {
            if (DetailPushPopState == PushPopState.Pushing || DetailPushPopState == PushPopState.Pushed)
                return;

            if (DetailPushPopState == PushPopState.Popping)
            {
                if (_popCompletionSource is null)
                    await WaitForPop();
                else
                    return;
            }

            DetailPushPopState = PushPopState.Pushing;
            _popCompletionSource = null;

            var size = new Size(ActualWidth, ActualHeight);
            var drawerMode = LocalUpdateIsInDrawerMode(size);
            LayoutDetailAndOverlay(size, 0.11, drawerMode);
            animation?.Invoke(0.11);
            if (drawerMode)
            {
                if (animated)
                {
                    Action<double> action = percent =>
                    {
                        animation?.Invoke(percent);
                        LayoutDetailAndOverlay(size, percent, true);
                    };
                    var animator = new P42.Utils.Uno.ActionAnimator(0.11, 0.95, TimeSpan.FromMilliseconds(300), action);
                    await animator.RunAsync();
                }

                LayoutDetailAndOverlay(size, 1, true);
                animation?.Invoke(1);

                if (Detail is IEventSubscriber subscriber)
                    subscriber.EnableEvents();
            }
            else
                await _targetedPopup.PushAsync(animated);

            DetailPushPopState = PushPopState.Pushed;
            _pushCompletionSource?.SetResult(true);
        }

        public async Task PopDetailAsync(bool animated = false)
        {

            if (DetailPushPopState == PushPopState.Popping || DetailPushPopState == PushPopState.Popped)
                return;

            if (DetailPushPopState == PushPopState.Pushing)
            {
                if (_pushCompletionSource is null)
                    await WaitForPush();
                else
                    return;
            }

            DetailPushPopState = PushPopState.Popping;
            _pushCompletionSource = null;

            var size = new Size(ActualWidth, ActualHeight);
            var drawerMode = LocalUpdateIsInDrawerMode(size);
            if (drawerMode)
            {
                if (Detail is IEventSubscriber subscriber)
                    subscriber.DisableEvents();

                if (animated)
                {
                    Action<double> action = percent => LayoutDetailAndOverlay(size, percent, true);
                    var animator = new P42.Utils.Uno.ActionAnimator(0.89, 0.11, TimeSpan.FromMilliseconds(300), action);
                    await animator.RunAsync();
                }
            }
            else
                await _targetedPopup.PopAsync(PopupPoppedCause.MethodCalled, animated);                

            LayoutDetailAndOverlay(size, 0, drawerMode);
            DetailPushPopState = PushPopState.Popped;
            _popCompletionSource?.SetResult(true);
        }

        async void OnTargetedPopupPopped(object sender, PopupPoppedEventArgs e)
        {
            if (e.Trigger == popupToDrawerResizeTrigger)
                return;

            if (PopOnPageOverlayTouch)
            {
                if (e.Cause == PopupPoppedCause.HardwareBackButtonPressed ||
                    e.Cause == PopupPoppedCause.BackgroundTouch ||
                    e.Cause == PopupPoppedCause.Timeout)
                {
                    var dismissEventArgs = new DismissPointerPressedEventArgs();
                    DismissPointerPressed?.Invoke(this, dismissEventArgs);
                    if (!dismissEventArgs.CancelDismiss)
                        await PopDetailAsync();
                }
                else
                    await PopDetailAsync();
            }
            //_targetedPopup.DismissPointerPressed -= OnTargetedPopupDismissPointerPressed;
            //_targetedPopup.Popped -= OnTargetedPopupPopped;
            //DetailPushPopState = PushPopState.Popped;
        }

        void OnTargetedPopupDismissPointerPressed(object sender, DismissPointerPressedEventArgs e)
        {
            if (PopOnPageOverlayTouch)
                DismissPointerPressed?.Invoke(this, e);
            else
                e.CancelDismiss = true;
        }

        async void OnDismissPointerPressed(object sender, TappedRoutedEventArgs e)
        {
            if (PopOnPageOverlayTouch)
            {
                var dismissEventArgs = new DismissPointerPressedEventArgs();
                DismissPointerPressed?.Invoke(this, dismissEventArgs);
                if (!dismissEventArgs.CancelDismiss)
                    await PopDetailAsync();
            }
        }

        TaskCompletionSource<bool> _popCompletionSource;
        public async Task<bool> WaitForPop()
        {
            _popCompletionSource = _popCompletionSource ?? new TaskCompletionSource<bool>();
            return await _popCompletionSource.Task;
        }


        TaskCompletionSource<bool> _pushCompletionSource;
        async Task<bool> WaitForPush()
        {
            _pushCompletionSource = _pushCompletionSource ?? new TaskCompletionSource<bool>();
            return await _pushCompletionSource.Task;
        }

#endregion

    }
}
