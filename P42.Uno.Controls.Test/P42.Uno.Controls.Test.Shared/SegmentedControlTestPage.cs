using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using P42.Uno.Markup;
using System.Collections.ObjectModel;
using P42.Utils.Uno;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Test
{
    public partial class SegmentedControlTestPage : Page
    {
        #region Fields
        ObservableCollection<string> labels = new ObservableCollection<string>();
        List<string> numbers = new List<string>
        {
            "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty", "twenty one", "twenty two", "twenty three", "twenty four", "twenty five", "twenty six", "twenty seven", "twenty eight", "twenty nine", "thirty"
        };
        #endregion
        
        public SegmentedControlTestPage()
        {
            Build();
        }

        #region Layout
        Slider _slider;
        NewSegmentedControl _segmentedControl;
        ComboBox _comboBox;

        void Build()
        {
            Content = new Grid()
                .Rows(40, 40, 40, "*")
                .Margin(10,50,10,50)
                .Children
                (
                    new TextBlock()
                        .Row(0)
                        .Text("SegementedControl Test Page")
                        .Center(),
                    new Slider()
                        .Assign(out _slider)
                        .Row(1)
                        .MinMax(0,20)
                        .Value(0),
                    new ComboBox()
                        .Assign(out _comboBox)
                        .Row(2)
                        .ItemsSource(Enum.GetValues(typeof(P42.Uno.Controls.SelectionMode)))
                        .SelectionMode(ListViewSelectionMode.Single)
                        .SelectedIndex(1),
                    new NewSegmentedControl()
                        .Assign(out _segmentedControl)
                        .Row(3)
                        .Bind(NewSegmentedControl.SelectionModeProperty, _comboBox, nameof(ComboBox.SelectedItem))
                );

            _segmentedControl.Labels = labels;
            _segmentedControl.SelectionChanged += _segmentedControl_SelectionChanged;
            _slider.ValueChanged += _slider_ValueChanged;

            _slider_ValueChanged(null, null);
        }

        private void _segmentedControl_SelectionChanged(object sender, (int SelectedIndex, string SelectedItem) e)
        {
            System.Diagnostics.Debug.WriteLine($"New Selection: [{e.SelectedItem}]");
        }

        private void _slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            while (_slider.Value < labels.Count)
                labels.RemoveAt(labels.Count - 1);

            while (_slider.Value > labels.Count)
                labels.Add(numbers[labels.Count]);
        }
        #endregion
    }
}
