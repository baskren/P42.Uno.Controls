using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace P42.Uno.Controls
{
    [Windows.UI.Xaml.Data.Bindable]
    [System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
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
            //    view._footerContentPresenter.Content = e.NewValue;

        }
        public object Footer
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


        #region DetailContentHeight Property
        public static readonly DependencyProperty DetailContentHeightProperty = DependencyProperty.Register(
            nameof(DetailContentHeight),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(300.0)
        );
        public double DetailContentHeight
        {
            get => (double)GetValue(DetailContentHeightProperty);
            set => SetValue(DetailContentHeightProperty, value);
        }
        #endregion DetailContentHeight Property


        public bool IsInDrawerMode
        {
            get
            {
                /*
                //return false;
                var idiom = Utils.Uno.Device.Idiom;
                if (idiom == Utils.Uno.DeviceIdiom.Phone)
                    return true;
                var aspect = Aspect;
                //if (aspect < 1)
                //    aspect = 1 / aspect;
                //System.Diagnostics.Debug.WriteLine("ContentAndDetailPresenter.IsInDrawerMode aspect:" + aspect + "  ActualWidth:" + ActualWidth);
                return (aspect < 1.0 / 1.25 && ActualWidth <= 500) || (aspect > 1.75 && ActualHeight <= 500 / DetailAspectRatio);
                */
                //return true;

                var popupSize = new Size(DetailContentHeight * DetailAspectRatio, DetailContentHeight);

                // landscape
                if (Aspect > DetailAspectRatio * 1.5 && popupSize.Width <= ActualWidth)
                {
                    var drawerSize = new Size(ActualHeight * DetailAspectRatio, ActualHeight);
                    if (drawerSize.Width <= ActualWidth * 0.5 && drawerSize.Height < popupSize.Height + popupMargin*2 && drawerSize.Width < popupSize.Width + popupMargin*2)
                    {
                        /*
                        System.Diagnostics.Debug.WriteLine("ContentAndDetailPresenter.IsInDrawerMode: ");
                        System.Diagnostics.Debug.WriteLine("\t Aspect: " + Aspect);
                        System.Diagnostics.Debug.WriteLine("\t DetailAspectRatio: " + DetailAspectRatio);
                        System.Diagnostics.Debug.WriteLine("\t PopupContentHeight: " + PopupContentHeight);
                        System.Diagnostics.Debug.WriteLine("\t ActualWidth: " + ActualWidth);
                        */
                        return true;
                    }
                }

                // portrait
                if (Aspect <(DetailAspectRatio * 0.66) && popupSize.Width <= ActualWidth * 1.5)
                {
                    var drawerSize = new Size(ActualWidth, ActualWidth / DetailAspectRatio);
                    if (drawerSize.Height <= ActualHeight * 0.5 && drawerSize.Height < popupSize.Height + popupMargin * 2 && drawerSize.Width < popupSize.Width + popupMargin * 2)
                    {
                        /*
                        System.Diagnostics.Debug.WriteLine("ContentAndDetailPresenter.IsInDrawerMode: ");
                        System.Diagnostics.Debug.WriteLine("\t Aspect: " + Aspect);
                        System.Diagnostics.Debug.WriteLine("\t DetailAspectRatio: " + DetailAspectRatio);
                        System.Diagnostics.Debug.WriteLine("\t PopupContentHeight: " + PopupContentHeight);
                        System.Diagnostics.Debug.WriteLine("\t ActualWidth: " + ActualWidth);
                        System.Diagnostics.Debug.WriteLine("\t ActualHeight: " + ActualHeight);
                        */
                        return true;
                    }
                }
                return false;
            }
        }

        double Aspect 
        {
            get
            {
                var size = EstimatedSize;
                if (size.Height > 0)
                    return size.Width / size.Height;
                return 0;
            }
        }

        Size EstimatedSize
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
            new PropertyMetadata(true)
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
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(LightDismissOverlayMode.On)
        );
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
            new PropertyMetadata(SystemColors.AltMedium.WithAlpha(0.1).ToBrush())
        );
        public Brush LightDismissOverlayBrush
        {
            get => (Brush)GetValue(LightDismissOverlayBrushProperty);
            set => SetValue(LightDismissOverlayBrushProperty, value);
        }
        #endregion LightDismissOverlayBrush Property

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

        bool _disposed;
#if NETFX_CORE
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
#else
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            base.Dispose();
        }
#endif

#if __ANDROID__ 
        protected override void Dispose(bool disposing)
#elif __MACOS__ || __IOS__
        protected virtual new void Dispose(bool disposing)
#else
        protected virtual void Dispose(bool disposing)
#endif
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
                SizeChanged -= OnSizeChanged;
            }
#if __ANDROID__ || __MACOS__ || __IOS__
            base.Dispose(disposing);
