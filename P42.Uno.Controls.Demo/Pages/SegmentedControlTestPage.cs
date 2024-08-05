

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

[Microsoft.UI.Xaml.Data.Bindable]
public partial class SegmentedControlTestPage : Page
{
    #region Fields
    private readonly System.Collections.ObjectModel.ObservableCollection<string> _labels = [];
    private readonly List<string> _numbers =
    [
        "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty", "twenty one", "twenty two", "twenty three", "twenty four", "twenty five", "twenty six", "twenty seven", "twenty eight", "twenty nine", "thirty"
    ];
    #endregion
    
    public SegmentedControlTestPage()
    {
        Build();
    }

    #region Layout
    private readonly Slider _slider = new();
    private readonly SegmentedControl _segmentedControl = new();
    private readonly ComboBox _comboBox = new();

    private void Build()
    {
        this.Background(Microsoft.UI.Colors.LightBlue);

        Content = new Grid()
            .Rows(40, 40, 'a', '*')
            .Margin(10,50,10,50)
            .Children
            (
                new TextBlock()
                    .Row(0)
                    .Text("SegmentedControl Test Page")
                    .Center(),
                _slider
                    .Row(1)
                    .MinMax(0,20)
                    .Value(0),
                _comboBox
                    .Row(2)
                    .ItemsSource(Enum.GetValues(typeof(SelectionMode)))
                    .SelectedIndex(1),
                _segmentedControl
                    .Row(3)
                    .Bind(SegmentedControl.SelectionModeProperty, _comboBox, nameof(ComboBox.SelectedItem))
            );

        _segmentedControl.Labels = _labels;
        _segmentedControl.SelectionChanged += _segmentedControl_SelectionChanged;
        _slider.ValueChanged += _slider_ValueChanged;
        _segmentedControl.IsOverflowedChanged += _segmentedControl_IsOverflowedChanged;

        _slider_ValueChanged(null, null);
    }

    private void _segmentedControl_IsOverflowedChanged(object? sender, bool e)
    {
        Console.WriteLine($"_segmentedControl_IsOverflowedChanged [{e}]");
    }

    private void _segmentedControl_SelectionChanged(object? sender, (int SelectedIndex, string SelectedItem) e)
    {
        System.Diagnostics.Debug.WriteLine($"New Selection: [{e.SelectedItem}]");
    }

    private void _slider_ValueChanged(object? sender, RangeBaseValueChangedEventArgs? e)
    {
        while (_slider.Value < _labels.Count)
            _labels.RemoveAt(_labels.Count - 1);

        while (_slider.Value > _labels.Count)
            _labels.Add(_numbers[_labels.Count]);

        Console.WriteLine($"TestPage._slider_ValueChanged label[{string.Join(", ", _labels)}]");
    }
    #endregion
}
