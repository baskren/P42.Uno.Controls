namespace P42.Uno.Controls.Demo;

[Bindable]
public class TextCell : Grid
{
    readonly TextBlock _valueLabel = new();
    readonly TextBlock _titleLabel = new();
    public  TextCell()
    {
        RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        Children.Add(_titleLabel);

        _valueLabel.HorizontalAlignment = HorizontalAlignment.Right;
        SetRow(_valueLabel, 1);
        Children.Add(_valueLabel);

        DataContextChanged += TextCell_DataContextChanged;
        Tapped += TextCell_Tapped;
    }

    async void TextCell_Tapped(object sender, TappedRoutedEventArgs e)
    {
        await (ListEditPage.Current?.OnCellClicked(_valueLabel) ?? Task.CompletedTask);
    }

#if !HAS_UNO
    private void TextCell_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
#else
    private void TextCell_DataContextChanged(DependencyObject sender, DataContextChangedEventArgs args)
#endif
    {
        Debug.WriteLine("TextCell.DataContextChanged!!!");
        switch (DataContext)
        {
            case null:
                _titleLabel.Text = _valueLabel.Text = string.Empty;
                break;
            case string value:
                _titleLabel.Text = _valueLabel.Text = value;
                break;
            case Type type:
                _titleLabel.Text = type.FullName ?? string.Empty;
                _valueLabel.Text = type.Name;
                break;
            default:
                _titleLabel.Text = DataContext.GetType().ToString();
                _valueLabel.Text = DataContext.ToString();
                break;
        }
    }
    
    /*
    public void ChangeValue(string newValue)
    {
        _valueLabel.Text = newValue ?? string.Empty;
    }
    */
}
