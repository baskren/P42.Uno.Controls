using System.Collections.ObjectModel;
using P42.Uno.Markup;

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

    #region LocalText Property
    public static readonly DependencyProperty LocalTextProperty = DependencyProperty.Register(
        nameof(LocalText),
        typeof(string),
        typeof(MainPage),
        new PropertyMetadata(default(string))
    );
    public string LocalText
    {
        get => (string)GetValue(LocalTextProperty);
        set => SetValue(LocalTextProperty, value);
    }
    #endregion LocalText Property

    readonly TextBox TextBox = new();

    readonly ViewModel ViewModel = new ViewModel();

    public MainPage()
    {
        this.InitializeComponent();

        StackPanel.Padding(5).Spacing(5);
        

        StackPanel.Children.Add(new TextBlock().Text("SOURCE IS DEPENDENCY OBJECT"));
        StackPanel.Children.Add(new TextBox().Bind(TextBox.TextProperty, this, nameof(LocalText), Microsoft.UI.Xaml.Data.BindingMode.TwoWay));  // this updates only when the focus is lost (and the text changed)
        StackPanel.Children.Add(new TextBox().WBind(TextBox.TextProperty, this, LocalTextProperty, Microsoft.UI.Xaml.Data.BindingMode.TwoWay));  // this updates with every edit action

        StackPanel.Children.Add(new TextBlock().Bind(TextBlock.TextProperty, this, nameof(LocalText)));
        StackPanel.Children.Add(new TextBlock().WBind(TextBlock.TextProperty, this, LocalTextProperty));

        LocalText = "DPROP";

        StackPanel.Children.Add(new TextBlock().Text("---------------------------------------"));

        StackPanel.Children.Add(new TextBlock().Text("SOURCE IS INOTIFYABLE OBJECT"));
        StackPanel.Children.Add(new TextBox().Bind(TextBox.TextProperty, ViewModel, nameof(ViewModel.Name), Microsoft.UI.Xaml.Data.BindingMode.TwoWay));  // this updates only when the focus is lost (and the text changed)
        StackPanel.Children.Add(new TextBox().WBind(TextBox.TextProperty, ViewModel, nameof(ViewModel.Name), Microsoft.UI.Xaml.Data.BindingMode.TwoWay));  // this updates with every edit action

        StackPanel.Children.Add(new TextBlock().Bind(TextBlock.TextProperty, ViewModel, nameof(ViewModel.Name)));
        StackPanel.Children.Add(new TextBlock().WBind(TextBlock.TextProperty, ViewModel, nameof(ViewModel.Name)));

        ViewModel.Name = "IPROP";

        StackPanel.Children.Add(new TextBlock().Text("---------------------------------------"));

        StackPanel.Children.Add(new Button().Content("UPDATE TEXT").AddClickHandler(OnButtonTapped));
    }

    private void OnButtonTapped(object sender, RoutedEventArgs e)
    {
        LocalText = "UPDATED";
        ViewModel.Name = "UPDATED";
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        LocalText = TextBox.Text;
    }
}
