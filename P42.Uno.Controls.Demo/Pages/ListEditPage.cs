
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public partial class ListEditPage 
{
    public static ListEditPage? Current { get; private set; }

    readonly System.Collections.ObjectModel.ObservableCollection<string> _items =
    [
            "1 one", "2 two", "3 three", "4 four", "5 five", "6 six", "7 seven", "8 eight", "9 nine", "10 ten", "11 eleven", "12 twelve", "13 thirteen", "14 fourteen", "15 fifteen", "16 sixteen", "17 seventeen", "18 eighteen", "19 nineteen", "20 twenty", "21 twenty one", "22 twenty two", "23 twenty three", "24 twenty four", "25 twenty five", "26 twenty six",
            "Item A", "Item B", "Item C", "Item D", "Item E", "Item F", "Item G", "Item H", "Item I", "Item J", "Item K", "Item L", "Item M", "Item N", "Item O", "Item P", "Item Q", "Item R", "Item S", "Item T", "Item U", "Item V", "Item W", "Item X", "Item Y", "Item Z",
            "Item A1", "Item B1", "Item C1", "Item D1", "Item E1", "Item F1", "Item G1", "Item H1", "Item I1", "Item J1", "Item K1", "Item L1", "Item M1", "Item N1", "Item O1", "Item P1", "Item Q1", "Item R1", "Item S1", "Item T1", "Item U1", "Item V1", "Item W1", "Item X1", "Item Y1", "Item Z1",
    ];

    public ListEditPage()
    {
        Current = this;
        //this.InitializeComponent();
        Build();

        _listView.ItemsSource = _items;

        //_listView.ItemClick += OnItemClicked;

    }

    //async void OnItemClicked(object sender, ItemClickEventArgs args)
    public async Task OnCellClicked(TextBlock textBlock)
    {
        System.Diagnostics.Debug.WriteLine("ListEditPage.CLICK!!!");
        //QuickMeasureList.Stopwatch.Restart();

        var items = new List<string> 
        { 
            "Item A", "Item B", "Item C", "Item D", "Item E", "Item F", "Item G", "Item H", "Item I", "Item J", "Item K", "Item L", "Item M", "Item N", "Item O", "Item P", "Item Q", "Item R", "Item S", "Item T", "Item U", "Item V", "Item W", "Item X", "Item Y", "Item Z",
            "Item A1", "Item B1", "Item C1", "Item D1", "Item E1", "Item F1", "Item G1", "Item H1", "Item I1", "Item J1", "Item K1", "Item L1", "Item M1", "Item N1", "Item O1", "Item P1", "Item Q1", "Item R1", "Item S1", "Item T1", "Item U1", "Item V1", "Item W1", "Item X1", "Item Y1", "Item Z1",
        };
        /*
#if __ANDROID__

        var aListView = new Android.Widget.ListView(global::Uno.UI.ContextHelper.Current)
        {
            LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent),
        };
        var adapter = new SimpleAdapter<string>(items); //new Android.Widget.ArrayAdapter(global::Uno.UI.ContextHelper.Current, aListView.Id, items);
        aListView.ItemClick += (s, e) =>
        {
            textBlock.Text = items[e.Position] ?? string.Empty;
        };
        aListView.Adapter = adapter;
        var listView = VisualTreeHelper.AdaptNative(aListView);
#else
        var listView = new QuickMeasureList
        {
            MinCellHeight = 40,
            SelectionMode = ListViewSelectionMode.Single,
            IsItemClickEnabled = true,
            IsMultiSelectCheckBoxEnabled = false,
            ItemsSource = items
        };
        listView.ItemClick += (s,e) =>
        {
            textBlock.Text = e.ClickedItem.ToString();
        };
#endif
        */

        var listView = new ListView
        {
            SelectionMode = ListViewSelectionMode.Multiple,
            IsItemClickEnabled = true,
            ItemsSource = items,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Width = 300
        };
        listView.ItemClick += (s, e) =>
        {
            textBlock.Text = e.ClickedItem.ToString();
        };


        Grid.SetRow(listView, 1);

        /*
        var before = (string)textBlock.Text;

        var button1 = new Button
        {
            Content = "Button 1",
            Background = new SolidColorBrush(Microsoft.UI.Colors.White)
        };
        var button2 = new Button
        {
            Content = "Button 2",
            Background = new SolidColorBrush(Microsoft.UI.Colors.White)
        };
        var button3 = new Button
        {
            Content = "ARRANGE",
            Foreground = new SolidColorBrush(Microsoft.UI.Colors.White),
            Background = new SolidColorBrush(Microsoft.UI.Colors.Red)
        };
        var stack = new StackPanel
        {
            //Margin = new Thickness(),
            HorizontalAlignment = HorizontalAlignment.Right,
            Orientation = Orientation.Horizontal,
            Padding = new Thickness(5),
            Spacing = 5,
            Background = new SolidColorBrush(Microsoft.UI.Colors.Blue),
            Children =
            {
                button1, button2, button3
            }
        };

        var grid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(40) },
                new RowDefinition { Height = GridLength.Auto }
            }
        };

        grid.Children.Add(stack);
        grid.Children.Add(listView);
        */

        if (IsUsingCdPresenter)
        {
            _cdPresenter.Detail = listView;

            _cdPresenter.Target = textBlock;

            //System.Diagnostics.Debug.WriteLine("ListEditPage t1: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);
            await _cdPresenter.PushDetailAsync();
            //System.Diagnostics.Debug.WriteLine("ListEditPage t2: " + QuickMeasureList.Stopwatch.ElapsedMilliseconds);
        }
        else
        {
            var popup = new TargetedPopup
                {
                    Content = _grid,
                    BackgroundColor = Microsoft.UI.Colors.Pink,
                    Target = textBlock
                }
                .Padding(4)
                // .PageOverlayBrush("#01FFFFFF")
                .Opacity(1)
                .Margin(100)
            .PreferredPointerDirection<TargetedPopup>(PointerDirection.Vertical)
            .FallbackPointerDirection(PointerDirection.Any);

            await popup.PushAsync();
        }
    }

}

