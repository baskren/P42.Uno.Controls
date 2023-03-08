using System;
using Windows.Foundation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SkiaSharp;
using Windows.UI;
using P42.Uno.Markup;
using SkiaSharp.Views.Windows;
using Microsoft.UI;

namespace P42.Uno.Controls
{
    /// <summary>
    /// Border used by Popups
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class Bubble : SkiaSharp.Views.Windows.SKXamlCanvas
    {

        #region Properties

        #region UserControl Propeties

        private static void Redraw(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Bubble bubble)
                bubble.Invalidate();
        }



        #region BackgroundColor Property
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(Bubble),
            new PropertyMetadata(default(Color),Redraw)
        );

        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        #endregion BackgroundColor Property


        #region BorderColor Property
        public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
            nameof(BorderColor),
            typeof(Color),
            typeof(Bubble),
            new PropertyMetadata(default(Color), Redraw)
        );
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        #endregion BorderColor Property


        #region BorderWidth Property
        public static readonly DependencyProperty BorderWidthProperty = DependencyProperty.Register(
            nameof(BorderWidth),
            typeof(double),
            typeof(Bubble),
            new PropertyMetadata(1.0, Redraw)
        );
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
        #endregion BorderWidth Property


        #region CornerRadius Property
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(double),
            typeof(Bubble),
            new PropertyMetadata(4.0, Redraw)
        );
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        #endregion CornerRadius Property




        #endregion


        #region Pointer Properties


        #region PointerBias Property
        public static readonly DependencyProperty PointerBiasProperty = DependencyProperty.Register(
            nameof(PointerBias),
            typeof(double),
            typeof(Bubble),
            new PropertyMetadata(0.5, Redraw)
        );
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
            typeof(Bubble),
            new PropertyMetadata(10.0, Redraw)
        );
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
            typeof(Bubble),
            new PropertyMetadata(1.0, Redraw)
        );
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
            typeof(Bubble),
            new PropertyMetadata(0.5, Redraw)
        );
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
            typeof(Bubble),
            new PropertyMetadata(PointerDirection.None, Redraw)
        );
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
            typeof(Bubble),
            new PropertyMetadata(default(double), Redraw)
        );
        public double PointerCornerRadius
        {
            get => (double)GetValue(PointerCornerRadiusProperty);
            set => SetValue(PointerCornerRadiusProperty, value);
        }
        #endregion PointerCornerRadius Property

        #endregion

        #endregion


        #region Private Properties

        Color WorkingBackgroundColor => BackgroundColor == default
                ? SystemColors.ColorButtonFace
                : BackgroundColor;

        Color WorkingBorderColor => BorderColor == default
                ? SystemColors.ColorButtonText
                : BorderColor;
        #endregion


        #region Fields
        internal float BlurSigma = 3f;
        internal bool ApplyBlur = false;
        //TextBlock TextBlock = new TextBlock { Text = "TXT", Foreground=new SolidColorBrush(Colors.Black), HorizontalAlignment=HorizontalAlignment.Center, VerticalAlignment=VerticalAlignment.Center };
        #endregion

        /*
        public Bubble()
        {
            SizeChanged += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"Bubble.SizeChanged : [{e.NewSize}]");
            };

        }
        */
        /*
        protected override Size MeasureOverride(Size availableSize)
        {
            System.Diagnostics.Debug.WriteLine($"Bubble.MeasureOverride : ENTER");
            var result = base.MeasureOverride(availableSize);
            TextBlock.Measure(availableSize);
            System.Diagnostics.Debug.WriteLine($"Bubble.MeasureOverride : EXIT");
            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Diagnostics.Debug.WriteLine($"Bubble.ArrangeOverride : ENTER");
            var size = base.ArrangeOverride(finalSize);
            TextBlock.Arrange(new Rect(0,0,finalSize.Width,finalSize.Height));
            System.Diagnostics.Debug.WriteLine($"Bubble.ArrangeOverride : EXIT");
            return size;
        }
        */




        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"Bubble.OnPaintSurface : ENTER");
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint();
            //paint.IsAntialias = true;
            var scale = info.Width / (float)ActualWidth;
            var size = new Size(info.Width, info.Height);
            //System.Diagnostics.Debug.WriteLine($"Bubble.OnPaintSurface : [{size}]");
            var path = GeneratePath(size, scale);

            paint.Style = SKPaintStyle.Fill;
            paint.Color = WorkingBackgroundColor.ToSKColor();

            if (ApplyBlur)
                paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, BlurSigma);
            if (WorkingBackgroundColor.A > 0)
                canvas.DrawPath(path, paint);

            paint.Style = SKPaintStyle.Stroke; 
            paint.Color = WorkingBorderColor.ToSKColor();
            paint.StrokeWidth = (float)BorderWidth * scale;
            if (!ApplyBlur && BorderWidth > 0 && BorderColor.A > 0)
                canvas.DrawPath(path, paint);

            //TextBlock.Arrange(new Rect(0, 0, ActualWidth, ActualHeight));

            //System.Diagnostics.Debug.WriteLine($"Bubble.OnPaintSurface : EXIT");
        }

        //float scale = -1;
        SKPath GeneratePath(Size measuredSize, float scale)
        {
            if (measuredSize == default)
                return new SKPath();

            var width = (float)measuredSize.Width;
            var height = (float)measuredSize.Height;
            System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : [{width},{height}]");

            if (width < 1 || height < 1)
                return new SKPath();

            System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : Scale [{scale}]");

            var borderWidth = 0.0f;
            if (WorkingBorderColor.A > 0 && BorderWidth > 0)
                borderWidth = (float)BorderWidth * scale;
            //borderWidth *= scale;
            //System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : BorderWidth [{BorderWidth}] borderWidth [{borderWidth}]");

            var pointerLength = PointerDirection == PointerDirection.None ? 0 : (float)PointerLength;
            pointerLength *= scale;
            //System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : PointerLength [{PointerLength}] [{pointerLength}]");

            var left = borderWidth / 2.0f;
            var top = borderWidth / 2.0f;

            if (ApplyBlur)
            {
                width -= 2 * BlurSigma;
                height -= 2 * BlurSigma;
                left += 2 * BlurSigma;
                top += 2 * BlurSigma;
            }

            var right = (float)(width - (borderWidth / 2.0f + 0.5f));
            var bottom = (float)(height - (borderWidth / 2.0f + 0.5f));

            width -= (PointerDirection.IsHorizontal() ? pointerLength : 0);
            height -= (PointerDirection.IsVertical() ? pointerLength : 0);

            //System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : [{width},{height}]");
            System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : [{left},{top},{right},{bottom}]");

            var cornerRadius = (float)CornerRadius * scale;

            if (cornerRadius * 2 > width)
                cornerRadius = width / 2.0f;
            if (cornerRadius * 2 > height)
                cornerRadius = height / 2.0f;


            var filetRadius = (float)PointerCornerRadius * scale;
            var tipRadius = (float)PointerTipRadius * scale;

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
                    : top + (bottom - top) * pointerPosition);


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
                        Math.Min(tipY + pointerSansFiletHalfWidth, bottom));
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
                var tipX = Math.Min(pointerPosition, (float)(right - PointerTipRadius * sqrt3d2));     // 1
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
                    Math.Max(Math.Min(tipX, right), left), // 7
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
                         filetRadius, dir * 30, dir * 60);
                    result.ArcWithCenterTo(
                        left + cornerRadius,
                        start + dir * (pointerLength + cornerRadius),
                        cornerRadius, dir * -90, dir * -90);
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


    }
}