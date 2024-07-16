using System;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using P42.Uno.Controls;
using P42.Uno.Markup;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace P42.Uno.Controls.Demo;

[Microsoft.UI.Xaml.Data.Bindable]
public sealed partial class LabelTestPage : Page
{
    readonly static string text1 = "Żyłę;^`g <b><em>Lorem</em></b> ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio ut enim blandit volutpat maecenas. Diam volutpat commodo sed egestas egestas fringilla phasellus. Odio eu feugiat pretium nibh ipsum consequat. Urna condimentum mattis pellentesque id nibh tortor. Ut lectus arcu bibendum at varius vel pharetra. Dui nunc mattis enim ut tellus. Nullam vehicula ipsum a arcu cursus vitae congue mauris. Libero nunc consequat interdum varius sit amet mattis vulputate. Pharetra pharetra massa massa ultricies. Lorem sed risus ultricies tristique nulla aliquet enim tortor at. Aliquam sem et tortor consequat id porta. Ultrices in iaculis nunc sed augue. Tincidunt vitae semper quis lectus nulla at volutpat diam. Vitae elementum curabitur vitae nunc sed velit.";

    readonly SegmentedControl hzAlignmentSelector = new(), vtAlignmentSelector = new(), fitSelector = new(), textWrappingSelector = new(), textTrimmingSelector = new();
    readonly Label label = new()
    {
        Lines = 5,
        LabelAutoFit = LabelAutoFit.None,
        Text = text1,
        VerticalAlignment = VerticalAlignment.Bottom,
        TextWrapping = TextWrapping.WrapWholeWords
    };
    readonly TextBlock vtAlignmentSelectorLabel = new(), unoLabel = new(), fontSizeLabel = new(), fittedFontSizeLabel = new(), labelSizeLabel = new(), lineHeightLabel = new(), heightRequestLabel = new(), linesLabel = new(), textTrimmingLabel = new(), textWrappingLabel = new(), textAlignmentLabel = new();
    readonly Slider fontSizeSlider = new(), linesSlider = new(), lineHeightSlider = new(), heightRequestSlider = new();
    //TextBox editor = new();
    readonly ToggleSwitch modeSwitch = new(), imposeHeightSwitch = new();
    readonly Border borderForUnoLabel = new();
    readonly Grid imposedHeightGrid = new();
    readonly ComboBox fontPicker = new();
    readonly ScrollViewer scrollViewer = new();

