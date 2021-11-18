using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace P42.Uno.Controls
{
    [Windows.UI.Xaml.Data.Bindable]
    [System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    [ContentProperty(Name = "Items")]
    public partial class MenuFlyout 
    {
        #region Items Property
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            nameof(Items),
            typeof(IList<MenuFlyoutItemBase>),
            typeof(MenuFlyout),
            new PropertyMetadata(new List<MenuFlyoutItemBase>())
        );
        public IList<MenuFlyoutItemBase> Items
        {
            get => (IList<MenuFlyoutItemBase>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }
        #endregion Items Property

        internal MenuFlyout MenuParent = null;
        FrameworkElement host;

        public MenuFlyout(FrameworkElement target= null) 
#if !WINDOWS_UWP
        : base(target)
#endif
        {
            host = target;
            Build();

            _listView.ItemClick += _listView_ItemClick;
        }

        async void _listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*
            if (e.ClickedItem is MenuFlyoutSubItem subItem)
            {
                var flyout = new MenuFlyout(subItem);
                flyout.PreferredPointerDirection = PointerDirection.Horizontal;
                flyout.MenuParent = this;
                flyout.Items = Items;
                await flyout.PushAsync();
            }
            */
        }
    }
}