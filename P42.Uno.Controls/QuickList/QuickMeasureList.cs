using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using P42.Utils.Uno;
using Windows.UI.Xaml.Markup;

namespace P42.Uno.Controls
{
    public partial class QuickMeasureList : ListView
    {
        public static Stopwatch Stopwatch = new Stopwatch();
        static Style ScrollStyle;
        static DataTemplate CellTemplate;
        static QuickMeasureList()
        {
            var markup =
"<ResourceDictionary \n" +
"xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" \n" +
"xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" \n" +
//    "xmlns:xamarin=\"http://uno.ui/xamarin\" \n" +
"xmlns:local=\"using:{type.Namespace}\">\n" +

//    "<xamarin:Style TargetType=\"ScrollViewer\" \n" +
"<Style TargetType=\"ScrollViewer\" \n" +
"x:Key=\"ListViewBaseScrollViewerStyle\">\n" +
"<Setter Property=\"Template\">\n" +
"<Setter.Value>\n" +
"<ControlTemplate TargetType=\"ScrollViewer\">\n" +
    //                    "<xamarin:ListViewBaseScrollContentPresenter x:Name=\"ScrollContentPresenter\"\n" +
    "<ListViewBaseScrollContentPresenter x:Name=\"ScrollContentPresenter\"\n" +
                            "Content=\"{TemplateBinding Content}\"\n" +
                            "ContentTemplate=\"{TemplateBinding ContentTemplate}\"\n" +
                            "ContentTemplateSelector=\"{TemplateBinding ContentTemplateSelector}\" />\n" +
"</ControlTemplate>\n" +
"</Setter.Value>\n" +
"</Setter>\n" +
//    "</xamarin:Style>\n" +
"</Style>\n" +
"</ResourceDictionary>\n";
            try
            {
                var dict = (ResourceDictionary)XamlReader.Load(markup);
                if (dict.TryGetValue("ListViewBaseScrollViewerStyle", out object value))
                    ScrollStyle = value as Style;

                CellTemplate = typeof(SimpleCell).AsDataTemplate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("QuickMeasureList.Exception: " + ex);
            }

        }

        #region MinCellHeight Property
        public static readonly DependencyProperty MinCellHeightProperty = DependencyProperty.Register(
            nameof(MinCellHeight),
            typeof(int),
            typeof(QuickMeasureList),
            new PropertyMetadata(default(int))
        );
        public int MinCellHeight
        {
            get => (int)GetValue(MinCellHeightProperty);
            set => SetValue(MinCellHeightProperty, value);
        }
        #endregion MinCellHeight Property

        public QuickMeasureList()
        {
            ItemTemplate = CellTemplate;
#if __ANDROID__
            Loading += QuickMeasureList_Loading; //(s, e) => System.Diagnostics.Debug.WriteLine("QuickMeasureList.Loading: " + Stopwatch.ElapsedMilliseconds);
            Loaded += QuickMeasureList_Loaded; //+= (s, e) => System.Diagnostics.Debug.WriteLine("QuickMeasureList.Loaded: " + Stopwatch.ElapsedMilliseconds);
#endif
        }

        private void QuickMeasureList_Loaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("QuickMeasureList.Loaded t0: " + Stopwatch.ElapsedMilliseconds);
            Resources.TryGetValue("ListViewBaseScrollViewerStyle", out object so);
            //if (so is Style style && this.GetScrollViewer() is ScrollViewer scrollViewer)
            //    scrollViewer.Style = style;
            if (this.GetScrollViewer() is ScrollViewer scrollViewer)
                scrollViewer.Style = ScrollStyle;
            System.Diagnostics.Debug.WriteLine("QuickMeasureList.Loaded t1: " + Stopwatch.ElapsedMilliseconds);
        }

        private void QuickMeasureList_Loading(DependencyObject sender, object args)
        {
            System.Diagnostics.Debug.WriteLine("QuickMeasureList.Loading: " + Stopwatch.ElapsedMilliseconds);
        }

        int _measurePass;
        protected override Size MeasureOverride(Size availableSize)
        {
            var pass = _measurePass++;
            System.Diagnostics.Debug.WriteLine($"QuickMeasureList.MeasureOverride[] t0: " + Stopwatch.ElapsedMilliseconds);
            if (ItemsSource is System.Collections.IList list)
            {
                if (list.Count * MinCellHeight >= availableSize.Height)
                    return availableSize;
            }
            else if (ItemsSource is System.Collections.IEnumerable items)
            {
                var height = 0;
                foreach (var item in items)
                {
                    height++;
                    if (height >= availableSize.Height)
                        return availableSize;
                }
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Diagnostics.Debug.WriteLine("QuickMeasureList.ArrangeOverride t0: " + Stopwatch.ElapsedMilliseconds);
            var result = base.ArrangeOverride(finalSize);
            System.Diagnostics.Debug.WriteLine("QuickMeasureList.ArrangeOverride t1: " + Stopwatch.ElapsedMilliseconds);
            return result;
        }
    }
}
