using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using P42.Uno.Markup;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class AlignedPlacementPanelTestPage : Page
{
    public AlignedPlacementPanelTestPage()
    {
        this.InitializeComponent();

        _grid.Children.Add(new P42.Uno.Controls.SegmentedControl()
            .Row(1)
            .Stretch()
            .Visible()
            .Foreground(Colors.Pink).Background(Colors.Blue)
            .SelectionMode(SelectionMode.Radio)
            .Labels(["LEFT", "CENTER", "RIGHT"])
        );

        _grid.Children.Add(new Button()
            .Row(2)
            .Margin(20, 0)
            .Foreground(Colors.Pink)
            .Background(Colors.Blue)
            .Content("ADD ITEM")
        );
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
