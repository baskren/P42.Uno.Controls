﻿using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
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

        #region DetailBackground Property
        public static readonly DependencyProperty DetailBackgroundProperty = DependencyProperty.Register(
            nameof(DetailBackground),
            typeof(Brush),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(SystemColors.BaseLow.ToBrush(), OnDetailBackgroundChanged)
        );

        private static void OnDetailBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentAndDetailPresenter cdp)
            {
                cdp._targetedPopup.Background = cdp.DetailBackground;
                cdp._detailDrawer.Background = cdp.DetailBackground;
            }    
        }

        public Brush DetailBackground
        {
            get => (Brush)GetValue(DetailBackgroundProperty);
            set => SetValue(DetailBackgroundProperty, value);
        }
        #endregion DetailBackground Property

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


        #region PopupProperties

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

        #region PopupWidth Property
        public static readonly DependencyProperty PopupWidthProperty = DependencyProperty.Register(
            nameof(PopupWidth),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(300.0)
        );
        public double PopupWidth
        {
            get => (double)GetValue(PopupWidthProperty);
            set => SetValue(PopupWidthProperty, value);
        }

        #endregion PopupWidth Property

        #region PopupHeight Property
        public static readonly DependencyProperty PopupHeightProperty = DependencyProperty.Register(
            nameof(PopupHeight),
            typeof(double),
            typeof(ContentAndDetailPresenter),
            new PropertyMetadata(double.NaN)
        );
        public double PopupHeight
        {
            get => (double)GetValue(PopupHeightProperty);
            set => SetValue(PopupHeightProperty, value);
        }
        #endregion PopupHeight Property

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


        public bool IsInDrawerMode => LocalIsInDrawerMode(ViewEstimatedSize);

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
        {
            /*
            var ΔHeight = args.NewSize.Height - args.PreviousSize.Height;
            var ΔWidth = args.NewSize.Width - args.PreviousSize.Width;
            if (ΔWidth <= 0 && ΔWidth > -1 && ΔHeight <= 0 && ΔHeight > -1)
                return;
            */
            LayoutDetailAndOverlay(args.NewSize, DetailPushPopState == PushPopState.Pushed ? 1 : 0);
        }

        void LayoutDetailAndOverlay(Size size, double percentOpen)
        {
            if (size.IsZero())
                return;
            if (double.IsNaN(size.Width))
                return;
            if (double.IsNaN(size.Height))
                return;

            if (LocalIsInDrawerMode(size))
            {
                _targetedPopup.Content = null;
                _detailDrawer.Child = Detail;
                _detailDrawer.Opacity = percentOpen;
                if (DrawerOrientation == Orientation.Horizontal)
                {
                    _drawerColumnDefinition.Width = new GridLength(percentOpen * DrawerSize.Width);
                    Grid.SetRow(_detailDrawer, 0);
                    Grid.SetRowSpan(_detailDrawer, 2);
                    Grid.SetColumn(_detailDrawer, 1);
                }
                else
                {
                    _drawerRowDefinition.Height = new GridLength(percentOpen * DrawerSize.Height);
                    Grid.SetRow(_detailDrawer, 2);
                    Grid.SetRowSpan(_detailDrawer, 1);
                    Grid.SetColumn(_detailDrawer, 0);
                }

            }
            else
            {
                _targetedPopup.Opacity = percentOpen;
                _detailDrawer.Child = null;
                _targetedPopup.Width = PopupWidth;
                _targetedPopup.Height = PopupHeight;
                //System.Diagnostics.Debug.WriteLine($"ContentAndDetailPresenter.LayoutDetailAndOverlay _targetedPopup.Size:[{_targetedPopup.Width},{_targetedPopup.Height}]");
                _targetedPopup.Content = Detail;

                //while (RowDefinitions.Count > 2)
                //    RowDefinitions.RemoveAt(RowDefinitions.Count - 1);
                //while (ColumnDefinitions.Count > 1)
                //    ColumnDefinitions.RemoveAt(ColumnDefinitions.Count - 1);
            }

            if (percentOpen > 0)
            {
                _overlay.Opacity = percentOpen;

                if (!Children.Contains(_overlay))
                    Children.Add(_overlay);
                if (LocalIsInDrawerMode(size) && !Children.Contains(_detailDrawer))
                    Children.Add(_detailDrawer);
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

        /*
        public Size PopupSize(Size availableSize, PointerDirection pointerDirection)
        {
            var footerHeight = Footer?.ActualHeight ?? 0;
            var targetHeight = Target?.ActualSize.Y ?? 100;

            var popHt = PopupHeight;
            var popWt = PopupWidth;
            if (double.IsNaN(popHt))
            {
                if (double.IsNaN(popWt))
                {
                    //Detail.Measure(availableSize);
                    var measurements = _targetedPopup.GetAlignmentMarginsAndPointerMeasurements(Detail);
                    popHt = measurements.Size.Width * DetailAspectRatio;
                }
                else
                {
                    popHt = DetailAspectRatio * popWt;
                }
            }

            if (double.IsNaN(popWt))
                popWt = popHt / DetailAspectRatio;

            popWt += _targetedPopup.Padding.Horizontal() + _targetedPopup.BorderWidth * 2;
            popHt += _targetedPopup.Padding.Vertical() + _targetedPopup.BorderWidth * 2;

            if (pointerDirection.IsVertical())
                popHt += _targetedPopup.PointerLength;
            if (pointerDirection.IsHorizontal())
                popWt += _targetedPopup.PointerLength;

            return new Size(popHt, popWt);
        }
        */

        public Size DrawerSize
        {
            get
            {
                var size = new Size(ActualWidth, ActualHeight);
                if (size.Width / size.Height > 1)
                    size.Width = size.Height / DetailAspectRatio;
                else
                    size.Height = size.Width * DetailAspectRatio;

                return size;
            }
        }


        bool LocalIsInDrawerMode(Size availableSize)
        {
            if (Detail is null)
                return false;

            var measurements = _targetedPopup.GetAlignmentMarginsAndPointerMeasurements(Detail);
            return !measurements.PointerDirection.IsVertical();
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

            LayoutDetailAndOverlay(size, 0.11);
            animation?.Invoke(0.11);

            if (!LocalIsInDrawerMode(size))
                await _targetedPopup.PushAsync(animated);
            if (animated)
            {
                Action<double> action = percent =>
                {
                    animation?.Invoke(percent);
                    LayoutDetailAndOverlay(size, percent);
                };
                var animator = new P42.Utils.Uno.ActionAnimator(0.11, 0.95, TimeSpan.FromMilliseconds(300), action);
                await animator.RunAsync();
            }

            LayoutDetailAndOverlay(size, 1);
            animation?.Invoke(1);

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

            if (IsAnimated)
            {
                Action<double> action = percent => LayoutDetailAndOverlay(size, percent);
                var animator = new P42.Utils.Uno.ActionAnimator(0.89, 0.11, TimeSpan.FromMilliseconds(300), action);
                await animator.RunAsync();
            }
            LayoutDetailAndOverlay(size, 0);
            if (!LocalIsInDrawerMode(size))
                await _targetedPopup.PopAsync(PopupPoppedCause.MethodCalled, animated);

            DetailPushPopState = PushPopState.Popped;
            _popCompletionSource?.SetResult(true);
        }

        async void OnTargetedPopupPopped(object sender, PopupPoppedEventArgs e)
        {
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
