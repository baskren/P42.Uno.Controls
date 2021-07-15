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
using P42.Uno.AsyncNavigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Test
{
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            this.InitializeComponent();

            var classes = GetTypesInNamespace(typeof(PageMenu).Assembly, "P42.Uno.Controls.Test").ToList();
            var pageClasses = classes.Where(c => c.Name.EndsWith("Page")).ToList();

            var flexClasses = GetTypesInNamespace(typeof(PageMenu).Assembly, "FlexPanelTest").ToList();
            var flexPageClasses = flexClasses.Where(c => c.Name.EndsWith("Page")).ToList();
            pageClasses.AddRange(flexPageClasses);

            _listView.ItemsSource = pageClasses;
            _listView.ItemClick += OnListView_ItemClick;
            _listView.IsItemClickEnabled = true;
        }

        async void OnListView_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            if (e.ClickedItem is Type type)
            {
                var page = (Page)Activator.CreateInstance(type);
                await this.PushAsync(page);
            }
        }

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }
    }
}
