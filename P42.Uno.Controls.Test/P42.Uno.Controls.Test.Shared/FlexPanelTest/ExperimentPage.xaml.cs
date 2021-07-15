using P42.Uno.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class ExperimentPage : Page
    {
        static Color[] colors = { Colors.Red, Colors.Magenta, Colors.Blue,
                                  Colors.Cyan, Colors.Green, Colors.Yellow };

        static string[] digitsText = { "", "One", "Two", "Three", "Four", "Five",
                                       "Six", "Seven", "Eight", "Nine", "Ten",
                                       "Eleven", "Twelve", "Thirteen", "Fourteen",
                                       "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

        static string[] decadeText = { "", "", "Twenty", "Thirty", "Forty", "Fifty",
                                       "Sixty", "Seventy", "Eighty", "Ninety" };

        public ExperimentPage()
        {
            this.InitializeComponent();
            OnNumberStepperValueChanged(flexPanel, numberStepper.Value);
        }

        private void Combo_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void OnNumberStepperValueChanged(object sender, double newValue)
        {
            if (flexPanel == null)
                return;

            int count = (int)newValue;

            while (flexPanel.Children.Count > count)
            {
                flexPanel.Children.RemoveAt(flexPanel.Children.Count - 1);
            }
            while (flexPanel.Children.Count < count)
            {
                int number = flexPanel.Children.Count + 1;
                string text = "";

                if (number < 20)
                {
                    text = digitsText[number];
                }
                else
                {
                    text = decadeText[number / 10] +
                           (number % 10 == 0 ? "" : "-") +
                                digitsText[number % 10];
                }

                var label = new TextBlock
                {
                    Text = text,
                    FontSize = 16 + 4 * ((number - 1) % 4),
                    Foreground = new SolidColorBrush(colors[(number - 1) % colors.Length]),
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    //Background = new SolidColorBrush(Colors.LightGray)
                };
                var border = new Border
                {
                    Background = new SolidColorBrush(Colors.LightGray),
                    BorderThickness = new Thickness(0),
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    //HorizontalAlignment = HorizontalAlignment.Center,
                    //VerticalAlignment = VerticalAlignment.Center,
                    Child = label,
                    
                };

                flexPanel.Children.Add(border);
            }
        }

        void OnFlexDirectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            //flexPanel.Direction = 
            if (sender is ComboBox picker)
            {
                var direction = (FlexDirection)picker.SelectedItem;
                flexPanel.Direction = direction;
            }
        }
    }
}
