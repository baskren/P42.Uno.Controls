using P42.Utils.Uno;

namespace P42.Uno.Controls.Demo;

[Microsoft.UI.Xaml.Data.Bindable]
public sealed partial class LabelTestPage : Page
{
    private static readonly string Text1 = "Żyłę;^`g <b><em>Lorem</em></b> ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio ut enim blandit volutpat maecenas. Diam volutpat commodo sed egestas egestas fringilla phasellus. Odio eu feugiat pretium nibh ipsum consequat. Urna condimentum mattis pellentesque id nibh tortor. Ut lectus arcu bibendum at varius vel pharetra. Dui nunc mattis enim ut tellus. Nullam vehicula ipsum a arcu cursus vitae congue mauris. Libero nunc consequat interdum varius sit amet mattis vulputate. Pharetra pharetra massa massa ultricies. Lorem sed risus ultricies tristique nulla aliquet enim tortor at. Aliquam sem et tortor consequat id porta. Ultrices in iaculis nunc sed augue. Tincidunt vitae semper quis lectus nulla at volutpat diam. Vitae elementum curabitur vitae nunc sed velit.";

    private readonly SegmentedControl 
        _hzAlignmentSelector = new(), 
        _vtAlignmentSelector = new(), 
        _fitSelector = new(), 
        _textWrappingSelector = new(), 
        _textTrimmingSelector = new();
    private readonly Label _label = new()
    {
        Lines = 5,
        LabelAutoFit = LabelAutoFit.None,
        Text = Text1,
        VerticalAlignment = VerticalAlignment.Bottom,
        TextWrapping = TextWrapping.WrapWholeWords
    };
    private readonly TextBlock 
        _vtAlignmentSelectorLabel = new(), 
        _unoLabel = new(), 
        _fontSizeLabel = new(), 
        _fittedFontSizeLabel = new(), 
        _labelSizeLabel = new(), 
        _lineHeightLabel = new(), 
        _heightRequestLabel = new(), 
        _linesLabel = new(), 
        _textTrimmingLabel = new(), 
        _textWrappingLabel = new(), 
        _textAlignmentLabel = new();
    private readonly Slider 
        _fontSizeSlider = new(), 
        _linesSlider = new(), 
        _lineHeightSlider = new(), 
        _heightRequestSlider = new();
    //TextBox editor = new();
    private readonly ToggleSwitch 
        _modeSwitch = new(), 
        _imposeHeightSwitch = new();
    private readonly Border _borderForUnoLabel = new();
    private readonly Grid _imposedHeightGrid = new();
    private readonly ComboBox _fontPicker = new();
    private readonly ScrollViewer _scrollViewer = new();

