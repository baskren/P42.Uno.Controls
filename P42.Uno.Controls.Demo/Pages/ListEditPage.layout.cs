using P42.Utils.Uno;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
[Microsoft.UI.Xaml.Data.Bindable]
public partial class ListEditPage : Page
{
    private readonly Grid _grid = new()
    {
        RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star )},
                new RowDefinition { Height = new GridLength(40) },
            },
        Children =
            {
                new TextBlock
                {
                    Text = "Hello, World!",
                    Margin = new Thickness(20),
                    FontSize = 30,
                    HorizontalAlignment = HorizontalAlignment.Center
                },
                //listView,
                //footer
            }
    };
    private readonly ListView _listView = new();
    private readonly ContentAndDetailPresenter _cdPresenter = new();

    private const bool IsUsingCdPresenter = true;

    private void Build()
    {
        //var itemTemplate = (DataTemplate)Application.Current.Resources ["TextCellTemplate"];
        var itemTemplate = typeof(TextCell).AsDataTemplate();

        _listView.SelectionMode = ListViewSelectionMode.Single;
        _listView.IsItemClickEnabled = true;
        _listView.ItemTemplate = itemTemplate;
        
        Grid.SetRow(_listView, 1);

        var button1 = new Button
        {
            Content = "TOP",
            Background = new SolidColorBrush(Microsoft.UI.Colors.White),
            Tag = ScrollToPosition.Start //P42.Uno.Controls.ScrollIntoViewAlignment.Leading
        };
        button1.Click += OnButtonClick;
        var button2 = new Button
        {
            Content = "CENTER",
            Background = new SolidColorBrush(Microsoft.UI.Colors.White),
            Tag = ScrollToPosition.Center //P42.Uno.Controls.ScrollIntoViewAlignment.Center
        };
        button2.Click += OnButtonClick;
        Grid.SetColumn(button2, 1);
        var button3 = new Button
        {
            Content = "BOTTOM",
            Background = new SolidColorBrush(Microsoft.UI.Colors.White),
            Tag = ScrollToPosition.End //P42.Uno.Controls.ScrollIntoViewAlignment.Trailing
        };
        button3.Click += OnButtonClick;
        Grid.SetColumn(button3, 2);

        var footer = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength( 1, GridUnitType.Star)},
                new ColumnDefinition { Width = new GridLength( 1, GridUnitType.Star)},
                new ColumnDefinition { Width = new GridLength( 1, GridUnitType.Star)},
            },
            Children = { button1, button2, button3 }
        };
        Grid.SetRow(footer, 2);

        _grid.Children.Add(_listView);
        _grid.Children.Add(footer);

        if (IsUsingCdPresenter)
        {
            Content = _cdPresenter;
            _cdPresenter.Content = _grid;
        }
        else
        {
            Content = _grid;
        }
        
        //Content = listView;
    }

    private async void OnButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Content: string value } button)
            return;

        if (_listView.SelectedIndex > -1 && _listView.SelectedIndex < _items.Count)
            await _listView.ScrollToAsync(_listView.SelectedIndex, (ScrollToPosition)button.Tag);
        else
            await _listView.ScrollToAsync(_items[25], (ScrollToPosition)button.Tag);
    }
}
