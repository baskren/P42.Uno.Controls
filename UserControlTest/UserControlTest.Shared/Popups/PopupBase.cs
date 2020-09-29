using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace UserControlTest.Popups
{
    public partial class PopupBase : ContentControl
    {
        #region Properties

        #region IsAnimationEnabled Property
        public static readonly DependencyProperty IsAnimationEnabledProperty = DependencyProperty.Register(
            nameof(IsAnimationEnabled),
            typeof(bool),
            typeof(PopupBase),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnIsAnimationEnabledChanged(e)))
        );
        protected virtual void OnIsAnimationEnabledChanged(DependencyPropertyChangedEventArgs e) { }
        public bool IsAnimationEnabled
        {
            get => (bool)GetValue(IsAnimationEnabledProperty);
            set => SetValue(IsAnimationEnabledProperty, value);
        }
        #endregion IsAnimationEnabled Property

        #region AppearingStoryboard Property
        public static readonly DependencyProperty AppearingStoryboardProperty = DependencyProperty.Register(
            nameof(AppearingStoryboard),
            typeof(Storyboard),
            typeof(PopupBase),
            new PropertyMetadata(default(Storyboard), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnAppearingStoryboardChanged(e)))
        );
        protected virtual void OnAppearingStoryboardChanged(DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        public Storyboard AppearingStoryboard
        {
            get => (Storyboard)GetValue(AppearingStoryboardProperty);
            set => SetValue(AppearingStoryboardProperty, value);
        }
        #endregion AppearingStoryboard Property

        #region DisappearingStoryboard Property
        public static readonly DependencyProperty DisappearingStoryboardProperty = DependencyProperty.Register(
            nameof(DisappearingStoryboard),
            typeof(Storyboard),
            typeof(PopupBase),
            new PropertyMetadata(default(Storyboard), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnDisappearingStoryboardChanged(e)))
        );
        protected virtual void OnDisappearingStoryboardChanged(DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        public Storyboard DisappearingStoryboard
        {
            get => (Storyboard)GetValue(DisappearingStoryboardProperty);
            set => SetValue(DisappearingStoryboardProperty, value);
        }
        #endregion DisappearingStoryboard Property

        #region PageOverlay Property
        public static readonly DependencyProperty PageOverlayProperty = DependencyProperty.Register(
            nameof(PageOverlay),
            typeof(Brush),
            typeof(PopupBase),
            new PropertyMetadata(default(Brush), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnPageOverlayChanged(e)))
        );
        protected virtual void OnPageOverlayChanged(DependencyPropertyChangedEventArgs e) { }
        public Brush PageOverlay
        {
            get => (Brush)GetValue(PageOverlayProperty);
            set => SetValue(PageOverlayProperty, value);
        }
        #endregion PageOverlay Property

        #region CancelOnPageOverlayTouch Property
        public static readonly DependencyProperty CancelOnPageOverlayTapProperty = DependencyProperty.Register(
            nameof(CancelOnPageOverlayTap),
            typeof(bool),
            typeof(PopupBase),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnCancelOnPageOverlayTouchChanged(e)))
        );
        protected virtual void OnCancelOnPageOverlayTouchChanged(DependencyPropertyChangedEventArgs e) { }
        public bool CancelOnPageOverlayTap
        {
            get => (bool)GetValue(CancelOnPageOverlayTapProperty);
            set => SetValue(CancelOnPageOverlayTapProperty, value);
        }
        #endregion CancelOnPageOverlayTouch Property

        #region CancelOnBackButtonTap Property
        public static readonly DependencyProperty CancelOnBackButtonTapProperty = DependencyProperty.Register(
            nameof(CancelOnBackButtonTap),
            typeof(bool),
            typeof(PopupBase),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnCancelOnBackButtonTapChanged(e)))
        );
        protected virtual void OnCancelOnBackButtonTapChanged(DependencyPropertyChangedEventArgs e) { }
        public bool CancelOnBackButtonTap
        {
            get => (bool)GetValue(CancelOnBackButtonTapProperty);
            set => SetValue(CancelOnBackButtonTapProperty, value);
        }
        #endregion CancelOnBackButtonTap Property

        #region PopAfter Property
        public static readonly DependencyProperty PopAfterProperty = DependencyProperty.Register(
            nameof(PopAfter),
            typeof(TimeSpan),
            typeof(PopupBase),
            new PropertyMetadata(default(TimeSpan), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnPopAfterChanged(e)))
        );
        protected virtual void OnPopAfterChanged(DependencyPropertyChangedEventArgs e) { }
        public TimeSpan PopAfter
        {
            get => (TimeSpan)GetValue(PopAfterProperty);
            set => SetValue(PopAfterProperty, value);
        }
        #endregion PopAfter Property

        #region Parameter Property
        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register(
            nameof(Parameter),
            typeof(object),
            typeof(PopupBase),
            new PropertyMetadata(default(object), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnParameterChanged(e)))
        );
        protected virtual void OnParameterChanged(DependencyPropertyChangedEventArgs e) { }
        public object Parameter
        {
            get => (object)GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }
        #endregion Parameter Property

        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(PopupBase),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnHasShadowChanged(e)))
        );
        protected virtual void OnHasShadowChanged(DependencyPropertyChangedEventArgs e) { }
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }
        #endregion HasShadow Property

        #region Visibility Property
        public static new readonly DependencyProperty VisibilityProperty = DependencyProperty.Register(
            nameof(Visibility),
            typeof(Visibility),
            typeof(PopupBase),
            new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnVisibilityChanged))
        );
        protected static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }
        public new Visibility Visibility
        {
            get => (Visibility)GetValue(VisibilityProperty);
            private set => SetValue(VisibilityProperty, value);
        }
        #endregion Visibility Property

        #region Origin Property
        internal static readonly DependencyProperty OriginProperty = DependencyProperty.Register(
            nameof(Origin),
            typeof(Point),
            typeof(PopupBase),
            new PropertyMetadata(default(Point), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnOriginChanged(e)))
        );
        protected virtual void OnOriginChanged(DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        internal Point Origin
        {
            get => (Point)GetValue(OriginProperty);
            set => SetValue(OriginProperty, value);
        }
        #endregion Origin Property

        #region Margin Property
        public static new readonly DependencyProperty MarginProperty = DependencyProperty.Register(
            nameof(Margin),
            typeof(Thickness),
            typeof(PopupBase),
            new PropertyMetadata(default(Thickness), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnMarginChanged(e)))
        );
        protected virtual void OnMarginChanged(DependencyPropertyChangedEventArgs e)
        {
            BorderMargin = Margin;
        }
        public new Thickness Margin
        {
            get => (Thickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }
        #endregion Margin Property

        #region BorderMargin Property
        public static readonly DependencyProperty BorderMarginProperty = DependencyProperty.Register(
            nameof(BorderMargin),
            typeof(Thickness),
            typeof(PopupBase),
            new PropertyMetadata(default(Thickness))
        );
        public Thickness BorderMargin
        {
            get => (Thickness)GetValue(BorderMarginProperty);
            set => SetValue(BorderMarginProperty, value);
        }
        #endregion BorderMargin Property

        #region Padding Property
        public static new readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(PopupBase),
            new PropertyMetadata(default(Thickness), new PropertyChangedCallback((d, e) => ((PopupBase)d).OnPaddingChanged(e)))
        );
        protected virtual void OnPaddingChanged(DependencyPropertyChangedEventArgs e)
        {
            ContentPresenterMargin = Padding;
        }
        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        #endregion Padding Property

        #region ContentPresenterMargin Property
        internal static readonly DependencyProperty ContentPresenterMarginProperty = DependencyProperty.Register(
            nameof(ContentPresenterMargin),
            typeof(Thickness),
            typeof(PopupBase),
            new PropertyMetadata(default(Thickness))
        );
        internal Thickness ContentPresenterMargin
        {
            get => (Thickness)GetValue(ContentPresenterMarginProperty);
            set => SetValue(ContentPresenterMarginProperty, value);
        }
        #endregion CotentPresenterMargin Property

        #region HorizontalAlignment Property
        public static new readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(PopupBase),
            new PropertyMetadata(HorizontalAlignment.Center, new PropertyChangedCallback(OnHorizontalAlignmentChanged))
        );
        protected static void OnHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PopupBase PopupBase)
            {
                PopupBase.BorderHorizontalAlignment = PopupBase.HorizontalAlignment;
            }
        }
        public new HorizontalAlignment HorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalAlignmentProperty);
            set => SetValue(HorizontalAlignmentProperty, value);
        }
        #endregion HorizontalAlignment Property

        #region BorderHorizontalAlignment Property
        public static readonly DependencyProperty BorderHorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(BorderHorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(PopupBase),
            new PropertyMetadata(HorizontalAlignment.Center)
        );
        public HorizontalAlignment BorderHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(BorderHorizontalAlignmentProperty);
            set => SetValue(BorderHorizontalAlignmentProperty, value);
        }
        #endregion BorderHorizontalAlignment Property

        #region VerticalAlignment Property
        public static new readonly DependencyProperty VerticalAlignmentProperty = DependencyProperty.Register(
            nameof(VerticalAlignment),
            typeof(VerticalAlignment),
            typeof(PopupBase),
            new PropertyMetadata(VerticalAlignment.Center, new PropertyChangedCallback(OnVerticalAlignmentChanged))
        );
        protected static void OnVerticalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PopupBase PopupBase)
            {
                PopupBase.BorderVerticalAlignment = PopupBase.VerticalAlignment;
            }
        }
        public new VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }
        #endregion VerticalAlignment Property

        #region BorderVerticalAlignment Property
        public static readonly DependencyProperty BorderVerticalAlignmentProperty = DependencyProperty.Register(
            nameof(BorderVerticalAlignment),
            typeof(VerticalAlignment),
            typeof(PopupBase),
            new PropertyMetadata(VerticalAlignment.Center)
        );
        public VerticalAlignment BorderVerticalAlignment
        {
            get => (VerticalAlignment)GetValue(BorderVerticalAlignmentProperty);
            set => SetValue(BorderVerticalAlignmentProperty, value);
        }
        #endregion BorderVerticalAlignment Property

        #region OutsideCornersRadius Property
        public static readonly DependencyProperty OutsideCornersRadiusProperty = DependencyProperty.Register(
            nameof(OutsideCornersRadius),
            typeof(double),
            typeof(PopupBase),
            new PropertyMetadata(default(double))
        );
        public double OutsideCornersRadius
        {
            get => (double)GetValue(OutsideCornersRadiusProperty);
            set => SetValue(OutsideCornersRadiusProperty, value);
        }
        #endregion OutsideCornersRadius Property



        #endregion


        #region Events
        /// <summary>
        /// Triggered when popup's background is clicked
        /// </summary>
        public event EventHandler BackgroundClicked;
        #endregion


        protected PopupBase()
        {
            base.Visibility = Visibility.Collapsed;
        }

        #region Pushing / Popping
        bool _appearing;
        public virtual async Task PushAsync(UIElement target=null)
        {
            if (_appearing || base.Visibility == Visibility.Visible)
                return;

            _appearing = true;
            await PushAsyncBegin();
            return;
        }


        Grid _contentGrid;
        protected Grid ContentGrid
        {
            get
            {
                if (_contentGrid is null)
                {
                    var templateRoot = VisualTreeHelper.GetChild(this, 0) as FrameworkElement;
                    _contentGrid = templateRoot.FindName("ContentGrid") as Grid;
                }
                return _contentGrid;
            }
        }

        

        async Task PushAsyncBegin()
        {
            await OnAppearingBeginAsync();

            if (Parent is Grid parentGrid)
            {
                Grid.SetColumnSpan(this, Math.Max(parentGrid.ColumnDefinitions.Count,1));
                Grid.SetRowSpan(this, Math.Max(parentGrid.RowDefinitions.Count,1));
            }
            else
                throw new Exception("Popups must be made a child of a grid before Pushing");

            base.Visibility = Visibility.Visible;
            Visibility = base.Visibility;
            if (IsAnimationEnabled && AppearingStoryboard != null)
            {
                foreach (var timeline in AppearingStoryboard.Children)
                    Storyboard.SetTarget(timeline, ContentGrid);
                
                AppearingStoryboard.Completed += OnAppearingStoryboardCompleted;
                AppearingStoryboard.Begin();
            }
            else
                OnAppearingStoryboardCompleted(null, null);
        }

        async void OnAppearingStoryboardCompleted(object sender, object e)
        {
            if (AppearingStoryboard != null)
                AppearingStoryboard.Completed -= OnAppearingStoryboardCompleted;
            await OnAppearingEndAsync();
            if (PopAfter != default)
            {
                Device.StartTimer(PopAfter, () =>
                {
                    PopAsync(PopupPoppedCause.Timeout);
                    return false;
                });
            }
            _appearing = false;
        }

        //bool _disappearing;
        PoppedTaskCompletionSource _poppedTaskCompleteSource;
        public virtual async Task<PopupPoppedEventArgs> PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, object trigger = null, [CallerMemberName] string callerName = null)
        {
            if (base.Visibility == Visibility.Collapsed)
                return new PopupPoppedEventArgs(PopupPoppedCause.NotPushed, callerName);

            _poppedTaskCompleteSource = _poppedTaskCompleteSource ?? new PoppedTaskCompletionSource(cause, trigger, callerName);

            var tcs = _poppedTaskCompleteSource;
            await PopAsyncBegin();
            return await tcs.Task;
        }

        async Task PopAsyncBegin()
        {
            await OnDisappearingBeginAsync();
            base.Visibility = Visibility.Collapsed;
            Visibility = base.Visibility;
            if (IsAnimationEnabled && DisappearingStoryboard != null)
            {
                foreach (var timeline in DisappearingStoryboard.Children)
                    Storyboard.SetTarget(timeline, ContentGrid);
                DisappearingStoryboard.Completed += OnDisappearingStoryboardCompleted;
                DisappearingStoryboard.Begin();
            }
            else
                OnDisappearingStoryboardCompleted(null, null);
        }

        async void OnDisappearingStoryboardCompleted(object sender, object e)
        {
            if (DisappearingStoryboard != null)
                DisappearingStoryboard.Completed -= OnDisappearingStoryboardCompleted;
            await OnDisappearingEndAsync();

            PopupPoppedEventArgs result = _poppedTaskCompleteSource.Cause == PopupPoppedCause.MethodCalled
                ? new PopupPoppedEventArgs(_poppedTaskCompleteSource.Cause, _poppedTaskCompleteSource.CallerName)
                : new PopupPoppedEventArgs(_poppedTaskCompleteSource.Cause, _poppedTaskCompleteSource.Trigger);

            _poppedTaskCompleteSource.SetResult(result);
            _poppedTaskCompleteSource = null;
        }

        #endregion


        #region Override Animation Methods
        /// <summary>
        /// Invoked at start on appearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnAppearingBeginAsync()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Invoked at end of appearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnAppearingEndAsync()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Invoked at start of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnDisappearingBeginAsync()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Invoked at end of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnDisappearingEndAsync()
        {
            return Task.FromResult(0);
        }

        #endregion


        #region Cancel Trigger Overrides
        /// <summary>
        /// Invoked when background is clicked
        /// </summary>
        /// <returns></returns>
        protected virtual bool OnPageOverlayTapped()
        {
            return CancelOnPageOverlayTap;
        }

        /// <summary>
        /// Invoked when back button has been pressed
        /// </summary>
        /// <returns></returns>
        protected virtual bool OnBackButtonPressed()
        {
            return CancelOnBackButtonTap;
        }
        #endregion


    }
}
