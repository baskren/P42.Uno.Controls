using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace P42.Uno.Controls
{
    internal class CollectionSelectionTracker<T> : INotifyCollectionChanged 
    {
        #region Properties
        
        SelectionMode _selectionMode = SelectionMode.None;
        public SelectionMode SelectionMode
        {
            get => _selectionMode;
            set
            {
                if (_selectionMode != value)
                {
                    _selectionMode = value;
                    SelectIndex(SelectedIndex);
                }
            }
        }

        public Func<T> SelectedItemWhenNoneSelected;

        public int SelectedIndex
        {
            get
            {
                if (_selectedIndexes.Any())
                    return _selectedIndexes.Last();
                return -1;
            }
        }

        public T SelectedItem
        {
            get
            {
                if (Collection is IList<T> collection)
                {
                    if (SelectedIndex > -1 && collection.Count > SelectedIndex)
                        return collection[SelectedIndex];
                }

                if (SelectedItemWhenNoneSelected is null)
                    return default;
                return SelectedItemWhenNoneSelected();
            }
        }

        List<int> _selectedIndexes = new List<int>();
        public IList<int> SelectedIndexes => _selectedIndexes;

        public IList<T> SelectedItems
        {
            get
            {
                var result = new List<T>();
                for (int i = 0; i < Collection.Count; i++)
                {
                    if (_selectedIndexes.Contains(i))
                        result.Add(Collection[i]);
                }
                return result;
            }
        }

        WeakReference<IList<T>> _weakCollectionRef;
        public IList<T> Collection
        {
            get
            {
                if (_weakCollectionRef?.TryGetTarget(out var target) ?? false)
                    return target;
                return null;
            }
            set
            {
                if (Collection != value)
                {
                    if (Collection is INotifyCollectionChanged oldCollection)
                        oldCollection.CollectionChanged -= OnCollection_CollectionChanged;
                    _selectedIndexes.Clear();
                    _weakCollectionRef = new WeakReference<IList<T>>(value);
                    if (Collection is INotifyCollectionChanged newCollection)
                        newCollection.CollectionChanged += OnCollection_CollectionChanged;
                    BoundSelections();
                }
            }
        }

        public bool AllowUnselectLastSelected = false;

        #endregion


        #region Events
        //public event EventHandler<(int OldIndex, int NewIndex)> SelectedIndexChanged;
        public event EventHandler<CollectionSelectionTrackerSelectionChangedArguments<T>> SelectionChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion


        #region Constructor
        public CollectionSelectionTracker(IList<T> collection = null)
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
            => SelectIndex(Collection.IndexOf(item));
        

        public void SelectIndexes(IEnumerable<int> indexes)
        {
            foreach (var index in indexes)
                SelectIndex(index);
        }

        public void SelectItems(IEnumerable<T> items)
        {
            foreach (var item in items)
                SelectIndex(Collection.IndexOf(item));
        }

        public void UnselectIndex(int index)
        {
            if (index < 0 || index >= (Collection?.Count ?? 0))
                return;

            if (AllowUnselectLastSelected || (_selectedIndexes?.Count ?? 0) > 1)
            {
                if (_selectedIndexes.Contains(index))
                {
                    var oldSelectedIndex = SelectedIndex;
                    var oldSelectedItem = SelectedItem;
                    var i = _selectedIndexes.IndexOf(index);
                    _selectedIndexes.Remove(index);

                    if (oldSelectedIndex != SelectedIndex)
                        SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<int>(index), i));
                }
            }
        }

        public void UnselectItem(T item)
            => UnselectIndex(Collection.IndexOf(item));

        public void UnselectIndexes(IEnumerable<int> indexes)
        {
            var groups = new Dictionary<int, List<int>>();
            var oldSelectedIndex = SelectedIndex;
            var oldSelectedItem = SelectedItem;


            for (int i = 0; i < _selectedIndexes.Count; i++)
            {
                if (AllowUnselectLastSelected || (_selectedIndexes?.Count ?? 0) > 1)
                {
                    if (indexes.Contains(_selectedIndexes[i]))
                    {
                        if (!groups.Any() || groups.Last().Key + groups.Last().Value.Count < i)
                            groups.Add(i, new List<int>());
                        groups.Last().Value.Add(_selectedIndexes[i]);
                        _selectedIndexes.Remove(_selectedIndexes[i]);
                    }
                }
            }

            if (oldSelectedIndex != SelectedIndex)
                SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));

            foreach (var kvp in groups)
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, kvp.Value, kvp.Key));

        }

        public void UnselectItems(IEnumerable<T> items)
        {
            var indexes = new List<int>();
            foreach (var item in items)
                indexes.Add(Collection.IndexOf(item));
            UnselectIndexes(indexes);
        }

        public void Clear()
        {
            var oldSelectedIndex = SelectedIndex;
            var oldSelectedItem = SelectedItem;
            var oldSelectedIndexes = _selectedIndexes.ToArray().ToList();

            _selectedIndexes.Clear();
            if (SelectedIndex != -1)
                SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));
            if (oldSelectedIndexes.Any())
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldSelectedIndex, 0));
        }
        #endregion


        #region Support Methods
        private void OnCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            => BoundSelections();

        void UpdateToSelectedMulti(int newSelectedIndex)
        {
            if (Collection is IList<T> collection)
            {
                if (newSelectedIndex >= collection.Count)
                    return;

                if (newSelectedIndex == SelectedIndex || newSelectedIndex >= (Collection?.Count ?? 0))
                    return;

                if (newSelectedIndex < 0)
                {
                    Clear();
                    return;
                }

                var oldSelectedIndex = SelectedIndex;
                var oldSelectedItem = SelectedItem;

                var movedFrom = -1;
                if (_selectedIndexes.Contains(newSelectedIndex))
                {
                    var i = _selectedIndexes.IndexOf(newSelectedIndex);
                    movedFrom = i;
                    _selectedIndexes.Remove(newSelectedIndex);
                }
                _selectedIndexes.Add(newSelectedIndex);

                if (SelectedIndex != oldSelectedIndex)
                    SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));
                if (movedFrom != -1)
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, SelectedIndex, collection.Count - 1, movedFrom));
                else
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, SelectedIndex, collection.Count - 1));
            }
        }

        void UpdateToSelectedRadio(int newSelectedIndex)
        {
            if (Collection is IList<T> collection)
            {
                if (newSelectedIndex >= collection.Count)
                    return;
            }

            if (newSelectedIndex == SelectedIndex || newSelectedIndex >= (Collection?.Count ?? 0))
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

            foreach (var index in _selectedIndexes.ToArray())
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
                    _selectedIndexes.Remove(index);
                }
            }

            var added = new List<int>();
            added.Add(newSelectedIndex);
            _selectedIndexes.Add(newSelectedIndex);

            if (SelectedIndex != oldSelectedIndex)
                SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));
            if (removedAfter.Any())
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedAfter, removedAfterIndex));
            if (removedBefore.Any())
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedBefore, removedBeforeIndex));
            if (added.Any())
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added));

        }

        void UpdateToSelectNone()
            => Clear();

        void BoundSelections()
        {
            if (Collection is IList<T> collection)
            {
                var oldSelectedIndex = SelectedIndex;
                var oldSelectedItem = SelectedItem;

                var removed = new List<int>();
                foreach (var index in _selectedIndexes.ToArray())
                {
                    if (index >= collection.Count)
                    {
                        _selectedIndexes.Remove(index);
                        removed.Add(index);
                    }
                }
                if (removed.Any())
                {
                    if (SelectedIndex != oldSelectedIndex)
                        SelectionChanged?.Invoke(this, new CollectionSelectionTrackerSelectionChangedArguments<T>(oldSelectedItem, oldSelectedIndex, SelectedItem, SelectedIndex));
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed, collection.Count));
                }
            }
        }
        #endregion
    }


    public class CollectionSelectionTrackerSelectionChangedArguments<T> : EventArgs
    {
        public int OldIndex { get; private set; }

        public T OldItem { get; private set; }

        public int NewIndex { get; private set; }

        public T NewItem { get; private set; }

        public CollectionSelectionTrackerSelectionChangedArguments(T oldItem, int oldIndex, T newItem, int newIndex)
        {
            OldItem = oldItem;
            OldIndex = oldIndex;
            NewItem = newItem;
            NewIndex = newIndex;
        }
    }
}
