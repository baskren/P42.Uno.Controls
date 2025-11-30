using P42.Utils.Uno;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

[Bindable]
public partial class PageMenu : Page
{
    
    private readonly ListView _listView = new();

    void Build()
    {

        //var itemTemplate = P42.Utils.Uno.UIElementExtensions.AsDataTemplate(typeof(P42.Uno.Controls.Demo.TextCell));
        var itemTemplate = typeof(PageMenuCellTemplate).AsDataTemplate();

        Content = new Grid()
            .Rows(40, "*", 40)
            .Children
            (
                new TextBlock()
                    .Text("Test Pages")
                    .StretchHorizontal()
                    .CenterVertical()
                    .CenterTextAlignment(),
                _listView
                    .Row(1)
                    .ItemTemplate(itemTemplate)
            //.ItemTemplate("<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\" xmlns:local=\"using:P42.Uno.Controls.Demo\" xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" mc:Ignorable=\"d\" x:DataType=\"system.Type\"><StackPanel Orientation=\"Horizontal\"><TextBlock Text=\"{Binding Name}\" HorizontalAlignment=\"Left\" /></StackPanel></DataTemplate>")
            );
    }

}

[Bindable]
public class PageMenuCellTemplate : UserControl
{
    private readonly TextBlock _textBlock = new();

    public PageMenuCellTemplate()
    {
        Content = _textBlock;
        DataContextChanged += CellTemplate_DataContextChanged;
    }


    private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        _textBlock.Text = "TYPE: "+ DataContext;
    }

}
