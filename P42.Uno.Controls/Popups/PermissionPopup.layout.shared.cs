namespace P42.Uno.Controls;

public partial class PermissionPopup : Alert
{
    protected Button _cancelButton;
    protected ContentPresenter _cancelButtonContentPresenter;
    protected Grid _buttonBar;

    private void Build()
    {
        _bubbleContentGrid.Children.Remove(_okButton);
        _okButton.Row(0).Column(0);

        new Button()
            .Assign(out _cancelButton)
            .Column(1)
            .Margin(0)
            .Stretch()
            .CornerRadius(2)
            .WBind(ForegroundProperty, this, CancelButtonForegroundProperty)
            .WBind(Button.BackgroundProperty, this, CancelButtonBackgroundProperty)
            .WBind(ContentProperty, this, CancelButtonContentProperty);
                
        new Grid()
            .Assign(out _buttonBar)
            .Row(2)
            .Column(1)
            .ColumnSpacing(5)
            .Columns(
                GridRowsColumns.Star, GridRowsColumns.Star
            )
            .Children(
                _okButton, _cancelButton
            );

        _bubbleContentGrid.Children.Add(_buttonBar);
    }
}
