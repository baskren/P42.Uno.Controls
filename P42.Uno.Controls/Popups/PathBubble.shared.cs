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
using System.Runtime.CompilerServices;
using P42.Utils.Uno;

namespace P42.Uno.Controls
{
    /// <summary>
    /// Border used by Popups
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class PathBubble : Microsoft.UI.Xaml.Shapes.Path
    {

        #region Properties


        #region CornerRadius Property
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(double),
            typeof(PathBubble),
            new PropertyMetadata(4.0, (d, e) => ((PathBubble)d).UpdatePath(nameof(CornerRadius)))
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
            new PropertyMetadata(10.0, (d, e) => ((PathBubble)d).UpdatePath(nameof(PointerLength)))
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
            new PropertyMetadata(1.0, (d, e) => ((PathBubble)d).UpdatePath(nameof(PointerTipRadius)))
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
            new PropertyMetadata(0.5, (d, e) => ((PathBubble)d).UpdatePath(nameof(PointerAxialPosition)))
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
            new PropertyMetadata(PointerDirection.None, (d, e) => ((PathBubble)d).UpdatePath(nameof(PointerDirection)))
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
            new PropertyMetadata(default(double), (d,e) => ((PathBubble)d).UpdatePath(nameof(PointerCornerRadius)))
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
            Fill = new SolidColorBrush(SystemColors.ButtonFace);
            Stroke = new SolidColorBrush(SystemColors.ChromeDisabledHigh);

            SizeChanged += OnSizeChanged;
        }


        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(10, 10);// base.MeasureOverride(availableSize);
        }

        bool _pendingUpdatePath;
        bool _updatingPath;
        Size _pendingPathSize;
        Size _currentPathSize;
        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine($"PathBubble.OnSizeChanged : [{args.NewSize}]");
            var ΔHeight = args.NewSize.Height - _currentPathSize.Height;
            var ΔWidth = args.NewSize.Width - _currentPathSize.Width;
            if (ΔWidth <= 0 && ΔWidth > -1 && ΔHeight <= 0 && ΔHeight > -1)
                return;

            UpdatePath($"OnSizeChanged({args.NewSize})");
        }
        

        protected void UpdatePath([CallerMemberName] string caller = null, bool force = false)
        {
            var size = new Size(ActualWidth, ActualHeight);
            if (force)
                _pendingUpdatePath = false;
            else if (_updatingPath)
            {
                _pendingPathSize = size;
                _pendingUpdatePath = true;
                return;
            }
            _updatingPath = true;
            _currentPathSize = size;

            System.Diagnostics.Debug.WriteLine($"PathBubble.RegeneratePath : [{caller}]");
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

#if __P42WASM__
            //System.Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            var x = this.GetFirstHtmlDescendent();
            x.SetHtmlContent($"<path fill-rule=\"even-odd\" d=\"{data}\"></path>");
#else
            //System.Console.WriteLine($"BubbleBorder.RegeneratePath [{data}]");
            Data = P42.Utils.Uno.StringToPathGeometryConverter.Current.Convert(data);
#endif

            if (_pendingUpdatePath)
                UpdatePath($"pending({_pendingPathSize})", true);
            else
                _updatingPath = false;
            //System.Diagnostics.Debug.WriteLine($"Bubble.OnPaintSurface : EXIT");
        }

    }
}