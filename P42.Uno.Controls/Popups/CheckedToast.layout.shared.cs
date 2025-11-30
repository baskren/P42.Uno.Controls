namespace P42.Uno.Controls;

public partial class CheckedToast : Alert
{
    protected CheckBox _checkBox;

    private void Build()
    {
        _bubbleContentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        _okButton.Row(3);

        _checkBox = new CheckBox()
            .Row(2).Column(1).Margin(0)
            .StretchHorizontal()
            .HorizontalContentAlignment(HorizontalAlignment.Left)
            .WBind(ToggleButton.IsCheckedProperty, this, IsCheckedProperty, BindingMode.TwoWay)
            .WBind(ContentProperty, this, CheckContentProperty);

        _bubbleContentGrid.Children.Add(_checkBox);

        this.DisableAlternativeCancel(false);
    }

}
