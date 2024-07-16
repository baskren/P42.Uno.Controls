using System.Reflection;
using P42.Utils.Uno;

namespace P42.Uno.Controls.Demo;

public sealed partial class MainPage : Page
{
    readonly Dictionary<string, Page> _pages = [];
    //Page[] pageTypes = [typeof()]

    public MainPage()
    {
        this.InitializeComponent();

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

        if (item.Content is string typeName && _pages.TryGetValue(typeName, out Page page))
        { 
            NavView.Content = page;
            return; 
        }


        NavView.Content = $"COULD NOT CREATE PAGE FROM ITEM CONTENT [{item.Content}] ";
    }

    private void _button_Click(object sender, RoutedEventArgs e)
    {
        
    }

    private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        return
          assembly.GetTypes()
                  .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                  .ToArray();
    }

}

[Microsoft.UI.Xaml.Data.Bindable]
public partial class NavItemTemplate : Grid
{
    readonly TextBlock textBlock = new();
    public NavItemTemplate()
    {
        Children.Add(textBlock);
        DataContextChanged += NavItemTemplate_DataContextChanged;
    }

    private void NavItemTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        if (DataContext is NavigationViewItem item)
        {
            if (item.Content is Type type)
                textBlock.Text = type.Name;
            else if (item.Content is Page page)
                textBlock.Text = page.GetType().Name;
            else
                textBlock.Text = item.Content?.ToString() ?? "NULL";
        }
        else
            textBlock.Text = DataContext?.ToString() ?? "NULL";
    }
}


