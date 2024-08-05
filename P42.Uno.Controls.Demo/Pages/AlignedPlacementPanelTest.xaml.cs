

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class AlignedPlacementPanelTestPage : Page
{
    public AlignedPlacementPanelTestPage()
    {
        InitializeComponent();

        Grid.Children.Add(new SegmentedControl()
            .Row(1)
            .Stretch()
            .Visible()
            .Foreground(Microsoft.UI.Colors.Pink).Background(Microsoft.UI.Colors.Blue)
            .SelectionMode(SelectionMode.Radio)
            .Labels(["LEFT", "CENTER", "RIGHT"])
        );

        Grid.Children.Add(new Button()
            .Row(2)
            .Margin(20, 0)
            .Foreground(Microsoft.UI.Colors.Pink)
            .Background(Microsoft.UI.Colors.Blue)
            .Content("ADD ITEM")
        );
    }

}
