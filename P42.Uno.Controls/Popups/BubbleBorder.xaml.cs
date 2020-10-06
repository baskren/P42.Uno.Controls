﻿using System;
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
using P42.Utils.Uno;
using Microsoft.Toolkit.Uwp.UI.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace P42.Uno.Controls
{
    [TemplatePart(Name = ContentPresenterName, Type=typeof(ContentPresenter))]
    [TemplatePart(Name = PathElementName, Type = typeof(Path))]
    [TemplatePart(Name = DropShadowPanelElementName, Type = typeof(DropShadowPanel))]
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


        #endregion

        #region Fields
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Left;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Top;
        const string ContentPresenterName = "_contentPresenter";
        const string PathElementName = "_path";
        const string DropShadowPanelElementName = "_dropShadow";
        ContentPresenter _contentPresenter;
        Windows.UI.Xaml.Shapes.Path _path;
        DropShadowPanel _dropShadow;
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
            _dropShadow = dropShadow as DropShadowPanel;
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
            return base.ArrangeOverride(finalSize);
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

            var length = PointerDirection == PointerDirection.None ? 0 : (float)PointerLength;

            var left = 0.0f + borderWidth / 2;
            var right = (float)(width - Margin.Left - Margin.Right - borderWidth / 2);
            var top = 0.0f + borderWidth / 2;
            var bottom = (float)(height - Margin.Top - Margin.Bottom - borderWidth / 2);

            width -= (PointerDirection.IsHorizontal() ? length : 0);
            height -= (PointerDirection.IsVertical() ? length : 0);

            var radius = (float)((CornerRadius.TopLeft + CornerRadius.TopRight + CornerRadius.BottomLeft + CornerRadius.BottomRight)/4.0);

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
                    ? left + (right - left) * position
                    : top + (bottom - top)  * position);


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

            if (length <= 1)
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