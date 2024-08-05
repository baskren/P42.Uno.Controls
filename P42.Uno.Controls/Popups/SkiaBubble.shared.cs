using System;
using Windows.Foundation;
using Microsoft.UI.Xaml;
using SkiaSharp;
using Windows.UI;
using P42.Uno.Markup;
using SkiaSharp.Views.Windows;
using System.Runtime.CompilerServices;

namespace P42.Uno.Controls
{
    /// <summary>
    /// Border used by Popups
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class SkiaBubble : SkiaSharp.Views.Windows.SKXamlCanvas
    {

        internal static readonly Color DefaultBorderColor = SystemColors.BaseMedium;
        internal static readonly Color DefaultFillColor = SystemColors.AltHigh;
        internal const double DefaultBorderWidth = 1.0;
        internal const double DefaultCornerRadius = 5.0;

        #region Properties

        void Redraw([CallerMemberName] string caller = null)
        {
            //System.Diagnostics.Debug.WriteLine($"SkiaBubble.Redraw : [{caller}]");
            Invalidate();
        }

        #region UserControl Properties


        #region BackgroundColor Property
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(SkiaBubble),
            new PropertyMetadata(DefaultFillColor,(d,e) => ((SkiaBubble)d).OnBackgroundColorChanged(e)) //.Redraw(nameof(BackgroundColor)))
        );

        private void OnBackgroundColorChanged(DependencyPropertyChangedEventArgs e)
        {
            Redraw(nameof(BackgroundColor));
        }

#if __IOS__
        public new Color BackgroundColor
#else
        public Color BackgroundColor
#endif
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        #endregion BackgroundColor Property


        #region BorderColor Property
        public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
            nameof(BorderColor),
            typeof(Color),
            typeof(SkiaBubble),
            new PropertyMetadata(DefaultBorderColor, (d, e) => ((SkiaBubble)d).Redraw(nameof(BorderColor)))
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
            typeof(SkiaBubble),
            new PropertyMetadata(DefaultBorderWidth, (d, e) => ((SkiaBubble)d).Redraw(nameof(BorderWidth)))
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
            typeof(SkiaBubble),
            new PropertyMetadata(DefaultCornerRadius, (d, e) => ((SkiaBubble)d).Redraw(nameof(CornerRadius)))
        );
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        #endregion CornerRadius Property


#endregion

        #region Pointer Properties


        #region PointerLength Property
        public static readonly DependencyProperty PointerLengthProperty = DependencyProperty.Register(
            nameof(PointerLength),
            typeof(double),
            typeof(SkiaBubble),
            new PropertyMetadata(10.0, (d, e) => ((SkiaBubble)d).Redraw(nameof(PointerLength)))
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
            typeof(SkiaBubble),
            new PropertyMetadata(1.0, (d, e) => ((SkiaBubble)d).Redraw(nameof(PointerTipRadius)))
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
            typeof(SkiaBubble),
            new PropertyMetadata(0.5, (d, e) => ((SkiaBubble)d).Redraw(nameof(PointerAxialPosition)))
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
            typeof(SkiaBubble),
            new PropertyMetadata(PointerDirection.None, (d, e) => ((SkiaBubble)d).Redraw(nameof(PointerDirection)))
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
            typeof(SkiaBubble),
            new PropertyMetadata(default(double), (d, e) => ((SkiaBubble)d).Redraw(nameof(PointerCornerRadius)))
        );
        public double PointerCornerRadius
        {
            get => (double)GetValue(PointerCornerRadiusProperty);
            set => SetValue(PointerCornerRadiusProperty, value);
        }
        #endregion PointerCornerRadius Property

        #endregion

        #region Private Properties
        Color WorkingBackgroundColor => BackgroundColor == default
            ? DefaultFillColor
            : BackgroundColor;
        Color WorkingBorderColor => BorderColor == default
            ? DefaultBorderColor
            : BorderColor;
        #endregion


#endregion


        #region Fields
        internal float BlurSigma = 6f;
        internal bool ApplyBlur = false;
        static long _instances;
        long _instance;
        #endregion

        public SkiaBubble()
        {
            _instance = _instances++;
#if __IOS__
            ((UIKit.UIView)this).BackgroundColor = UIKit.UIColor.Clear;
#endif

            //RegisterPropertyChangedCallback(MarginProperty, OnMarginChanged);
        }

        private void OnMarginChanged(DependencyObject sender, DependencyProperty dp)
        {
            Invalidate();

            System.Diagnostics.Debug.WriteLine($"SkiaBubble.OnMarginChanged: {Margin}");
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"Bubble.OnPaintSurface : ENTER");
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColor.Empty);
            
            SKPaint paint = new SKPaint();
            //paint.IsAntialias = true;
            var scale = info.Width / (float)ActualWidth;
            var size = new Size(info.Width, info.Height);
            var borderWidth = 0.0f;
            if (WorkingBorderColor.A > 0 && BorderWidth > 0)
                borderWidth = (float)BorderWidth * scale;
            var pointerLength = PointerDirection == PointerDirection.None ? 0 : (float)PointerLength;
            pointerLength *= scale;
            var blurOffset = 0.0f;
            if (ApplyBlur)
                blurOffset = BlurSigma * scale;

            var pointerPosition = (float)PointerAxialPosition;
            if (Math.Abs(PointerAxialPosition) > 1)
                pointerPosition *= scale;

#if __IOS__ || __ANDROID__
            //System.Diagnostics.Debug.WriteLine($"Popup.VISUAL TREE : ");
            //System.Diagnostics.Debug.WriteLine(this.ShowLocalVisualTree(100));
