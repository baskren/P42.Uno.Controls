namespace P42.Uno.Controls;

public partial class Alert : Toast
{
    protected Button _okButton = new();
    //protected ContentPresenter _okButtonContentPresenter;

    private void Build()
    {
        Width = 300;

        _okButton
            .Row(2)
            .Column(1)
            .Margin(3)
            .Stretch()
            .CornerRadius(2)
            .Height(40)
            .AltBind(ContentProperty, this, OkButtonContentProperty);

        if (StaticResources.TryGetAs<Style>(Application.Current.Resources, "AccentButtonStyle", out var style))
            _okButton.Style = style;

        _bubbleContentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        _bubbleContentGrid.Children.Add(_okButton);

        this.DisableAlternativeCancel(); 
    }

}
