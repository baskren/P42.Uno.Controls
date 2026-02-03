namespace P42.Uno.Controls;

public partial class PermissionPopup : Alert
{
    protected Button _cancelButton = new();
    //protected ContentPresenter _cancelButtonContentPresenter;
    protected Grid _buttonBar = new();

    private void Build()
    {
        _bubbleContentGrid.Children.Remove(_okButton);
        _okButton.Row(0).Column(0);

        _cancelButton
            .Column(1)
            .Margin(0)
            .Stretch()
            .CornerRadius(2)
            .AltBind(ForegroundProperty, this, CancelButtonForegroundProperty)
            .AltBind(Button.BackgroundProperty, this, CancelButtonBackgroundProperty)
            .AltBind(ContentProperty, this, CancelButtonContentProperty);
                
        _buttonBar
            .Row(2)
            .Column(1)
            .ColumnSpacing(5)
            .Columns(
                GridLengthExtensions.Star(), GridLengthExtensions.Star()
            )
            .Children(
                _okButton, _cancelButton
            );

        _bubbleContentGrid.Children.Add(_buttonBar);
    }
}
