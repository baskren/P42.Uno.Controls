

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using System.Reflection;

namespace P42.Uno.Controls.Demo;

public partial class PageMenu
{
    public static PageMenu? Current { get; private set; }
    public PageMenu()
    {
        //this.InitializeComponent();
        Build();
        Current = this;

        var classes = GetTypesInNamespace(typeof(PageMenu).Assembly, "P42.Uno.Controls.Demo").ToList();
        var pageClasses = classes.Where(c => c.Name.EndsWith("Page")).ToList();

        var flexClasses = GetTypesInNamespace(typeof(PageMenu).Assembly, "FlexPanelTest").ToList();
        var flexPageClasses = flexClasses.Where(c => c.Name.EndsWith("Page")).ToList();
        pageClasses.AddRange(flexPageClasses);

        _listView.ItemsSource = pageClasses.Select(t => t.FullName + ", " + t.Assembly);
        _listView.ItemClick += OnListView_ItemClick;
        _listView.IsItemClickEnabled = true;
    }

    private void OnListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        ItemClickProcess(e.ClickedItem);
    }

    private void ItemClickProcess(object? item)
    {
        if (item is string text)
            item = Type.GetType(text);
        if (item is Type type)
        {
            //var page = (Page)Activator.CreateInstance(type);
            //await this.PushAsync(page);
            Frame.Navigate(type);
        }
    }

    private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        return
          assembly.GetTypes()
                  .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                  .ToArray();
    }
}
