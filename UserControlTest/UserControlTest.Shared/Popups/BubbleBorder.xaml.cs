using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SkiaSharp;
using SkiaSharp.Views.UWP;
using Windows.Graphics.Display;
using Windows.UI;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UserControlTest.Popups
{
    [TemplatePart(Name = ContentPresenterName, Type=typeof(ContentPresenter))]
    public partial class BubbleBorder : ContentControl
    {
        #region Properties

        #region TargetBias Property
        public static readonly DependencyProperty TargetBiasProperty = DependencyProperty.Register(
            nameof(TargetBias),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(0.5, new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnTargetBiasChanged(e)))
        );
        protected virtual void OnTargetBiasChanged(DependencyPropertyChangedEventArgs e)
        {
            RegeneratePath();
        }
        public double TargetBias
        {
            get => (double)GetValue(TargetBiasProperty);
            set => SetValue(TargetBiasProperty, value);
        }
        #endregion TargetBias Property


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
            new PropertyMetadata(PointerDirection.Any, new PropertyChangedCallback((d, e) => ((BubbleBorder)d).OnPointerDirectionChanged(e)))
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
        public static readonly DependencyProperty ContentPresenterMarginProperty = DependencyProperty.Register(
            nameof(ContentPresenterMargin),
            typeof(Thickness),
            typeof(BubbleBorder),
            new PropertyMetadata(default(Thickness))
        );
        public Thickness ContentPresenterMargin
        {
            get => (Thickness)GetValue(ContentPresenterMarginProperty);
            set => SetValue(ContentPresenterMarginProperty, value);
        }
        #endregion ContentPresenterMargin Property


        #region OutsideCornersRadius Property
        public static readonly DependencyProperty OutsideCornersRadiusProperty = DependencyProperty.Register(
            nameof(OutsideCornersRadius),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(5.0)
        );
        public double OutsideCornersRadius
        {
            get => (double)GetValue(OutsideCornersRadiusProperty);
            set => SetValue(OutsideCornersRadiusProperty, value);
        }
        #endregion CornerRadius Property


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


        #region Fields
        const string ContentPresenterName = "ContentPresenter";
        ContentPresenter _contentPresenter;
        TextBlock textBlock = new TextBlock();
        #endregion


        #region Construction / Loading
        public BubbleBorder()
        {
            this.InitializeComponent();
        }

        protected override void OnApplyTemplate()
        {
            _contentPresenter = GetTemplateChild(ContentPresenterName) as ContentPresenter;
        }
        #endregion


        #region LayoutUpdate
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

        void UpdateContentPresenterMargin()
        {
            var result = new Thickness(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
            switch (PointerDirection)
            {
                case PointerDirection.Left:
                    result.Left += PointerLength;
                    break;
                case PointerDirection.Up:
                    result.Top += PointerLength;
                    break;
                case PointerDirection.Right:
                    result.Right += PointerLength;
                    break;
                case PointerDirection.Down:
                    result.Bottom += PointerLength;
                    break;
            }
            ContentPresenterMargin = result;
        }

        void RegeneratePath(Size size = default)
        {
            var path = GeneratePath(size);
            var data = path.ToSvgPathData();
            PathGeometry = PathConverter.StringToPathGeometryConverter.Current.Convert(data);
        }

        Size ContentPresenterSize(Size availableSize=default)
        {
            Size result = Size.Empty;
            if (_contentPresenter is FrameworkElement element)
            {
                if (availableSize != default)
                    element.Measure(availableSize);
                result = element.DesiredSize;
                System.Diagnostics.Debug.WriteLine(GetType() + ".MeasureOverride element.DesiredSize:" + result);
            }
            result.Width += PointerDirection.IsHorizontal() ? PointerLength : 0;
            result.Height += PointerDirection.IsVertical() ? PointerLength : 0;
            return result;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            base.MeasureOverride(availableSize);
            var result = ContentPresenterSize(availableSize);
            RegeneratePath(result);
            return result;
        }

        SKPath GeneratePath(Size measuredSize = default)
        {
            var width = (float)Math.Min(DesiredSize.Width, MinWidth);
            var height = (float)Math.Min(DesiredSize.Height, MinHeight);

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

            var length = PointerDirection == PointerDirection.None ? 0 : (float)PointerLength;
            width -= (PointerDirection.IsHorizontal() ? length : 0);
            height -= (PointerDirection.IsVertical() ? length : 0);

            var radius = (float)OutsideCornersRadius;

            if (radius * 2 > width)
                radius = width / 2.0f;
            if (radius * 2 > height)
                radius = height / 2.0f;


            var filetRadius = (float)PointerCornerRadius;
            var tipRadius = (float)PointerTipRadius;

            if (filetRadius / 2.0 + tipRadius / 2.0 > length)
            {
                filetRadius = 2 * (length - tipRadius / 2.0f);
                if (filetRadius < 0)
                {
                    filetRadius = 0;
                    tipRadius = 2 * length;
                }
            }

            if (length - filetRadius / 2.0 < tipRadius / 2.0)
                tipRadius = 2 * (length - filetRadius / 2.0f);

            var result = new SKPath();
            var position = (float)PointerAxialPosition;
            if (position <= 1.0)
                position = (float)(PointerDirection == PointerDirection.Down || PointerDirection == PointerDirection.Up
                    ? width * position
                    : height * position);

            var left = 0.0f + borderWidth / 2;
            var right = width - borderWidth / 2;
            var top = 0.0f + borderWidth / 2;
            var bottom = height - borderWidth / 2;

            const float sqrt3 = (float)1.732050807568877;
            const float sqrt3d2 = (float)0.86602540378444;

            var tipCornerHalfWidth = tipRadius * sqrt3d2;
            var pointerToCornerIntercept = (float)Math.Sqrt((2 * radius * Math.Sin(Math.PI / 12.0)) * (2 * radius * Math.Sin(Math.PI / 12.0)) - (radius * radius / 4.0));

            var pointerAtLimitSansTipHalfWidth = (float)(pointerToCornerIntercept + radius / (2.0 * sqrt3) + (length - tipRadius / 2.0) / sqrt3);
            var pointerAtLimitHalfWidth = pointerAtLimitSansTipHalfWidth + tipRadius * sqrt3d2;

            var pointerSansFiletHalfWidth = (float)(tipCornerHalfWidth + (length - filetRadius / 2.0 - tipRadius / 2.0) / sqrt3);
            var pointerFiletWidth = filetRadius * sqrt3d2;
            var pointerAndFiletHalfWidth = pointerSansFiletHalfWidth + pointerFiletWidth;

            var dir = 1;

            if (PointerDirection == PointerDirection.None)
            {
                result.MoveTo(right - radius, top);
                result.ArcTo(right, top, right, bottom - radius, radius);
                result.ArcTo(right, bottom, left + radius, bottom, radius);
                result.ArcTo(left, bottom, left, top + radius, radius);
                result.ArcTo(left, top, right - radius, top, radius);
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
                var baseX = start + dir * length;

                var tipY = Math.Min(position, (float)(height - pointerAtLimitHalfWidth));
                tipY = Math.Max(tipY, pointerAtLimitHalfWidth);

                if (height <= 2 * pointerAtLimitHalfWidth)
                    tipY = (float)(height / 2.0);

                result.MoveTo(start + dir * (length + radius), top);
                result.ArcTo(end, top, end, bottom, radius);
                result.ArcTo(end, bottom, start, bottom, radius);

                // bottom half
                if (tipY >= height - pointerAndFiletHalfWidth - radius)
                {
                    result.LineTo(start + dir * (length + radius), bottom);
                    var endRatio = (float)((height - tipY) / (pointerAndFiletHalfWidth + radius));
                    result.CubicTo(
                        start + dir * (length + radius - endRatio * 4 * radius / 3.0f), bottom,
                        start + dir * (length - filetRadius / 2.0f + filetRadius * sqrt3d2), top + tipY + pointerSansFiletHalfWidth + filetRadius / 2.0f,
                        start + dir * (length - filetRadius / 2.0f), top + tipY + pointerSansFiletHalfWidth);
                }
                else
                {
                    result.ArcTo(baseX, bottom, baseX, top, radius);
                    result.ArcWithCenterTo(start + dir * (length - filetRadius), top + tipY + pointerAndFiletHalfWidth, filetRadius, 90 - 90 * dir, dir * -60);
                }

                //tip
                result.ArcWithCenterTo(start + dir * tipRadius, top + tipY, tipRadius, 90 + dir * 30, dir * 2 * 60);

                // top half
                if (tipY <= pointerAndFiletHalfWidth + radius)
                {
                    var startRatio = tipY / (pointerAndFiletHalfWidth + radius);
                    result.CubicTo(
                        start + dir * (length - filetRadius / 2.0f + filetRadius * sqrt3d2), top + tipY - pointerSansFiletHalfWidth - filetRadius / 2.0f,
                        start + dir * (length + radius - startRatio * 4 * radius / 3.0f), top,
                        start + dir * (length + radius), top);
                }
                else
                {
                    result.ArcWithCenterTo(start + dir * (length - filetRadius), top + tipY - pointerAndFiletHalfWidth, filetRadius, 90 - dir * 30, dir * -60);
                    result.ArcWithCenterTo(start + dir * (length + radius), top + radius, radius, 90 + dir * 90, dir * 90);
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
                var tip = Math.Min(position, (float)(width - pointerAtLimitHalfWidth));
                tip = Math.Max(tip, pointerAtLimitHalfWidth);
                if (width <= 2 * pointerAtLimitHalfWidth)
                    tip = (float)(width / 2.0);
                result.MoveTo(left, start + dir * (length + radius));
                result.ArcTo(left, end, right, end, radius);
                result.ArcTo(right, end, right, start, radius);

                // right half
                if (tip > width - pointerAndFiletHalfWidth - radius)
                {
                    var endRatio = (float)((width - tip) / (pointerAndFiletHalfWidth + radius));
                    result.CubicTo(
                        right, start + dir * (length + radius - endRatio * 4 * radius / 3.0f),
                        left + tip + pointerSansFiletHalfWidth + filetRadius / 2.0f, start + dir * (length - filetRadius / 2.0f + filetRadius * sqrt3d2),
                        left + tip + pointerSansFiletHalfWidth, start + dir * (length - filetRadius / 2.0f)
                        );
                }
                else
                {
                    result.ArcWithCenterTo(
                        right - radius,
                        start + dir * (length + radius),
                        radius, 0, dir * -90
                        );
                    result.ArcWithCenterTo(
                        left + tip + pointerAndFiletHalfWidth,
                        start + dir * (length - filetRadius),
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
                if (tip < pointerAndFiletHalfWidth + radius)
                {
                    var startRatio = tip / (pointerAndFiletHalfWidth + radius);
                    result.CubicTo(
                            left + tip - pointerSansFiletHalfWidth - filetRadius / 2.0f,
                            start + dir * (length - filetRadius / 2.0f + filetRadius * sqrt3d2),
                            left,
                            start + dir * (length + radius - startRatio * 4 * radius / 3.0f),
                            left,
                            start + dir * (length + radius)
                        );
                }
                else
                {
                   result.ArcWithCenterTo(
                        left + tip - pointerAndFiletHalfWidth,
                        start + dir * (length - filetRadius),
                        filetRadius,
                        dir * 30,
                        dir * 60
                        );
                    result.ArcWithCenterTo(
                        left + radius,
                        start + dir * (length + radius),
                        radius,
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
