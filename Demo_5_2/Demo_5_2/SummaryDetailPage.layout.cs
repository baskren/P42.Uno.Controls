using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Microsoft.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
[Microsoft.UI.Xaml.Data.Bindable]
public sealed partial class SummaryDetailPage : Page
{
    ContentAndDetailPresenter _ContentAndDetailPresenter = new();
    ListView _listView = new();

    void Build()
    {
        Content = new Grid()
            .Children
            (
                _ContentAndDetailPresenter
                    .Content
                    (
                        _listView
                            .ItemTemplate(typeof(SummaryDetailPageCellTemplate))
                    )
            );

    }

}

[Bindable]
public partial class SummaryDetailPageCellTemplate : Button
{
    public SummaryDetailPageCellTemplate()
    {
        this.Padding(20, 2)
            .StretchHorizontal()
            .ContentRight()
            .Background(Colors.Beige)
            .BorderBrush(Colors.Green)
            .BorderThickness(1)
            //.AddTapHandler(BorderTapped)
            .CornerRadius(5);

        DataContextChanged += CellTemplate_DataContextChanged;
    }


    private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        Content = DataContext?.ToString() ?? string.Empty;
    }
}
