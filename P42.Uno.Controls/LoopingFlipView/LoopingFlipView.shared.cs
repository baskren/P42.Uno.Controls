using System.Collections;
using System.Collections.Specialized;
using Windows.Foundation;
using Windows.UI;
using Microsoft.UI.Input;

namespace P42.Uno.Controls;

[Bindable]
public class LoopingFlipView : UserControl
{

    #region Properties

    #region SelectedItem Property
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
        nameof(SelectedItem),
        typeof(UIElement),
        typeof(LoopingFlipView),
        new PropertyMetadata(default(UIElement), (s,e) => ((LoopingFlipView)s).OnSelectedItemChanged(e))
    );
    public UIElement SelectedItem
    {
        get => (UIElement)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    #endregion SelectedItem Property

    #region SelectedIndex Property
    public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
        nameof(SelectedIndex),
        typeof(int),
        typeof(LoopingFlipView),
        new PropertyMetadata(-1, (s,e) => ((LoopingFlipView)s).OnSelectedIndexChanged(e))
    );
    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set
        {
            if (value != SelectedIndex)
            {
                if (ItemsSource?.Any() ?? false)
                {
                    if (value < 0)
                        value = ItemsSource.Count() - 1;
                    if (value >= ItemsSource.Count())
                        value = 0;
                }
            }
            SetValue(SelectedIndexProperty, value);
        }
    }
    #endregion SelectedIndex Property

    #region ItemsSource Property
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
        nameof(ItemsSource),
        typeof(IEnumerable<UIElement>),
        typeof(LoopingFlipView),
        new PropertyMetadata(default(IEnumerable), (s,e)=>((LoopingFlipView)s).OnItemsSourceChanged(e))
    );
    public IEnumerable<UIElement> ItemsSource
    {
        get => (IEnumerable<UIElement>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    #endregion ItemsSource Property

    #region Spacing Property
    public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
        nameof(Spacing),
        typeof(double),
        typeof(LoopingFlipView),
        new PropertyMetadata(2.0)
    );
    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }
    #endregion Spacing Property

    #endregion


    #region Events
    public event EventHandler<int> SelectedIndexChanged;
    public event EventHandler<UIElement> SelectedItemChanged;
    #endregion


    #region Fields

    private Grid ContentGrid = new();
    private Grid OverlayGrid = new();
    private double _dx;
    private bool _isDown;
    private double _downX;
    private PointerPoint _lastPoint;
    private double _lastVelocity;
    private CancellationTokenSource _cancellationTokenSource;
    private InsetShadow InsetShadow = new() { Orientation = Orientation.Horizontal };
    #endregion


    #region Construction
    public LoopingFlipView()
    {
        Content = OverlayGrid
            .Background(Color.FromArgb(1, 1, 1, 1))
            .Children
            (
                ContentGrid,
                InsetShadow
            );
        // bind to user control properties here!

        OverlayGrid.PointerPressed += PointerStart;
        OverlayGrid.PointerMoved += PointerMove;
        OverlayGrid.PointerExited += PointerEnd;
        OverlayGrid.PointerCanceled += PointerEnd;
        OverlayGrid.PointerCaptureLost += PointerEnd;
        OverlayGrid.PointerReleased += PointerEnd;

        SizeChanged += (s,e) => LayoutChildren();
    }
    #endregion


    #region Pointer Gesture Handling

    private async void PointerEnd(object sender, PointerRoutedEventArgs e)
    {
        if (!_isDown)
            return;
        _isDown = false;

        if (_cancellationTokenSource != null)
            return;

        if (Math.Abs(_dx) > 2 * ActualWidth / 3 ||
            (Math.Abs(_dx) > ActualWidth /2 && _lastVelocity > 0.0001)
           )
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var animator = new ActionAnimator(_dx, Math.Sign(_dx) *ActualWidth, TimeSpan.FromSeconds(0.5), x =>
            {
                _dx = x;
                LayoutChildren();
            });
            await animator.RunAsync(_cancellationTokenSource.Token);

            if (!(_cancellationTokenSource?.Token.IsCancellationRequested ?? false))
            {
                var selectedIndex = SelectedIndex - Math.Sign(_dx);
                _dx = 0;
                SelectedIndex = selectedIndex;
            }
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
        else if (_dx != 0)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var animator = new ActionAnimator(_dx, 0, TimeSpan.FromSeconds(0.5), x =>
            {
                _dx = x;
                LayoutChildren();
            });

            await animator.RunAsync(_cancellationTokenSource.Token);

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        _dx = 0;
        LayoutChildren();

    }

    private void PointerMove(object sender, PointerRoutedEventArgs e)
    {
        if (_isDown)
        {
            var point = e.GetCurrentPoint(null);
            _lastVelocity = Math.Abs(point.Position.X - _lastPoint.Position.X) / (point.Timestamp - _lastPoint.Timestamp);

            _lastPoint = point; 
            _dx = _lastPoint.Position.X - _downX;
            LayoutChildren();
        }
    }

    private void PointerStart(object sender, PointerRoutedEventArgs e)
    {
        if (!_isDown)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();  // is this going to cause problems?
            _cancellationTokenSource = null;
            _lastPoint = e.GetCurrentPoint(null);
            _downX = _lastPoint.Position.X;
            _isDown = true;
        }
    }
    #endregion


    #region Layout

    private void LayoutChildren(bool ignoreIndex = false)
    {
        if (double.IsNaN(ActualWidth) || double.IsNaN(ActualHeight) || ActualWidth < 1 || ActualHeight < 1)
            return;

        ContentGrid.Clip = new RectangleGeometry { Rect = new Rect(0, 0, ActualWidth, ActualHeight) };

        switch (ContentGrid.Children.Count)
        {
            case 0:
                _dx = 0;
                SelectedIndex = -1;
                return;
            case 1:
                ContentGrid.Children[0].RenderTransform = new TranslateTransform { X = 0 };
                _dx = 0;
                SelectedIndex = 0;
                return;
            default:
            {
                var indexes = new List<int>();
                var selectedIndex = Math.Max(SelectedIndex, 0);
                var index = selectedIndex;
                while (index < ContentGrid.Children.Count)
                    indexes.Add(index++);
                index = 0;
                while (index < selectedIndex)
                    indexes.Add(index++);

                var offset = ActualWidth + Spacing;

                var mid = ContentGrid.Children.Count / 2;
                var dx = _dx;
                if (dx > 0)
                {
                    for (var i = indexes.Count - 1; i >= mid; i--)
                    {
                        dx -= offset;
                        ContentGrid.Children[indexes[i]].RenderTransform = new TranslateTransform { X = dx };
                    }
                    dx = _dx;
                    for (var i = 0; i < mid; i++)
                    {
                        ContentGrid.Children[indexes[i]].RenderTransform = new TranslateTransform { X = dx };
                        dx += offset;
                    }
                }
                else
                {
                    for (var i = 0; i <= mid; i++)
                    {
                        ContentGrid.Children[indexes[i]].RenderTransform = new TranslateTransform { X = dx };
                        dx += offset;
                    }
                    dx = _dx;
                    for (var i = indexes.Count - 1; i > mid; i--)
                    {
                        dx -= offset;
                        ContentGrid.Children[indexes[i]].RenderTransform = new TranslateTransform { X = dx };
                    }

                }

                SelectedIndex = selectedIndex;
            }
                return;
        }
    }

    #endregion


    #region Property Change Handlers

    private void OnSelectedItemChanged(DependencyPropertyChangedEventArgs e)
    {
        if (ItemsSource is null)
        {
            SelectedIndex = -1;
            return;
        }

        var itemsSource = ItemsSource as IList<UIElement> ?? new List<UIElement>(ItemsSource);
        SelectedIndex = itemsSource.IndexOf(SelectedItem);
        SelectedItemChanged?.Invoke(this, e.NewValue as UIElement);
    }

    private async void OnSelectedIndexChanged(DependencyPropertyChangedEventArgs e)
    {
        var count = ContentGrid.Children.Count;
        var newIndex = (int)e.NewValue;
        if (newIndex < 0 || newIndex >= count)
            return;

        if (ContentGrid.Children[newIndex].RenderTransform is TranslateTransform t)
        {
            if (_cancellationTokenSource != null)
                return;

            _cancellationTokenSource = new CancellationTokenSource();

            var animator = new ActionAnimator(0, -t.X, TimeSpan.FromSeconds(0.4), dx =>
            {
                foreach (var child in ContentGrid.Children)
                {
                    if (child.RenderTransform is TranslateTransform tt)
                    {
                        var x = tt.X + dx;
                        child.RenderTransform = new TranslateTransform { X = x };
                    }
                }    
                    
            }, delta: true);
            await animator.RunAsync(_cancellationTokenSource.Token);

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;

            _dx = 0;
            LayoutChildren();

        }

        LayoutChildren();
        SelectedIndexChanged?.Invoke(this, (int)e.NewValue);
    }
       
    private void OnItemsSourceChanged(DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is IEnumerable<UIElement> oldItemSource)
        {
            if (oldItemSource is INotifyCollectionChanged notifiable)
                notifiable.CollectionChanged -= OnItemsSourceCollectionChanged;
        }
        foreach (var child in ContentGrid.Children)
            if (child is IEventSubscriber eventSubscriber)
                eventSubscriber.DisableEvents();
        ContentGrid.Children.Clear();
        if (e.NewValue is IEnumerable<UIElement> newItemSource)
        {
            if (newItemSource is INotifyCollectionChanged notifiable)
                notifiable.CollectionChanged += OnItemsSourceCollectionChanged;
            foreach (var child in newItemSource)
            {
                ContentGrid.Children.Add(new LoopingFlipViewItem(child));
                if (child is IEventSubscriber eventSubscriber)
                    eventSubscriber.EnableEvents();
            }
        }
        LayoutChildren();
    }

    private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                InsertNewItems(e.NewStartingIndex, e.NewItems);
                break;
            case NotifyCollectionChangedAction.Remove:
                RemoveItemsAt(e.OldStartingIndex, e.OldItems.Count);
                break;
            case NotifyCollectionChangedAction.Replace:
                RemoveItemsAt(e.OldStartingIndex, e.OldItems.Count);
                InsertNewItems(e.NewStartingIndex, e.NewItems);
                break;
            case NotifyCollectionChangedAction.Move:
                RemoveItemsAt(e.OldStartingIndex, e.OldItems.Count);
                InsertNewItems(e.NewStartingIndex, e.OldItems);
                break;
            case NotifyCollectionChangedAction.Reset:
                foreach (var child in ContentGrid.Children)
                    if (child is IEventSubscriber eventSubscriber)
                        eventSubscriber.DisableEvents();
                ContentGrid.Children.Clear();
                break;
        }
        LayoutChildren();
    }

    private void RemoveItemsAt(int index, int count)
    {
        for (var i = 0; i < count; i++)
        {
            if (ContentGrid.Children[index] is IEventSubscriber eventSubscriber)
                eventSubscriber.DisableEvents();
            ContentGrid.Children.RemoveAt(index);
        }
    }

    private void InsertNewItems(int index, IList items)
    {
        var newItems = items.Cast<UIElement>().ToList();
        for (var i = newItems.Count - 1; i >= 0; i--)
            InsertItem(index, newItems[i]);
    }

    private void InsertItem(int index, UIElement item)
    {
        var xitem = new LoopingFlipViewItem(item);
        ContentGrid.Children.Insert(index, xitem);
        if (xitem is IEventSubscriber eventSubscriber)
            eventSubscriber.EnableEvents();
    }

    #endregion


}
