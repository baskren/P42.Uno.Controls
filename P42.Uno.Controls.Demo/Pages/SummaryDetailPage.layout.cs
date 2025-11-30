

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
[Bindable]
public sealed partial class SummaryDetailPage : Page
{
    private readonly ContentAndDetailPresenter _contentAndDetailPresenter = new();
    private readonly ListView _listView = new();

    private void Build()
    {
        Content = new Grid()
            .Children
            (
                _contentAndDetailPresenter
                    .Content
                    (
                        _listView
                            .ItemTemplate(typeof(SummaryDetailPageCellTemplate))
                    )
            );

    }

}

[Bindable]
public class SummaryDetailPageCellTemplate : Button
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
