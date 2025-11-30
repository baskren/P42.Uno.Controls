
namespace P42.Uno.Controls.Demo;

[Bindable]
public class TypeCell : Grid
{
    private readonly TextBlock _textBlock = new TextBlock
    {
        HorizontalAlignment = HorizontalAlignment.Stretch,
        HorizontalTextAlignment = TextAlignment.Left,
        VerticalAlignment = VerticalAlignment.Stretch
    };

    public TypeCell()
    {
        Children.Add(_textBlock);
        DataContextChanged += OnDataContextChanged;
    }

#if !HAS_UNO

    private void OnDataContextChanged(Microsoft.UI.Xaml.FrameworkElement sender, Microsoft.UI.Xaml.DataContextChangedEventArgs args)
#else
    private void OnDataContextChanged(DependencyObject sender, DataContextChangedEventArgs args)
#endif
    {
        if (DataContext is Type type)
            _textBlock.Text = type.Namespace + "." + type.Name;
    }
}
