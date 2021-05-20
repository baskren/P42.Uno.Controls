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
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    public partial class SimpleListView
    {
        static double _scale = -1;
        internal static double DisplayScale
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

        static int instances = 0;
        int instance;
        internal ObservableCollection<object> _selectedItems = new ObservableCollection<object>();
        internal ObservableCollection<int> NativeCellHeights = new ObservableCollection<int>();
        Android.Views.View _headerView;
        Android.Views.View _footerView;

        Android.Widget.ListView _nativeListView;

        SimpleAdapter _adapter;

        public void PlatformBuild()
        {
            instance = instances++;
            SelectedItems = _selectedItems;
            _selectedItems.CollectionChanged += OnSelectedItems_CollectionChanged;
            NativeCellHeights.CollectionChanged += OnNativeCellHeights_CollectionChanged;
            InjectNativeListView();
        }

        void InjectNativeListView()
        {
            if (_nativeListView != null)
            {
                _adapter?.NotifyDataSetInvalidated();
                _nativeListView.Adapter = null;
                _adapter?.Dispose();
                _nativeListView.Dispose();
            }

            _adapter = new SimpleAdapter(this);
            _nativeListView = new Android.Widget.ListView(global::Uno.UI.ContextHelper.Current)
            {
                LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent),
                Divider = null,
                Adapter = _adapter
            };
            Content = VisualTreeHelper.AdaptNative(_nativeListView);
        }

        #region Header / Footer Change Handlers
        void UpdateFooter()
        {
            if (_footerView != null)
                _nativeListView.RemoveFooterView(_footerView);
            _footerView?.Dispose();
            _footerView = null;
            if (Footer != null)
            {
                if (Footer is Android.Views.View view)
                {
                    _footerView = view;
                    _nativeListView.AddFooterView(view);
                }
                else if (FooterTemplate?.LoadContent() is FrameworkElement newFooter)
                {
                    _footerView = newFooter;
                    newFooter.DataContext = Footer;
                    _nativeListView.AddFooterView(newFooter);
                }
            }
        }

        private static void OnFooterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SimpleListView listView)
                listView.UpdateFooter();
        }

        private static void OnFooterTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SimpleListView listView)
                listView.UpdateFooter();
        }

        void UpdateHeader()
        {
            if (_headerView != null)
                _nativeListView.RemoveHeaderView(_headerView);
            _headerView?.Dispose();
            _headerView = null;
            if (Header != null)
            {
                if (Header is Android.Views.View view)
                {
                    _headerView = view;
                    _nativeListView.AddHeaderView(view);
                }
                else if (HeaderTemplate?.LoadContent() is FrameworkElement newHeader)
                {
                    _headerView = newHeader;
                    newHeader.DataContext = Header;
                    _nativeListView.AddHeaderView(newHeader);
                }
            }
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SimpleListView listView)
                listView.UpdateHeader();
        }

        private static void OnHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SimpleListView listView)
                listView.UpdateHeader();
        }
        #endregion


        #region Click / Selection Change Handlers
        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SimpleListView listView)
            {
                listView.SelectItem(listView.SelectedItem);
            }
        }

        bool _repondingToSelectedItemsCollectionChanged;
        private void OnSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _repondingToSelectedItemsCollectionChanged = true;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    if (e.NewItems?.Any() ?? false)
                        SelectedItem = e.NewItems[0];
                    else if (SelectedItem != null && (e.OldItems?.Contains(SelectedItem) ?? false))
                        SelectedItem = null;
                    SelectionChanged?.Invoke(this, new P42.Uno.Controls.SelectionChangedEventArgs(this, e.OldItems, e.NewItems));
                    break;
                case NotifyCollectionChangedAction.Move:
                default:
                    break;
            }
            _repondingToSelectedItemsCollectionChanged = false;
        }

        internal async Task OnWrapperClicked(CellWrapper wrapper)
        {
            System.Diagnostics.Debug.WriteLine("SimpleListView. CLICK");
            SelectItem(wrapper.DataContext);
            await Task.Delay(10);
            if (IsItemClickEnabled)
                ItemClick?.Invoke(this, new ItemClickEventArgs(this, wrapper.DataContext, wrapper.Child));
        }

        void SelectItem(object item)
        {
            if (_repondingToSelectedItemsCollectionChanged)
                return;
            if (SelectionMode == ListViewSelectionMode.Single)
            {
                if (!_selectedItems.Contains(item))
                {
                    _selectedItems.Clear();
                    _selectedItems.Add(item);
                }
            }
            else if (SelectionMode == ListViewSelectionMode.Multiple)
            {
                if (_selectedItems.Contains(item))
                    _selectedItems.Remove(item);
                else
                    _selectedItems.Add(item);
            }
        }
        #endregion


        #region Source / Template Change Handlers
        private static void OnItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SimpleListView simpleListView)
            {
                simpleListView._adapter.SetItems(simpleListView.ItemsSource);
                simpleListView.InvalidateMeasure();
            }
        }

        private static void OnItemTemplateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SimpleListView simpleListView)
                simpleListView.InjectNativeListView();
        }

        private static void OnItemTemplateSelectorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SimpleListView simpleListView)
                simpleListView.InjectNativeListView();
        }
        #endregion


        #region ListViewBase Methods

        int _waitingForIndex = -1;
        ScrollIntoViewAlignment _waitingAlignment = ScrollIntoViewAlignment.Default;
        public async Task ScrollIntoView(object item, P42.Uno.Controls.ScrollIntoViewAlignment alignment)
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
                //var viewHeight = _nativeListView.Height;
                if (index < NativeCellHeights.Count)
                {
                    var cellHeight = NativeCellHeights[index];
                    InternalScrollTo(index, alignment, cellHeight);
                    /*
                    if (alignment == ScrollIntoViewAlignment.Center)
                        _nativeListView.SmoothScrollToPositionFromTop(index, (viewHeight - cellHeight) / 2);
                    else
                        _nativeListView.SmoothScrollToPositionFromTop(index, viewHeight - cellHeight);
                    */
                }
                else
                {
                    var estCellHeight = (int)(NativeCellHeights.Average() + 0.5);
                    _waitingForIndex = index;
                    _waitingAlignment = alignment;
                    InternalScrollTo(index, alignment, estCellHeight);
                    /*
                    if (alignment == ScrollIntoViewAlignment.Center)
                        _nativeListView.SmoothScrollToPositionFromTop(index, (viewHeight - estCellHeight) / 2);
                    else
                        _nativeListView.SmoothScrollToPositionFromTop(index, viewHeight - estCellHeight);
                    */
                }
                await Task.Delay(10);
            }
        }

        private void OnNativeCellHeights_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_waitingForIndex >= e.NewStartingIndex && _waitingForIndex < e.NewStartingIndex + e.NewItems.Count)
            {
                var index = _waitingForIndex;
                _waitingForIndex = -1;
                var cellHeight = NativeCellHeights[index];
                /*
                var viewHeight = _nativeListView.Height;
                if (_waitingAlignment == ScrollIntoViewAlignment.Trailing)
                    _nativeListView.SmoothScrollToPositionFromTop(index, viewHeight - cellHeight);
                else if (_waitingAlignment == ScrollIntoViewAlignment.Center)
                    _nativeListView.SmoothScrollToPositionFromTop(index, (viewHeight - cellHeight) / 2);
                else if (_waitingAlignment == ScrollIntoViewAlignment.Default ||
                    _waitingAlignment == ScrollIntoViewAlignment.Leading)
                    _nativeListView.SmoothScrollToPosition(index);
                */
                InternalScrollTo(index, _waitingAlignment, cellHeight);
                _waitingAlignment = ScrollIntoViewAlignment.Default;
            }
        }

        void InternalScrollTo(int index, ScrollIntoViewAlignment alignment, int cellHeight)
        {
            var viewHeight = _nativeListView.Height;
            if (_waitingAlignment == ScrollIntoViewAlignment.Trailing)
                _nativeListView.SmoothScrollToPositionFromTop(index, viewHeight - cellHeight);
            else if (_waitingAlignment == ScrollIntoViewAlignment.Center)
                _nativeListView.SmoothScrollToPositionFromTop(index, (viewHeight - cellHeight) / 2);
            else if (_waitingAlignment == ScrollIntoViewAlignment.Default ||
                _waitingAlignment == ScrollIntoViewAlignment.Leading)
                _nativeListView.SmoothScrollToPosition(index);
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
        DataTemplate ItemTemplate => SimpleListView.ItemTemplate;
        DataTemplateSelector TemplateSelector => SimpleListView.ItemTemplateSelector;
        SimpleListView SimpleListView;
        //List<DataTemplate> Templates = new List<DataTemplate>();

        public SimpleAdapter(SimpleListView simpleListView)
        {
            SimpleListView = simpleListView;
            SetItems(SimpleListView.ItemsSource);
        }

        public void SetItems(IEnumerable items)
        {
            if (items != Items)
            {
                if (Items is INotifyCollectionChanged oldCollection)
                    oldCollection.CollectionChanged -= OnCollectionChaged;
                Items = items;
                if (Items is INotifyCollectionChanged newCollection)
                    newCollection.CollectionChanged += OnCollectionChaged;
                NotifyDataSetChanged();
            }
        }

        private void OnCollectionChaged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged();
        }



        public override object this[int position]
        {
            get
            {
                if (position > -1 && position <= Items.Count())
                    return Items?.ElementAt(position);
                return null;
            }
        }

        public override int Count 
            => Items?.Count() ?? 0;

        public override long GetItemId(int position) => position;

        public override int ViewTypeCount
        {
            get
            {
                if (ItemTemplate != null || TemplateSelector is null)
                    return 1;
                return TemplateSelector.Templates.Count() + 1;
            }
        }

        public override int GetItemViewType(int position)
        {
            if (ItemTemplate != null || TemplateSelector is null)
                return 0;
            if (TemplateSelector?.SelectTemplate(this[position]) is DataTemplate template)
                return TemplateSelector.Templates.IndexOf(template) + 1;
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


        private void OnCellWrapper_SizeChanged(object sender, SizeChangedEventArgs args)
        {
            RecordCellHeight();
        }

        void RecordCellHeight()
        {
            if (ActualHeight >-1 && DataContext != null && SimpleListView is SimpleListView parent)
            {
                var height = (int)(ActualHeight * SimpleListView.DisplayScale + 0.5);
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

        async void OnCellWrapper_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("CellWrapper.TAPPED");
            await SimpleListView.OnWrapperClicked(this);
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
