using System.Diagnostics;
using Windows.UI;
using Windows.UI.Text;
using Microsoft.UI.Xaml.Media.Animation;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace P42.Uno.Controls;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Border used by Popups
/// </summary>
[Bindable]
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
        new PropertyMetadata(SkiaBubble.DefaultFillColor, OnBackgroundColorChanged)
    );

    private static void OnBackgroundColorChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        Debug.WriteLine($"BubbleBorder.OnBackgroundColorChanged : [{e.NewValue}] ");
            
    }
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
        new PropertyMetadata(null, OnContentChanged)
    );

    private static void OnContentChanged(object sender, DependencyPropertyChangedEventArgs p)
    {
        if (sender is not BubbleBorder border)
            return;
#if __MOBILE__
        if (p.OldValue is FrameworkElement oldContent)
            oldContent.SizeChanged -= border.OnContentSizeChanged;
        if (p.NewValue is FrameworkElement newContent)
            newContent.SizeChanged += border.OnContentSizeChanged;
#endif
    }


    public object? Content
    {
        get => GetValue(ContentProperty);
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
    public new static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
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
        typeof(FontFamily),
        typeof(BubbleBorder),
        new PropertyMetadata(default(FontFamily))
    );
    public FontFamily FontFamily
    {
        get => (FontFamily)GetValue(FontFamilyProperty);
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
        new PropertyMetadata(SystemTextBoxBrushes.Foreground)
    );
        
    public Brush Foreground
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
    public new static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
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

    private bool HasBorder
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

        Margin = new Thickness(0);
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
                    .AltBind(SkiaBubble.BackgroundColorProperty, this, BackgroundColorProperty)
                    .AltBind(SkiaBubble.BorderColorProperty, this, BorderColorProperty)
                    .AltBind(SkiaBubble.BorderWidthProperty, this, BorderWidthProperty)
                    .AltBind(SkiaBubble.CornerRadiusProperty, this, CornerRadiusProperty)
                    .AltBind(SkiaBubble.PointerLengthProperty, this, PointerLengthProperty)
                    .AltBind(SkiaBubble.PointerAxialPositionProperty, this, PointerAxialPositionProperty)
                    .AltBind(SkiaBubble.PointerTipRadiusProperty, this, PointerTipRadiusProperty)
                    .AltBind(SkiaBubble.PointerCornerRadiusProperty, this, PointerCornerRadiusProperty)
                    .AltBind(SkiaBubble.PointerDirectionProperty, this, PointerDirectionProperty),

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
                        .Fill(Microsoft.UI.Colors.Pink),
                    */

                new ContentPresenter()
                    .Name(out _contentPresenter)
                    .Padding(0)
                    .Margin(0)
                    //Background
                    //BackgroundSizing
                    //BorderBrush
                    //BorderThickness
                    .AltBind(MinHeightProperty, this, MinHeightProperty)
                    .AltBind(MaxHeightProperty, this, MaxHeightProperty)
                    .AltBind(MinWidthProperty, this, MinWidthProperty)
                    .AltBind(MaxWidthProperty, this, MaxWidthProperty)

                    .AltBind(ContentPresenter.ContentProperty, this, ContentProperty)
                    .AltBind(ContentPresenter.ContentTemplateProperty, this, ContentTemplateProperty)
                    .AltBind(ContentPresenter.ContentTemplateSelectorProperty, this, ContentTemplateSelectorProperty)
                    .AltBind(ContentPresenter.ContentTransitionsProperty, this, ContentTransitionsProperty)
                    //CornerRadius

                    .AltBind(ContentPresenter.CharacterSpacingProperty, this, CharacterSpacingProperty)
                    .AltBind(ContentPresenter.FontFamilyProperty, this, FontFamilyProperty)
                    .AltBind(ContentPresenter.FontSizeProperty, this, FontSizeProperty)
                    .AltBind(ContentPresenter.FontStretchProperty, this, FontStretchProperty)
                    .AltBind(ContentPresenter.FontStyleProperty, this, FontStyleProperty)
                    .AltBind(ContentPresenter.FontWeightProperty, this, FontWeightProperty)
                    .AltBind(ContentPresenter.ForegroundProperty, this, ForegroundProperty)
                    .AltBind(ContentPresenter.IsTextScaleFactorEnabledProperty, this, IsTextScaleFactorEnabledProperty)
                    .AltBind(ContentPresenter.LineHeightProperty, this, LineHeightProperty)
                    .AltBind(ContentPresenter.LineStackingStrategyProperty, this, LineStackingStrategyProperty)
                    .AltBind(ContentPresenter.MaxLinesProperty, this, MaxLinesProperty)
                    .AltBind(ContentPresenter.OpticalMarginAlignmentProperty, this, OpticalMarginAlignmentProperty)
                    .AltBind(ContentPresenter.TextLineBoundsProperty, this, TextLineBoundsProperty)
                    .AltBind(ContentPresenter.TextWrappingProperty, this, TextWrappingProperty)

                    .AltBind(ContentPresenter.HorizontalContentAlignmentProperty, this, HorizontalContentAlignmentProperty)
                    .AltBind(ContentPresenter.VerticalContentAlignmentProperty, this, VerticalContentAlignmentProperty)

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

    private void UpdateConetntPresenterMarginAndCorners()
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
