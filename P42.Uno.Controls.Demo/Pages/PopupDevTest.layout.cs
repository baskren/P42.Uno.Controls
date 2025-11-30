using P42.Utils.Uno;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
[Bindable]
public partial class PopupDevTest : Page
{
    private readonly Grid _grid = new();
    private readonly Border _altBorder = new();
    private readonly TextBox 
        _marginTextBox = new(), 
        _paddingTextBox = new(), 
        _borderTextBox = new();
    private readonly ComboBox 
        _pointerDirectionCombo = new(), 
        _hzAlignCombo = new(), 
        _vtAlignCombo = new();
    private readonly Button _button = new();
    private readonly ListView _listView = new();
    private readonly TextBox _textBox = new();
    //private readonly TextBlock _textBlock = new();
    private readonly ToggleSwitch 
        _shadowToggleSwitch = new(), 
        _overlayToggleSwitch = new(), 
        _hitTransparentOverlayToggleSwitch = new();
    private readonly Rectangle 
        //_targetRectangle = new(), 
        _bubbleTestRectangle = new();
    // private readonly BubbleBorder _bubble;

    private void Build()
    {
        this.Padding(0)
            .Margin(0);

        Content = _grid
            .Margin(0)
            .Padding(0)
            .Background(Colors.Yellow)
            .Rows(150, 50, 50, "*", 50, 50)
            .Children
            (
                _bubbleTestRectangle
                    .Row(0)
                    .Stretch()
                    .Fill(Colors.Beige),
                new StackPanel()
                    .Row(1)
                    .Horizontal()
                    .Children
                    (
                        new TextBlock().Text("Margin:").Foreground(Colors.Black),
                        _marginTextBox.Text("0").Foreground(Colors.Black),
                        new TextBlock().Text("Padding:").Foreground(Colors.Black),
                        _paddingTextBox.Text("0").Foreground(Colors.Black),
                        new TextBlock().Text("Pointer:").Foreground(Colors.Black),
                        _pointerDirectionCombo
                            .Text("PointerDir")
                            .Foreground(Colors.Black)
                            .ItemTemplate(typeof(EnumItemTemplate))
                            .ItemsSource(Enum.GetValues(typeof(PointerDirection)))
                            .SelectedIndex(0)
                            .AddSelectionChangedHandler(OnPointerDirChanged),
                        new TextBlock().Text("HzAlign:").Foreground(Colors.Black),
                        _hzAlignCombo
                            .Text("HzAlign")
                            .Foreground(Colors.Black)
                            .ItemTemplate(typeof(EnumItemTemplate))
                            .ItemsSource(Enum.GetValues(typeof(HorizontalAlignment)))
                            .SelectedIndex(0)
                            .AddSelectionChangedHandler(OnHzAlignChanged),
                        new TextBlock().Text("VtAlign:").Foreground(Colors.Black),
                        _vtAlignCombo
                            .Text("VtAlign")
                            .Foreground(Colors.Black)
                            .ItemTemplate(typeof(EnumItemTemplate))
                            .ItemsSource(Enum.GetValues(typeof(VerticalAlignment)))
                            .SelectedIndex(0)
                            .AddSelectionChangedHandler(OnVtAlignChanged),
                        _button
                            .Content("Show Popup")
                            .AddTapHandler(OnButton_Click)
                            .Foreground(Colors.Black)
                    ),
                new StackPanel()
                    .Row(2)
                    .Horizontal()
                    .Children(
                        new TextBlock().Text("Shadow:").Foreground(Colors.Black),
                        _shadowToggleSwitch.On().AddToggledHandler(OnShadowToggleButtonChanged),
                        new TextBlock().Text("Overlay:").Foreground(Colors.Black),
                        _overlayToggleSwitch.On().AddToggledHandler(OnPageOverlayToggleSwitchChanged),
                        new TextBlock().Text("Hitable:").Foreground(Colors.Black),
                        _hitTransparentOverlayToggleSwitch.On().AddToggledHandler(OnHitTransparentToggleButtonChanged),
                        new TextBlock().Text("Border:").Foreground(Colors.Black),
                        _borderTextBox.Text("1").Foreground(Colors.Black).AddTextChangedHandler(_borderTextBox_TextChanged)

                    ),
                _listView
                    .Row(3)
                    .ItemTemplate(typeof(MainPageButtonRowTemplate))
                    .ItemsSource(new List<int> { 1, 2, 3, 4 }),
                _textBox
                    .Row(5)
                    .Text("My Text Test Here!")
                    .AddTextChangedHandler(OnTextChanged) ,
                _altBorder
                    .Row(3)
                    .Size(50, 50)
                    .CenterHorizontal()
                    .Child(new TextBlock { Text = "TARGET", VerticalAlignment = VerticalAlignment.Center })
                    .Background(Colors.Pink)
                    .AddTapHandler(OnElementTapped)
                    /*,
                new BubbleBorder()
                    .Assign(out _bubble)
                    .Content(new Border()
                        .Background(Microsoft.UI.Colors.Pink)
                        .Stretch()
                        .Child(new TextBlock()
                            .Assign(out _textBlock)
                            .Margin(10)
                            .Bind(TextBlock.TextProperty, _textBox, nameof(TextBox.Text))
                            .Foreground(Microsoft.UI.Colors.Gray)
                        )
                    )*/
            );

        SafeArea.SetInsets(_grid, SafeArea.InsetMask.All);



        OnPointerDirChanged(null, null);
        OnVtAlignChanged(null, null);
        OnHzAlignChanged(null, null);
        OnTextChanged(null, null);
    }

