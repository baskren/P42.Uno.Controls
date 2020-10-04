using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Uno.Extensions;
using Uno.Extensions.ValueType;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

#if NETFX_CORE
using Popup = Windows.UI.Xaml.Controls.Primitives.Popup;
#else
using Popup = Windows.UI.Xaml.Controls.Popup;
#endif


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace P42.Uno.Popups
{
    
    [TemplatePart(Name = ContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = BorderElementName, Type = typeof(BubbleBorder))]
    [TemplatePart(Name = PopupElementName, Type = typeof(Windows.UI.Xaml.Controls.Primitives.Popup))]
    public partial class TargetedPopup : ContentControl
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
            /*
            if (ActualPointerDirection == PointerDirection.None)
                _border.HorizontalAlignment = HorizontalAlignment;
            else
                _border.HorizontalAlignment = HorizontalAlignment.Left;
            */
            UpdateMarginAndAlignment(true);
            //if ((HorizontalAlignment)e.OldValue == HorizontalAlignment.Stretch || (HorizontalAlignment)e.NewValue == HorizontalAlignment.Stretch)
            //    InvalidateMeasure();
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
            /*
            if (ActualPointerDirection == PointerDirection.None)
                _border.VerticalAlignment = VerticalAlignment;
            else
                _border.VerticalAlignment = VerticalAlignment.Top;
            */
            UpdateMarginAndAlignment(true);
            //if ((VerticalAlignment)e.OldValue == VerticalAlignment.Stretch || (VerticalAlignment)e.NewValue == VerticalAlignment.Stretch)
            //    InvalidateMeasure();
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

        public PointerDirection ActualPointerDirection => _lastStats.PointerDirection;

        #region PreferredPointerDirection Property
        public static readonly DependencyProperty PreferredPointerDirectionProperty = DependencyProperty.Register(
            nameof(PreferredPointerDirection),
            typeof(PointerDirection),
            typeof(TargetedPopup),
            new PropertyMetadata(default(PointerDirection), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).OnPreferredPointerDirectionChanged(e)))
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


        #endregion

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


        #region Construction / Initialization
        public TargetedPopup()
        {
            //this.InitializeComponent();
            this.DefaultStyleKey = typeof(TargetedPopup);
            /*
            var startStyle = Style;
            var defaultStyleObject = Application.Current.Resources["BaseTargetedPopupStyle"];
            if (defaultStyleObject is Style defaultStyle)
                Style = defaultStyle;
            */
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _contentPresenter = GetTemplateChild(ContentPresenterName) as ContentPresenter;
            _border = GetTemplateChild(BorderElementName) as BubbleBorder;
            _border.HorizontalAlignment = HorizontalAlignment;
            _border.VerticalAlignment = VerticalAlignment;
            _border.PointerLength = PointerLength;
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
        public async Task PushAsync()
        {
            if (Parent is Grid grid)
                grid.Children.Remove(this);

            _border.SizeChanged += OnBorderSizeChanged;

            await OnPushBeginAsync();
            _popup.IsOpen = true;
            await OnPushEndAsync();
        }

        public async Task PopAsync()
        {
            _border.SizeChanged -= OnBorderSizeChanged;
            await OnPopBeginAsync();
            _popup.IsOpen = false;
            await OnPopEndAsync();
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
            _lastMeasuredSize = args.NewSize;
            UpdateMarginAndAlignment();
        }

        #endregion


        #region Layout

        Size _lastMeasuredSize;
        protected override Size MeasureOverride(Size availableSize)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride(" + availableSize + ") ================================================  margin: "+ Margin);

            availableSize.Width -= Margin.Horizontal();
            availableSize.Height -= Margin.Vertical();

            _border.Measure(availableSize);
            var result = _border.DesiredSize;
            _lastMeasuredSize = result;

            //System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride result: "+ result);
            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".ArrangeOverride("+finalSize+")");
            return base.ArrangeOverride(finalSize);
        }
        

#if __WASM__ || NETSTANDARD
        Size _firstRenderSize;
