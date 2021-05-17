using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using P42.Utils.Uno;
using ScrollIntoViewAlignment = Windows.UI.Xaml.Controls.ScrollIntoViewAlignment;

namespace P42.Uno.Controls
{
    public partial class SimpleListView
    {
        ListView _listView = new ListView
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Top
        };

        public void PlatformBuild()
        {
            SelectedItems = _listView.SelectedItems;
            _listView.Bind(ListView.IsItemClickEnabledProperty, this, nameof(IsItemClickEnabled));
            _listView.Bind(ListView.SelectionModeProperty, this, nameof(SelectionMode));
            _listView.Bind(ListView.SelectedIndexProperty, this, nameof(SelectedIndex));
            _listView.Bind(ListView.SelectedItemProperty, this, nameof(SelectedItem));
            Content = _listView;
            _listView.ItemClick += OnListView_ItemClick;
            _listView.SelectionChanged += OnListView_SelectionChanged;
        }

        private void OnListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        private void OnListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClick?.Invoke(this, e);
        }

        private static void OnItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SimpleListView simpleListView)
                simpleListView._listView.ItemsSource = simpleListView.ItemsSource;
        }

        private static void OnItemTemplateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SimpleListView simpleListView)
                simpleListView._listView.ItemTemplate = simpleListView.ItemTemplate;
        }

        private static void OnItemTemplateSelectorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SimpleListView simpleListView)
                simpleListView._listView.ItemTemplateSelector = simpleListView.ItemTemplateSelector;
        }


        #region ListViewBase Methods


        public void ScrollIntoView(object item, ScrollIntoViewAlignment alignment)
        {
            _listView.ScrollIntoView(item, alignment);
        }

        public void SelectAll()
        {
            _listView.SelectAll();
        }
        /*
        #region ItemsControl Methods

        public DependencyObject ContainerFromIndex(int index)
        {

        }

        public DependencyObject ContainerFromItem(object item)
        {

        }

        public int IndexFromContainer(DependencyObject container)
        {

        }

        public object ItemFromContainer(DependencyObject container)
        {

        }

        protected virtual void OnItemsChanged(object e)
        {

        }

        protected void OnItemTemplateChanged(DataTemplate oldItemTemplate, DataTemplate newItemTemplate)
        {

        }

        protected void OnItemTemplateSelectorChanged(DataTemplateSelector oldItemTemplateSelector, DataTemplateSelector newItemTemplateSelector)
        {

        }

        #endregion
        */
        #endregion

    }
}
