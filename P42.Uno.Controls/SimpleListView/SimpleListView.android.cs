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
using Android.Runtime;
using Android.Util;

namespace P42.Uno.Controls
{
    public partial class SimpleListView
    {
        static int instances = 0;
        int instance;
        internal ObservableCollection<object> _selectedItems = new ObservableCollection<object>();
        internal ObservableCollection<int> NativeCellHeights = new ObservableCollection<int>();

        Android.Widget.ListView _nativeListView = new Android.Widget.ListView(global::Uno.UI.ContextHelper.Current)
        {
            LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent),
            Divider = null
        };

        SimpleAdapter _adapter;

        public void PlatformBuild()
        {
            instance = instances++;
            SelectedItems = _selectedItems;
            _selectedItems.CollectionChanged += OnSelectedItems_CollectionChanged;
            NativeCellHeights.CollectionChanged += OnNativeCellHeights_CollectionChanged;
            _nativeListView.Adapter = _adapter = new SimpleAdapter(this);
            var listView = VisualTreeHelper.AdaptNative(_nativeListView);
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
            Content = listView;

        }

        private void OnSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    SelectionChanged?.Invoke(this, new P42.Uno.Controls.SelectionChangedEventArgs(this, e.OldItems, e.NewItems));
                    break;
                case NotifyCollectionChangedAction.Move:
                default:
                    break;
            }
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
                ItemClick?.Invoke(this, new ItemClickEventArgs(this, wrapper.DataContext, wrapper.Child));
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

        int _waitingForIndex = -1;
        ScrollIntoViewAlignment _waitingAlignment = ScrollIntoViewAlignment.Default;
        public void ScrollIntoView(object item, P42.Uno.Controls.ScrollIntoViewAlignment alignment)
        {
            if (ItemsSource.IndexOf(item) is int index && index > -1)
            {
                if (alignment == ScrollIntoViewAlignment.Default)
                {
                    _nativeListView.SmoothScrollToPosition(index);
                    return;
                }
                else if (alignment == ScrollIntoViewAlignment.Leading)
                {
                    _nativeListView.SmoothScrollToPositionFromTop(index, 0);
                    return;
                }
                var viewHeight = _nativeListView.Height;
                if (index < NativeCellHeights.Count)
                {
                    var cellHeight = NativeCellHeights[index];
                    if (alignment == ScrollIntoViewAlignment.Center)
                        _nativeListView.SmoothScrollToPositionFromTop(index, (viewHeight + cellHeight) / 2);
                    else
                        _nativeListView.SmoothScrollToPositionFromTop(index, viewHeight - cellHeight);
                }
                else
                {
                    var estCellHeight = (int)(NativeCellHeights.Average() + 0.5);
                    _waitingForIndex = index;
                    _waitingAlignment = alignment;
                    if (alignment == ScrollIntoViewAlignment.Center)
                        _nativeListView.SmoothScrollToPositionFromTop(index, (viewHeight + estCellHeight) / 2);
                    else
                        _nativeListView.SmoothScrollToPositionFromTop(index, viewHeight - estCellHeight);
                }
            }
        }

        private void OnNativeCellHeights_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_waitingForIndex >= e.NewStartingIndex && _waitingForIndex < e.NewStartingIndex + e.NewItems.Count)
            {
                var index = _waitingForIndex;
                _waitingForIndex = -1;
                var viewHeight = _nativeListView.Height;
                var cellHeight = NativeCellHeights[index];
                if (_waitingAlignment == ScrollIntoViewAlignment.Trailing)
                    _nativeListView.SmoothScrollToPositionFromTop(index, viewHeight - cellHeight);
                else if (_waitingAlignment == ScrollIntoViewAlignment.Center)
                    _nativeListView.SmoothScrollToPositionFromTop(index, (viewHeight + cellHeight) / 2);
                else if (_waitingAlignment == ScrollIntoViewAlignment.Default ||
                    _waitingAlignment == ScrollIntoViewAlignment.Leading)
                    _nativeListView.SmoothScrollToPosition(index);

                _waitingAlignment = ScrollIntoViewAlignment.Default;
            }
        }


        public void SelectAll()
        {
            var items = ItemsSource.ToObjectArray();
            foreach (var item in items)
            {
                if (!SelectedItems.Contains(item))
                    SelectedItems.Add(item);
            }
        }

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

        // ALWAYS SET INDEX BEFORE DATACONTEXT
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
            SizeChanged += OnCellWrapper_SizeChanged;
        }

        static double _scale = -1;
        static double DisplayScale
        {
            get
            {
                if (_scale > 0)
                    return _scale;
                using var displayMetrics = new DisplayMetrics();
                using var service = global::Uno.UI.ContextHelper.Current.GetSystemService(Android.Content.Context.WindowService);
                using var windowManager = service?.JavaCast<IWindowManager>();
                var display = windowManager?.DefaultDisplay;
                display?.GetRealMetrics(displayMetrics);
                _scale = (double)displayMetrics?.Density;
                return _scale;
            }
        }

        private void OnCellWrapper_SizeChanged(object sender, SizeChangedEventArgs args)
        {
            RecordCellHeight();
        }

        void RecordCellHeight()
        {
            if (ActualHeight >-1 && DataContext != null && SimpleListView is SimpleListView parent)
            {
                var height = (int)(ActualHeight * DisplayScale + 0.5);
                if (Index < parent.NativeCellHeights.Count)
                {
                    parent.NativeCellHeights[Index] = height;
                    return;
                }
                while (Index > parent.NativeCellHeights.Count)
                    parent.NativeCellHeights.Add(0);
                parent.NativeCellHeights.Add(height);
            }
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
            RecordCellHeight();
        }

        private void UpdateSelection()
        {
            Background = IsSelected
                ? SystemColors.ListLow.ToBrush()
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

    /*
    #region ScrollListener 
    class ScrollListener : Java.Lang.Object, Android.Widget.ListView.IOnScrollListener
    {
        public SimpleListView ListView;
        public bool IsBuildingLayOut;

        public void OnScroll(Android.Widget.AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            if (!IsBuildingLayOut)
                ListView?.OnScrolling(this, EventArgs.Empty);
        }

        public void OnScrollStateChanged(Android.Widget.AbsListView view, [GeneratedEnum] Android.Widget.ScrollState scrollState)
        {
            if (scrollState == ScrollState.Idle)
                ListView?.OnScrolled(this, EventArgs.Empty);
        }

    }
    #endregion
    */
}