#endif

        DirectionStats _lastStats = default;
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


            if (PreferredPointerDirection == PointerDirection.None)
            {
                _lastStats = default; 
                ImplementWorkingMarginAndAlignment();
                return;
            }

            var targetBounds = TargetBounds();
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateBorderMarginAndAlignment targetBounds:["+targetBounds+"]");
            var availableSpace = AvailableSpace(targetBounds);
            var stats = _lastStats;
            if (!isAlignmentOnlyChange || stats.BorderSize.IsEmpty)
                stats = BestFit(availableSpace);
            _lastStats = stats;

            if (stats.PointerDirection == PointerDirection.None)
            {
                ImplementWorkingMarginAndAlignment();
                return;
            }

            _popup.HorizontalOffset = 0;
            _popup.VerticalOffset = 0;

            var baseMargin = Margin;
            if (stats.PointerDirection.IsHorizontal())
            {
                if (stats.PointerDirection == PointerDirection.Left)
                {
                    baseMargin.Left = targetBounds.Right;
                    base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment == HorizontalAlignment.Stretch ? HorizontalAlignment.Stretch : HorizontalAlignment.Left;
                }    
                else
                {
                    baseMargin.Left = targetBounds.Left - stats.BorderSize.Width - PointerLength;
                    base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment == HorizontalAlignment.Stretch ? HorizontalAlignment.Stretch : HorizontalAlignment.Right;
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
                    baseMargin.Top = targetBounds.Top - stats.BorderSize.Height - PointerLength;
                    base.VerticalAlignment = _border.VerticalAlignment = VerticalAlignment == VerticalAlignment.Stretch ? VerticalAlignment.Stretch : VerticalAlignment.Bottom;
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
            }
            _border.PointerDirection = stats.PointerDirection;
            base.Margin = _border.Margin = baseMargin;
        }

        void ImplementWorkingMarginAndAlignment()
        {             //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment");

            if (_border is null || _popup is null)
                return;

            base.Margin = _border.Margin = Margin;

            var windowSize = AppWindow.Size();
            if (windowSize.Width < 1 || windowSize.Height < 1)
                return;

            base.HorizontalAlignment = _border.HorizontalAlignment = HorizontalAlignment;
            base.VerticalAlignment = _border.VerticalAlignment = VerticalAlignment;


            var windowWidth = windowSize.Width - Margin.Horizontal();
            var windowHeight = windowSize.Height - Margin.Vertical();

            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment: window("+windowWidth+","+windowHeight+")   content("+ _lastMeasuredSize + ")");

            double hOffset = 0.0;
            if (HorizontalAlignment == HorizontalAlignment.Center)
                hOffset = (windowWidth - _lastMeasuredSize.Width) / 2;
            else if (HorizontalAlignment == HorizontalAlignment.Right)
                hOffset = (windowWidth - _lastMeasuredSize.Width);

            double vOffset = 0.0;
            if (VerticalAlignment == VerticalAlignment.Center)
                vOffset = (windowHeight - _lastMeasuredSize.Height) / 2;
            else if (VerticalAlignment == VerticalAlignment.Bottom)
                vOffset = (windowHeight - _lastMeasuredSize.Height);

#if __WASM__ || NETSTANDARD
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment WASM || NETSTANDARD");

            if (_firstRenderSize.Width < 1 || _firstRenderSize.Height < 1)
                _firstRenderSize = windowSize;

            hOffset -= (_firstRenderSize.Width + Margin.Left) / 2.0 ; 
            //vOffset -= windowHeight / 2.0;
#endif
            System.Diagnostics.Debug.WriteLine(GetType() + ".ImplementWorkingMarginAndAlignmentOffset:[" + hOffset + ", " + vOffset + "]");
            System.Diagnostics.Debug.WriteLine("\t WorkingMargin:[" + Margin + "] WorkingHzAlign:[" + HorizontalAlignment + "] WorkingVtAlign:[" + VerticalAlignment + "] ");
            System.Diagnostics.Debug.WriteLine("\t windowWidth:[" + windowWidth + "]  _lastMeasuredSize:["+_lastMeasuredSize+"]");
            _popup.HorizontalOffset = hOffset;
            _popup.VerticalOffset = vOffset;
        }

        DirectionStats BestFit(Thickness availableSpace)
        {
            //var stats = new List<DirectionStats>();
            // given the amount of free space, determine if the border will fit 
            var windowSpace = new Size(AppWindow.Size().Width - Margin.Horizontal(), AppWindow.Size().Height - Margin.Vertical());
            var cleanBorder = RectangleBorderSize(windowSpace);
            var cleanStat = new DirectionStats
            {
                PointerDirection = PointerDirection.None,
                BorderSize = cleanBorder,
                //MinFree = Math.Min(windowSpace.Width - cleanBorder.Width, windowSpace.Height - cleanBorder.Height),
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

            double targetLeft = Target is null ? TargetPoint.X : targetBounds.Left;
            double targetRight = Target is null ? TargetPoint.X : targetBounds.Right;
            double targetTop = Target is null ? TargetPoint.Y : targetBounds.Top;
            double targetBottom = Target is null ? TargetPoint.Y : targetBounds.Bottom;

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
            var stats = new List<DirectionStats>();
            if (pointerDirection.LeftAllowed() && cleanStat.FreeSpace.Width >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Left;
                stat.BorderSize.Width += PointerLength;
                stat.FreeSpace.Width = availableSpace.Right - stat.BorderSize.Width;
                if (stat.MinFree >= 0)
                    stats.Add(stat);
            }
            if (pointerDirection.RightAllowed() && cleanStat.FreeSpace.Width >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Right;
                stat.BorderSize.Width += PointerLength;
                stat.FreeSpace.Width = availableSpace.Left - stat.BorderSize.Width;
                if (stat.MinFree >= 0)
                    stats.Add(stat);
            }
            if (pointerDirection.UpAllowed() && cleanStat.FreeSpace.Height >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Up;
                stat.BorderSize.Height += PointerLength;
                stat.FreeSpace.Height = availableSpace.Bottom - stat.BorderSize.Height;
                if (stat.MinFree >= 0)
                    stats.Add(stat);
            }
            if (pointerDirection.DownAllowed() && cleanStat.FreeSpace.Height >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Down;
                stat.BorderSize.Height += PointerLength;
                stat.FreeSpace.Height = availableSpace.Top - stat.BorderSize.Height;
                if (stat.MinFree >= 0)
                    stats.Add(stat);
            }
            return stats;
        }

        List<DirectionStats> GetMeasuredStatsForDirection(PointerDirection pointerDirection, DirectionStats cleanStat, Thickness availableSpace, Size windowSpace)
        {
            var stats = new List<DirectionStats>();
            if (pointerDirection.LeftAllowed())
            {
                var size = new Size(availableSpace.Right, windowSpace.Height);
                var border = RectangleBorderSize(size);
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Left;
                stat.BorderSize = border;
                stat.FreeSpace.Width = availableSpace.Right - border.Width;
                stats.Add(stat);
            }
            if (pointerDirection.RightAllowed())
            {
                var size = new Size(availableSpace.Left, windowSpace.Height);
                var border = RectangleBorderSize(size);
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Right;
                stat.BorderSize = border;
                stat.FreeSpace.Width = availableSpace.Left - border.Width;
                stats.Add(stat);
            }
            if (pointerDirection.UpAllowed())
            {
                var size = new Size(windowSpace.Width, availableSpace.Bottom);
                var border = RectangleBorderSize(size);
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Up;
                stat.BorderSize = border;
                stat.FreeSpace.Height = availableSpace.Bottom - border.Height;
                stats.Add(stat);
            }
            if (pointerDirection.DownAllowed())
            {
                var size = new Size(windowSpace.Width, availableSpace.Top);
                var border = RectangleBorderSize(size);
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Down;
                stat.BorderSize = border;
                stat.FreeSpace.Height = availableSpace.Top - border.Height;
                stats.Add(stat);
            }
            return stats;
        }

        Size RectangleBorderSize(Size available)
        {
            var hasBorder = (BorderThickness.Average() > 0) && BorderBrush is SolidColorBrush brush && brush.Color.A > 0;
            var border = BorderThickness.Average() * (hasBorder ? 1 : 0) * 2;
            available.Width -= Padding.Horizontal() - border;
            available.Height -= Padding.Vertical() - border;
            _contentPresenter.Measure(available);
            var result = _contentPresenter.DesiredSize;
            result.Width += Padding.Horizontal();
            result.Height += Padding.Vertical();
            return result;
        }
        #endregion

    }
}
