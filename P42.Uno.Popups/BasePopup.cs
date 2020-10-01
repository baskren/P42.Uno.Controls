using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public partial class BasePopup : ContentControl
    {
        #region Properties

        #region Overridden Properties
        
        #region HorizontalAlignment Property
        public static readonly new DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(BasePopup),
            new PropertyMetadata(DefaultHorizontalAlignment, new PropertyChangedCallback((d, e) => ((BasePopup)d).OnHorizontalAlignmentChanged(e)))
        );
        protected virtual void OnHorizontalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_border != null)
                _border.HorizontalAlignment = HorizontalAlignment;
            if ((HorizontalAlignment)e.OldValue == HorizontalAlignment.Stretch || (HorizontalAlignment)e.NewValue == HorizontalAlignment.Stretch)
                InvalidateMeasure();
            UpdateAlignment();
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
            typeof(BasePopup),
            new PropertyMetadata(DefaultVerticalAlignment, new PropertyChangedCallback((d, e) => ((BasePopup)d).OnVerticalAlignmentChanged(e)))
        );
        protected virtual void OnVerticalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_border != null)
                    _border.VerticalAlignment = VerticalAlignment;
            if ((VerticalAlignment)e.OldValue == VerticalAlignment.Stretch || (VerticalAlignment)e.NewValue == VerticalAlignment.Stretch)
                InvalidateMeasure();
            UpdateAlignment();
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
            typeof(BasePopup),
            new PropertyMetadata(default(Thickness), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnMarginChanged(e)))
        );
        protected virtual void OnMarginChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateBorderMargin();
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
            typeof(BasePopup),
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
            typeof(BasePopup),
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
            typeof(BasePopup),
            new PropertyMetadata(default(UIElement), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnTargetChanged(e)))
        );
        protected virtual void OnTargetChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateBorderMargin();
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
            typeof(BasePopup),
            new PropertyMetadata(default(Point), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnTargetPointChanged(e)))
        );
        protected virtual void OnTargetPointChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateBorderMargin();
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
            typeof(BasePopup),
            new PropertyMetadata(0.5, new PropertyChangedCallback((d, e) => ((BasePopup)d).OnPointerBiasChanged(e)))
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
            typeof(BasePopup),
            new PropertyMetadata(default(double))
        );
        public double PointerCornerRadius
        {
            get => (double)GetValue(PointerCornerRadiusProperty);
            set => SetValue(PointerCornerRadiusProperty, value);
        }
        #endregion PointerCornerRadius Property

        #region Pointer Directions

        #region ActualPointerDirection Property
        public static readonly DependencyProperty ActualPointerDirectionProperty = DependencyProperty.Register(
            nameof(ActualPointerDirection),
            typeof(PointerDirection),
            typeof(BasePopup),
            new PropertyMetadata(default(PointerDirection), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnActualPointerDirectionChanged(e)))
        );
        protected virtual void OnActualPointerDirectionChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }
        public PointerDirection ActualPointerDirection
        {
            get => (PointerDirection)GetValue(ActualPointerDirectionProperty);
            set => SetValue(ActualPointerDirectionProperty, value);
        }
        #endregion ActualPointerDirection Property

        #region PreferredPointerDirection Property
        public static readonly DependencyProperty PreferredPointerDirectionProperty = DependencyProperty.Register(
            nameof(PreferredPointerDirection),
            typeof(PointerDirection),
            typeof(BasePopup),
            new PropertyMetadata(default(PointerDirection), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnPreferredPointerDirectionChanged(e)))
        );
        protected virtual void OnPreferredPointerDirectionChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateBorderMargin();
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
            typeof(BasePopup),
            new PropertyMetadata(default(PointerDirection), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnFallbackPointerDirectionChanged(e)))
        );
        protected virtual void OnFallbackPointerDirectionChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateBorderMargin();
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
            typeof(BasePopup),
            new PropertyMetadata(default(double), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnPointerLengthChanged(e)))
        );
        protected virtual void OnPointerLengthChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateBorderMargin();
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
            typeof(BasePopup),
            new PropertyMetadata(default(double), new PropertyChangedCallback((d, e) => ((BasePopup)d).OnPointerTipRadiusChanged(e)))
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

        #endregion 

        #endregion


        #region Fields
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Center;
        const string ContentPresenterName = "BasePopup_Popup_Border_ContentPresenter";
        const string BorderElementName = "BasePopup_Popup_Border";
        const string PopupElementName = "BasePopup_Popup";
        ContentPresenter _contentPresenter;
        BubbleBorder _border;
        Popup _popup;
        #endregion


        #region Construction / Initialization
        public BasePopup()
        {
            //this.InitializeComponent();
            this.DefaultStyleKey = typeof(BasePopup);
            /*
            var startStyle = Style;
            var defaultStyleObject = Application.Current.Resources["BaseBasePopupStyle"];
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
            UpdateBorderMargin();
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
            UpdateAlignment();
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
        void UpdateAlignment()
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment");
            var windowSize = AppWindow.Size();
            if (windowSize.Width < 1 || windowSize.Height < 1)
                return;

            var windowWidth = windowSize.Width - Margin.Horizontal();
            var windowHeight = windowSize.Height - Margin.Vertical();

            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAlignment: window("+windowWidth+","+windowHeight+")   content("+ _lastMeasuredSize + ")");

            if (_border is null)
                return;
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
#elif __ANDROID__
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment ANDROID");
#elif __iOS__
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment iOS");
#elif __MACOS__
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment MACOS");
#elif NETFX_CORE
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment UWP");
#else
            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment ????");
#endif
            //System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateAligment Offset: " + hOffset + ", " + vOffset);

            _popup.HorizontalOffset = hOffset;
            _popup.VerticalOffset = vOffset;
            
        }

        protected Thickness FreeSpace(PointerDirection pointerDirection)
        {
            if (Target != null || (TargetPoint.X > 0 || TargetPoint.Y > 0) )
            {
                var windowBounds = AppWindow.Size();
                var targetBounds = Target is null ? Rect.Empty : Target.GetBounds();
                
                double left = Target is null ? TargetPoint.X : targetBounds.Left;
                double right = Target is null ? TargetPoint.X : targetBounds.Right;
                double top = Target is null ? TargetPoint.Y : targetBounds.Top;
                double bottom = Target is null ? TargetPoint.Y : targetBounds.Bottom;

                if (right > 0 && left < windowBounds.Width && bottom > 0 && top < windowBounds.Height)
                {
                    var pointerLength = pointerDirection == PointerDirection.None ? 0 : PointerLength;
                    var availL = left - Margin.Left -  pointerLength;
                    var availR = windowBounds.Width - right - Margin.Right - pointerLength;
                    var availT = top - Margin.Top - pointerLength;
                    var availB = windowBounds.Height - bottom - Margin.Bottom - pointerLength;

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
                    return new Thickness(availL - _lastMeasuredSize.Width, availT - _lastMeasuredSize.Height, 
                        availR - _lastMeasuredSize.Width, availB - _lastMeasuredSize.Height);
                }
            }
            return new Thickness(-1, -1, -1, -1);

        }


        protected void UpdateBorderMargin()
        {
            if (_border is null)
                return;

            var margin = Margin;
            var pointerDirection = PointerDirection.None;
            if (Target is null && TargetPoint.X == default)
            {
                _border.Margin = margin;
            }
            else
            {
                var available = FreeSpace(PreferredPointerDirection);
                var bestDirection = PreferredPointerDirection.BestFitDirection(available);
                if (bestDirection == PointerDirection.None)
                {
                    available = FreeSpace(FallbackPointerDirection);
                    bestDirection = FallbackPointerDirection.BestFitDirection(available);
                }
                if (bestDirection == PointerDirection.None)
                {
                    _border.Margin = margin;
                }
                else
                {
                    var targetBounds = Target is null ? Rect.Empty : Target.GetBounds();

                    double right = (Target is null ? TargetPoint.X : targetBounds.Left) + Margin.Right;
                    double left = (Target is null ? TargetPoint.X : targetBounds.Right) + Margin.Left;
                    double bottom = (Target is null ? TargetPoint.Y : targetBounds.Top) + Margin.Bottom;
                    double top = (Target is null ? TargetPoint.Y : targetBounds.Bottom) + Margin.Top;

                    pointerDirection = bestDirection;
                    if (bestDirection == PointerDirection.Left)
                        margin.Left = left;
                    else if (bestDirection == PointerDirection.Right)
                        margin.Right = right;
                    else if (bestDirection == PointerDirection.Up)
                        margin.Top = top;
                    else if (bestDirection == PointerDirection.Down)
                        margin.Bottom = bottom;
                }
            }
            _border.Margin = margin;
            ActualPointerDirection = _border.PointerDirection  = pointerDirection;
        }


        #endregion

    }
}
