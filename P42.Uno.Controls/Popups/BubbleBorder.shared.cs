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
using P42.Utils.Uno;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.UI.Text;
using Microsoft.UI.Xaml.Data;
using Windows.UI.ViewManagement;

namespace P42.Uno.Controls
{
    /// <summary>
    /// Border used by Popups
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class BubbleBorder : Grid
        // An important note.  It appears that ContentControl does not resize itself smaller if its content gets smaller. Also, it does not resize itself when going from Stretch Alignment to something else
    {
        #region Properties

        #region Alternative Properties (Because Shape.Path doesn't work in Android?!?!)

        #region BackgroundColor Property
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(BubbleBorder),
            new PropertyMetadata(SkiaBubble.DefaultFillColor, (d,e) => ((BubbleBorder)d).OnBackgroundColorChanged(e))
        );

        private void OnBackgroundColorChanged(DependencyPropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"BubbleBorder.OnBackgroundColorChanged : [{e.NewValue}] ");
            
        }
#if __IOS__ || __MACCATALYST__
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
            typeof(BubbleBorder),
            new PropertyMetadata(SkiaBubble.DefaultBorderColor)
        );
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        #endregion BorderColor Property

        #region BorderWidth
        public static readonly DependencyProperty BorderWidthProperty = DependencyProperty.Register(
            nameof(BorderWidth),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(SkiaBubble.DefaultBorderWidth, OnBorderWidthChanged)
        );
        private static void OnBorderWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BubbleBorder border)
                border.UpdateConetntPresenterMarginAndCorners();
        }
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
        #endregion BorderWidth Property