    private void Build()
    {
        this.Background(Microsoft.UI.Colors.LightGray);

        Content = _scrollViewer
            .Padding(0)
            .Content
            (
                new StackPanel()
                    .Padding(0)
                    .Orientation(Orientation.Vertical)
                    .Children
                    (
                        new StackPanel()
                            .Padding(0)
                            .Orientation(Orientation.Horizontal)
                            .Children
                            (
                                new TextBlock { Text = "HTML SOURCE:"},
                                _modeSwitch
                                    .Off()
                                    .Right()
                                    .AddToggledHandler(OnModeSwitchToggled)
                            ),
                        new TextBlock()
                            .Text("Microsoft.UI.Xaml.Controls.TextBlock:"),
                        _borderForUnoLabel
                            .Height(100)
                            .Padding(0)
                            .BorderBrush(Microsoft.UI.Colors.DarkGray)
                            .BorderThickness(1)
                            .CornerRadius(2)
                            .Background(Microsoft.UI.Colors.Black)
                            .Child
                            (
                                _unoLabel
                                    .Foreground(Microsoft.UI.Colors.White)
                                    .FontSize(15)
                                    .Text(Text1)
                                    .WrapWords()
                            ),
                        new TextBlock()
                            .Text("P42.Uno.Controls.Label:"),
                        _label
                            .Height(100)
                            .Padding(0)
                            .BorderBrush(Microsoft.UI.Colors.DarkGray)
                            .BorderThickness(1)
                            .CornerRadius(2)
                            .Foreground(Microsoft.UI.Colors.White)
                            .FontSize(15)
                            .Background(Microsoft.UI.Colors.Black),
                        _labelSizeLabel
                            .Text("label.Size: "),
                        new StackPanel()
                            .Padding(0)
                            .Orientation(Orientation.Horizontal)
                            .Children
                            (
                                new TextBlock { Text = "FONT:" },
                                _fontPicker
                                    .PlaceholderText("Default")
                                    .StretchHorizontal()
                                    .ItemsSource(AvailableFonts.Names)
                                    .AddSelectionChangedHandler(OnSelectedFontChanged)
                            ),
                        _fontSizeLabel
                            .Text($"Font Size: {_label.FontSize}"),
                        _fontSizeSlider
                            .MinMaxStep(4,104,1)
                            .Value(_label.FontSize)
                            .AddValueChangedHandler(OnFontSizeChanged),
                        _lineHeightLabel
                            .Text($"Line Ht: {_label.LineHeight}"), 
                        _lineHeightSlider
                            .MinMax(0.01, 8)
                            .Value(_label.LineHeight)
                            .AddValueChangedHandler(OnLineHeightValueChanged),
                        _fittedFontSizeLabel
                            .Text($"FittedFontSize: {_label.FittedFontSize}"),
                        new TextBlock()
                            .Text("AutoFit:"),
                        _fitSelector
                            .Labels(Enum.GetNames(typeof(LabelAutoFit)))
                            .RadioSelect()
                            .AllowUnselectAll(false)
                            .SelectedLabel(_label.LabelAutoFit.ToString())
                            .AddSelectionChangedHandler(OnAutoFitChanged),
                        _linesLabel
                            .Text($"Lines: {_label.Lines}"),
                        _linesSlider
                            .MinMaxStep(0, 8,1)
                            .Value(_label.Lines)
                            .AddValueChangedHandler(OnLinesChanged),
                        _imposedHeightGrid
                            .Rows("*","*")
                            .Columns("auto","*")
                            .Children
                            (
                                new TextBlock()
                                    .Text("Impose Height?"),
                                _heightRequestLabel
                                    .RowCol(1, 0)
                                    .Text($"Height: {_label.Height}"),
                                _imposeHeightSwitch
                                    .RowCol(0, 1)
                                    .On()
                                    .AddToggledHandler(OnImposeHeightChanged),
                                _heightRequestSlider
                                    .RowCol(1,1)
                                    .MinMaxStep(10,800,1)
                                    .Value(_label.Height)
                                    .AddValueChangedHandler(OnHeightRequestValueChanged)
                            ),
                        _vtAlignmentSelectorLabel
                            .Text($"VerticalAlignment: {_label.VerticalAlignment}"),
                        _vtAlignmentSelector
                            .Labels(Enum.GetNames(typeof(VerticalAlignment)))
                            .RadioSelect().AllowUnselectAll(false)
                            .SelectedLabel(_label.VerticalTextAlignment.ToString())
                            .AddSelectionChangedHandler(OnVerticalAlignmentChanged),
                        _textAlignmentLabel
                            .Text($"TextAlignment: {_label.TextAlignment}"),
                        _hzAlignmentSelector
                            .Labels(Enum.GetNames(typeof(TextAlignment)))
                            .RadioSelect().AllowUnselectAll(false)
                            .SelectedLabel(_label.TextAlignment.ToString())
                            .AddSelectionChangedHandler(OnHorizontalAlignmentChanged),
                        _textWrappingLabel
                            .Text($"TextWrapping: {_label.TextWrapping}"),
                        _textWrappingSelector
                            .Labels(Enum.GetNames(typeof(TextWrapping)))
                            .RadioSelect().AllowUnselectAll(false)
                            .SelectedLabel(_label.TextWrapping.ToString())
                            .AddSelectionChangedHandler(OnTextWrappingChanged),
                        _textTrimmingLabel
                            .Text($"TextTrimming: {_label.TextTrimming}"),
                        _textTrimmingSelector
                            .Labels(Enum.GetNames(typeof(TextTrimming)))
                            .RadioSelect().AllowUnselectAll(false)
                            .SelectedLabel(_label.TextTrimming.ToString())   
                            .AddSelectionChangedHandler(OnTextTrimmingChanged)
                    )
            );

        // SizeChanged += OnSizeChanged;
        //editor.TextChanged += OnEditorTextChanged;

        _label.FittedFontSizeChanged += OnFittedFontSizeChanged;
        _label.SizeChanged += OnLabelSizeChanged;

        // UpdateMargin();
    }

