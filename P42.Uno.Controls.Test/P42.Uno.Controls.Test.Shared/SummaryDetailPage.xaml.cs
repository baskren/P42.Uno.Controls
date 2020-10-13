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
using Windows.UI.Xaml.Shapes;

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
            _listView.ChoosingItemContainer += _listView_ChoosingItemContainer;
            _listView.ContainerContentChanging += _listView_ContainerContentChanging;
        }

        private void _listView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var itemContainer = args.ItemContainer;
            var index = args.ItemIndex;
            //var phase = args.Phase;

            if (args.ItemContainer?.ContentTemplateRoot is StringButtonUserControl templateRoot)
            {

                Grid grid = (Grid)templateRoot.FindName("_grid");
                Rectangle rect = (Rectangle)templateRoot.FindName("_rectangle");
                Button button = (Button)templateRoot.FindName("_button");

                button.Opacity = 1;
                rect.Opacity = 0;
                //args.RegisterUpdateCallback(SetCellTitle);
            }
            System.Diagnostics.Debug.WriteLine("SummaryDetailPage._listView_ContainerContentChanging itemContainer:" + itemContainer + " index:" + index);
        }

        private void SetCellTitle(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.ItemContainer?.ContentTemplateRoot is StringButtonUserControl templateRoot)
            {
                Grid grid = (Grid)templateRoot.FindName("_grid");
                Rectangle rect = (Rectangle)templateRoot.FindName("_rectangle");
                Button button = (Button)templateRoot.FindName("_button");

                button.Opacity = 1;
                rect.Opacity = 0;
            }
        }


        private void _listView_ChoosingItemContainer(ListViewBase sender, ChoosingItemContainerEventArgs args)
        {
            //throw new NotImplementedException();
            var itemContainer = args.ItemContainer;
            var index = args.ItemIndex;
            System.Diagnostics.Debug.WriteLine("SummaryDetailPage._listView_ChoosingItemContainer itemContainer:" + itemContainer + " index:" + index);
        }

        async void OnItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(GetType() + ".OnItemClick: sender:" + sender + "  e.ClickedItem:" + e.ClickedItem + " e.OriginalSource:" + e.OriginalSource);

            var ring = await P42.Uno.Controls.BusyPopup.CreateAsync(null, "I'm very busy", TimeSpan.FromSeconds(2));
            await ring.WaitForPoppedAsync();

            var listViewItem = _listView.ContainerFromItem(e.ClickedItem);
            if (listViewItem is ListViewItem item)
            {
                var cellView = item.ContentTemplateRoot;
                if (cellView is StringButtonUserControl view)
                {
                    view.CellTapped();
                }
            }
        }




    }
}
