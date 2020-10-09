using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SummaryDetailPage : Page
    {
        public SummaryDetailPage()
        {
            this.InitializeComponent();
            _listView.ItemsSource = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };
            _listView.ItemClick += OnItemClick;
            //_listView.SelectionMode = ListViewSelectionMode.Single;
            _listView.IsItemClickEnabled = true;
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            
            System.Diagnostics.Debug.WriteLine(GetType() + ".OnItemClick: sender:" + sender + "  e.ClickedItem:" + e.ClickedItem + " e.OriginalSource:" + e.OriginalSource);
        }

        async void BorderTapped(object sender, TappedRoutedEventArgs e)
        {
            _ContentAndDetailPresenter.Target = (UIElement)sender;
            _ContentAndDetailPresenter.Detail = new TextBlock { Text = ((Button)sender).Content as string };
            await _ContentAndDetailPresenter.PushDetail();
        }



    }
}
