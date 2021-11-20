using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace P42.Uno.Controls
{
    public partial class MenuFlyout : IDisposable
    {
        Windows.UI.Xaml.Controls.MenuFlyout _flyout;
        ItemsCollectionChangeHandler _collectionChangedHandler;


        void Build()
        {
            _flyout = new Windows.UI.Xaml.Controls.MenuFlyout();
            _collectionChangedHandler = new ItemsCollectionChangeHandler(ObsvItems, _flyout.Items);
            ObsvItems.CollectionChanged += _collectionChangedHandler.OnItemsCollectionChanged;
        }

        private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MenuFlyout flyout)
            {
                if (e.OldValue is Button oldButton)
                    oldButton.Flyout = null;
                else if (e.OldValue is UIElement oldElement)
                    oldElement.ContextFlyout = null;

                if (flyout.Target is Button button)
                    button.Flyout = flyout._flyout;
                else if (flyout.Target is UIElement element)
                    element.ContextFlyout = flyout._flyout;
            }
        }

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _flyout.Items.Clear();
                    ObsvItems.CollectionChanged -= _collectionChangedHandler.OnItemsCollectionChanged;
                    _collectionChangedHandler = null;
                    if (_flyout is IDisposable disposable)
                        disposable.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }


    internal class ItemsCollectionChangeHandler
    {
        public IList<Windows.UI.Xaml.Controls.MenuFlyoutItemBase> WinItems;
        public ObservableCollection<MenuItemBase> Items;

        public ItemsCollectionChangeHandler(ObservableCollection<MenuItemBase> items, IList<Windows.UI.Xaml.Controls.MenuFlyoutItemBase> winItems)
        {
            Items = items;
            WinItems = winItems;
        }

        public void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int i = 0;
                        foreach (MenuItemBase item in e.NewItems)
                            WinItems.InsertItem(e.NewStartingIndex + i++, item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (MenuItemBase item in e.OldItems)
                            WinItems.DeleteItem(e.OldStartingIndex, item);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        foreach (MenuItemBase item in e.OldItems)
                            WinItems.DeleteItem(e.OldStartingIndex, item);
                        int i = 0;
                        foreach (MenuItemBase item in e.NewItems)
                            WinItems.InsertItem(e.NewStartingIndex + i++, item);
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    {
                        foreach (MenuItemBase item in e.OldItems)
                            WinItems.DeleteItem(e.OldStartingIndex, item);
                        int i = 0;
                        foreach (MenuItemBase item in e.NewItems)
                            WinItems.InsertItem(e.NewStartingIndex + i++, item);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ClearItems();
                    break;
            }
        }

        void ClearItems()
        {
            foreach (var item in WinItems)
                item.DeleteItem();
        }

    }

    internal static class MenuItemExtensions
    {

        public static void InsertItem(this IList<Windows.UI.Xaml.Controls.MenuFlyoutItemBase> winItems, int index, MenuItemBase item)
        {
            winItems.Insert(index, item.AsMenuFlyoutItem());
        }

        public static void DeleteItem(this IList<Windows.UI.Xaml.Controls.MenuFlyoutItemBase> winItems, int index, MenuItemBase item)
        {
            if (index < 0 || index > winItems.Count)
                return;
            var winItem = winItems[index];
            if (winItem.Tag != item)
                throw new Exception("out of sync");
            winItem.DeleteItem();
        }

        public static void DeleteItem(this Windows.UI.Xaml.Controls.MenuFlyoutItemBase winItemBase)
        {
            if (winItemBase is Windows.UI.Xaml.Controls.MenuFlyoutItem winItem)
                winItem.Click -= OnItemClick;
            if (winItemBase is Windows.UI.Xaml.Controls.MenuFlyoutSubItem winSubItem)
            {
                if (winSubItem.Tag is ItemsCollectionChangeHandler collectionChangedHandler)
                {
                    collectionChangedHandler.Items.CollectionChanged -= collectionChangedHandler.OnItemsCollectionChanged;
                    collectionChangedHandler.Items = null;
                    collectionChangedHandler.WinItems = null;
                }
                foreach (var x in winSubItem.Items)
                    x.DeleteItem();
            }
            winItemBase.Tag = null;
        }


        static void OnItemClick(object sender, RoutedEventArgs e)
        {
            if (sender is Windows.UI.Xaml.Controls.MenuFlyoutItem winItem && winItem.Tag is MenuItemBase item)
                item.OnItemClicked();
        }


        public static Windows.UI.Xaml.Controls.MenuFlyoutItemBase AsMenuFlyoutItem(this MenuItemBase itemBase)
        {
            if (itemBase is MenuItem)
            {
                var winItem = new Windows.UI.Xaml.Controls.MenuFlyoutItem
                {
                    Text = itemBase.Text,
                    Icon = itemBase.IconSource.AsIconElement(),
                    Tag = itemBase
                };
                winItem.Click += OnItemClick;
                return winItem;
            }
            else if (itemBase is MenuGroup group)
            {
                var winGroupItem = new Windows.UI.Xaml.Controls.MenuFlyoutSubItem
                {
                    Text = itemBase.Text,
                    Icon = itemBase.IconSource.AsIconElement(),
                };
                var collectionChangedHandler = new ItemsCollectionChangeHandler(group.ObsvItems, winGroupItem.Items);
                winGroupItem.Tag = collectionChangedHandler;
                group.ObsvItems.CollectionChanged += collectionChangedHandler.OnItemsCollectionChanged;

                foreach (var i in group.Items)
                    winGroupItem.Items.Add(i.AsMenuFlyoutItem());

                return winGroupItem;
            }
            return null;
        }
    }

}