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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class ListEditPage : Page
    {
        Grid grid;
        SimpleListView listView;
        ContentAndDetailPresenter cdPresenter;

        bool IsUsingCdPresenter = true;

        void Build()
        {
            //var itemTemplate = (DataTemplate)Application.Current.Resources ["TextCellTemplate"];
            var itemTemplate = P42.Utils.Uno.UIElementExtensions.AsDataTemplate(typeof(P42.Uno.Controls.Test.TextCell));

            listView = new SimpleListView
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
                Tag = P42.Uno.Controls.ScrollIntoViewAlignment.Leading
            };
            button1.Click += OnButtonClick;
            var button2 = new Button
            {
                Content = "CENTER",
                Background = new SolidColorBrush(Colors.White),
                Tag = P42.Uno.Controls.ScrollIntoViewAlignment.Center
            };
            button2.Click += OnButtonClick;
            Grid.SetColumn(button2, 1);
            var button3 = new Button
            {
                Content = "BOTTOM",
                Background = new SolidColorBrush(Colors.White),
                Tag = P42.Uno.Controls.ScrollIntoViewAlignment.Trailing
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

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is string value)
            {
                if (listView.SelectedIndex > -1 && listView.SelectedIndex < items.Count)
                {
                    listView.ScrollIntoView(listView.SelectedIndex, (P42.Uno.Controls.ScrollIntoViewAlignment)button.Tag);
                    /*
                    var container = (ListViewItem)listView.ContainerFromIndex(listView.SelectedIndex);
                    var cell = container.ContentTemplateRoot as TextCell;
                    cell.ChangeValue(value);
                    */
                }
                else
                    listView.ScrollIntoView(items[25], (P42.Uno.Controls.ScrollIntoViewAlignment)button.Tag);
            }
        }
    }
}