    private void OnLabelSizeChanged(object? sender, SizeChangedEventArgs args)
    {
        _labelSizeLabel.Text = $"label.Size: {_label.ActualSize}";
        _heightRequestLabel.Text = $"Height: {_label.ActualHeight}";
    }

    private void OnTextTrimmingChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var trimming = Enum.Parse<TextTrimming>(e.SelectedLabel);
        _textTrimmingLabel.Text($"TextTrimming: {trimming}");
        _label.TextTrimming = trimming;
        _unoLabel.TextTrimming = trimming;
    }

    private void OnTextWrappingChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var wrapping = Enum.Parse<TextWrapping>(e.SelectedLabel);
        _textWrappingLabel.Text($"TextWrapping: {wrapping}");
        _label.TextWrapping = wrapping;
        _unoLabel.TextWrapping = wrapping;
    }

    private void OnHorizontalAlignmentChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var align = Enum.Parse<TextAlignment>(e.SelectedLabel);
        _textAlignmentLabel.Text($"TextAlignment: {align}");
        _label.TextAlignment = align;
        _unoLabel.TextAlignment = align;
    }

    private void OnVerticalAlignmentChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var align = Enum.Parse<VerticalAlignment>(e.SelectedLabel);
        _vtAlignmentSelectorLabel.Text($"VerticalAlignment: {align}");
        _label.VerticalTextAlignment = align;
        _unoLabel.VerticalAlignment = align;
    }

    private void OnHeightRequestValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        var heightRequest = _imposeHeightSwitch.IsOn
            ? _heightRequestSlider.Value
            : double.NaN;
        _label.Height = heightRequest;
        _borderForUnoLabel.Height = heightRequest;
        _heightRequestLabel.Text = "HeightRequest: " + _heightRequestSlider.Value.ToString("####.###");
    }

    private void OnImposeHeightChanged(object? sender, RoutedEventArgs e)
    {
        var heightRequest = _imposeHeightSwitch.IsOn
            ? _heightRequestSlider.Value
            : double.NaN;
        _label.Height = heightRequest;
        _borderForUnoLabel.Height = heightRequest;
        _heightRequestSlider.Visible();
        _heightRequestLabel.Visible();
        _vtAlignmentSelector.Visible();
        _vtAlignmentSelectorLabel.Visible();
    }

    private void OnLinesChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        var value = ((int)Math.Round(_linesSlider.Value));
        _linesLabel.Text = "Lines: " + value;
        _label.Lines = value;
    }

    private void OnAutoFitChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var fit = Enum.Parse<LabelAutoFit>(_fitSelector.SelectedLabel);
        _label.LabelAutoFit = fit;
    }

    private void OnLineHeightValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        _lineHeightLabel.Text = "LineHeight: " + e.NewValue;
        _unoLabel.LineHeight = _label.LineHeight = e.NewValue;
    }

    private void OnFittedFontSizeChanged(object? sender, double e)
    {
        _fittedFontSizeLabel.Text = "FittedFontSize: " + e.ToString("#,###.##");
    }

    private void OnFontSizeChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        _fontSizeLabel.Text = "FontSize: " + e.NewValue;
        _unoLabel.FontSize = _label.FontSize = _fontSizeSlider.Value;
    }

    private void OnSelectedFontChanged(object? sender, SelectionChangedEventArgs args)
    {
        _unoLabel.FontFamily = _label.FontFamily = AvailableFonts.FontFamily(_fontPicker.SelectedItem as string);
    }

    private async void OnModeSwitchToggled(object? sender, RoutedEventArgs e)
    {
        // HTML is not yet implmented
        if (_modeSwitch.IsOn)
        {
            await Toast.CreateAsync("Not implemented", "Html source not yet implmented for P42.Uno.Label");
            _modeSwitch.Off();
        }
    }

    private void OnEditorTextChanged(object? sender, TextChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    /*
    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        UpdateMargin();
    }

    private void UpdateMargin()
    {
#if HAS_UNO
        _scrollViewer.Padding = new Thickness(
                Math.Max(5, Uno.UI.Toolkit.VisibleBoundsPadding.WindowPadding.Left),
                0,
                Math.Max(25, VisibleBoundsPadding.WindowPadding.Right),
                20
            );
        Padding = new Thickness(
                0,
                Math.Max(5, VisibleBoundsPadding.WindowPadding.Top),
                0,
                Math.Max(5, VisibleBoundsPadding.WindowPadding.Bottom)
            );
#endif
    }
    */
}
