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
    public partial class NewBubbleBorder : Grid
    {
        #region Properties

        #region Content Property
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(Content),
            typeof(UIElement),
            typeof(NewBubbleBorder),
            new PropertyMetadata(default(UIElement), OnContentChanged)
        );
        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NewBubbleBorder border)
            {
                if (e.OldValue is UIElement oldElement)
                    border.Children.Remove(oldElement);
                if (e.NewValue is UIElement newElement)
                {
                    newElement.RowCol(1, 1);
                    border.Children.Add(newElement);
                }
            }
        }

        public UIElement Content
        {
            get => (UIElement)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
        #endregion Content Property

        #region Override Properties

        #region Padding Property
        public static readonly new DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(NewBubbleBorder),
            new PropertyMetadata(default,OnPaddingChanged)
        );

        private static void OnPaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NewBubbleBorder border)
                border.UpdatePadding();
        }

        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        #endregion Padding Property

        #region BackgroundColor Property
        [Obsolete("Use BackgroundColor instead")]
        public new Brush Background { get; set; }

        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(NewBubbleBorder),
            new PropertyMetadata(default(Color))
        );

        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        #endregion BackgroundColor Property

        #region BorderColor Property
        [Obsolete("Use BorderColor instead")]
        public new Brush BorderBrush { get; set; }

        public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
            nameof(BorderColor),
            typeof(Color),
            typeof(NewBubbleBorder),
            new PropertyMetadata(default(Color), OnBorderColorChanged)
        );

        private static void OnBorderColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NewBubbleBorder border)
                border.UpdatePadding();
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        #endregion BorderColor Property

        #region BorderWidth Property
        [Obsolete("Use BorderWidth instead")]
        public new Thickness BorderThickness { get; set; }

        public static readonly DependencyProperty BorderWidthProperty = DependencyProperty.Register(
            nameof(BorderWidth),
            typeof(double),
            typeof(NewBubbleBorder),
            new PropertyMetadata(1.0, OnBorderWidthChanged)
        );

        private static void OnBorderWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NewBubbleBorder border)
                border.UpdatePadding();
        }

        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
        #endregion BorderWidth Property

        #region CornerRadius Property
        public static readonly new DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(double),
            typeof(NewBubbleBorder),
            new PropertyMetadata(4.0)
        );
        public new double CornerRadius
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
            typeof(NewBubbleBorder),
            new PropertyMetadata(10.0, OnPointerLengthChanged)
        );

        private static void OnPointerLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NewBubbleBorder border && e.NewValue is double length)
                border.UpdatePadding();
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
            typeof(NewBubbleBorder),
            new PropertyMetadata(1.0)
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
            typeof(NewBubbleBorder),
            new PropertyMetadata(0.5)
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
            typeof(NewBubbleBorder),
            new PropertyMetadata(PointerDirection.None, OnPointerDirectionChanged)
        );

        private static void OnPointerDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NewBubbleBorder border && e.NewValue is PointerDirection dir)
                border.UpdatePadding();
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
            typeof(NewBubbleBorder),
            new PropertyMetadata(default(double))
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
        Color WorkingBorderColor => BorderColor == default
            ? SystemColors.ChromeDisabledHigh
            : BorderColor;
        #endregion


        #region Construction
        public NewBubbleBorder()
        {
            base.Margin = new Thickness(0);
            base.Padding = new Thickness(0);

            Children.Add(new SkiaBubble()
                .Stretch()
                .RowSpan(3)
                .ColumnSpan(3)
                .Bind(SkiaBubble.BackgroundColorProperty, this, nameof(BackgroundColor))
                .Bind(SkiaBubble.BorderColorProperty, this, nameof(BorderColor))
                .Bind(SkiaBubble.BorderWidthProperty, this, nameof(BorderWidth))
                .Bind(SkiaBubble.CornerRadiusProperty, this, nameof(CornerRadius))
                .Bind(SkiaBubble.PointerLengthProperty, this, nameof(PointerLength))
                .Bind(SkiaBubble.PointerAxialPositionProperty, this, nameof(PointerAxialPosition))
                .Bind(SkiaBubble.PointerTipRadiusProperty, this, nameof(PointerTipRadius))
                .Bind(SkiaBubble.PointerCornerRadiusProperty, this, nameof (PointerCornerRadius))
                .Bind(SkiaBubble.PointerDirectionProperty, this, nameof(PointerDirection))
                );

        }
        #endregion


        #region Private Methods
        void UpdatePadding()
        {
            var padding = Padding;

            switch (PointerDirection)
            {
                case PointerDirection.None:
                    break;
                case PointerDirection.Left:
                    padding.Left += PointerLength;
                    break;
                case PointerDirection.Right:
                    padding.Right += PointerLength;
                    break;
                case PointerDirection.Up:
                    padding.Top += PointerLength;
                    break;
                case PointerDirection.Down:
                    padding.Bottom += PointerLength;
                    break;
                default:
                    throw new InvalidOperationException("BubbleBorder PointerDirection must be either Left, Right, Top, Bottom, or None");
            }

            var borderWidth = (BorderWidth > 0 && WorkingBorderColor.A > 0)
                ? BorderWidth
                : 0;

            this
                .Rows(padding.Top + borderWidth, "*", padding.Bottom + borderWidth)
                .Columns(padding.Left + borderWidth, "*", padding.Right + borderWidth);

            //System.Diagnostics.Debug.WriteLine($"NewBubbleBorder.UpdatePadding : padding [{padding}] BorderWidth[{BorderWidth}] BorderColor[{BorderColor.A},{BorderColor.R},{BorderColor.G},{BorderColor.B}]");
        }
        #endregion

    }
}