#endregion

        #region ContentPresenter Properties

        #region Background Property

        [Obsolete("Use BackgroundColor, instead")]
        public static readonly new DependencyProperty BackgroundProperty = DependencyProperty.Register(
            nameof(Background),
            typeof(Brush),
            typeof(BubbleBorder),
            new PropertyMetadata(default)
        );
        
        [Obsolete("Use BackgroundColor, instead")]
        public new Brush Background
        {
            get => BackgroundColor.ToBrush();
            set
            {
                //if (value is SolidColorBrush brush)
                //    BackgroundColor = brush.Color;
                //else
                    throw new Exception("Only SolidColorBrush will work");
            }
        }
        #endregion Background Property

        /*
        #region BackgroundSizing Property
        public static readonly new DependencyProperty BackgroundSizingProperty = DependencyProperty.Register(
            nameof(BackgroundSizing),
            typeof(BackgroundSizing),
            typeof(BubbleBorder),
            new PropertyMetadata(default(BackgroundSizing))
        );
        public new BackgroundSizing BackgroundSizing
        {
            get => (BackgroundSizing)GetValue(BackgroundSizingProperty);
            set => SetValue(BackgroundSizingProperty, value);
        }
        #endregion BackgroundSizing Property

        #region BackgroundTransition Property
        public static readonly DependencyProperty BackgroundTransitionProperty = DependencyProperty.Register(
            nameof(BackgroundTransition),
            typeof(BrushTransition),
            typeof(BubbleBorder),
            new PropertyMetadata(default(BrushTransition))
        );
        public new BrushTransition BackgroundTransition
        {
            get => (BrushTransition)GetValue(BackgroundTransitionProperty);
            set => SetValue(BackgroundTransitionProperty, value);
        }
        #endregion BackgroundTransition Property
        */

        #region BorderBrush Property
        [Obsolete("Use BorderWidth, instead")]
        public static readonly new DependencyProperty BorderBrushProperty = DependencyProperty.Register(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(BubbleBorder),
            new PropertyMetadata(default)
        );
        [Obsolete("Use BorderWidth, instead")]
        public new Brush BorderBrush
        {
            get => new SolidColorBrush(BorderColor);
            set
            {
                if (value is SolidColorBrush brush)
                    BorderColor = brush.Color;
                else
                    throw new Exception("Can only use SolidColorBrush for BorderBrush");
            }
        }
        #endregion BorderBrush Property

        #region CharacterSpacing Property
        public static readonly DependencyProperty CharacterSpacingProperty = DependencyProperty.Register(
            nameof(CharacterSpacing),
            typeof(int),
            typeof(BubbleBorder),
            new PropertyMetadata(default(int))
        );
        public int CharacterSpacing
        {
            get => (int)GetValue(CharacterSpacingProperty);
            set => SetValue(CharacterSpacingProperty, value);
        }
        #endregion CharacterSpacing Property

        #region Content Property
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(Content),
            typeof(object),
            typeof(BubbleBorder),
            new PropertyMetadata(null, (d,p) => ((BubbleBorder)d).OnContentChanged(p))
        );

        private void OnContentChanged(DependencyPropertyChangedEventArgs p)
        {
#if __MOBILE__
            if (p.OldValue is FrameworkElement oldContent)
                oldContent.SizeChanged -= OnContentSizeChanged;
            if (p.NewValue is FrameworkElement newContent)
                newContent.SizeChanged += OnContentSizeChanged;
#endif
        }


        public object Content
        {
            get => (object)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
        #endregion Content Property

        #region ContentTemplate Property
        public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register(
            nameof(ContentTemplate),
            typeof(DataTemplate),
            typeof(BubbleBorder),
            new PropertyMetadata(default(DataTemplate))
        );
        public DataTemplate ContentTemplate
        {
            get => (DataTemplate)GetValue(ContentTemplateProperty);
            set => SetValue(ContentTemplateProperty, value);
        }
        #endregion ContentTemplate Property

        #region ContentTemplateSelector Property
        public static readonly DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(ContentTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(BubbleBorder),
            new PropertyMetadata(default(DataTemplateSelector))
        );
        public DataTemplateSelector ContentTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty);
            set => SetValue(ContentTemplateSelectorProperty, value);
        }
        #endregion ContentTemplateSelector Property

        #region ContentTransitions Property
        public static readonly DependencyProperty ContentTransitionsProperty = DependencyProperty.Register(
            nameof(ContentTransitions),
            typeof(TransitionCollection),
            typeof(BubbleBorder),
            new PropertyMetadata(default(TransitionCollection))
        );
        public TransitionCollection ContentTransitions
        {
            get => (TransitionCollection)GetValue(ContentTransitionsProperty);
            set => SetValue(ContentTransitionsProperty, value);
        }
        #endregion ContentTransitions Property

        #region CornerRadius Property
        public static readonly new DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(SkiaBubble.DefaultCornerRadius)
        );
        public new double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        #endregion CornerRadius Property

        #region FontFamily Property
        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register(
            nameof(FontFamily),
            typeof(Microsoft.UI.Xaml.Media.FontFamily),
            typeof(BubbleBorder),
            new PropertyMetadata(default(Microsoft.UI.Xaml.Media.FontFamily))
        );
        public Microsoft.UI.Xaml.Media.FontFamily FontFamily
        {
            get => (Microsoft.UI.Xaml.Media.FontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }
        #endregion FontFamily Property

        #region FontSize Property
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            nameof(FontSize),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(12.0)
        );
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        #endregion FontSize Property

        #region FontStretch Property
        public static readonly DependencyProperty FontStretchProperty = DependencyProperty.Register(
            nameof(FontStretch),
            typeof(FontStretch),
            typeof(BubbleBorder),
            new PropertyMetadata(default(FontStretch))
        );
        public FontStretch FontStretch
        {
            get => (FontStretch)GetValue(FontStretchProperty);
            set => SetValue(FontStretchProperty, value);
        }
        #endregion FontStretch Property

        #region FontStyle Property
        public static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register(
            nameof(FontStyle),
            typeof(FontStyle),
            typeof(BubbleBorder),
            new PropertyMetadata(default(FontStyle))
        );
        public FontStyle FontStyle
        {
            get => (FontStyle)GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }
        #endregion FontStyle Property

        #region FontWeight Property
        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register(
            nameof(FontWeight),
            typeof(FontWeight),
            typeof(BubbleBorder),
            new PropertyMetadata(default(FontWeight))
        );
        public FontWeight FontWeight
        {
            get => (FontWeight)GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }
        #endregion FontWeight Property

        #region Foreground Property
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
            nameof(Foreground),
            typeof(Brush),
            typeof(BubbleBorder),
            new PropertyMetadata(P42.Uno.Markup.SystemTextBoxBrushes.Foreground)
        );
#if __ANDROID__
        public new Brush Foreground
#else
        public Brush Foreground
