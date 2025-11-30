#if !HAS_UNO
using System.ComponentModel;
using Microsoft.UI.Xaml.Markup;

namespace P42.Uno.Controls
{
    [System.ComponentModel.Bindable(BindableSupport.Yes)]
    [ContentProperty(Name = "Items")]
    public partial class MenuFlyout : IDisposable
    {
        #region Properties
        private MenuItemBase SelectedItem
        {
            get => _listView?.SelectedItem as MenuItemBase;
            set => _listView.SelectedItem = value;
        }

        private MenuFlyoutCell SelectedCell
        {
            get
            {
                if (SelectedItem is MenuItemBase item)
                {
                    if (item.MenuFlyoutCellWeakRef.TryGetTarget(out MenuFlyoutCell cell))
                    {
                        return cell;
                    }
                }
                return null;
            }
        }
        #endregion


        #region Fields
        internal TargetedPopup _popup;
        private ListView _listView;
        private MenuFlyout _parentMenu;
        private MenuFlyout _childMenu;
        #endregion


        #region Construction / Disposal
        private void Build()
        {
            _popup = new TargetedPopup(Target)
            {
                PreferredPointerDirection = PointerDirection.Left,
                PageOverlayBrush = null
            };
            _popup.Popped += OnPopup_Popped;
            _listView = new ListView
            {
                SelectionMode = ListViewSelectionMode.Single,
                ItemTemplate = typeof(MenuFlyoutCell).AsDataTemplate(),
            };
            _listView.SelectionChanged += OnListView_SelectionChanged;
            _popup.Content = _listView;
        }

        private bool isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _listView.SelectionChanged -= OnListView_SelectionChanged;
                    _popup.Popped -= OnPopup_Popped;
                    _childMenu?.Dispose();
                }
                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private async void OnListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _childMenu = _childMenu ?? new MenuFlyout();
            _childMenu._parentMenu = this;
            await _childMenu._popup.PopAsync();

            if (SelectedItem is MenuGroup group)
            {
                _childMenu.Items = group.Items;
                _childMenu.Target = SelectedCell;
                await _childMenu._popup.PushAsync();
            }
            else if (SelectedItem is MenuItem item)
            {
                item.OnItemClicked();
                await (_parentMenu ?? this)._popup.PopAsync();
            }
        }

        private static void OnItemsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is MenuFlyout flyout)
                flyout._listView.ItemsSource = flyout.Items;
        }
        /*
        private void OnObsvItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (MenuItemBase item in e.NewItems)
                        {
                            item.MenuFlyoutWeakRef = new WeakReference<MenuFlyout>(this);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (MenuItemBase item in e.OldItems)
                        {
                            item.MenuFlyoutWeakRef = null;
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldItems != null)
                    {
                        foreach (MenuItemBase item in e.OldItems)
                        {
                            item.MenuFlyoutWeakRef = null;
                        }
                    }

                    if (e.NewItems != null)
                    {
                        foreach (MenuItemBase item in e.NewItems)
                        {
                            item.MenuFlyoutWeakRef = new WeakReference<MenuFlyout>(this);
                        }
                    }

                    break;
                default:
                    break;
            }
        }
        */

        private async void OnPopup_Popped(object sender, PopupPoppedEventArgs e)
        {
            if (_childMenu is MenuFlyout childMenu)
                await childMenu._popup.PopAsync(animated: false);
            _listView.SelectedItem = null;
        }

        private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MenuFlyout mf)
            {
                if (e.OldValue is FrameworkElement oldElement)
                    oldElement.Tapped -= mf.OnTargetTapped;
                mf._popup.Target = e.NewValue as UIElement;
                if (!(e.NewValue is MenuFlyoutCell) && e.NewValue is FrameworkElement newElement)
                    newElement.Tapped += mf.OnTargetTapped;
            }
        }

        private async void OnTargetTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!(Target is MenuFlyoutCell cell))
            {
                _popup.PreferredPointerDirection = PointerDirection.Vertical;
                await _popup.PushAsync();
            }
        }

    }

    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class MenuFlyoutCell : Grid
    {
        public MenuFlyoutCell()
        {
            this
                .Padding(0)
                .Margin(0)
                .Left()
                .Columns("auto", "*", 20, "auto");

            DataContextChanged += CellTemplate_DataContextChanged;
        }

        private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Children.Clear();
            if (args.NewValue is MenuItemBase baseItem)
            {
                baseItem.MenuFlyoutCellWeakRef = new WeakReference<MenuFlyoutCell>(this);
                if (baseItem.IconSource is IconSource iconSource && iconSource.AsIconElement() is IconElement iconElement)
                    Children.Add(iconElement.Center().Margin(5, 0));
                if (baseItem.Text is string text)
                    Children.Add(new TextBlock { Text = text }.Margin(5, 0).Column(1).CenterVertical());
                if (args.NewValue is MenuGroup subItem)
                    Children.Add(new TextBlock { Text = "\uE76C", FontFamily = new FontFamily("Segoe MDL2 Assets") }.Margin(5, 0).Column(3).CenterVertical().Right());
            }
        }

    }

}
#endif
