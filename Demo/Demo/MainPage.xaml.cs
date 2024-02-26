using System.Collections.ObjectModel;

namespace Demo;

public sealed partial class MainPage : Page
{
    ObservableCollection<Item> Items = new ObservableCollection<Item>
    {
        { "Item 1", "Value1" },
        { "Item 2", "Value2" },
        { "Item 3", "Value3" },
        { "Item 4", "Value4" },
        { "Item 5", "Value5" },
        { "Item 6", "Value6" },
        { "Item 7", "Value7" },
        { "Item 8", "Value8" },
        { "Item 9", "Value9" },
    };

    public MainPage()
    {
        this.InitializeComponent();
    }
}