#endif
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }
        #endregion Foreground Property

        #region HorizontalContentAlignment Property
        public static readonly DependencyProperty HorizontalContentAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalContentAlignment),
            typeof(HorizontalAlignment),
            typeof(BubbleBorder),
            new PropertyMetadata(default(HorizontalAlignment))
        );
        public HorizontalAlignment HorizontalContentAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty);
            set => SetValue(HorizontalContentAlignmentProperty, value);
        }
        #endregion HorizontalContentAlignment Property

        #region IsTextScaleFactorEnabled Property
        public static readonly DependencyProperty IsTextScaleFactorEnabledProperty = DependencyProperty.Register(
            nameof(IsTextScaleFactorEnabled),
            typeof(bool),
            typeof(BubbleBorder),
            new PropertyMetadata(default(bool))
        );
        public bool IsTextScaleFactorEnabled
        {
            get => (bool)GetValue(IsTextScaleFactorEnabledProperty);
            set => SetValue(IsTextScaleFactorEnabledProperty, value);
        }
        #endregion IsTextScaleFactorEnabled Property

        #region LineHeight Property
        public static readonly DependencyProperty LineHeightProperty = DependencyProperty.Register(
            nameof(LineHeight),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(default(double))
        );
        public double LineHeight
        {
            get => (double)GetValue(LineHeightProperty);
            set => SetValue(LineHeightProperty, value);
        }
        #endregion LineHeight Property

        #region LineStackingStrategy Property
        public static readonly DependencyProperty LineStackingStrategyProperty = DependencyProperty.Register(
            nameof(LineStackingStrategy),
            typeof(LineStackingStrategy),
            typeof(BubbleBorder),
            new PropertyMetadata(default(LineStackingStrategy))
        );
        public LineStackingStrategy LineStackingStrategy
        {
            get => (LineStackingStrategy)GetValue(LineStackingStrategyProperty);
            set => SetValue(LineStackingStrategyProperty, value);
        }
        #endregion LineStackingStrategy Property

        #region MaxLines Property
        public static readonly DependencyProperty MaxLinesProperty = DependencyProperty.Register(
            nameof(MaxLines),
            typeof(int),
            typeof(BubbleBorder),
            new PropertyMetadata(default(int))
        );
        public int MaxLines
        {
            get => (int)GetValue(MaxLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }
        #endregion MaxLines Property

        #region OpticalMarginAlignment Property
        public static readonly DependencyProperty OpticalMarginAlignmentProperty = DependencyProperty.Register(
            nameof(OpticalMarginAlignment),
            typeof(OpticalMarginAlignment),
            typeof(BubbleBorder),
            new PropertyMetadata(default(OpticalMarginAlignment))
        );
        public OpticalMarginAlignment OpticalMarginAlignment
        {
            get => (OpticalMarginAlignment)GetValue(OpticalMarginAlignmentProperty);
            set => SetValue(OpticalMarginAlignmentProperty, value);
        }
        #endregion OpticalMarginAlignment Property

        #region Padding Property
        public static readonly new DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(BubbleBorder),
            new PropertyMetadata(default, OnPaddingChanged)
        );

        private static void OnPaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BubbleBorder border)
                border.UpdateConetntPresenterMarginAndCorners();
        }

        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        #endregion Padding Property

        #region TextLineBounds Property
        public static readonly DependencyProperty TextLineBoundsProperty = DependencyProperty.Register(
            nameof(TextLineBounds),
            typeof(TextLineBounds),
            typeof(BubbleBorder),
            new PropertyMetadata(default(TextLineBounds))
        );
        public TextLineBounds TextLineBounds
        {
            get => (TextLineBounds)GetValue(TextLineBoundsProperty);
            set => SetValue(TextLineBoundsProperty, value);
        }
        #endregion TextLineBounds Property

        #region TextWrapping Property
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(
            nameof(TextWrapping),
            typeof(TextWrapping),
            typeof(BubbleBorder),
            new PropertyMetadata(default(TextWrapping))
        );
        public TextWrapping TextWrapping
        {
            get => (TextWrapping)GetValue(TextWrappingProperty);
            set => SetValue(TextWrappingProperty, value);
        }
        #endregion TextWrapping Property

        #region VerticalContentAlignment Property
        public static readonly DependencyProperty VerticalContentAlignmentProperty = DependencyProperty.Register(
            nameof(VerticalContentAlignment),
            typeof(VerticalAlignment),
            typeof(BubbleBorder),
            new PropertyMetadata(default(VerticalAlignment))
        );
        public VerticalAlignment VerticalContentAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalContentAlignmentProperty);
            set => SetValue(VerticalContentAlignmentProperty, value);
        }
        #endregion VerticalContentAlignment Property

        #endregion

        #region Pointer Properties


        #region PointerLength Property
        public static readonly DependencyProperty PointerLengthProperty = DependencyProperty.Register(
            nameof(PointerLength),
            typeof(double),
            typeof(BubbleBorder),
            new PropertyMetadata(10.0, OnPointerLengthChanged)
        );

        private static void OnPointerLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BubbleBorder border)
                border.UpdateConetntPresenterMarginAndCorners();
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
            typeof(BubbleBorder),
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
            typeof(BubbleBorder),
            new PropertyMetadata(PointerDirection.None, OnPointerDirectionChanged)
        );

        private static void OnPointerDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BubbleBorder border)
                border.UpdateConetntPresenterMarginAndCorners();
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
        bool HasBorder
        {
            get
            {
                if (BorderWidth <= 0)
                    return false;
                return BorderColor.A > 0;
            }
        }

        #endregion


        #region Fields
        internal ContentPresenter _contentPresenter;
        //SkiaBubble _bubble;
        #endregion


        #region Construction
        public BubbleBorder()
        {

            base.Margin = new Thickness(0);
            base.Padding = new Thickness(0);

            //BorderColor = P42.Uno.Markup.SystemTeachingTipBrushes.Border.AsColor();
            //BorderColor = SystemColors.ChromeAltLow;
            //BackgroundColor = P42.Uno.Markup.SystemTeachingTipBrushes.Background.AsColor();
            //BackgroundColor = SystemColors.AltHigh;

            this
                .Children
                (
                    new SkiaBubble()
                        //.Assign(out _bubble)
                        .Margin(0)
                        .WBind(SkiaBubble.BackgroundColorProperty, this, BackgroundColorProperty)
                        .WBind(SkiaBubble.BorderColorProperty, this, BorderColorProperty)
                        .WBind(SkiaBubble.BorderWidthProperty, this, BorderWidthProperty)
                        .WBind(SkiaBubble.CornerRadiusProperty, this, CornerRadiusProperty)
                        .WBind(SkiaBubble.PointerLengthProperty, this, PointerLengthProperty)
                        .WBind(SkiaBubble.PointerAxialPositionProperty, this, PointerAxialPositionProperty)
                        .WBind(SkiaBubble.PointerTipRadiusProperty, this, PointerTipRadiusProperty)
                        .WBind(SkiaBubble.PointerCornerRadiusProperty, this, PointerCornerRadiusProperty)
                        .WBind(SkiaBubble.PointerDirectionProperty, this, PointerDirectionProperty),

                    /* Does not work in Android - perhaps a bug with Shape.Path in Android?
                        new PathBubble()
                            .Bind(PathBubble.FillProperty, this, nameof(Background))
                            .Bind(PathBubble.StrokeProperty, this, nameof(BorderBrush))
                            .Bind(PathBubble.StrokeThicknessProperty, this, nameof(BorderWidth))
                            .Bind(PathBubble.CornerRadiusProperty, this, nameof(CornerRadius))
                            .Bind(PathBubble.PointerLengthProperty, this, nameof(PointerLength))
                            .Bind(PathBubble.PointerAxialPositionProperty, this, nameof(PointerAxialPosition))
                            .Bind(PathBubble.PointerTipRadiusProperty, this, nameof(PointerTipRadius))
                            .Bind(PathBubble.PointerCornerRadiusProperty, this, nameof(PointerCornerRadius))
                            .Bind(PathBubble.PointerDirectionProperty, this, nameof(PointerDirection)),
                        /*
                        new Microsoft.UI.Xaml.Shapes.Rectangle()
                            .RowCol(1,1)
                            .Stretch()
                            .Fill(Colors.Pink),
                        */

                    new ContentPresenter()
                        .Assign(out _contentPresenter)
                        .Padding(0)
                        .Margin(0)
                        //Background
                        //BackgroundSizing
                        //BorderBrush
                        //BorderThickness
                        .WBind(ContentPresenter.MinHeightProperty, this, MinHeightProperty)
                        .WBind(ContentPresenter.MaxHeightProperty, this, MaxHeightProperty)
                        .WBind(ContentPresenter.MinWidthProperty, this, MinWidthProperty)
                        .WBind(ContentPresenter.MaxWidthProperty, this, MaxWidthProperty)

                        .WBind(ContentPresenter.ContentProperty, this, ContentProperty)
                        .WBind(ContentPresenter.ContentTemplateProperty, this, ContentTemplateProperty)
                        .WBind(ContentPresenter.ContentTemplateSelectorProperty, this, ContentTemplateSelectorProperty)
                        .WBind(ContentPresenter.ContentTransitionsProperty, this, ContentTransitionsProperty)
                        //CornerRadius

                        .WBind(ContentPresenter.CharacterSpacingProperty, this, CharacterSpacingProperty)
                        .WBind(ContentPresenter.FontFamilyProperty, this, FontFamilyProperty)
                        .WBind(ContentPresenter.FontSizeProperty, this, FontSizeProperty)
                        .WBind(ContentPresenter.FontStretchProperty, this, FontStretchProperty)
                        .WBind(ContentPresenter.FontStyleProperty, this, FontStyleProperty)
                        .WBind(ContentPresenter.FontWeightProperty, this, FontWeightProperty)
                        .WBind(ContentPresenter.ForegroundProperty, this, ForegroundProperty)
                        .WBind(ContentPresenter.IsTextScaleFactorEnabledProperty, this, IsTextScaleFactorEnabledProperty)
                        .WBind(ContentPresenter.LineHeightProperty, this, LineHeightProperty)
                        .WBind(ContentPresenter.LineStackingStrategyProperty, this, LineStackingStrategyProperty)
                        .WBind(ContentPresenter.MaxLinesProperty, this, MaxLinesProperty)
                        .WBind(ContentPresenter.OpticalMarginAlignmentProperty, this, OpticalMarginAlignmentProperty)
                        .WBind(ContentPresenter.TextLineBoundsProperty, this, TextLineBoundsProperty)
                        .WBind(ContentPresenter.TextWrappingProperty, this, TextWrappingProperty)

                        .WBind(ContentPresenter.HorizontalContentAlignmentProperty, this, HorizontalContentAlignmentProperty)
                        .WBind(ContentPresenter.VerticalContentAlignmentProperty, this, VerticalContentAlignmentProperty)

                );

            UpdateConetntPresenterMarginAndCorners();
            //RegisterPropertyChangedCallback(MarginProperty, OnMarginChanged);
        }


