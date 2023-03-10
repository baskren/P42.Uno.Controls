using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using P42.Utils.Uno;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class ListEditPage : Page
    {
        Grid grid;
        ListView listView;
        ContentAndDetailPresenter cdPresenter;

        bool IsUsingCdPresenter = true;

        void Build()
        {
            //var itemTemplate = (DataTemplate)Application.Current.Resources ["TextCellTemplate"];
            var itemTemplate = P42.Utils.Uno.UIElementExtensions.AsDataTemplate(typeof(P42.Uno.Controls.Demo.TextCell));

            listView = new ListView
            {
                SelectionMode = ListViewSelectionMode.Single,
                IsItemClickEnabled = true,
                ItemTemplate = itemTemplate,
            };
            
            Grid.SetRow(listView, 1);

            var button1 = new Button
            {
                Content = "TOP",
                Background = new SolidColorBrush(Colors.White),
                Tag = P42.Utils.Uno.ScrollToPosition.Start //P42.Uno.Controls.ScrollIntoViewAlignment.Leading
            };
            button1.Click += OnButtonClick;
            var button2 = new Button
            {
                Content = "CENTER",
                Background = new SolidColorBrush(Colors.White),
                Tag = P42.Utils.Uno.ScrollToPosition.Center //P42.Uno.Controls.ScrollIntoViewAlignment.Center
            };
            button2.Click += OnButtonClick;
            Grid.SetColumn(button2, 1);
            var button3 = new Button
            {
                Content = "BOTTOM",
                Background = new SolidColorBrush(Colors.White),
                Tag = P42.Utils.Uno.ScrollToPosition.End //P42.Uno.Controls.ScrollIntoViewAlignment.Trailing
            };
            button3.Click += OnButtonClick;
            Grid.SetColumn(button3, 2);

            var footer = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength( 1, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength( 1, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength( 1, GridUnitType.Star)},
                },
                Children = { button1, button2, button3 }
            };
            Grid.SetRow(footer, 2);

            grid = new Grid
            {
                RowDefinitions = 
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star )},
                    new RowDefinition { Height = new GridLength(40) },
                },
                Children =
                {
                    new TextBlock 
                    {
                        Text = "Hello, World!",
                        Margin = new Thickness(20),
                        FontSize = 30,
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    listView,
                    footer
                }
            };
            if (IsUsingCdPresenter)
            {
                Content = cdPresenter = new ContentAndDetailPresenter
                {
                    Content = grid
                };
            }
            else
            {
                Content = grid;
            }
            
            //Content = listView;
        }

        async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is string value)
            {
                if (listView.SelectedIndex > -1 && listView.SelectedIndex < items.Count)
                    await listView.ScrollToAsync(listView.SelectedIndex, (P42.Utils.Uno.ScrollToPosition)button.Tag);
                else
                    await listView.ScrollToAsync(items[25], (P42.Utils.Uno.ScrollToPosition)button.Tag);
            }
        }
    }
}