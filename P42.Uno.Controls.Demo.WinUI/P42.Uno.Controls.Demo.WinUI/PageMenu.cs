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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo
{
    public partial class PageMenu : Page
    {
        public static PageMenu Current;
        public PageMenu()
        {
            //this.InitializeComponent();
            Build();
            Current = this;

            var classes = GetTypesInNamespace(typeof(PageMenu).Assembly, "P42.Uno.Controls.Demo").ToList();
            var pageClasses = classes.Where(c => c.Name.EndsWith("Page")).ToList();

            var flexClasses = GetTypesInNamespace(typeof(PageMenu).Assembly, "FlexPanelTest").ToList();
            var flexPageClasses = flexClasses.Where(c => c.Name.EndsWith("Page")).ToList();
            pageClasses.AddRange(flexPageClasses);

            _listView.ItemsSource = pageClasses.Select(t => t.FullName + ", " + t.Assembly);
            _listView.ItemClick += OnListView_ItemClick;
            _listView.IsItemClickEnabled = true;
        }

        void OnListView_ItemClick(object sender, Microsoft.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            ItemClickProcess(e.ClickedItem);
        }

        public void ItemClickProcess(object item)
        {
            if (item is string text)
                item = Type.GetType(text);
            if (item is Type type)
            {
                //var page = (Page)Activator.CreateInstance(type);
                //await this.PushAsync(page);
                Frame.Navigate(type);
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