    private void OnHitTransparentToggleButtonChanged(object sender, RoutedEventArgs e)
    {
        TargetedPopup.IsPageOverlayHitTestVisible = _hitTransparentOverlayToggleSwitch.IsOn;
    }

    private void OnShadowToggleButtonChanged(object sender, RoutedEventArgs e)
    {
        TargetedPopup.HasShadow = _shadowToggleSwitch.IsOn;
    }

    private void OnPageOverlayToggleSwitchChanged(object sender, RoutedEventArgs e)
    {
        TargetedPopup.PageOverlayBrush = _overlayToggleSwitch.IsOn
            ? new SolidColorBrush(Colors.Black.WithAlpha(0.25))
            : null;
    }

    private void OnPointerDirChanged(object? sender, SelectionChangedEventArgs? e)
    {
        if (_pointerDirectionCombo.SelectedItem is not PointerDirection dir)
            dir = PointerDirection.None;
        TargetedPopup.PreferredPointerDirection = dir;

        /*
        if (dir == PointerDirection.Horizontal ||  dir == PointerDirection.Vertical || dir == PointerDirection.Any)
            _bubble.PointerDirection = PointerDirection.None;
        else
            _bubble.PointerDirection = dir;
        */
    }

    private void OnVtAlignChanged(object? sender, SelectionChangedEventArgs? e)
    {
        if (_vtAlignCombo.SelectedItem is not VerticalAlignment align)
            align = VerticalAlignment.Top;

        TargetedPopup.VerticalAlignment = align;
        //_bubble.VerticalAlignment = align;
    }

    private void OnHzAlignChanged(object? sender, SelectionChangedEventArgs? e)
    {
        if (_hzAlignCombo.SelectedItem is not HorizontalAlignment align)
            align = HorizontalAlignment.Left;

        TargetedPopup.HorizontalAlignment = align;
        //_bubble.HorizontalAlignment = align;
    }
}

[Bindable]
public class MainPageButtonRowTemplate : Button
{
    public MainPageButtonRowTemplate()
    {
        this.Padding(20, 2)
            .StretchHorizontal()
            .HorizontalContentAlignment(HorizontalAlignment.Right)
            .Background(Colors.Beige)
            .BorderBrush(Colors.Green)
            .BorderThickness(1)
            .CornerRadius(5)
            .AddTapHandler(BorderTapped)
            .Content(new TextBlock { Text = "ZAP" });

        DataContextChanged += CellTemplate_DataContextChanged;
    }

    private void BorderTapped(object sender, TappedRoutedEventArgs e)
    {
        var page = this.FindAncestor<PopupDevTest>();
        Debug.WriteLine($"page=[{page}]");
    }

    private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        Content = DataContext?.ToString() ?? string.Empty;
    }
}

[Bindable]
public class EnumItemTemplate : Grid
{
    private readonly TextBlock _textBlock = new();

    public EnumItemTemplate()
    {
        this.Padding(20, 2)
            .Children(_textBlock);
        DataContextChanged += CellTemplate_DataContextChanged;
    }

    private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        _textBlock.Text = DataContext?.ToString() ?? string.Empty;
    }
}