#if __MOBILE__
        private void OnContentSizeChanged(object sender, SizeChangedEventArgs args)
        {

            //System.Diagnostics.Debug.WriteLine($"BubbleBorder.OnContentSizeChanged : [{args.PreviousSize}] => [{args.NewSize}]");
            //InvalidateMeasure();
            InvalidateArrange();
        }
#endif


#endregion


        #region Private Methods
        void UpdateConetntPresenterMarginAndCorners()
        {
            if (_contentPresenter is null)
                return;

            var borderWidth = HasBorder
#if __ANDROID__
                ? BorderWidth + 1
#else
                ? BorderWidth
#endif
                : 0;

            var margin = Padding.Add(borderWidth);
#if __ANDROID__
            if (HasBorder)
                borderWidth -= 1;
#endif

            switch (PointerDirection)
            {
                case PointerDirection.None:
                    break;
                case PointerDirection.Left:
                    margin.Left += PointerLength;
                    break;
                case PointerDirection.Right:
                    margin.Right += PointerLength;
                    break;
                case PointerDirection.Up:
                    margin.Top += PointerLength;
                    break;
                case PointerDirection.Down:
                    margin.Bottom += PointerLength;
                    break;
                default:
                    throw new InvalidOperationException("BubbleBorder PointerDirection must be either Left, Right, Top, Bottom, or None");
            }


            _contentPresenter.Margin = margin;

            //System.Diagnostics.Debug.WriteLine($"BubbleBorder.ContentPresenter.Margin : [{margin}]");

            _contentPresenter.CornerRadius = new CornerRadius(
                    Math.Max(0, CornerRadius - borderWidth - (Padding.Left + Padding.Top)/2.0),
                    Math.Max(0, CornerRadius - borderWidth - (Padding.Top + Padding.Right) / 2.0),
                    Math.Max(0, CornerRadius - borderWidth - (Padding.Right + Padding.Bottom) / 2.0),
                    Math.Max(0, CornerRadius - borderWidth - (Padding.Bottom + Padding.Left) / 2.0)
                    );

            //System.Diagnostics.Debug.WriteLine($"BubbleBorder.UpdatePadding : padding [{padding}] BorderWidth[{BorderWidth}] BorderColor[{BorderColor.A},{BorderColor.R},{BorderColor.G},{BorderColor.B}]");
        }
#endregion


    }
}
