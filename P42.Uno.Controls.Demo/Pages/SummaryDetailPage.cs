

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SummaryDetailPage 
{
    public SummaryDetailPage()
    {
        //this.InitializeComponent();
        Build();

        _listView.ItemsSource = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };
        _listView.ItemClick += OnItemClick;
        //_listView.SelectionMode = ListViewSelectionMode.Single;
        _listView.IsItemClickEnabled = true;
        _listView.ChoosingItemContainer += OnListView_ChoosingItemContainer;
        _listView.ContainerContentChanging += OnListView_ContainerContentChanging;
    }

    private static void OnListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
    {
        //var itemContainer = args.ItemContainer;
        //var index = args.ItemIndex;
        //var phase = args.Phase;

        if (args.ItemContainer?.ContentTemplateRoot is not StringButtonUserControl templateRoot)
            return;

        //var grid = (Grid)templateRoot.FindName("_grid");
        var rect = (Rectangle)templateRoot.FindName("_rectangle");
        var button = (Button)templateRoot.FindName("_button");

        button.Opacity = 1;
        rect.Opacity = 0;
        //args.RegisterUpdateCallback(SetCellTitle);
        //System.Diagnostics.Debug.WriteLine("SummaryDetailPage._listView_ContainerContentChanging itemContainer:" + itemContainer + " index:" + index);
    }

    /*
    private void SetCellTitle(ListViewBase sender, ContainerContentChangingEventArgs args)
    {
        if (args.ItemContainer?.ContentTemplateRoot is not StringButtonUserControl templateRoot)
            return;

        //var grid = (Grid)templateRoot.FindName("_grid");
        var rect = (Microsoft.UI.Xaml.Shapes.Rectangle)templateRoot.FindName("_rectangle");
        var button = (Button)templateRoot.FindName("_button");

        button.Opacity = 1;
        rect.Opacity = 0;
    }
    */

    private static void OnListView_ChoosingItemContainer(ListViewBase sender, ChoosingItemContainerEventArgs args)
    {
        //throw new NotImplementedException();
        var itemContainer = args.ItemContainer;
        var index = args.ItemIndex;
        Debug.WriteLine("SummaryDetailPage._listView_ChoosingItemContainer itemContainer:" + itemContainer + " index:" + index);
    }

    private async void OnItemClick(object sender, ItemClickEventArgs e)
    {
        Debug.WriteLine(GetType() + ".OnItemClick: sender:" + sender + "  e.ClickedItem:" + e.ClickedItem + " e.OriginalSource:" + e.OriginalSource);

        var ring = await BusyPopup.CreateAsync(null, "I'm very busy", TimeSpan.FromSeconds(2));
        await ring.WaitForPoppedAsync();

        return;
        var listViewItem = _listView.ContainerFromItem(e.ClickedItem);
        if (listViewItem is not ListViewItem item)
            return;

        var cellView = item.ContentTemplateRoot;
        if (cellView is StringButtonUserControl view)
            view.OnCellTapped();
        
    }

    /*
    private async void BorderTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(GetType() + ".BorderTapped: sender:" + sender + " e.PointerDeviceType:" + e.PointerDeviceType);
        if (sender is not Button { Content: string text })
            return;

        _contentAndDetailPresenter.Detail = new TextBlock { Text = text };
        await _contentAndDetailPresenter.PushDetailAsync();
    }
    */



}