    void Build()
    {
        this.Background(Colors.LightGray);

        Content = scrollViewer
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
                                modeSwitch
                                    .Off()
                                    .Right()
                                    .AddToggledHandler(OnModeSwitchToggled)
                            ),
                        new TextBlock()
                            .Text("Microsoft.UI.Xaml.Controls.TextBlock:"),
                        borderForUnoLabel
                            .Height(100)
                            .Padding(0)
                            .BorderBrush(Colors.DarkGray)
                            .BorderThickness(1)
                            .CornerRadius(2)
                            .Background(Colors.Black)
                            .Child
                            (
                                unoLabel
                                    .Foreground(Colors.White)
                                    .FontSize(15)
                                    .Text(text1)
                                    .WrapWords()
                            ),
                        new TextBlock()
                            .Text("P42.Uno.Controls.Label:"),
                        label
                            .Height(100)
                            .Padding(0)
                            .BorderBrush(Colors.DarkGray)
                            .BorderThickness(1)
                            .CornerRadius(2)
                            .Foreground(Colors.White)
                            .FontSize(15)
                            .Background(Colors.Black),
                        labelSizeLabel
                            .Text($"label.Size: "),
                        new StackPanel()
                            .Padding(0)
                            .Orientation(Orientation.Horizontal)
                            .Children
                            (
                                new TextBlock { Text = "FONT:" },
                                fontPicker
                                    .PlaceholderText("Default")
                                    .StretchHorizontal()
                                    .ItemsSource(P42.Utils.Uno.AvailableFonts.Names)
                                    .AddSelectionChangedHandler(OnSelectedFontChanged)
                            ),
                        fontSizeLabel
                            .Text($"Font Size: {label.FontSize}"),
                        fontSizeSlider
                            .MinMaxStep(4,104,1)
                            .Value(label.FontSize)
                            .AddValueChangedHandler(OnFontSizeChanged),
                        lineHeightLabel
                            .Text($"Line Ht: {label.LineHeight}"), 
                        lineHeightSlider
                            .MinMax(0.01, 8)
                            .Value(label.LineHeight)
                            .AddValueChangedHandler(OnLineHeightValueChanged),
                        fittedFontSizeLabel
                            .Text($"FittedFontSize: {label.FittedFontSize}"),
                        new TextBlock()
                            .Text("AutoFit:"),
                        fitSelector
                            .Labels(Enum.GetNames(typeof(LabelAutoFit)))
                            .RadioSelect()
                            .AllowUnselectAll(false)
                            .SelectedLabel(label.LabelAutoFit.ToString())
                            .AddSelectionChangedHandler(OnAutoFitChanged),
                        linesLabel
                            .Text($"Lines: {label.Lines}"),
                        linesSlider
                            .MinMaxStep(0, 8,1)
                            .Value(label.Lines)
                            .AddValueChangedHandler(OnLinesChanged),
                        imposedHeightGrid
                            .Rows("*","*")
                            .Columns("auto","*")
                            .Children
                            (
                                new TextBlock()
                                    .Text("Impose Height?"),
                                heightRequestLabel
                                    .RowCol(1, 0)
                                    .Text($"Height: {label.Height}"),
                                imposeHeightSwitch
                                    .RowCol(0, 1)
                                    .On()
                                    .AddToggledHandler(OnImposeHeightChanged),
                                heightRequestSlider
                                    .RowCol(1,1)
                                    .MinMaxStep(10,800,1)
                                    .Value(label.Height)
                                    .AddValueChangedHandler(OnHeightRequestValueChanged)
                            ),
                        vtAlignmentSelectorLabel
                            .Text($"VerticalAlignment: {label.VerticalAlignment}"),
                        vtAlignmentSelector
                            .Labels(Enum.GetNames(typeof(VerticalAlignment)))
                            .RadioSelect().AllowUnselectAll(false)
                            .SelectedLabel(label.VerticalTextAlignment.ToString())
                            .AddSelectionChangedHandler(OnVerticalAlignmentChanged),
                        textAlignmentLabel
                            .Text($"TextAlignment: {label.TextAlignment}"),
                        hzAlignmentSelector
                            .Labels(Enum.GetNames(typeof(TextAlignment)))
                            .RadioSelect().AllowUnselectAll(false)
                            .SelectedLabel(label.TextAlignment.ToString())
                            .AddSelectionChangedHandler(OnHorizontalAlignmentChanged),
                        textWrappingLabel
                            .Text($"TextWrapping: {label.TextWrapping}"),
                        textWrappingSelector
                            .Labels(Enum.GetNames(typeof(TextWrapping)))
                            .RadioSelect().AllowUnselectAll(false)
                            .SelectedLabel(label.TextWrapping.ToString())
                            .AddSelectionChangedHandler(OnTextWrappingChanged),
                        textTrimmingLabel
                            .Text($"TextTrimming: {label.TextTrimming}"),
                        textTrimmingSelector
                            .Labels(Enum.GetNames(typeof(TextTrimming)))
                            .RadioSelect().AllowUnselectAll(false)
                            .SelectedLabel(label.TextTrimming.ToString())   
                            .AddSelectionChangedHandler(OnTextTrimmingChanged)
                    )
            );

        SizeChanged += OnSizeChanged;
        //editor.TextChanged += OnEditorTextChanged;

        label.FittedFontSizeChanged += OnFittedFontSizeChanged;
        label.SizeChanged += OnLabelSizeChanged;

        UpdateMargin();
    }

    private void OnLabelSizeChanged(object? sender, SizeChangedEventArgs args)
    {
        labelSizeLabel.Text = $"label.Size: {label.ActualSize}";
        heightRequestLabel.Text = $"Height: {label.ActualHeight}";
    }

    private void OnTextTrimmingChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var trimming = Enum.Parse<TextTrimming>(e.SelectedLabel);
        textTrimmingLabel.Text($"TextTrimming: {trimming}");
        label.TextTrimming = trimming;
        unoLabel.TextTrimming = trimming;
    }

    private void OnTextWrappingChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var wrapping = Enum.Parse<TextWrapping>(e.SelectedLabel);
        textWrappingLabel.Text($"TextWrapping: {wrapping}");
        label.TextWrapping = wrapping;
        unoLabel.TextWrapping = wrapping;
    }

    private void OnHorizontalAlignmentChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var align = Enum.Parse<TextAlignment>(e.SelectedLabel);
        textAlignmentLabel.Text($"TextAlignment: {align}");
        label.TextAlignment = align;
        unoLabel.TextAlignment = align;
    }

    private void OnVerticalAlignmentChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var align = Enum.Parse<VerticalAlignment>(e.SelectedLabel);
        vtAlignmentSelectorLabel.Text($"VerticalAlignment: {align}");
        label.VerticalTextAlignment = align;
        unoLabel.VerticalAlignment = align;
    }

    private void OnHeightRequestValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        var heightRequest = imposeHeightSwitch.IsOn
            ? heightRequestSlider.Value
            : double.NaN;
        label.Height = heightRequest;
        borderForUnoLabel.Height = heightRequest;
        heightRequestLabel.Text = "HeightRequest: " + heightRequestSlider.Value.ToString("####.###");
    }

    private void OnImposeHeightChanged(object? sender, RoutedEventArgs e)
    {
        var heightRequest = imposeHeightSwitch.IsOn
            ? heightRequestSlider.Value
            : double.NaN;
        label.Height = heightRequest;
        borderForUnoLabel.Height = heightRequest;
        heightRequestSlider.Visible();
        heightRequestLabel.Visible();
        vtAlignmentSelector.Visible();
        vtAlignmentSelectorLabel.Visible();
    }

    private void OnLinesChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        var value = ((int)Math.Round(linesSlider.Value));
        linesLabel.Text = "Lines: " + value;
        label.Lines = value;
    }

    private void OnAutoFitChanged(object? sender, (int SelectedIndex, string SelectedLabel) e)
    {
        var fit = Enum.Parse<LabelAutoFit>(fitSelector.SelectedLabel);
        label.LabelAutoFit = fit;
    }

    private void OnLineHeightValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        lineHeightLabel.Text = "LineHeight: " + e.NewValue;
        unoLabel.LineHeight = label.LineHeight = e.NewValue;
    }

    private void OnFittedFontSizeChanged(object? sender, double e)
    {
        fittedFontSizeLabel.Text = "FittedFontSize: " + e.ToString("#,###.##");
    }

    private void OnFontSizeChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        fontSizeLabel.Text = "FontSize: " + e.NewValue;
        unoLabel.FontSize = label.FontSize = fontSizeSlider.Value;
    }

    private void OnSelectedFontChanged(object? sender, SelectionChangedEventArgs args)
    {
        unoLabel.FontFamily = label.FontFamily = P42.Utils.Uno.AvailableFonts.FontFamily(fontPicker.SelectedItem as string);
    }

    async void OnModeSwitchToggled(object? sender, RoutedEventArgs e)
    {
        // HTML is not yet implmented
        if (modeSwitch.IsOn)
        {
            await P42.Uno.Controls.Toast.CreateAsync("Not implemented", "Html source not yet implmented for P42.Uno.Label");
            modeSwitch.Off();
        }
    }

    private void OnEditorTextChanged(object? sender, TextChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        UpdateMargin();
    }

    void UpdateMargin()
    {
#if HAS_UNO
        scrollViewer.Padding = new Thickness(
                Math.Max(5, global::Uno.UI.Toolkit.VisibleBoundsPadding.WindowPadding.Left),
                0,
                Math.Max(25, global::Uno.UI.Toolkit.VisibleBoundsPadding.WindowPadding.Right),
                20
            );
        this.Padding = new Thickness(
                0,
                Math.Max(5, global::Uno.UI.Toolkit.VisibleBoundsPadding.WindowPadding.Top),
                0,
                Math.Max(5, global::Uno.UI.Toolkit.VisibleBoundsPadding.WindowPadding.Bottom)
            );
#endif
    }
}
