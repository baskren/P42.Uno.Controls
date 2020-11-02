using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace P42.Uno.Controls
{
    public partial class ContentAndDetailPresenter : Panel
    {
        #region Properties

        #region Content Property
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(Content),
            typeof(object),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(null, new PropertyChangedCallback(OnContentChanged))
        );
        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentAndDetailPresenter view)
                view._contentPresenter.Content = e.NewValue;
        }
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
        #endregion Content Property


        #region Detail Properties

        #region Detail Property
        public static readonly DependencyProperty DetailProperty = DependencyProperty.Register(
            nameof(Detail),
            typeof(object),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(null, new PropertyChangedCallback(OnDetailChanged))
        );
        private static void OnDetailChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentAndDetailPresenter view)
                view._detailContentPresenter.Content = e.NewValue;
        }
        public object Detail
        {
            get => GetValue(DetailProperty);
            set => SetValue(DetailProperty, value);
        }
        #endregion Detail Property


        #region DetailPaneBackground Property
        public static readonly DependencyProperty DetailPaneBackgroundProperty = DependencyProperty.Register(
            nameof(DetailPaneBackground),
            typeof(Brush),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(SystemColors.BaseLow.ToBrush())
        );
        public Brush DetailPaneBackground
        {
            get => (Brush)GetValue(DetailPaneBackgroundProperty);
            set => SetValue(DetailPaneBackgroundProperty, value);
        }
        #endregion DetailPaneBackground Property


        #region DetailAspectRatio Property
        public static readonly DependencyProperty DetailAspectRatioProperty = DependencyProperty.Register(
            nameof(DetailAspectRatio),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(1.0, new PropertyChangedCallback((d, e) => ((ContentAndDetailPresenter)d).OnDetailAspectRatioChanged(e)))
        );
        protected virtual void OnDetailAspectRatioChanged(DependencyPropertyChangedEventArgs e)
        {
            if (DetailAspectRatio < 2.0 / 3.0 || DetailAspectRatio > 3.0 / 2.0)
                throw new Exception("Invalid DetailAspectRatio [" + DetailAspectRatio + "].  Must be between 2/3 and 3/2");
        }
        public double DetailAspectRatio
        {
            get => (double)GetValue(DetailAspectRatioProperty);
            set => SetValue(DetailAspectRatioProperty, value);
        }
        #endregion DetailAspectRatio Property

        #endregion


        #region Footer Property
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(
            nameof(Footer),
            typeof(object),
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
                    view.Children.Add(newElement);
                }
            }
            //    view._footerContentPresenter.Content = e.NewValue;
            
        }
        public object Footer
        {
            get => GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }
        #endregion Footer Property


        #region PopupContentHeight Property
        public static readonly DependencyProperty PopupContentHeightProperty = DependencyProperty.Register(
            nameof(PopupContentHeight),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(300.0)
        );
        public double PopupContentHeight
        {
            get => (double)GetValue(PopupContentHeightProperty);
            set => SetValue(PopupContentHeightProperty, value);
        }
        #endregion PopupContentHeight Property


        public bool IsInDrawerMode
        {
            get
            {
                var idiom = Utils.Uno.Device.Idiom;
                if (idiom == Utils.Uno.DeviceIdiom.Phone)
                    return true;
                var aspect = Aspect;
                if (aspect < 1)
                    aspect = 1 / aspect;
                return aspect > 1.75 && ActualWidth <= 500;
            }
        }

        double Aspect 
        {
            get
            {
                if (EstHeight > 0)
                    return EstWidth / EstHeight;
                return 0;
            }
        }

        double EstWidth
        {
            get
            {
                if (ActualWidth > 0)
                    return ActualWidth;
                if (DesiredSize.Width > 0)
                    return DesiredSize.Width;
                return AppWindow.Size().Width;
            }
        }

        double EstHeight
        {
            get
            {
                if (ActualHeight > 0)
                    return ActualHeight;
                if (DesiredSize.Height > 0)
                    return DesiredSize.Height;
                return AppWindow.Size().Height;
            }
        }

        public PushPopState DetailPushPopState { get; private set; } = PushPopState.Popped;

        #region Target Property
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(UIElement),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(default(UIElement), new PropertyChangedCallback(OnTargetChanged))
        );
        protected static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentAndDetailPresenter view)
                view._targetedPopup.Target = e.NewValue as UIElement;
        }
        public UIElement Target
        {
            get => (UIElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        #endregion Target Property


        #region IsAnimated Property
        public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register(
            nameof(IsAnimated),
            typeof(bool),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(true)
        );
        public bool IsAnimated
        {
            get => (bool)GetValue(IsAnimatedProperty);
            set => SetValue(IsAnimatedProperty, value);
        }
        #endregion IsAnimated Property


        #region LightDismiss Properties

        #region IsLightDismissEnabled Property
        public static readonly DependencyProperty IsLightDismissEnabledProperty = DependencyProperty.Register(
            nameof(IsLightDismissEnabled),
            typeof(bool),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(true, new PropertyChangedCallback(OnIsLightDismissEnabledChanged))
        );
        private static void OnIsLightDismissEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentAndDetailPresenter sdv)
                sdv._targetedPopup.IsLightDismissEnabled = sdv.IsLightDismissEnabled;
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
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(LightDismissOverlayMode.On, new PropertyChangedCallback(OnLightDismissOverlayModeChanged))
        );
        private static void OnLightDismissOverlayModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentAndDetailPresenter sdv)
                sdv._targetedPopup.LightDismissOverlayMode = sdv.LightDismissOverlayMode;
        }
        public LightDismissOverlayMode LightDismissOverlayMode
        {
            get => (LightDismissOverlayMode)GetValue(LightDismissOverlayModeProperty);
            set => SetValue(LightDismissOverlayModeProperty, value);
        }
        #endregion LightDismissOverlayMode Property

        #region LightDismissOverlayBrush Property
        public static readonly DependencyProperty LightDismissOverlayBrushProperty = DependencyProperty.Register(
            nameof(LightDismissOverlayBrush),
            typeof(Brush),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(SystemColors.AltMedium.ToBrush())
        );
        public Brush LightDismissOverlayBrush
        {
            get => (Brush)GetValue(LightDismissOverlayBrushProperty);
            set => SetValue(LightDismissOverlayBrushProperty, value);
        }
        #endregion LightDismissOverlayBrush Property

        #endregion

        #endregion



        #region Construction / Initialization
        public ContentAndDetailPresenter()
        {
            Build();
            SizeChanged += OnSizeChanged;
        }

        #endregion


        #region Layout
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChildrenMeasure(e.NewSize, true);
        }


        protected override Size MeasureOverride(Size availableSize)
        {
            //ChildrenMeasure(availableSize);
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            ChildrenMeasure(finalSize, true);
            return finalSize;
        }

        void ChildrenMeasure(Size size, bool arrange = false)
        {
            if (size.IsZero())
                return;


            if (double.IsNaN(size.Width))
                size.Width = AppWindow.Size().Width;
            if (double.IsNaN(size.Height))
                size.Height = AppWindow.Size().Height;

            var y = size.Height;

            if (arrange && Footer is UIElement footer)
            {
                //if (!arrange || _footerContentPresenter.DesiredSize.Width != size.Width)
                    footer.Measure(size);
                y -= footer.DesiredSize.Height;
                if (arrange)
                {
                    var rect = new Rect(new Point(0, y), new Size(size.Width, footer.DesiredSize.Height));
                    footer.Arrange(rect);
                }
            }

            if (DetailPushPopState == PushPopState.Popped ||
                (!IsInDrawerMode && DetailPushPopState == PushPopState.Pushed))
            {
                var s = new Size(size.Width, y);
                if (!arrange || _contentPresenter.DesiredSize.Width != size.Width)
                    _contentPresenter.Measure(s);
                if (arrange)
                    _contentPresenter.Arrange(new Rect(new Point(), s));
            }
            else if (DetailPushPopState == PushPopState.Pushed)
            {
                _targetedPopup.PopupContent = null;
                _detailDrawerBorder.Child = _detailContentPresenter;
                var aspect = size.Width / size.Height;
                Rect drawerRect;
                Size contentSize;
                if (aspect > 1)
                {
                    var drawerSize = new Size(size.Height / DetailAspectRatio, size.Height);
                    contentSize = new Size(size.Width - drawerSize.Width, size.Height);
                    if (!arrange || _detailDrawerBorder.DesiredSize != drawerSize)
                        _detailDrawerBorder.Measure(drawerSize);
                    if (!arrange || _contentPresenter.DesiredSize != contentSize)
                        _contentPresenter.Measure(contentSize);
                    var x = size.Width - _detailDrawerBorder.DesiredSize.Width;
                    drawerRect = new Rect(new Point(x, 0), new Size(_detailDrawerBorder.DesiredSize.Width, size.Height));
                }
                else
                {
                    var drawerSize = new Size(size.Width, size.Width * DetailAspectRatio);
                    contentSize = new Size(size.Width, size.Height - drawerSize.Height);
                    //if (!arrange || _detailDrawerBorder.DesiredSize != drawerSize)
                        _detailDrawerBorder.Measure(drawerSize);
                    if (!arrange || _contentPresenter.DesiredSize != contentSize)
                        _contentPresenter.Measure(contentSize);
                    y = size.Height - _detailDrawerBorder.DesiredSize.Height;
                    drawerRect = new Rect(new Point(0, y), new Size(size.Width, _detailDrawerBorder.DesiredSize.Height));
                }
                if (arrange && !_detailDrawerBorder.DesiredSize.IsZero())
                {
                    _contentPresenter.Arrange(new Rect(new Point(), contentSize));
                    _overlay.Arrange(new Rect(new Point(), contentSize));
                    _detailDrawerBorder.Arrange(drawerRect);
                }
            }
        }
        #endregion


        #region Push / Pop

        public async Task PushDetail()
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


            // where is the detail going?
            if (IsInDrawerMode)
            {
                _targetedPopup.PopupContent = null;
                _detailDrawerBorder.Child = _detailContentPresenter;

                _overlay.Opacity = 0.0;
                _overlay.Visibility = LightDismissOverlayMode == LightDismissOverlayMode.On
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                _overlay.PointerPressed += OnDismissPointerPressed;

                double from = 0.0;
                double to = 0.0;
                Action<double> action;
                if (Aspect > 1)
                {
                    var height = EstHeight;
                    var width = DetailAspectRatio * height;
                    var size = new Size(width, height);
                    _detailDrawerBorder.Height = height;
                    _detailDrawerBorder.Width = DetailAspectRatio * height;
                    _detailDrawerBorder.BorderThickness = new Thickness(1, 0, 0, 0);
                    from = EstWidth;
                    to = EstWidth - width;
                    action = x =>
                    {
                        _contentPresenter.Arrange(new Rect(0, 0, x, height));
                        _overlay.Opacity = (from - x) / (from - to);
                        _overlay.Arrange(new Rect(0, 0, x, height));
                        _detailDrawerBorder.Arrange(new Rect(x, 0, width, height));
                    };
                }
                else
                {
                    var width = EstWidth;
                    var height = width / DetailAspectRatio;
                    var size = new Size(width, height);
                    _detailDrawerBorder.Height = height;
                    _detailDrawerBorder.Width = width;
                    _detailDrawerBorder.BorderThickness = new Thickness(0, 1, 0, 0);
                    from = EstHeight;
                    to = EstHeight - height;
                    action = y =>
                    {
                        _contentPresenter.Arrange(new Rect(0, 0, width, y));
                        _overlay.Opacity = (from - y) / (from - to);
                        _overlay.Arrange(new Rect(0, 0, width, y));
                        _detailDrawerBorder.Arrange(new Rect(0, y, width, height));
                    };
                }

                _detailDrawerBorder.Visibility = Visibility.Visible;

                //#if NETFX_CORE
                if (IsAnimated)
                {
                    var animator = new P42.Utils.Uno.ActionAnimator(from, to, TimeSpan.FromMilliseconds(500), action);
                    await animator.RunAsync();
                }
                else
                    action(to);

                _overlay.Opacity = 1.0;
            }
            else
            {

                _detailDrawerBorder.Child = null;
                _targetedPopup.PopupContent = _detailContentPresenter;

                _targetedPopup.Target = Target;
                _detailContentPresenter.Height = PopupContentHeight;
                _detailContentPresenter.Width = PopupContentHeight * DetailAspectRatio;
                _targetedPopup.Popped += OnTargetedPopupPopped;
                await _targetedPopup.PushAsync();

            }

            DetailPushPopState = PushPopState.Pushed;
            _pushCompletionSource?.SetResult(true);
        }

        private void OnTargetedPopupPopped(object sender, PopupPoppedEventArgs e)
        {
            _targetedPopup.Popped -= OnTargetedPopupPopped;
            DetailPushPopState = PushPopState.Popped;
        }

        async void OnDismissPointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (IsLightDismissEnabled)
            {
                await PopDetailAsync();
            }
        }

        public async Task PopDetailAsync()
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

            _overlay.PointerPressed -= OnDismissPointerPressed;

            if (_targetedPopup.PopupContent != null)
                await _targetedPopup.PopAsync();
            else
            {
                double from = 0.0;
                double to = 0.0;
                var width = _detailDrawerBorder.ActualWidth;
                var height = _detailDrawerBorder.ActualHeight;
                var size = new Size(width, height);
                Action<double> action;
                if (Aspect > 1)
                {
                    from = EstWidth - width;
                    to = EstWidth;
                    action = x =>
                    {
                        _contentPresenter.Arrange(new Rect(0, 0, x, height));
                        _overlay.Opacity = (to - x) / (to - from);
                        _overlay.Arrange(new Rect(0, 0, x, height));
                        _detailDrawerBorder.Arrange(new Rect(x, 0, width, height));
                    };
                }
                else
                {
                    from = EstHeight - height;
                    to = EstHeight;
                    action = y =>
                    {
                        _contentPresenter.Arrange(new Rect(0, 0, width, y));
                        _overlay.Opacity = (to - y) / (to - from);
                        _overlay.Arrange(new Rect(0, 0, width, y));
                        _detailDrawerBorder.Arrange(new Rect(0, y, width, height));
                    };
                }

                System.Diagnostics.Debug.WriteLine("ContentAndDetailPresenter.PopDetailAsync A");
                //#if NETFX_CORE
                if (IsAnimated)
                {
                    var animator = new P42.Utils.Uno.ActionAnimator(from, to, TimeSpan.FromMilliseconds(500), action);
                    await animator.RunAsync();
                }
                else
                    action(to);

                _detailDrawerBorder.Collapsed();
            }

            System.Diagnostics.Debug.WriteLine("ContentAndDetailPresenter.PopDetailAsync D");
            if (LightDismissOverlayMode == LightDismissOverlayMode.On)
                _overlay.Visibility = Visibility.Collapsed;
            _overlay.Opacity = 1.0;

            System.Diagnostics.Debug.WriteLine("ContentAndDetailPresenter.PopDetailAsync E");
            DetailPushPopState = PushPopState.Popped;
            _popCompletionSource?.SetResult(true);
            System.Diagnostics.Debug.WriteLine("ContentAndDetailPresenter.PopDetailAsync F");


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