#endif

            //System.Diagnostics.Debug.WriteLine($"Bubble.OnPaintSurface : [{size}]");
            var path = GeneratePath(
                size, 
                borderWidth, 
                pointerLength, 
                blurOffset, 
                PointerDirection,
                pointerPosition,
                (float)CornerRadius * scale,
                (float)PointerTipRadius * scale,
                (float)PointerCornerRadius * scale);

            paint.Style = SKPaintStyle.Fill;
            paint.Color = WorkingBackgroundColor.ToSKColor();

            if (ApplyBlur)
            {
                paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, BlurSigma * scale);
            }
            if (paint.Color.Alpha > 0)
                canvas.DrawPath(path, paint);

            if (borderWidth > 0)
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = WorkingBorderColor.ToSKColor();
                paint.StrokeWidth = borderWidth;
                canvas.DrawPath(path, paint);
            }

            //System.Diagnostics.Debug.WriteLine($"Bubble.OnPaintSurface : EXIT");
        }

        internal static SKPath GeneratePath(
            Size measuredSize, 
            float borderWidth, 
            float pointerLength, 
            float blurOffset, 
            PointerDirection pointerDirection,
            float pointerPosition,
            float cornerRadius,
            float tipRadius,
            float filetRadius,
            [CallerMemberName] string callerMember = null,
            [CallerFilePath] string callerPath = null,
            [CallerLineNumber] int callerLineNumber = default)
        {
            if (measuredSize == default)
                return new SKPath();

            var width = (float)measuredSize.Width;
            var height = (float)measuredSize.Height;
            //System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : [{width},{height}]");

            if (width < 1 || height < 1)
                return new SKPath();

            //System.Diagnostics.Debug.WriteLine($"SkiaBubble.GeneratePath : [{callerMember}]:[{callerLineNumber}]:[{callerPath}]");
            //System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : scale [{scale}]");
            //System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : borderWidth [{borderWidth}]");
            //System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : pointerLength [{pointerLength}]");

            var left = borderWidth / 2.0f;
            var top = borderWidth / 2.0f;

            width -= 2 * blurOffset;
            height -= 2 * blurOffset;
            left += 2 * blurOffset;
            top += 2 * blurOffset;

            var right = width - (borderWidth / 2.0f + 0.5f);
            var bottom = height - (borderWidth / 2.0f + 0.5f);

            width -= (pointerDirection.IsHorizontal() ? pointerLength : 0);
            height -= (pointerDirection.IsVertical() ? pointerLength : 0);

            //System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : [{width},{height}]");
            //System.Diagnostics.Debug.WriteLine($"Bubble.GeneratePath : [{left},{top},{right},{bottom}]");

            if (cornerRadius * 2 > width)
                cornerRadius = width / 2.0f;
            if (cornerRadius * 2 > height)
                cornerRadius = height / 2.0f;

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
            if (Math.Abs(pointerPosition) <= 1.0)
            {
                if(pointerPosition > 0)
                    pointerPosition = (pointerDirection == PointerDirection.Down || pointerDirection == PointerDirection.Up)
                        ? left + (right - left) * pointerPosition
                        : top + (bottom - top) * pointerPosition;
                else
                    pointerPosition = (pointerDirection == PointerDirection.Down || pointerDirection == PointerDirection.Up)
                        ? right - (right - left) * pointerPosition
                        : bottom - (bottom - top) * pointerPosition;
            }
            else if (pointerPosition < -1)
            {
                pointerPosition = (pointerDirection == PointerDirection.Down || pointerDirection == PointerDirection.Up)
                    ? right - pointerPosition
                    : bottom - pointerPosition;
            }
            //System.Diagnostics.Debug.WriteLine($"SkiaBubble.GeneratePath : pointerPosition [{pointerPosition}]");

            const float sqrt3 = 1.732050807568877f;
            const float sqrt3d2 = 0.86602540378444f;

            var tipCornerHalfWidth = tipRadius * sqrt3d2;
            var pointerToCornerIntercept = Math.Sqrt((2 * cornerRadius * Math.Sin(Math.PI / 12.0f)) * (2 * cornerRadius * Math.Sin(Math.PI / 12.0f)) - (cornerRadius * cornerRadius / 4.0f));

            var pointerAtLimitSansTipHalfWidth = pointerToCornerIntercept + cornerRadius / (2.0f * sqrt3) + (pointerLength - tipRadius / 2.0f) / sqrt3;
            var pointerAtLimitHalfWidth = pointerAtLimitSansTipHalfWidth + tipRadius * sqrt3d2;

            var pointerSansFiletHalfWidth = tipCornerHalfWidth + (pointerLength - filetRadius / 2.0f - tipRadius / 2.0f) / sqrt3;
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
            else if (pointerDirection.IsHorizontal())
            {
                var start = left;
                var end = right;
                if (pointerDirection == PointerDirection.Right)
                {
                    dir = -1;
                    start = right;
                    end = left;
                }
                //var baseX = start + dir * pointerLength;

                var tipY = Math.Min(pointerPosition, bottom - tipRadius * sqrt3d2);
                tipY = Math.Max(tipY, top + tipRadius * sqrt3d2);
                if (height <= 2 * pointerAtLimitHalfWidth)
                    tipY = (top + bottom) / 2.0f;
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
                    var endRatio = (height - tipY) / (pointerAndFiletHalfWidth + cornerRadius);
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
                if (pointerDirection == PointerDirection.Down)
                {
                    dir = -1;
                    start = bottom;
                    end = top;
                }
                var tipX = Math.Min(pointerPosition, (right - tipRadius * sqrt3d2));     // 1
                tipX = Math.Max(tipX, left + tipRadius * sqrt3d2);                               // 1
                if (width <= 2 * pointerAtLimitHalfWidth)
                    tipX = (left + right) / 2.0f;   // 8
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
                    var endRatio = (right - tipX) / (pointerAndFiletHalfWidth + cornerRadius);
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
