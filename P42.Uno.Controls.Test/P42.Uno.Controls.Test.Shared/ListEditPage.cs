using P42.Uno.Markup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

#if NETFX_CORE
using Popup = Windows.UI.Xaml.Controls.Primitives.Popup;
#else
using Popup = Windows.UI.Xaml.Controls.Popup;
#endif

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class ListEditPage : Page
    {
        public static ListEditPage Current;

    ObservableCollection<string> items = new ObservableCollection<string> { "1 one", "2 two", "3 three", "4 four", "5 five", "6 six", "7 seven", "8 eight", "9 nine", "10 ten", "11 eleven", "12 twelve", "13 thirteen", "14 fourteen", "15 fifteen", "16 sixteen", "17 seventeen", "18 eighteen", "19 nineteen", "20 twenty", "21 twenty one", "22 twenty two", "23 twenty three", "24 twenty four", "25 twenty five", "26 twenty six" };

        public ListEditPage()
        {
            Current = this;
            //this.InitializeComponent();
            Build();

            listView.ItemsSource = items;

            //_listView.ItemClick += OnItemClicked;

        }

        //async void OnItemClicked(object sender, ItemClickEventArgs args)
        public async Task OnCellClicked(TextBlock textBlock)
        {
            var items = new List<string> { "Item A", "Item B", "Item C", "Item D" };
            var listView = new ListView
            {
                SelectionMode = ListViewSelectionMode.Single,
                IsItemClickEnabled = true,
                IsMultiSelectCheckBoxEnabled = false,
                ItemsSource = items
            };
            listView.ItemClick += (s,e) =>
            {
                textBlock.Text = e.ClickedItem.ToString();
            };



            /*
            var before = (string)textBlock.Text;

            var button1 = new Button
            {
                Content = "Button 1",
                Background = new SolidColorBrush(Colors.White)
            };
            var button2 = new Button
            {
                Content = "Button 2",
                Background = new SolidColorBrush(Colors.White)
            };
            Grid.SetRow(button2, 1);
            var button3 = new Button
            {
                Content = "ARRANGE",
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.Red)
            };
            Grid.SetRow(button3, 2);
            var grid = new Grid
            {
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                RowSpacing = 10,
                Background = new SolidColorBrush(Colors.Blue),
                RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                    },
                Children =
                    {
                        button1, button2, button3
                    }
            };

            button1.Click += (s, e) =>
            {
                textBlock.Text = "BUTTON 1";
            };
            button2.Click += (s, e) =>
            {
                textBlock.Text = "BUTTON 2";
            };
            button3.Click += (s, e) =>
            {
                listView.Measure(new Size(ActualWidth, ActualHeight));
                listView.Arrange(new Rect(new Point(0,0), listView.DesiredSize));
                textBlock.Text = "ARRANGED";
            };

            grid.Loaded += async (s, e) =>
            {
                await Task.Delay(100);
                System.Diagnostics.Debug.WriteLine("LOADED");
            };
            /*
            var popup = new Popup
            {
                HorizontalOffset = 100,
                VerticalOffset = 100,
                Child = grid,
                LightDismissOverlayMode = LightDismissOverlayMode.On,
                IsLightDismissEnabled = true
            };
            popup.IsOpen = true;
            */
            if (IsUsingCdPresenter)
            {
                cdPresenter.Detail = listView;

                cdPresenter.Target = textBlock;
                await cdPresenter.PushDetailAsync();
            }
            else
            {
                var popup = new TargetedPopup
                {
                    PopupContent = grid,
                    Background = new SolidColorBrush(Colors.Pink),
                    Target = textBlock
                }
                .Padding(4)
                //.LightDismissOverlayBrush("#01FFFFFF")
                .Opacity(1)
                .Margin(100)
                .PreferredPointerDirection(PointerDirection.Vertical)
                .FallbackPointerDirection(PointerDirection.Any);

                await popup.PushAsync();
            }
        }

    }
}
