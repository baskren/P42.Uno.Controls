using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using P42.Uno.Markup;
using System.Collections.ObjectModel;
using P42.Utils.Uno;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo
{
    [Microsoft.UI.Xaml.Data.Bindable]
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
        SegmentedControl _segmentedControl;
        ComboBox _comboBox;

        void Build()
        {
            this.Background(Microsoft.UI.Colors.LightBlue);

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
                        .SelectedIndex(1),
                    new SegmentedControl()
                        .Assign(out _segmentedControl)
                        .Row(3)
                        .Bind(SegmentedControl.SelectionModeProperty, _comboBox, nameof(ComboBox.SelectedItem))
                );

            _segmentedControl.Labels = labels;
            _segmentedControl.SelectionChanged += _segmentedControl_SelectionChanged;
            _slider.ValueChanged += _slider_ValueChanged;
            _segmentedControl.IsOverflowedChanged += _segmentedControl_IsOverflowedChanged;

            _slider_ValueChanged(null, null);
        }

        private void _segmentedControl_IsOverflowedChanged(object sender, bool e)
        {
            Console.WriteLine($"_segmentedControl_IsOverflowedChanged [{e}]");
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

            Console.WriteLine($"TestPage._slider_ValueChanged label[{string.Join(", ", labels)}]");
        }
        #endregion
    }
}
