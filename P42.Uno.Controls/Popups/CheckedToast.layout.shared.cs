namespace P42.Uno.Controls;

public partial class CheckedToast : Alert
{
    protected CheckBox _checkBox = new();

    private void Build()
    {
        _bubbleContentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        _okButton.Row(3);

        _checkBox
            .Row(2).Column(1).Margin(0)
            .Stretch()
            .HorizontalContentAlignment(HorizontalAlignment.Left)
            .AltBind(ToggleButton.IsCheckedProperty, this, IsCheckedProperty, BindingMode.TwoWay)
            .AltBind(ContentProperty, this, CheckContentProperty);

        _bubbleContentGrid.Children.Add(_checkBox);

        this.DisableAlternativeCancel(false);
    }

}
