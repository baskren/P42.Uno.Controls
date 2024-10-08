

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
[Microsoft.UI.Xaml.Data.Bindable]
public sealed partial class LoopingFlipViewTestPage : Page
{
    private readonly Grid 
        _innerGrid = new(), 
        _outerGrid = new();
    private readonly Button _button = new();
    private readonly ContentPresenter _contentPresenter = new();

    private void Build()
    {
        _contentPresenter.Content = 
        _outerGrid
            .Rows(30, 30, "auto")
            .Children(
                new Grid()
                    .Row(2)
                    .Columns(80, 80, 80, 80, 80)
                    .Rows(50, 50, 50, 50, 50)
                    .RowSpacing(1)
                    .ColumnSpacing(1)
                    .Children(
                        _innerGrid
                            .ColumnSpan(4)
                            .Padding(0)
                            .Margin(0)
                            .ColumnSpacing(1),
                        new Item("A5").RowCol(0, 4),
                        new Item("B1").RowCol(1, 0),
                        new Item("B2").RowCol(1, 1),
                        new Item("B3").RowCol(1, 2),
                        new Item("B4").RowCol(1, 3),
                        new Item("B5").RowCol(1, 4),
                        new Item("C1").RowCol(2, 0),
                        new Item("C2").RowCol(2, 1),
                        new Item("C3").RowCol(2, 2),
                        new Item("C4").RowCol(2, 3),
                        new Item("C5").RowCol(2, 4),
                        new Item("D1").RowCol(3, 0),
                        new Item("D2").RowCol(3, 1),
                        new Item("D3").RowCol(3, 2),
                        new Item("D4").RowCol(3, 3),
                        new Item("D5").RowCol(3, 4),
                        new Item("E1").RowCol(4, 0),
                        new Item("E2").RowCol(4, 1),
                        new Item("E3").RowCol(4, 2),
                        new Item("E4").RowCol(4, 3),
                        new Item("e5").RowCol(4, 4)
                    )
                );
        /*
        _innerGrid = new Grid
        {
            Padding = new Thickness(0),
            Margin = new Thickness(0),
            BorderBrush = new SolidColorBrush(Microsoft.UI.Colors.Blue),
            BorderThickness = new Thickness(2,2,2,2)
        };
        */

        Content = _button
            .Content("Show Popup")
            .Center();

        _button.Click += OnButtonClick;
    }

    private static int _clicks;
    private static bool _clicking;
    private async void OnButtonClick(object sender, RoutedEventArgs e)
    {
        if (_clicking)
            return;
        _clicking = true;
        System.Diagnostics.Debug.WriteLine($"CLICKS: {++_clicks}");

        var popup = await TargetedPopup.CreateAsync(_button, _contentPresenter);
        await popup.WaitForPoppedAsync();
        popup.Content = null;
        _clicking = false;
    }
}
