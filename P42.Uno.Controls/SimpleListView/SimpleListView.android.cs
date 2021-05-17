using Android.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using Uno.Extensions.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using P42.Utils.Uno;
using Windows.UI;
using ScrollIntoViewAlignment = Windows.UI.Xaml.Controls.ScrollIntoViewAlignment;
using System.Collections.ObjectModel;

namespace P42.Uno.Controls
{
    public partial class SimpleListView
    {
        internal ObservableCollection<object> _selectedItems = new ObservableCollection<object>();

        Android.Widget.ListView _nativeListView = new Android.Widget.ListView(global::Uno.UI.ContextHelper.Current)
        {
            LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent),
            Divider = null
        };

        SimpleAdapter _adapter;

        public void PlatformBuild()
        {
            SelectedItems = _selectedItems;
            _nativeListView.Adapter = _adapter = new SimpleAdapter(this);
            var listView = VisualTreeHelper.AdaptNative(_nativeListView);
            Content = listView;
        }

        internal void OnWrapperClicked(CellWrapper wrapper)
        {
            System.Diagnostics.Debug.WriteLine("SimpleListView. CLICK");
            if (SelectionMode == ListViewSelectionMode.Single)
            {
                _selectedItems.Clear();
                _selectedItems.Add(wrapper.DataContext);
            }
            else if (SelectionMode == ListViewSelectionMode.Multiple)
            {
                if (_selectedItems.Contains(wrapper.DataContext))
                    _selectedItems.Remove(wrapper.DataContext);
                else
                    _selectedItems.Add(wrapper.DataContext);
            }
            if (IsItemClickEnabled)
                ItemClick?.Invoke(this, new ItemClickEventArgs { ClickedItem = wrapper.DataContext });
        }

        private static void OnItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SimpleListView simpleListView)
                simpleListView._adapter.SetItems(simpleListView.ItemsSource);
        }

        private static void OnItemTemplateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SimpleListView simpleListView)
                simpleListView._adapter.SetItemTemplate(simpleListView.ItemTemplate);
        }

        private static void OnItemTemplateSelectorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SimpleListView simpleListView)
                simpleListView._adapter.SetTemplateSelector(simpleListView.ItemTemplateSelector);
        }


        #region ListViewBase Methods


        public void ScrollIntoView(object item, ScrollIntoViewAlignment alignment)
        {

        }

        public void SelectAll()
        {

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

    class SimpleAdapter : Android.Widget.BaseAdapter<object>
    {
        IEnumerable Items;
        DataTemplate ItemTemplate;
        DataTemplateSelector TemplateSelector;
        SimpleListView SimpleListView;
        //List<DataTemplate> Templates = new List<DataTemplate>();

        public SimpleAdapter(SimpleListView simpleListView)
        {
            SimpleListView = simpleListView;
        }

        public void SetItems(IEnumerable items)
        {
            if (Items is INotifyCollectionChanged oldCollection)
                oldCollection.CollectionChanged -= OnCollectionChaged;
            Items = items;
            if (Items is INotifyCollectionChanged newCollection)
                newCollection.CollectionChanged += OnCollectionChaged;
        }

        public void SetItemTemplate(DataTemplate template)
        {
            ItemTemplate = template;
            NotifyDataSetChanged();
        }

        public void SetTemplateSelector(DataTemplateSelector selector)
        {
            TemplateSelector = selector;
            //Templates.Clear();
            NotifyDataSetChanged();
        }

        private void OnCollectionChaged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged();
        }



        public override object this[int position] => Items?.ElementAt(position);

        public override int Count => Items?.Count() ?? 0;

        public override long GetItemId(int position) => position;

        public override int ViewTypeCount
        {
            get
            {
                if (ItemTemplate != null || TemplateSelector is null)
                    return 1;
                return TemplateSelector.Templates.Count();
            }
        }

        public override int GetItemViewType(int position)
        {
            if (ItemTemplate != null || TemplateSelector is null)
                return 0;
            if (position > -1 && position <= Items.Count())
            {
                var item = this[position];
                if (TemplateSelector?.SelectTemplate(item) is DataTemplate template)
                {
                    var index = 0;
                    foreach (var t in TemplateSelector.Templates)
                    {
                        if (template == t)
                            return index;
                        index++;
                    }
                }
            }
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView is CellWrapper wrapper)
            {
                wrapper.Index = position;
                wrapper.DataContext = this[position];
                return convertView;
            }

            if (ItemTemplate?.LoadContent() is FrameworkElement newElement)
            {
                return new CellWrapper(SimpleListView)
                {
                    Child = newElement,
                    Index = position,
                    DataContext = this[position]
                };
            }

            if (TemplateSelector?.SelectTemplate(this[position]) is DataTemplate template)
            {
                if (template.LoadContent() is FrameworkElement newSelectedElement)
                    return new CellWrapper(SimpleListView)
                    {
                        Child = newSelectedElement,
                        Index = position,
                        DataContext = this[position] 
                    };
            }

            return new CellWrapper(SimpleListView)
            {
                Child = new Cell(),
                Index = position,
                DataContext = this[position],
            };
        }

    }

    partial class CellWrapper : Border
    {
        SimpleListView SimpleListView;

        public bool IsSelected => DataContext is null
                ? false
                : (SimpleListView?._selectedItems?.Contains(DataContext) ?? false);

        int _index;
        public int Index 
        { 
            get => _index; 
            set
            {
                if (_index != value)
                {
                    _index = value;
                    UpdateSelection();
                }
            }
        }

        public CellWrapper(SimpleListView simpleListView)
        {
            SimpleListView = simpleListView;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Top;
            Tapped += OnCellWrapper_Tapped;
            SimpleListView._selectedItems.CollectionChanged += OnSelectedItems_CollectionChanged;
        }

        private void OnSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSelection();
        }

        private void OnCellWrapper_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("CellWrapper.TAPPED");
            SimpleListView.OnWrapperClicked(this);
        }

        protected override void OnDataContextChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnDataContextChanged(e);
            UpdateSelection();
            Child.DataContext = DataContext;
        }

        private void UpdateSelection()
        {
            Background = IsSelected
                ? SystemColors.Accent.ToBrush()
                : Colors.Transparent.ToBrush();
        }
    }

    partial class Cell : TextBlock
    {

        public Cell()
        {
            Margin = new Thickness(10, 5);
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Center;
        }

        protected override void OnDataContextChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnDataContextChanged(e);
            Text = DataContext.ToString();
        }
    }

}