#endif
        }



        #endregion


        #region Layout

        void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutDetailAndOverlay(e.NewSize, DetailPushPopState == PushPopState.Pushed ? 1 : 0);
        }


        void LayoutDetailAndOverlay(Size size, double percentOpen)
        {
            if (size.IsZero())
                return;
            if (double.IsNaN(size.Width))
                return;
            if (double.IsNaN(size.Height))
                return;

            if (IsInDrawerMode)
            {
                _targetedPopup.Content = null;
                _detailDrawer.Child = Detail;
                _detailDrawer.Opacity = percentOpen;
                var aspect = size.Width / size.Height;
                if (aspect > 1)
                {
                    var drawerWidth = percentOpen * size.Height / DetailAspectRatio;
                    _drawerColumnDefinition.Width = new GridLength(drawerWidth);
                    while (RowDefinitions.Count > 2)
                        RowDefinitions.RemoveAt(RowDefinitions.Count - 1);
                    if (ColumnDefinitions.Count == 1)
                        ColumnDefinitions.Add(_drawerColumnDefinition);
                    Grid.SetRow(_detailDrawer, 0);
                    Grid.SetRowSpan(_detailDrawer, 2);
                    Grid.SetColumn(_detailDrawer, 1);
                }
                else
                {
                    var drawerHeight = percentOpen * size.Width * DetailAspectRatio;
                    _drawerRowDefinition.Height = new GridLength(drawerHeight);
                    while (ColumnDefinitions.Count > 1)
                        ColumnDefinitions.RemoveAt(ColumnDefinitions.Count - 1);
                    if (RowDefinitions.Count == 2)
                        RowDefinitions.Add(_drawerRowDefinition);
                    Grid.SetRow(_detailDrawer, 2);
                    Grid.SetRowSpan(_detailDrawer, 1);
                    Grid.SetColumn(_detailDrawer, 0);
                }

            }
            else
            {
                _targetedPopup.Opacity = percentOpen;
                _detailDrawer.Child = null;
                _targetedPopup.PopupContent = Detail;

                while (RowDefinitions.Count > 2)
                    RowDefinitions.RemoveAt(RowDefinitions.Count - 1);
                while (ColumnDefinitions.Count > 1)
                    ColumnDefinitions.RemoveAt(ColumnDefinitions.Count - 1);
            }

            if (percentOpen > 0)
            {
                _overlay.Opacity = percentOpen;
                if (!Children.Contains(_overlay))
                    Children.Add(_overlay);
            }
            else
            {
                if (Children.Contains(_overlay))
                    Children.Remove(_overlay);
            }

            System.Diagnostics.Debug.WriteLine("ContentAndDetailPresenter.LayoutDetailAndOverlay percentOpen: " + percentOpen);
        }
#endregion


#region Push / Pop
        bool _freshPushCycle;
        public async Task PushDetailAsync()
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

            LayoutDetailAndOverlay(size, 0.11);
            if (!IsInDrawerMode)
                await _targetedPopup.PushAsync();
            if (IsAnimated)
            {
                Action<double> action = percent => LayoutDetailAndOverlay(size, percent);
                var animator = new P42.Utils.Uno.ActionAnimator(0.11, 0.95, TimeSpan.FromMilliseconds(300), action);
                await animator.RunAsync();
            }
            LayoutDetailAndOverlay(size, 1);

            DetailPushPopState = PushPopState.Pushed;
            _pushCompletionSource?.SetResult(true);
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

            var size = new Size(ActualWidth, ActualHeight);

            if (IsAnimated)
            {
                Action<double> action = percent => LayoutDetailAndOverlay(size, percent);
                var animator = new P42.Utils.Uno.ActionAnimator(0.89, 0.11, TimeSpan.FromMilliseconds(300), action);
                await animator.RunAsync();
            }
            LayoutDetailAndOverlay(size, 0);
            if (!IsInDrawerMode)
                await _targetedPopup.PopAsync();

            DetailPushPopState = PushPopState.Popped;
            _popCompletionSource?.SetResult(true);
        }

        async void OnTargetedPopupPopped(object sender, PopupPoppedEventArgs e)
        {
            //_targetedPopup.DismissPointerPressed -= OnTargetedPopupDismissPointerPressed;
            //_targetedPopup.Popped -= OnTargetedPopupPopped;
            //DetailPushPopState = PushPopState.Popped;
            await PopDetailAsync();
        }

        void OnTargetedPopupDismissPointerPressed(object sender, DismissPointerPressedEventArgs e)
        {
            if (IsLightDismissEnabled)
                DismissPointerPressed?.Invoke(this, e);
            else
                e.CancelDismiss = true;
        }

        async void OnDismissPointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (IsLightDismissEnabled)
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
