using System;
using System.IO;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using SkiaSharp;
using Windows.UI;
using P42.Utils.Uno;
using Uno.UI.Toolkit;
using P42.Uno.Markup;
using Windows.UI.Xaml.Markup;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace P42.Uno.Controls
{
    /// <summary>
    /// Border used by Popups
    /// </summary>
    [Windows.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    [ContentProperty(Name = nameof(XamlContent))]
    partial class BubbleBorder : UserControl
    {
        #region Properties

        #region Override Properties

        #region Content Property
        public static readonly DependencyProperty XamlContentProperty = DependencyProperty.Register(
            nameof(XamlContent),
            typeof(object),
            typeof(BubbleBorder),
            new PropertyMetadata(null, OnContentChanged)
        );

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is BubbleBorder popup)
            popup._contentPresenter.Content = args.NewValue;
        }

        public object XamlContent
        {
            get => GetValue(XamlContentProperty);
            set => SetValue(XamlContentProperty, value);
        }

        public new object Content
        {
            get => XamlContent;
            set => XamlContent = value;
        }
        #endregion

        #region Padding Property
        public static readonly new DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(BubbleBorder),
            new PropertyMetadata(default(Thickness), OnPaddingChanged)
        );

        private static void OnPaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BubbleBorder border)
                border.UpdateContentPresenterMargin();
        }

        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        #endregion Padding Property


        #region HorizontalAlignment Property
        public static readonly new DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(BubbleBorder),
            new PropertyMetadata(DefaultHorizontalAlignment, new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnHorizontalAlignmentChanged(e)))
        );
        protected virtual void OnHorizontalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            base.HorizontalAlignment = HorizontalAlignment;
            if ((HorizontalAlignment)e.NewValue == HorizontalAlignment.Stretch || (HorizontalAlignment)e.OldValue == HorizontalAlignment.Stretch)
            {
                InvalidateMeasure();
            }
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
            typeof(BubbleBorder),
            new PropertyMetadata(DefaultVerticalAlignment, new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnVerticalAlignmentChanged(e)))
        );
        protected virtual void OnVerticalAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            base.VerticalAlignment = VerticalAlignment;
            if ((VerticalAlignment)e.NewValue == VerticalAlignment.Stretch || (VerticalAlignment)e.OldValue == VerticalAlignment.Stretch)
                InvalidateMeasure();
        }
        public new VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }
        #endregion VerticalAlignment Property

        #endregion

        #region Unique Properties

        #region PointerBias Property
        public static readonly DependencyProperty PointerBiasProperty = DependencyProperty.Register(
            nameof(PointerBias),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(0.5, new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnPointerBiasChanged(e)))
        );
        protected virtual void OnPointerBiasChanged(DependencyPropertyChangedEventArgs e)
        {
            RegeneratePath();
        }
        public double PointerBias
        {
            get => (double)GetValue(PointerBiasProperty);
            set => SetValue(PointerBiasProperty, value);
        }
        #endregion PointerBias Property


        #region PointerLength Property
        public static readonly DependencyProperty PointerLengthProperty = DependencyProperty.Register(
            nameof(PointerLength),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(10.0, new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnPointerLengthChanged(e)))
        );
        protected virtual void OnPointerLengthChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateContentPresenterMargin();
            //InvalidateMeasure();
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
            typeof(BubbleBorder),
            new PropertyMetadata(1.0, new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnPointerTipRadiusChanged(e)))
        );
        protected virtual void OnPointerTipRadiusChanged(DependencyPropertyChangedEventArgs e)
        {
            RegeneratePath();
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


        #region PointerAxialPosition Property
        public static readonly DependencyProperty PointerAxialPositionProperty = DependencyProperty.Register(
            nameof(PointerAxialPosition),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(0.5, new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnPointerAxialPositionChanged(e)))
        );
        protected virtual void OnPointerAxialPositionChanged(DependencyPropertyChangedEventArgs e)
        {
            RegeneratePath();
        }
        /// <summary>
        /// Gets or sets the position of the bubble's pointer along the face it's on.
        /// </summary>
        /// <value>The pointer axial position (left/top is zero).</value>
        public double PointerAxialPosition
        {
            get => (double)GetValue(PointerAxialPositionProperty);
            set => SetValue(PointerAxialPositionProperty, value);
        }
        #endregion PointerAxialPosition Property


        #region PointerDirection Property
        public static readonly DependencyProperty PointerDirectionProperty = DependencyProperty.Register(
            nameof(PointerDirection),
            typeof(PointerDirection),
            typeof(BubbleBorder),
            new PropertyMetadata(PointerDirection.None, new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnPointerDirectionChanged(e)))
        );
        protected virtual void OnPointerDirectionChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateContentPresenterMargin();
            //InvalidateMeasure();
            //InvalidateArrange();
        }
        /// <summary>
        /// Gets or sets the direction in which the pointer pointing.
        /// </summary>
        /// <value>The pointer direction.</value>
        public PointerDirection PointerDirection
        {
            get => (PointerDirection)GetValue(PointerDirectionProperty);
            set => SetValue(PointerDirectionProperty, value);
        }
        #endregion PointerDirection Property


        #region PointerCornerRadius Property
        public static readonly DependencyProperty PointerCornerRadiusProperty = DependencyProperty.Register(
            nameof(PointerCornerRadius),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(default(double), new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnPointerCornerRadiusChanged(e)))
        );
        protected virtual void OnPointerCornerRadiusChanged(DependencyPropertyChangedEventArgs e)
        {
            RegeneratePath();
        }
        public double PointerCornerRadius
        {
            get => (double)GetValue(PointerCornerRadiusProperty);
            set => SetValue(PointerCornerRadiusProperty, value);
        }
        #endregion PointerCornerRadius Property

        /*
        #region ContentPresenterMargin Property
        internal static readonly DependencyProperty ContentPresenterMarginProperty = DependencyProperty.Register(
            nameof(ContentPresenterMargin),
            typeof(Thickness),
            typeof(BubbleBorder),
            new PropertyMetadata(default(Thickness), OnContentPresenterMarginChanged)
        );

        private static void OnContentPresenterMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BubbleBorder bubble && e.NewValue is Thickness margin)
                bubble._contentPresenter.Margin = margin;
        }

        internal Thickness ContentPresenterMargin
        {
            get => (Thickness)GetValue(ContentPresenterMarginProperty);
            set => SetValue(ContentPresenterMarginProperty, value);
        }
        #endregion ContentPresenterMargin Property
        */
        /*
        #region PathGeometry Property
        internal static readonly DependencyProperty PathGeometryProperty = DependencyProperty.Register(
            nameof(PathGeometry),
            typeof(PathGeometry),
            typeof(BubbleBorder),
            new PropertyMetadata(new PathGeometry(), OnPathGeometryChanged)
        );

        private static void OnPathGeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BubbleBorder bubble && e.NewValue is PathGeometry path)
                bubble._path.Data = path;
        }

        internal PathGeometry PathGeometry
        {
            get => (PathGeometry)GetValue(PathGeometryProperty);
            set => SetValue(PathGeometryProperty, value);
        }
        #endregion PathGeometry Property
        */

        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(BubbleBorder),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnHasShadowChanged(e)))
        );
        protected virtual void OnHasShadowChanged(DependencyPropertyChangedEventArgs e)
        {
            ShadowOpacity = HasShadow ? 0.25 : 0;
        }
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }
        #endregion HasShadow Property


        #region ShadowOpacity Property
        internal static readonly DependencyProperty ShadowOpacityProperty = DependencyProperty.Register(
            nameof(ShadowOpacity),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(default(double))
        );
        internal double ShadowOpacity
        {
            get => (double)GetValue(ShadowOpacityProperty);
            set => SetValue(ShadowOpacityProperty, value);
        }
        #endregion ShadowOpacity Property

        #endregion

        #region Size Property
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            nameof(Size),
            typeof(Size),
            typeof(BubbleBorder),
            new PropertyMetadata(default(Size))
        );
        public Size Size
        {
            get => (Size)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
        #endregion Size Property

        public bool IsEmpty
        {
            get
            {
                var contentPresenter = _contentPresenter;
                while (contentPresenter?.Content is ContentPresenter cp)
                    contentPresenter = cp;
                return contentPresenter?.Content is null;
            }
        }

        #region InternalThickness Property
        internal static readonly DependencyProperty InternalThicknessProperty = DependencyProperty.Register(
            nameof(InternalThickness),
            typeof(Thickness),
            typeof(BubbleBorder),
            new PropertyMetadata(new Thickness(1), new PropertyChangedCallback(OnInternalThicknessChanged))
        );
        protected static void OnInternalThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BubbleBorder BubbleBorder)
            {
                BubbleBorder._path.StrokeThickness = ((Thickness)e.NewValue).Average();
            }
        }
        internal Thickness InternalThickness
        {
            get => (Thickness)GetValue(InternalThicknessProperty);
            set => SetValue(InternalThicknessProperty, value);
        }
        #endregion InternalThickness Property


        #endregion


        #region Fields
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Left;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Top;
        ContentPresenter _contentPresenter;
        Windows.UI.Xaml.Shapes.Path _path;
        #endregion


        #region Construction / Loading
        public BubbleBorder()
        {
            base.Padding = new Thickness(0);
            base.Margin = new Thickness(0);
            base.HorizontalAlignment = DefaultHorizontalAlignment;
            base.VerticalAlignment = DefaultVerticalAlignment;
            Build();
            UpdateContentPresenterMargin();
        }

        #endregion


        #region LayoutUpdate
        void UpdateContentPresenterMargin()
        {
            var result = new Thickness(Padding.Left + BorderThickness.Left, Padding.Top + BorderThickness.Top, Padding.Right + BorderThickness.Right, Padding.Bottom + BorderThickness.Bottom);
            switch ((int)PointerDirection)
            {
                case (int)PointerDirection.Left:
                    result.Left += PointerLength;
                    break;
                case (int)PointerDirection.Up:
                    result.Top += PointerLength;
                    break;
                case (int)PointerDirection.Right:
                    result.Right += PointerLength;
                    break;
                case (int)PointerDirection.Down:
                    result.Bottom += PointerLength;
                    break;
                default:
                    break;
            }
            _contentPresenter.Margin = result;
        }

        void RegeneratePath(Size size = default)
        { 
            if (_path is Windows.UI.Xaml.Shapes.Path p)
                p.StrokeThickness = BorderThickness.Average();
            var path = GeneratePath(size);
            var data = path.ToSvgPathData();
#if __WASM__
            //System.Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            var x = _path.GetFirstHtmlDescendent();
            x.SetHtmlContent($"<path fill-rule=\"even-odd\" d=\"{data}\"></path>");
#else
            //System.Console.WriteLine($"BubbleBorder.RegeneratePath [{data}]");
            //PathGeometry = P42.Utils.Uno.StringToPathGeometryConverter.Current.Convert(data);
            _path.Data = P42.Utils.Uno.StringToPathGeometryConverter.Current.Convert(data);
#endif
        }


        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Size = args.NewSize;
        }
        
        protected override Size MeasureOverride(Size availableSize)
        {
            if (IsEmpty)
                return new Size(50 + Padding.Horizontal(), 50 + Padding.Vertical());

            UpdateContentPresenterMargin();

            var windowSize = AppWindow.Size(this);
            var windowWidth = windowSize.Width;
            var windowHeight = windowSize.Height;

            if (!this.HasPrescribedWidth())
                availableSize.Width = windowWidth;
            if (!this.HasPrescribedHeight())
                availableSize.Height = windowHeight;

            base.MeasureOverride(availableSize);

            Size result = Size.Empty;
            if (_contentPresenter is FrameworkElement element)
                result = element.DesiredSize;

            if (HorizontalAlignment == HorizontalAlignment.Stretch || this.HasPrescribedWidth())
                result.Width = availableSize.Width;
            if (VerticalAlignment == VerticalAlignment.Stretch || this.HasPrescribedHeight())
                result.Height = availableSize.Height;

            RegeneratePath(result);
            return result;
        }

        float scale = -1;
        SKPath GeneratePath(Size measuredSize = default)
        {
            var windowSize = AppWindow.Size(this);
            var windowWidth = windowSize.Width;
            var windowHeight = windowSize.Height;

            var width = (float)Math.Min(DesiredSize.Width, windowWidth - Margin.Horizontal());
            var height = (float)Math.Min(DesiredSize.Height, windowHeight - Margin.Vertical());

            if (measuredSize != default)
            {
                width = (float)measuredSize.Width;
                height = (float)measuredSize.Height;
            }

            if (width < 1 || height < 1)
                return new SKPath();

            var strokeColor = SKColors.Transparent;
            if (BorderBrush is SolidColorBrush borderBrush && borderBrush.Color is Color winStrokeColor)
                strokeColor = new SKColor(winStrokeColor.R, winStrokeColor.G, winStrokeColor.B, winStrokeColor.A); //borderBrush.Color.ToSKColor();
            var borderWidth = 0.0f;
            if (strokeColor.Alpha > 0 && BorderThickness.Left > 0)
                borderWidth = (float)BorderThickness.Left;

            var pointerLength = PointerDirection == PointerDirection.None ? 0 : (float)PointerLength;

            var left = 0.0f + borderWidth / 2;
            var top = 0.0f + borderWidth / 2;

//#if __ANDROID__
            
            if (scale < 0)
                scale = (float)AppWindow.DisplayScale(this);

            var right = (float)(width - Margin.Horizontal() - borderWidth * scale);
            var bottom = (float)(height - Margin.Vertical() - borderWidth * scale);
//#else
//            var right = (float)(width - Margin.Horizontal() - borderWidth / 2);
//            var bottom = (float)(height - Margin.Vertical() - borderWidth / 2);
//
//#endif

            width -= (PointerDirection.IsHorizontal() ? pointerLength : 0);
            height -= (PointerDirection.IsVertical() ? pointerLength : 0);

            var cornerRadius = (float)((CornerRadius.TopLeft + CornerRadius.TopRight + CornerRadius.BottomLeft + CornerRadius.BottomRight)/4.0);

            if (cornerRadius * 2 > width)
                cornerRadius = width / 2.0f;
            if (cornerRadius * 2 > height)
                cornerRadius = height / 2.0f;


            var filetRadius = (float)PointerCornerRadius;
            var tipRadius = (float)PointerTipRadius;

            if (filetRadius / 2.0 + tipRadius / 2.0 > pointerLength)
            {
                filetRadius = 2 * (pointerLength - tipRadius / 2.0f);
                if (filetRadius < 0)
                {
                    filetRadius = 0;
                    tipRadius = 2 * pointerLength;
                }
            }

            if (pointerLength - filetRadius / 2.0 < tipRadius / 2.0)
                tipRadius = 2 * (pointerLength - filetRadius / 2.0f);

            var result = new SKPath();
            var pointerPosition = (float)PointerAxialPosition;
            if (pointerPosition <= 1.0)
                pointerPosition = (float)(PointerDirection == PointerDirection.Down || PointerDirection == PointerDirection.Up
                    ? left + (right - left) * pointerPosition
                    : top + (bottom - top)  * pointerPosition);


            const float sqrt3 = (float)1.732050807568877;
            const float sqrt3d2 = (float)0.86602540378444;

            var tipCornerHalfWidth = tipRadius * sqrt3d2;
            var pointerToCornerIntercept = (float)Math.Sqrt((2 * cornerRadius * Math.Sin(Math.PI / 12.0)) * (2 * cornerRadius * Math.Sin(Math.PI / 12.0)) - (cornerRadius * cornerRadius / 4.0));

            var pointerAtLimitSansTipHalfWidth = (float)(pointerToCornerIntercept + cornerRadius / (2.0 * sqrt3) + (pointerLength - tipRadius / 2.0) / sqrt3);
            var pointerAtLimitHalfWidth = pointerAtLimitSansTipHalfWidth + tipRadius * sqrt3d2;

            var pointerSansFiletHalfWidth = (float)(tipCornerHalfWidth + (pointerLength - filetRadius / 2.0 - tipRadius / 2.0) / sqrt3);
            var pointerFiletWidth = filetRadius * sqrt3d2;
            var pointerAndFiletHalfWidth = pointerSansFiletHalfWidth + pointerFiletWidth;

            var dir = 1;

            if (pointerLength <= 1)
            {
                result.MoveTo(right - cornerRadius, top);
                result.ArcTo(new SKRect(right - 2 * cornerRadius, top, right, top + 2 * cornerRadius), 270, 90, false);
                result.ArcTo(new SKRect(right - 2 * cornerRadius, bottom - 2 * cornerRadius, right, bottom), 0, 90, false);
                result.ArcTo(new SKRect(left, bottom - 2 * cornerRadius, left + 2 * cornerRadius, bottom), 90, 90, false);
                result.ArcTo(new SKRect(left, top, left + 2 * cornerRadius, top + 2 * cornerRadius), 180, 90, false);
                result.Close();
            }
            else if (PointerDirection.IsHorizontal())
            {
                var start = left;
                var end = right;
                if (PointerDirection == PointerDirection.Right)
                {
                    dir = -1;
                    start = right;
                    end = left;
                }
                var baseX = start + dir * pointerLength;

                var tipY = Math.Min(pointerPosition, (float)(bottom - PointerTipRadius * sqrt3d2));
                tipY = Math.Max(tipY, top + (float)PointerTipRadius * sqrt3d2);
                if (height <= 2 * pointerAtLimitHalfWidth)
                    tipY = (float)((top + bottom) / 2.0);
                result.MoveTo(start + dir * (pointerLength + cornerRadius), top);
                result.ArcWithCenterTo(
                    end - dir * cornerRadius,
                    top + cornerRadius,
                    cornerRadius, 270, dir * 90);
                result.ArcWithCenterTo(
                    end - dir * cornerRadius,
                    bottom - cornerRadius,
                    cornerRadius, 90 - dir * 90, dir * 90);
                result.LineTo(start + dir * (pointerLength + cornerRadius), bottom);

                // bottom half
                if (tipY > bottom - pointerAndFiletHalfWidth - cornerRadius)
                {
                    result.LineTo(start + dir * (pointerLength + cornerRadius), bottom);
                    var endRatio = (float)((height - tipY) / (pointerAndFiletHalfWidth + cornerRadius));
                    result.CubicTo(
                        start + dir * (pointerLength + cornerRadius - endRatio * 4 * cornerRadius / 3.0f), 
                        bottom,
                        start + dir * (pointerLength - filetRadius / 2.0f + filetRadius * sqrt3d2), 
                        Math.Min(tipY + pointerSansFiletHalfWidth + filetRadius / 2.0f, bottom),
                        start + dir * (pointerLength - filetRadius / 2.0f), 
                        Math.Min(tipY + pointerSansFiletHalfWidth,bottom));
                }
                else
                {
                    result.ArcWithCenterTo(
                        start + dir * (pointerLength + cornerRadius),
                        bottom - cornerRadius,
                        cornerRadius, 90, dir * 90
                        );
                    result.ArcWithCenterTo(
                        start + dir * (pointerLength - filetRadius), 
                        Math.Max(tipY + pointerAndFiletHalfWidth, top + 2 * pointerAndFiletHalfWidth),
                        filetRadius, 90 - 90 * dir, dir * -60);
                }

                //tip
                result.ArcWithCenterTo(
                    start + dir * tipRadius, 
                    Math.Max(Math.Min(tipY, bottom), top),
                    tipRadius, 90 + dir * 30, dir * 2 * 60);

                // top half
                if (tipY < top + pointerAndFiletHalfWidth + cornerRadius)
                {
                    var startRatio = tipY / (pointerAndFiletHalfWidth + cornerRadius);
                    result.CubicTo(
                        start + dir * (pointerLength - filetRadius / 2.0f + filetRadius * sqrt3d2), 
                        Math.Max(tipY - pointerSansFiletHalfWidth - filetRadius / 2.0f, top),
                        start + dir * (pointerLength + cornerRadius - startRatio * 4 * cornerRadius / 3.0f), 
                        top,
                        start + dir * (pointerLength + cornerRadius), 
                        top);
                }
                else
                {
                    result.ArcWithCenterTo(
                        start + dir * (pointerLength - filetRadius), 
                        Math.Min(tipY - pointerAndFiletHalfWidth, bottom - pointerAndFiletHalfWidth * 2),
                        filetRadius, 90 - dir * 30, dir * -60);
                    result.ArcWithCenterTo(
                        start + dir * (pointerLength + cornerRadius), 
                        top + cornerRadius, 
                        cornerRadius, 90 + dir * 90, dir * 90);
                }

                if (dir > 0)
                {
                    var reverse = new SKPath();
                    reverse.AddPathReverse(result);
                    return reverse;
                }
            }
            else
            {
                var start = top;
                var end = bottom;
                if (PointerDirection == PointerDirection.Down)
                {
                    dir = -1;
                    start = bottom;
                    end = top;
                }
                var tipX = Math.Min(pointerPosition, (float)(right -  PointerTipRadius * sqrt3d2));     // 1
                tipX = Math.Max(tipX, left + (float)PointerTipRadius * sqrt3d2);                               // 1
                if (width <= 2 * pointerAtLimitHalfWidth)
                    tipX = (float)((left + right) / 2.0);   // 8
                result.MoveTo(left, start + dir * (pointerLength + cornerRadius));
                //result.LineTo(left, end - dir * cornerRadius);
                result.ArcWithCenterTo(
                    left + cornerRadius,
                    end - dir * cornerRadius,
                    cornerRadius, 180, dir * -90
                    );
                result.ArcWithCenterTo(
                    right - cornerRadius,
                    end - dir * cornerRadius,
                    cornerRadius, 180 - dir * 90, dir * -90);
                result.LineTo(right, start + dir * (pointerLength + cornerRadius));                             //2

                // right half
                if (tipX > right - pointerAndFiletHalfWidth - cornerRadius)
                {
                    var endRatio = (float)((right - tipX) / (pointerAndFiletHalfWidth + cornerRadius));
                    result.CubicTo(
                        right, 
                        start + dir * (pointerLength + cornerRadius - endRatio * 4 * cornerRadius / 3.0f),
                        Math.Min(tipX + pointerSansFiletHalfWidth + filetRadius / 2.0f, right), 
                        start + dir * (pointerLength - filetRadius / 2.0f + filetRadius * sqrt3d2),   //3
                        Math.Min(tipX + pointerSansFiletHalfWidth, right), 
                        start + dir * (pointerLength - filetRadius / 2.0f)                                                 // 3
                        );
                }
                else
                {
                    result.ArcWithCenterTo(
                        right - cornerRadius,
                        start + dir * (pointerLength + cornerRadius),
                        cornerRadius, 0, dir * -90
                        );
                    result.ArcWithCenterTo(
                        Math.Max(tipX + pointerAndFiletHalfWidth, left + 2 * pointerAndFiletHalfWidth), // 5
                        start + dir * (pointerLength - filetRadius),
                        filetRadius, dir * 90, dir * 60
                        );
                }
                //tip
                result.ArcWithCenterTo(
                    Math.Max(Math.Min(tipX, right ) , left ), // 7
                    start + dir * tipRadius,
                    tipRadius,
                    dir * -30,
                    dir * -2 * 60
                    );


                // left half
                if (tipX < left + pointerAndFiletHalfWidth + cornerRadius)  // 6
                {
                    var startRatio = tipX / (pointerAndFiletHalfWidth + cornerRadius);
                    result.CubicTo(
                            Math.Max(tipX - pointerSansFiletHalfWidth - filetRadius / 2.0f, left), //6
                            start + dir * (pointerLength - filetRadius / 2.0f + filetRadius * sqrt3d2),
                            left,
                            start + dir * (pointerLength + cornerRadius - startRatio * 4 * cornerRadius / 3.0f),
                            left,
                            start + dir * (pointerLength + cornerRadius)
                        );
                }
                else
                {
                   result.ArcWithCenterTo(
                        Math.Min(tipX - pointerAndFiletHalfWidth, right - pointerAndFiletHalfWidth * 2),  // 4
                        start + dir * (pointerLength - filetRadius),
                        filetRadius, dir * 30, dir * 60 );
                    result.ArcWithCenterTo(
                        left + cornerRadius,
                        start + dir * (pointerLength + cornerRadius),
                        cornerRadius,dir * -90,dir * -90);
                }
                if (dir < 0)
                {
                    var reverse = new SKPath();
                    reverse.AddPathReverse(result);
                    return reverse;
                }
            }
            return result;
        }

#endregion
    }

}
