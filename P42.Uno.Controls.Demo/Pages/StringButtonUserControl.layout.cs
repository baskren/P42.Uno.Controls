

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace P42.Uno.Controls.Demo;

[Bindable]
public partial class StringButtonUserControl : UserControl
{
    private readonly Grid _grid = new();
    private readonly Rectangle _rectangle = new();
    private readonly Button _button = new();

    private void Build()
    {
        Content = _grid
                    .Stretch()
                    .Children
                    (
                        _rectangle
                            .Stretch()
                            .Fill(Colors.Gray),
                        _button
                            .Padding(20,2)
                            .StretchHorizontal()
                            .CenterVertical()
                            .ContentCenter()
                            .Background(Colors.Beige)
                            .BorderBrush(Colors.Green)
                            .BorderThickness(1)
                            .CornerRadius(5)
                            .Opacity(0)
                            .AddTapHandler(BorderTapped)
                            .Bind(ContentProperty, this, nameof(Content))
                    );
    }

}
