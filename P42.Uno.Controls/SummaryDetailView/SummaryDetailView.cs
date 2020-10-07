using Microsoft.Toolkit.Uwp.UI.Animations;
using P42.Utils.Uno;
using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace P42.Uno.Controls
{
    [TemplatePart(Name = DetailPaneContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = DetailPopupContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = Row1Name, Type = typeof(RowDefinition))]
    [TemplatePart(Name = Col1Name, Type = typeof(ColumnDefinition))]
    [TemplatePart(Name = TargetedPopupName, Type = typeof(TargetedPopup))]
    [TemplatePart(Name = DetailPaneBorderName, Type = typeof(Border))]
    [TemplatePart(Name = LightDismissOverlayName, Type = typeof(Rectangle))]
    public partial class SummaryDetailView : ContentControl
    {
        #region Properties

        #region Detail Properties

        #region Detail Property
        public static readonly DependencyProperty DetailProperty = DependencyProperty.Register(
            nameof(Detail),
            typeof(UIElement),
            typeof(SummaryDetailView),
            new PropertyMetadata(default(UIElement), new PropertyChangedCallback((d, e) => ((SummaryDetailView)d).OnDetailChanged(e)))
        );
        protected virtual void OnDetailChanged(DependencyPropertyChangedEventArgs e)
        {
            if (IsInPaneMode)
                _detailPaneContentPresenter.Content = Detail;
            else
                _detailPopupContentPresenter.Content = Detail;
        }
        public UIElement Detail
        {
            get => (UIElement)GetValue(DetailProperty);
            set => SetValue(DetailProperty, value);
        }
        #endregion Detail Property


        #region DetailPaneBackground Property
        public static readonly DependencyProperty DetailPaneBackgroundProperty = DependencyProperty.Register(
            nameof(DetailPaneBackground),
            typeof(Brush),
            typeof(SummaryDetailView),
            new PropertyMetadata(default(int))
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
            typeof(SummaryDetailView),
            new PropertyMetadata(1.0, new PropertyChangedCallback((d, e) => ((SummaryDetailView)d).OnDetailAspectRatioChanged(e)))
        );
        protected virtual void OnDetailAspectRatioChanged(DependencyPropertyChangedEventArgs e)
        {
            if (DetailAspectRatio < 2.0/3.0 || DetailAspectRatio > 3.0 / 2.0)
                throw new Exception("Invalid DetailAspectRatio [" + DetailAspectRatio + "].  Must be between 2/3 and 3/2");
        }
        public double DetailAspectRatio
        {
            get => (double)GetValue(DetailAspectRatioProperty);
            set => SetValue(DetailAspectRatioProperty, value);
        }
        #endregion DetailAspectRatio Property

        #endregion


        #region PopupContentHeight Property
        public static readonly DependencyProperty PopupContentHeightProperty = DependencyProperty.Register(
            nameof(PopupContentHeight),
            typeof(double),
            typeof(SummaryDetailView),
            new PropertyMetadata(200.0, new PropertyChangedCallback((d, e) => ((SummaryDetailView)d).OnPopupHeightChanged(e)))
        );
        protected virtual void OnPopupHeightChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PopupContentHeight <= 4)
                throw new Exception("Invalid PopupHeight [" + PopupContentHeight + "].  Must be greater than 4.");
        }
        public double PopupContentHeight
        {
            get => (double)GetValue(PopupContentHeightProperty);
            set => SetValue(PopupContentHeightProperty, value);
        }
        #endregion PopupHeight Property


        public bool IsInPaneMode
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
                var aspect = 0.0;
                if (ActualWidth > 0 && ActualWidth > 0)
                    aspect = ActualWidth / ActualHeight;
                else if (DesiredSize.Width > 0 && DesiredSize.Height > 0)
                    aspect = DesiredSize.Width / DesiredSize.Height;
                else if (AppWindow.Size().Width > 0 && AppWindow.Size().Height > 0)
                    aspect = AppWindow.Size().Width / AppWindow.Size().Height;
                return aspect;
            }
        }


        #region Target Property
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(UIElement),
            typeof(SummaryDetailView),
            new PropertyMetadata(default(UIElement), new PropertyChangedCallback((d, e) => ((SummaryDetailView)d).OnTargetChanged(e)))
        );
        protected virtual void OnTargetChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        public UIElement Target
        {
            get => (UIElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        #endregion Target Property


        #region Row1/Col1 Properties

        #region Row1Height Property
        private static readonly DependencyProperty Row1HeightProperty = DependencyProperty.Register(
            nameof(Row1Height),
            typeof(double),
            typeof(SummaryDetailView),
            new PropertyMetadata(default(double), new PropertyChangedCallback(OnRow1HeightChanged))
        );
        private static void OnRow1HeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SummaryDetailView sdv)
                sdv._row1.Height = new GridLength(sdv.Row1Height);
        }
        private double Row1Height
        {
            get => (double)GetValue(Row1HeightProperty);
            set => SetValue(Row1HeightProperty, value);
        }
        #endregion Row1Height Property

        #region Column1Width Property
        private static readonly DependencyProperty Column1WidthProperty = DependencyProperty.Register(
            nameof(Column1Width),
            typeof(double),
            typeof(SummaryDetailView),
            new PropertyMetadata(default(double), new PropertyChangedCallback(OnColumn1WidthChanged))
        );
        private static void OnColumn1WidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SummaryDetailView sdv)
                sdv._col1.Width = new GridLength(sdv.Column1Width);
        }
        private double Column1Width
        {
            get => (double)GetValue(Column1WidthProperty);
            set => SetValue(Column1WidthProperty, value);
        }
        #endregion Column1Width Property

        #endregion

        #region IsAnimated Property
        public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register(
            nameof(IsAnimated),
            typeof(bool),
            typeof(SummaryDetailView),
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
            typeof(SummaryDetailView),
            new PropertyMetadata(true, new PropertyChangedCallback(OnIsLightDismissEnabledChanged))
        );
        private static void OnIsLightDismissEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SummaryDetailView sdv)
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
            typeof(SummaryDetailView),
            new PropertyMetadata(LightDismissOverlayMode.On,new PropertyChangedCallback(OnLightDismissOverlayModeChanged))
        );
        private static void OnLightDismissOverlayModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SummaryDetailView sdv)
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
            typeof(SummaryDetailView),
            new PropertyMetadata(default(SolidColorBrush), new PropertyChangedCallback((d, e) => ((SummaryDetailView)d).OnLightDismissOverlayBrushChanged(e)))
        );

        protected virtual void OnLightDismissOverlayBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            if (LightDismissOverlayBrush is SolidColorBrush brush && brush.Color != LightDismissOverlayColor)
                LightDismissOverlayColor = brush.Color;
        }

        public Brush LightDismissOverlayBrush
        {
            get => (Brush)GetValue(LightDismissOverlayBrushProperty);
            set => SetValue(LightDismissOverlayBrushProperty, value);
        }
        #endregion LightDismissOverlayBrush Property

        #region LightDismissOverlayColor Property
        public static readonly DependencyProperty LightDismissOverlayColorProperty = DependencyProperty.Register(
            nameof(LightDismissOverlayColor),
            typeof(Color),
            typeof(SummaryDetailView),
            new PropertyMetadata(default(Color), new PropertyChangedCallback((d, e) => ((SummaryDetailView)d).OnLightDismissOverlayColorChanged(e)))
        );
        protected virtual void OnLightDismissOverlayColorChanged(DependencyPropertyChangedEventArgs e)
        {
            if (!(LightDismissOverlayBrush is SolidColorBrush brush) || brush.Color != LightDismissOverlayColor)
                LightDismissOverlayBrush = new SolidColorBrush(LightDismissOverlayColor);
        }
        public Color LightDismissOverlayColor
        {
            get => (Color)GetValue(LightDismissOverlayColorProperty);
            set => SetValue(LightDismissOverlayColorProperty, value);
        }
        #endregion LightDismissOverlayColor Property



        #endregion


        #endregion


        #region Fields
        const string DetailPaneContentPresenterName = "_detailPaneContentPresenter";
        const string DetailPopupContentPresenterName = "_detailPopupContentPresenter";
        const string Row1Name = "_row1";
        const string Col1Name = "_col1";
        const string TargetedPopupName = "_targetedPopup";
        const string DetailPaneBorderName = "_detailPaneBorder";
        const string LightDismissOverlayName = "_lightDismissOverlay";

        ContentPresenter _detailPaneContentPresenter;
        ContentPresenter _detailPopupContentPresenter;
        RowDefinition _row1;
        ColumnDefinition _col1;
        TargetedPopup _targetedPopup;
        Border _detailPaneBorder;
        Rectangle _lightDismissOverlay;

        public PushPopState DetailPushPopState = PushPopState.Popped;

        #endregion


        #region Construction / Initialization
        public SummaryDetailView()
        {
            DefaultStyleKey = typeof(SummaryDetailView);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _detailPaneContentPresenter = (ContentPresenter)GetTemplateChild(DetailPaneContentPresenterName);
            _detailPopupContentPresenter = (ContentPresenter)GetTemplateChild(DetailPopupContentPresenterName);
            _row1 = (RowDefinition)GetTemplateChild(Row1Name);
            _col1 = (ColumnDefinition)GetTemplateChild(Col1Name);
            _targetedPopup = (TargetedPopup)GetTemplateChild(TargetedPopupName);
            _detailPaneBorder = (Border)GetTemplateChild(DetailPaneBorderName);
            _lightDismissOverlay = (Rectangle)GetTemplateChild(LightDismissOverlayName);
        }
        #endregion


        #region Layout

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
            if (IsInPaneMode)
            {
                _detailPopupContentPresenter.Content = null;

                _lightDismissOverlay.Opacity = 0.0;
                if (LightDismissOverlayMode == LightDismissOverlayMode.On)
                    _lightDismissOverlay.Visibility = Visibility.Visible;
                _lightDismissOverlay.PointerPressed += OnDismissPointerPressed;

                var storyboard = new Storyboard();
                var flyoutAnimation = new DoubleAnimation
                {
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                    Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                    EnableDependentAnimation = true,
                    From = 0,
                };
                var opacityAnimation = new DoubleAnimation
                {
                    Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                    From = 0.0,
                    To = 1.0
                };

                if (Aspect > 1)
                {
                    var height = ActualHeight;
                    var width = DetailAspectRatio * height;
                    _detailPaneBorder.Height = height;
                    _detailPaneBorder.Width = DetailAspectRatio * height;
                    Grid.SetColumn(_detailPaneBorder, 1);
                    Grid.SetRow(_detailPaneBorder, 0);

                    if (IsAnimated)
                    {
                        flyoutAnimation.To = width;
                        Storyboard.SetTargetProperty(flyoutAnimation, nameof(Column1Width));
                    }
                    else
                        Column1Width = width;
                }
                else
                {
                    var width = ActualWidth;
                    var height = width / DetailAspectRatio;
                    _detailPaneBorder.Height = height;
                    _detailPaneBorder.Width = width;
                    Grid.SetColumn(_detailPaneBorder, 0);
                    Grid.SetRow(_detailPaneBorder, 1);

                    if (IsAnimated)
                    {
                        flyoutAnimation.To = height;
                        Storyboard.SetTargetProperty(flyoutAnimation, nameof(Row1Height));
                    }
                    else
                        Row1Height = height;
                }
                _detailPaneContentPresenter.Content = Detail;
                _detailPaneBorder.Visibility = Visibility.Visible;

                if (IsAnimated)
                {
                    Storyboard.SetTarget(flyoutAnimation, this);
                    Storyboard.SetTarget(opacityAnimation, _lightDismissOverlay);
                    Storyboard.SetTargetProperty(opacityAnimation, nameof(UIElement.Opacity));
                    storyboard.Children.Add(opacityAnimation);
                    storyboard.Children.Add(flyoutAnimation);
                    await storyboard.BeginAsync();
                }
                _lightDismissOverlay.Opacity = 1.0;
            }
            else
            {
                _detailPaneContentPresenter.Content = null;

                _targetedPopup.Target = Target;
                _detailPopupContentPresenter.Height = PopupContentHeight;
                _detailPopupContentPresenter.Width = PopupContentHeight * DetailAspectRatio;
                _detailPopupContentPresenter.Content = Detail;
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

            _lightDismissOverlay.PointerPressed -= OnDismissPointerPressed;

            if (_detailPopupContentPresenter.Content != null)
                await _targetedPopup.PopAsync();
            else
            {
                var storyboard = new Storyboard();
                var flyoutAnimation = new DoubleAnimation
                {
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                    Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                    To = 0,
                    EnableDependentAnimation = true,
                };
                var opacityAnimation = new DoubleAnimation
                {
                    Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                    From = 1.0,
                    To = 0.0
                };

                if (Row1Height > 0)
                {
                    flyoutAnimation.From = Row1Height;
                    Storyboard.SetTargetProperty(flyoutAnimation, nameof(Row1Height));
                }
                else
                {
                    flyoutAnimation.From = Column1Width;
                    Storyboard.SetTargetProperty(flyoutAnimation, nameof(Column1Width));
                }

                if (IsAnimated)
                {
                    Storyboard.SetTarget(flyoutAnimation, this);
                    Storyboard.SetTarget(opacityAnimation, _lightDismissOverlay);
                    Storyboard.SetTargetProperty(opacityAnimation, nameof(UIElement.Opacity));
                    storyboard.Children.Add(flyoutAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    await storyboard.BeginAsync();
                }
                else
                {
                    Row1Height = 0;
                    Column1Width = 0;
                }
            }

            if (LightDismissOverlayMode == LightDismissOverlayMode.On)
                _lightDismissOverlay.Visibility = Visibility.Collapsed;
            _lightDismissOverlay.Opacity = 1.0;

            DetailPushPopState = PushPopState.Popped;
            _popCompletionSource?.SetResult(true);


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