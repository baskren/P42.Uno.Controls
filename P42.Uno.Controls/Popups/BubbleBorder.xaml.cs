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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace P42.Uno.Controls
{
    [TemplatePart(Name = ContentPresenterName, Type=typeof(ContentPresenter))]
    [TemplatePart(Name = PathElementName, Type = typeof(Path))]
    [TemplatePart(Name = DropShadowPanelElementName, Type = typeof(ElevatedView))]
    public partial class BubbleBorder : ContentControl
    {
        #region Properties

        #region Override Properties

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


        #region ContentPresenterMargin Property
        internal static readonly DependencyProperty ContentPresenterMarginProperty = DependencyProperty.Register(
            nameof(ContentPresenterMargin),
            typeof(Thickness),
            typeof(BubbleBorder),
            new PropertyMetadata(default(Thickness))
        );
        internal Thickness ContentPresenterMargin
        {
            get => (Thickness)GetValue(ContentPresenterMarginProperty);
            set => SetValue(ContentPresenterMarginProperty, value);
        }
        #endregion ContentPresenterMargin Property


        #region PathGeometry Property
        internal static readonly DependencyProperty PathGeometryProperty = DependencyProperty.Register(
            nameof(PathGeometry),
            typeof(PathGeometry),
            typeof(BubbleBorder),
            new PropertyMetadata(new PathGeometry())
        );
        internal PathGeometry PathGeometry
        {
            get => (PathGeometry)GetValue(PathGeometryProperty);
            set => SetValue(PathGeometryProperty, value);
        }
        #endregion PathGeometry Property


        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(BubbleBorder),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnHasShadowChanged(e)))
        );
        protected virtual void OnHasShadowChanged(DependencyPropertyChangedEventArgs e)
        {
            ShadowOpacity = HasShadow ? 0.5 : 0;
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

        #endregion


        #region Fields
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Left;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Top;
        const string ContentPresenterName = nameof(_contentPresenter);
        const string PathElementName = nameof(_path);
        const string DropShadowPanelElementName = nameof(_dropShadow);
        ContentPresenter _contentPresenter;
        Windows.UI.Xaml.Shapes.Path _path;
        ElevatedView _dropShadow;
        #endregion


        #region Construction / Loading
        public BubbleBorder()
        {
            base.HorizontalAlignment = DefaultHorizontalAlignment;
            base.VerticalAlignment = DefaultVerticalAlignment;
            this.InitializeComponent();
            UpdateContentPresenterMargin();
        }


        protected override void OnApplyTemplate()
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".OnApplyTemplate ==============================================================");
            var contentPresenter = GetTemplateChild(ContentPresenterName);
            _contentPresenter = contentPresenter as ContentPresenter;
            //System.Diagnostics.Debug.WriteLine(GetType() + "\t contentPresenter.GetType: " + contentPresenter?.GetType());

            var path = GetTemplateChild(PathElementName);
            _path = path as Windows.UI.Xaml.Shapes.Path;
            //System.Diagnostics.Debug.WriteLine(GetType() + "\t path.GetType: " + path?.GetType());

            var dropShadow = GetTemplateChild(DropShadowPanelElementName);
            _dropShadow = dropShadow as ElevatedView;
            //System.Diagnostics.Debug.WriteLine(GetType() + "\t dropShadow.GetType: " + dropShadow?.GetType());
            //System.Diagnostics.Debug.WriteLine(GetType() + ".OnApplyTemplate ==============================================================");
        }
        #endregion


        #region LayoutUpdate
        /*
        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            // the canvas and properties
            var canvas = e.Surface.Canvas;

            // get the screen density for scaling
            var display = DisplayInformation.GetForCurrentView();
            var scale = display.LogicalDpi / 96.0f;
            //var scaledSize = new SKSize(e.Info.Width / scale, e.Info.Height / scale);

            // handle the device screen density
            canvas.Scale(scale);

            // make sure the canvas is blank
            canvas.Clear(SKColors.Yellow);

            SKColor fillColor =  SKColors.Transparent;
            if (Background is SolidColorBrush backgroundBrush && backgroundBrush.Color is Color winBackgroundColor)
                fillColor = new SKColor(winBackgroundColor.R, winBackgroundColor.G, winBackgroundColor.B, winBackgroundColor.A); // backgroundBrush.Color.ToSKColor();

            SKColor strokeColor = SKColors.Transparent;
            if (BorderBrush is SolidColorBrush borderBrush && borderBrush.Color is Color winStrokeColor)
                strokeColor = new SKColor(winStrokeColor.R, winStrokeColor.G, winStrokeColor.B, winStrokeColor.A); //borderBrush.Color.ToSKColor();

            //var scaledSize = new Size(DesiredSize.Width, DesiredSize.Height);
            var borderSize = ContentPresenterSize();             
            var path = GeneratePath(ContentPresenterSize());

            // draw some text
            var paint = new SKPaint
            {
                Color = fillColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
            };
            if (fillColor.Alpha > 0)
                canvas.DrawPath(path, paint);

            if (strokeColor.Alpha > 0 && BorderThickness.Left > 0)
            {
                paint.Color = strokeColor;
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = (float)(BorderThickness.Left);
                canvas.DrawPath(path, paint);
            }
        }
        */

        void UpdateContentPresenterMargin()
        {
            var result = new Thickness(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
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
            ContentPresenterMargin = result;
        }

        void RegeneratePath(Size size = default)
        {
            var path = GeneratePath(size);
            var data = path.ToSvgPathData();
            PathGeometry = P42.Utils.Uno.StringToPathGeometryConverter.Current.Convert(data);
        }

        
        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".OnSizeChanged()");
            //RegeneratePath(DesiredSize);
            Size = args.NewSize;
        }
        
        protected override Size MeasureOverride(Size availableSize)
        {
            if (IsEmpty)
                return new Size(50 + Padding.Horizontal(), 50 + Padding.Vertical());

            UpdateContentPresenterMargin();
            //System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride(" + availableSize + ") ======= hzAlign: " + HorizontalAlignment + " ======= margin: " + Margin + " ====== WindowSize: " + AppWindow.Size());

            var windowWidth = AppWindow.Size().Width;
            var windowHeight = AppWindow.Size().Height;

            //System.Diagnostics.Debug.WriteLine("\t ContentPresenterMargin: " + ContentPresenterMargin);
            //this.DebugLogProperties();


            if (double.IsInfinity(availableSize.Width))
                availableSize.Width = windowWidth;
            if (double.IsInfinity(availableSize.Height))
                availableSize.Height = windowHeight;

            base.MeasureOverride(availableSize);
            Size result = Size.Empty;
            if (_contentPresenter is FrameworkElement element)
            {
                if (availableSize != default)
                    element.Measure(availableSize);
                result = element.DesiredSize;
                //System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride element.DesiredSize:" + result);
            }
            //System.Diagnostics.Debug.WriteLine("\t HorizontalAlignment: " + HorizontalAlignment);
            if (HorizontalAlignment == HorizontalAlignment.Stretch)
                result.Width = availableSize.Width;
            if (VerticalAlignment == VerticalAlignment.Stretch)
                result.Height = availableSize.Height;


            result.Width = Math.Max(0,Math.Min(windowWidth - Margin.Horizontal(), result.Width));
            result.Height = Math.Max(0,Math.Min(windowHeight - Margin.Vertical(), result.Height));

            var borderSize = result;
            borderSize.Width += Margin.Horizontal();
            borderSize.Height += Margin.Vertical();

            RegeneratePath(borderSize);


#if NETFX_CORE
#else
            // the following fixes the DropShadowPanel clipping issue
            _dropShadow?.Measure(borderSize);
#endif
            //System.Diagnostics.Debug.WriteLine("\t RESULT: " + result);
            return result;
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".ArrangeOverride(" + finalSize + ")");

            if (_contentPresenter.Content != null)
                return base.ArrangeOverride(finalSize);
            else
                return finalSize;
        }

        SKPath GeneratePath(Size measuredSize = default)
        {
            var windowWidth = AppWindow.Size().Width;
            var windowHeight = AppWindow.Size().Height;

            var width = (float)Math.Min(DesiredSize.Width, windowWidth - Margin.Horizontal());
            var height = (float)Math.Min(DesiredSize.Height, windowHeight - Margin.Vertical());

            if (measuredSize != default)
            {
                width = (float)measuredSize.Width;
                height = (float)measuredSize.Height;
            }

            if (width < 1 || height < 1)
                return new SKPath();

            SKColor strokeColor = SKColors.Transparent;
            if (BorderBrush is SolidColorBrush borderBrush && borderBrush.Color is Color winStrokeColor)
                strokeColor = new SKColor(winStrokeColor.R, winStrokeColor.G, winStrokeColor.B, winStrokeColor.A); //borderBrush.Color.ToSKColor();
            float borderWidth = 0.0f;
            if (strokeColor.Alpha > 0 && BorderThickness.Left > 0)
                borderWidth = (float)BorderThickness.Left;

            var pointerLength = PointerDirection == PointerDirection.None ? 0 : (float)PointerLength;

            var left = 0.0f + borderWidth / 2;
            var right = (float)(width - Margin.Left - Margin.Right - borderWidth / 2);
            var top = 0.0f + borderWidth / 2;
            var bottom = (float)(height - Margin.Top - Margin.Bottom - borderWidth / 2);

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
                result.ArcTo(right, top, right, bottom - cornerRadius, cornerRadius);
                result.ArcTo(right, bottom, left + cornerRadius, bottom, cornerRadius);
                result.ArcTo(left, bottom, left, top + cornerRadius, cornerRadius);
                result.ArcTo(left, top, right - cornerRadius, top, cornerRadius);
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

                var tipY = Math.Min(pointerPosition, (float)(height - pointerAtLimitHalfWidth));
                tipY = Math.Max(tipY, pointerAtLimitHalfWidth);

                if (height <= 2 * pointerAtLimitHalfWidth)
                    tipY = (float)(height / 2.0);

                result.MoveTo(start + dir * (pointerLength + cornerRadius), top);
                result.ArcTo(end, top, end, bottom, cornerRadius);
                result.ArcTo(end, bottom, start, bottom, cornerRadius);

                // bottom half
                if (tipY >= bottom - pointerAndFiletHalfWidth - cornerRadius)
                {
                    result.LineTo(start + dir * (pointerLength + cornerRadius), bottom);
                    var endRatio = (float)((height - tipY) / (pointerAndFiletHalfWidth + cornerRadius));
                    result.CubicTo(
                        start + dir * (pointerLength + cornerRadius - endRatio * 4 * cornerRadius / 3.0f), bottom,
                        start + dir * (pointerLength - filetRadius / 2.0f + filetRadius * sqrt3d2), top + tipY + pointerSansFiletHalfWidth + filetRadius / 2.0f,
                        start + dir * (pointerLength - filetRadius / 2.0f), top + tipY + pointerSansFiletHalfWidth);
                }
                else
                {
                    result.ArcTo(baseX, bottom, baseX, top, cornerRadius);
                    result.ArcWithCenterTo(start + dir * (pointerLength - filetRadius), top + tipY + pointerAndFiletHalfWidth, filetRadius, 90 - 90 * dir, dir * -60);
                }

                //tip
                result.ArcWithCenterTo(start + dir * tipRadius, top + tipY, tipRadius, 90 + dir * 30, dir * 2 * 60);

                // top half
                if (tipY <= pointerAndFiletHalfWidth + cornerRadius)
                {
                    var startRatio = tipY / (pointerAndFiletHalfWidth + cornerRadius);
                    result.CubicTo(
                        start + dir * (pointerLength - filetRadius / 2.0f + filetRadius * sqrt3d2), top + tipY - pointerSansFiletHalfWidth - filetRadius / 2.0f,
                        start + dir * (pointerLength + cornerRadius - startRatio * 4 * cornerRadius / 3.0f), top,
                        start + dir * (pointerLength + cornerRadius), top);
                }
                else
                {
                    result.ArcWithCenterTo(start + dir * (pointerLength - filetRadius), top + tipY - pointerAndFiletHalfWidth, filetRadius, 90 - dir * 30, dir * -60);
                    result.ArcWithCenterTo(start + dir * (pointerLength + cornerRadius), top + cornerRadius, cornerRadius, 90 + dir * 90, dir * 90);
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
                var tip = Math.Min(pointerPosition, (float)(width - pointerAtLimitHalfWidth));
                tip = Math.Max(tip, pointerAtLimitHalfWidth);
                if (width <= 2 * pointerAtLimitHalfWidth)
                    tip = (float)(width / 2.0);
                result.MoveTo(left, start + dir * (pointerLength + cornerRadius));
                result.ArcTo(left, end, left + cornerRadius, end, cornerRadius);
                result.ArcTo(right, end, right, start + cornerRadius, cornerRadius);

                // right half
                if (tip > right - pointerAndFiletHalfWidth - cornerRadius)
                {
                    var endRatio = (float)((width - tip) / (pointerAndFiletHalfWidth + cornerRadius));
                    /*
                    result.LineTo(right, (float)(start + dir * (pointerLength - PointerTipRadius)));
                    result.CubicTo(
                        right, start + dir * (pointerLength + cornerRadius - endRatio * 4 * cornerRadius / 3.0f),
                        left + tip + pointerSansFiletHalfWidth + filetRadius / 2.0f, start + dir * (pointerLength - filetRadius / 2.0f + filetRadius * sqrt3d2),
                        left + tip + pointerSansFiletHalfWidth, start + dir * (pointerLength - filetRadius / 2.0f)
                        );
                    */
                    //result.ArcTo(right, start + dir * (pointerLength), right - (float)PointerTipRadius, start + dir * (pointerLength), (float)PointerTipRadius);
                    result.LineTo(right, start + dir * (pointerLength - (float)PointerTipRadius));
                    //result.LineTo(right - (float)PointerTipRadius, start + dir * pointerLength);
                    //result.ArcWithCenterTo(
                    //    right - (float)PointerTipRadius,
                    //    start + dir * (pointerLength - (float)PointerTipRadius),
                    //    (float)PointerTipRadius, 0, dir * -120
                    //    );
                }
                else
                {
                    result.ArcWithCenterTo(
                        right - cornerRadius,
                        start + dir * (pointerLength + cornerRadius),
                        cornerRadius, 0, dir * -90
                        );
                    result.ArcWithCenterTo(
                        left + tip + pointerAndFiletHalfWidth,
                        start + dir * (pointerLength - filetRadius),
                        filetRadius, dir * 90, dir * 60
                        );
                }
                //tip
                result.ArcWithCenterTo(
                    left + tip,
                    start + dir * tipRadius,
                    tipRadius,
                    dir * -30,
                    dir * -2 * 60
                    );


                // left half
                if (tip < pointerAndFiletHalfWidth + cornerRadius)
                {
                    var startRatio = tip / (pointerAndFiletHalfWidth + cornerRadius);
                    result.CubicTo(
                            left + tip - pointerSansFiletHalfWidth - filetRadius / 2.0f,
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
                        left + tip - pointerAndFiletHalfWidth,
                        start + dir * (pointerLength - filetRadius),
                        filetRadius,
                        dir * 30,
                        dir * 60
                        );
                    result.ArcWithCenterTo(
                        left + cornerRadius,
                        start + dir * (pointerLength + cornerRadius),
                        cornerRadius,
                        dir * -90,
                        dir * -90
                        );
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
