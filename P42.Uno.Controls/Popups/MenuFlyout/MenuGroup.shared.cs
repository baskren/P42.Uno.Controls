using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;
using P42.Uno.Markup;
using System;
using System.Collections.ObjectModel;

namespace P42.Uno.Controls
{
    [Microsoft.UI.Xaml.Data.Bindable]
    [ContentProperty(Name = "Items")]
    public partial class MenuGroup : MenuItemBase
    {
        #region Items Property
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            nameof(Items),
            typeof(IList<MenuItemBase>),
            typeof(MenuGroup),
            new PropertyMetadata(default(IList<MenuItemBase>))
        );
        public IList<MenuItemBase> Items
        {
            get => (IList<MenuItemBase>)GetValue(ItemsProperty);
            private set => SetValue(ItemsProperty, value);
        }
        #endregion Items Property

        internal ObservableCollection<MenuItemBase> ObsvItems;

        public MenuGroup()
        {
            Items = ObsvItems = new ObservableCollection<MenuItemBase>();
        }

    }
}