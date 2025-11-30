#if __ANDROID__
#endif

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
[TemplateVisualState(GroupName = "State", Name = "Collapsed")]
[TemplateVisualState(GroupName = "State", Name = "Visible")]
public partial class PopupDevTest 
{
    //string[]? _hzSource;
    //string[]? _vtSource;


    TargetedPopup? _targetedPopup;
    TargetedPopup TargetedPopup => _targetedPopup ??= MakePopup();

    private TargetedPopup MakePopup()
    {
        var result = new TargetedPopup()
            //.Background(Microsoft.UI.Colors.Transparent)
            .PushEffect(Effect.Info, EffectMode.On)
            .VerticalContentAlignment(VerticalAlignment.Stretch)
            .HorizontalContentAlignment(HorizontalAlignment.Stretch)
            .Content(
                new StackPanel()
                    .Stretch()
                    .Children
                    (
                        new Grid()
                            .Background(SystemColors.AltHigh)
                            .BorderBrush(Colors.Gray)
                            .Padding(10)
                            .Children
                            (
                                new TextBlock()
                                    .Text("TITLE")
                                    .BoldFontWeight()
                            ),
                        new TextBlock()
                            .WrapWords()
                            .Margin(10)
                            .Bind(TextBlock.TextProperty, _textBox, nameof(TextBox.Text)),
                        new Grid()
                            .Background(Colors.Pink)
                            .BorderBrush(Colors.Gray)
                            .Padding(10).Children
                            (
                                new Button()
                                    .Content("CANCEL")
                                    .AddClickHandler(OnTargetedPopupCancelButtonClicked)
                                    .Center()
                            )
                    )
            );
        //result.BackgroundColor = Colors.Transparent;
        return result;
    }

    

    async void OnTargetedPopupCancelButtonClicked(object sender, RoutedEventArgs e)
    {
        await TargetedPopup.PopAsync();
    }


    public PopupDevTest()
    {
        Build();

        _marginTextBox.TextChanged += _marginTextBox_TextChanged;
        _paddingTextBox.TextChanged += _paddingTextBox_TextChanged;
    }

    private void _paddingTextBox_TextChanged(object? sender, TextChangedEventArgs? e)
    {
        if (double.TryParse(_paddingTextBox.Text, out var result))
        {
            //_bubble.Padding(result);
            TargetedPopup.Padding(result);
        }
    }

    private void _marginTextBox_TextChanged(object? sender, TextChangedEventArgs? e)
    {
        if (double.TryParse(_marginTextBox.Text, out var result))
        {
            //_bubble.Margin(result);
            TargetedPopup.Margin(result);
        }
    }

    private void _borderTextBox_TextChanged(object? sender, TextChangedEventArgs? e)
    {
        if (double.TryParse(_borderTextBox.Text, out var result))
        {
            //_bubble.BorderWidth(result);
            TargetedPopup.BorderWidth(result);
        }
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs? e)
    {
#if __ANDROID__
        //_textBlock.Text = _textBox.Text;
#endif
    }


    private void OnElementTapped(object sender, TappedRoutedEventArgs e)
    {
        OnButton_Click(sender, e);
    }

    private async void OnButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not UIElement element)
            return;

        var permission = await PermissionPopup.CreateAsync(element, "PERMISSION", "Is this ok?");
        await permission.WaitForPoppedAsync();

        TargetedPopup.Target = element;
        if (sender == _button)
            TargetedPopup.Target = null;

        await TargetedPopup.PushAsync();
    }
    

}
