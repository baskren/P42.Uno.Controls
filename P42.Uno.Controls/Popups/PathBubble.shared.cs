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
    public partial class PathBubble : Microsoft.UI.Xaml.Shapes.Path
    {

        #region Properties

        static void Redraw(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PathBubble bubble)
                bubble.RegeneratePath();
        }

        #region CornerRadius Property
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(double),
            typeof(PathBubble),
            new PropertyMetadata(4.0, Redraw)
        );
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        #endregion CornerRadius Property

        #region Pointer Properties

        #region PointerLength Property
        public static readonly DependencyProperty PointerLengthProperty = DependencyProperty.Register(
            nameof(PointerLength),
            typeof(double),
            typeof(PathBubble),
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
            typeof(PathBubble),
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
            typeof(PathBubble),
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
            typeof(PathBubble),
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
            typeof(PathBubble),
            new PropertyMetadata(default(double), Redraw)
        );
        public double PointerCornerRadius
        {
            get => (double)GetValue(PointerCornerRadiusProperty);
            set => SetValue(PointerCornerRadiusProperty, value);
        }
        #endregion PointerCornerRadius Property

        #endregion

        #region Private Properties
        bool HasBorder
        {
            get
            {
                if (StrokeThickness <= 0)
                    return false;
                if (Stroke is Brush brush)
                {
                    if (brush is SolidColorBrush solidBrush)
                        return solidBrush.Color.A > 0;
                    return true;
                }
                return false;
            }
        }
        #endregion


        #endregion

        public PathBubble()
        {
            Fill = new SolidColorBrush(SystemColors.ColorButtonFace);
            Stroke = new SolidColorBrush(SystemColors.ChromeDisabledHigh);

            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
            =>RegeneratePath();
        

        protected void RegeneratePath()
        {
            var size = new Size(ActualWidth, ActualHeight);
            var borderWidth = 0.0f;
            if (HasBorder)
                borderWidth = (float)StrokeThickness;
            var pointerLength = PointerDirection == PointerDirection.None ? 0 : (float)PointerLength;
            var pointerPosition = (float)PointerAxialPosition;

            var path = SkiaBubble.GeneratePath(
                size,
                borderWidth,
                pointerLength,
                0.0f,
                PointerDirection,
                pointerPosition,
                (float)CornerRadius,
                (float)PointerTipRadius,
                (float)PointerCornerRadius);
            var data = path.ToSvgPathData();

#if __WASM__
            //System.Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            var x = this.GetFirstHtmlDescendent();
            x.SetHtmlContent($"<path fill-rule=\"even-odd\" d=\"{data}\"></path>");
#else
            //System.Console.WriteLine($"BubbleBorder.RegeneratePath [{data}]");
            Data = P42.Utils.Uno.StringToPathGeometryConverter.Current.Convert(data);
#endif

            //System.Diagnostics.Debug.WriteLine($"Bubble.OnPaintSurface : EXIT");
        }

    }
}