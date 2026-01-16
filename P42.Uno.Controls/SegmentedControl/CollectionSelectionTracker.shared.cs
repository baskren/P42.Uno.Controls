using System.Collections.Specialized;
using AsyncAwaitBestPractices;

namespace P42.Uno.Controls;

internal partial class CollectionSelectionTracker<T> : INotifyCollectionChanged 
{
    #region Properties

    public SelectionMode SelectionMode
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            SelectIndex(SelectedIndex);
        }
    } = SelectionMode.None;

    public Func<T>? SelectedItemWhenNoneSelected { get; set; }

    public int SelectedIndex
    {
        get
        {
            if (SelectedIndexes.Count != 0)
                return SelectedIndexes.Last();
            return -1;
        }
    }

    public T? SelectedItem
    {
        get
        {
            if (SelectedIndex > -1 && Collection!.Count > SelectedIndex)
                return Collection[SelectedIndex];

            return SelectedItemWhenNoneSelected is null 
                ? default 
                : SelectedItemWhenNoneSelected();
        }
    }

    public List<int> SelectedIndexes
    {
        get => field;
        set
        {
            switch (SelectionMode)
            {
                case SelectionMode.Radio:
                    var index = value.Count!=0
                        ? value.Last()
                        : -1;
                    UpdateToSelectedRadio(index);
                    break;
                case SelectionMode.Multi:
                    SetSelectedIndexesMulti(value);
                    break;
            }
        }
    } = [];

    public List<T> SelectedItems
    {
        get
        {
            var result = new List<T>();
            for (var i = 0; i < Collection!.Count; i++)
            {
                if (SelectedIndexes.Contains(i))
                    result.Add(Collection[i]);
            }
            return result;
        }
        set
        {
            switch (SelectionMode)
            {
                case SelectionMode.Radio:
                    var index = -1;
                    if (value is not null && value.Count != 0)
                        index = Collection!.IndexOf(value.First());
                    UpdateToSelectedRadio(index);
                    break;
                case SelectionMode.Multi:
                    SetSelectedItemsMulti(value);
                    break;
            }
        }
    }

    private WeakReference<IList<T>>? _weakCollectionRef;
    public IList<T>? Collection
    {
        get
        {
            if (_weakCollectionRef != null && _weakCollectionRef.TryGetTarget(out var target))
                return target;
            target = [];
            _weakCollectionRef = new WeakReference<IList<T>>(target);
            return target;
        }
        set
        {
            var selections = SelectedItems.ToList();
            SelectedIndexes.Clear();
            value ??= [];
            _weakCollectionRef = new WeakReference<IList<T>>(value);
            foreach (var selection in selections)
            {
                if (value.IndexOf(selection) is int index and > -1)
                    SelectedIndexes.Add(index);
            }
        }
    } 

    public bool AllowUnselectAll = false;

    #endregion


    #region Events
    private readonly WeakEventManager<CollectionSelectionTrackerSelectionChangedArguments<T>> _selectionChangedEventManager = new ();
    public event EventHandler<CollectionSelectionTrackerSelectionChangedArguments<T>> SelectionChanged
    {
        add => _selectionChangedEventManager.AddEventHandler(value);
        remove => _selectionChangedEventManager.RemoveEventHandler(value);
    }

    private readonly WeakEventManager _collectionChangedEventManager = new ();
    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => _collectionChangedEventManager.AddEventHandler(value);
        remove => _collectionChangedEventManager.RemoveEventHandler(value);
    }
    #endregion


    #region Constructor
    public CollectionSelectionTracker(IList<T>? collection = null)
    {
        if (collection != null)
            Collection = collection;
    }
    #endregion


    #region Methods
    public void SelectIndex(int index)
    {
        switch (SelectionMode)
        {
            case SelectionMode.None:
                UpdateToSelectNone();
                break;
            case SelectionMode.Radio:
                UpdateToSelectedRadio(index);
                break;
            case SelectionMode.Multi:
                UpdateToSelectedMulti(index);
                break;
        }
    }

    public void SelectItem(T item)
        => SelectIndex(Collection!.IndexOf(item));

    public void SelectIndexes(IEnumerable<int> indexes)
    {
        foreach (var index in indexes)
            SelectIndex(index);
    }       

    public void SelectItems(IEnumerable<T> items)
    {
        foreach (var item in items)
            SelectIndex(Collection!.IndexOf(item));
    }

    public void UnselectIndex(int index)
    {
        if (index < 0 || index >= Collection!.Count)
            return;

        if (AllowUnselectAll || SelectedIndexes.Count > 1)
        {
            if (SelectedIndexes.Contains(index))
            {
                var oldSelectedIndex = SelectedIndex;
                var oldSelectedItem = SelectedItem;
                var i = SelectedIndexes.IndexOf(index);
                SelectedIndexes.Remove(index);

                if (oldSelectedIndex != SelectedIndex)
                    _selectionChangedEventManager.RaiseEvent(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex), nameof(SelectionChanged));
                //SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));
                _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<int>(index), i), nameof(CollectionChanged));
                //CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<int>(index), i));
            }
        }
    }

    public void UnselectItem(T item)
        => UnselectIndex(Collection!.IndexOf(item));

    public void UnselectIndexes(IEnumerable<int> indexes)
    {
        var groups = new Dictionary<int, List<int>>();
        var oldSelectedIndex = SelectedIndex;
        var oldSelectedItem = SelectedItem;

        for (var i = 0; i < SelectedIndexes!.Count; i++)
        {
            if (AllowUnselectAll || SelectedIndexes!.Count > 1)
            {
                var index = SelectedIndexes![i];
                if (index < 0 || index >= Collection!.Count)
                    continue;
                if (indexes.Contains(index))
                {
                    if (groups.Count == 0 || groups.Last().Key + groups.Last().Value.Count < i)
                        groups.Add(i, []);
                    groups.Last().Value.Add(index);
                    SelectedIndexes.Remove(index);
                }
            }
        }

        if (oldSelectedIndex != SelectedIndex)
            _selectionChangedEventManager.RaiseEvent(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex), nameof(SelectionChanged));

        foreach (var kvp in groups)
            _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, kvp.Value, kvp.Key), nameof(CollectionChanged));

    }

    public void UnselectItems(IEnumerable<T> items)
    {
        var indexes = new List<int>();
        foreach (var item in items)
            indexes.Add(Collection!.IndexOf(item));
        UnselectIndexes(indexes);
    }

    public void Clear()
    {
        var oldSelectedIndex = SelectedIndex;
        var oldSelectedItem = SelectedItem;
        var oldSelectedIndexes = SelectedIndexes.ToArray().ToList();

        SelectedIndexes.Clear();
        if (SelectedIndex != -1)
            _selectionChangedEventManager.RaiseEvent(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex), nameof(SelectionChanged));
        //SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));
        if (oldSelectedIndexes.Count != 0)
            _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldSelectedIndexes, 0), nameof(CollectionChanged));
        //CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldSelectedIndex, 0));
    }
    #endregion


    #region Support Methods

    private void SetSelectedItemsMulti(List<T> newSelectedItems)
    {
        var newSelectedIndexes = new List<int>();
        if (newSelectedItems != null && newSelectedItems.Count != 0)
        {
            for (var i = newSelectedItems.Count - 1; i >= 0; i--)
            {
                if (Collection!.IndexOf(newSelectedItems[i]) is int index && index > -1)
                    newSelectedIndexes.Add(index);
            }
        }

        SetSelectedIndexesMulti(newSelectedIndexes);
    }

    private void SetSelectedIndexesMulti(List<int>? newSelectedIndexes)
    {
        var newSelectedIndex = -1;
        var newSelectedIndexSet = false;
        newSelectedIndexes ??= [];
        if (newSelectedIndexes.Count > 0)
        {
            for (var i = newSelectedIndexes.Count-1; i >= 0; i--)
            {
                if (newSelectedIndexes[i] >= Collection!.Count)
                {
                    newSelectedIndexes.Remove(newSelectedIndexes[i]);
                }
                else if (!newSelectedIndexSet)
                {
                    newSelectedIndex = newSelectedIndexes[i];
                    newSelectedIndexSet = true;
                }
            }
        }

        if (newSelectedIndex >= Collection!.Count)
            return;

        if (newSelectedIndex < 0)
        {
            Clear();
            return;
        }

        var oldSelectedIndex = SelectedIndex;
        var oldSelectedItem = SelectedItem;

        var changed = SelectedIndexes.Count != newSelectedIndexes.Count;
        if (!changed)
        {
            for (var i = 0; i < SelectedIndexes.Count; i++)
            {
                if (SelectedIndexes[i] != newSelectedIndexes[i])
                {
                    changed = true;
                    break;
                }
            }
        }

        if (changed)
        {
            SelectedIndexes.Clear();
            SelectedIndexes.AddRange(newSelectedIndexes);
            if (SelectedIndex != oldSelectedIndex)
                _selectionChangedEventManager.RaiseEvent(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex), nameof(SelectionChanged));
            _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset), nameof(CollectionChanged));
            _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newSelectedIndexes, 0), nameof(CollectionChanged));
        }
    }

    private void UpdateToSelectedMulti(int newSelectedIndex)
    {
        if (newSelectedIndex >= Collection!.Count)
            return;

        if (newSelectedIndex == SelectedIndex || newSelectedIndex >= Collection.Count)
            return;

        if (newSelectedIndex < 0)
        {
            Clear();
            return;
        }

        var oldSelectedIndex = SelectedIndex;
        var oldSelectedItem = SelectedItem;

        var movedFrom = -1;
        if (SelectedIndexes.Contains(newSelectedIndex))
        {
            var i = SelectedIndexes.IndexOf(newSelectedIndex);
            movedFrom = i;
            SelectedIndexes.Remove(newSelectedIndex);
        }
        SelectedIndexes.Add(newSelectedIndex);

        if (SelectedIndex != oldSelectedIndex)
            _selectionChangedEventManager.RaiseEvent(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex), nameof(SelectionChanged));
        //SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));
        if (movedFrom != -1)
            _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, SelectedIndex, Collection.Count - 1, movedFrom), nameof(CollectionChanged));
        //CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, SelectedIndex, collection.Count - 1, movedFrom));
        else
            _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, SelectedIndex, Collection.Count - 1), nameof(CollectionChanged));
        //CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, SelectedIndex, collection.Count - 1));
    }

    private void UpdateToSelectedRadio(int newSelectedIndex)
    {
        if (newSelectedIndex >= Collection!.Count)
            return;

        if (newSelectedIndex == SelectedIndex || newSelectedIndex >= Collection!.Count)
            return;

        if (newSelectedIndex < 0)
        {
            Clear();
            return;
        }

        var oldSelectedIndex = SelectedIndex;
        var oldSelectedItem = SelectedItem;

        var removedBefore = new List<int>();
        var removedAfter = new List<int>();
        var removedBeforeIndex = -1;
        var removedAfterIndex = -1;

        foreach (var index in SelectedIndexes.ToArray())
        {
            if (index != newSelectedIndex)
            {
                if (index < newSelectedIndex)
                {
                    removedBefore.Add(index);
                    if (removedBeforeIndex == -1)
                        removedBeforeIndex = index;
                }
                else
                {
                    removedAfter.Add(index);
                    if (removedAfterIndex == -1)
                        removedAfterIndex = index;
                }
                SelectedIndexes.Remove(index);
            }
        }

        var added = new List<int>
        {
            newSelectedIndex
        };
        SelectedIndexes.Add(newSelectedIndex);

        if (SelectedIndex != oldSelectedIndex)
            _selectionChangedEventManager.RaiseEvent(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex), nameof(SelectionChanged));
        //SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));
        if (removedAfter.Count != 0)
            _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedAfter, removedAfterIndex), nameof(CollectionChanged));
        //CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedAfter, removedAfterIndex));
        if (removedBefore.Count != 0)
            _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedBefore, removedBeforeIndex), nameof(CollectionChanged));
        //CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedBefore, removedBeforeIndex));
        if (added.Count != 0)
            _collectionChangedEventManager.RaiseEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added), nameof(CollectionChanged));
        //CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added));

    }

    private void UpdateToSelectNone()
        => Clear();

    #endregion
}


public class CollectionSelectionTrackerSelectionChangedArguments<T>(T? oldItem, int oldIndex, T? newItem, int newIndex) : EventArgs
{
    public int OldIndex { get; private set; } = oldIndex;

    public T? OldItem { get; private set; } = oldItem;

    public int NewIndex { get; private set; } = newIndex;

    public T? NewItem { get; private set; } = newItem;
}
