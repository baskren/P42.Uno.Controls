namespace P42.Uno.Controls.Demo;

public sealed partial class MainPage : Page
{
    readonly Dictionary<string, Page> _pages = [];
    //Page[] pageTypes = [typeof()]

    public MainPage()
    {
        InitializeComponent();

        var classes = GetTypesInNamespace(typeof(PageMenu).Assembly, "P42.Uno.Controls.Demo").ToList();
        var pageClasses = classes.Where(c => c.Name.EndsWith("Page")).ToList();

        var flexClasses = GetTypesInNamespace(typeof(PageMenu).Assembly, "FlexPanelTest").ToList();
        var flexPageClasses = flexClasses.Where(c => c.Name.EndsWith("Page")).ToList();
        pageClasses.AddRange(flexPageClasses);

        //NavView.MenuItemTemplate = typeof(NavItemTemplate).AsDataTemplate();

        foreach (var type in pageClasses)
        {
            if (type == typeof(MainPage))
                continue;

            if (Activator.CreateInstance(type) is not Page page)
                continue;

            _pages[type.Name] = page;
            NavView.MenuItems.Add(new NavigationViewItem
            {
                Content = type.Name,
            });

            NavView.SelectionChanged += OnNavViewSelectionChanged;
        }
    }

    private void OnNavViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is not NavigationViewItem item)
            return;

        if (item.Content is string typeName && _pages.TryGetValue(typeName, out var page))
        { 
            NavView.Content = page;
            HeaderTextBlock.Text = string.IsNullOrWhiteSpace(page.Name)
                ? page.GetType().Name
                :page.Name;
            return; 
        }


        NavView.Content = $"COULD NOT CREATE PAGE FROM ITEM CONTENT [{item.Content}] ";
    }

    /*
    private void _button_Click(object sender, RoutedEventArgs e)
    {
        
    }
    */

    private static Type[] GetTypesInNamespace(System.Reflection.Assembly assembly, string nameSpace)
    {
        return
          assembly.GetTypes()
                  .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                  .ToArray();
    }

}

[Microsoft.UI.Xaml.Data.Bindable]
public partial class NavItemTemplate : Grid
{
    private readonly TextBlock _textBlock = new();
    public NavItemTemplate()
    {
        Children.Add(_textBlock);
        DataContextChanged += NavItemTemplate_DataContextChanged;
    }

    private void NavItemTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        if (DataContext is NavigationViewItem item)
        {
            _textBlock.Text = item.Content switch
            {
                Type type => type.Name,
                Page page => page.GetType().Name,
                _ => item.Content?.ToString() ?? "NULL"
            };
        }
        else
            _textBlock.Text = DataContext?.ToString() ?? "NULL";
    }
}


