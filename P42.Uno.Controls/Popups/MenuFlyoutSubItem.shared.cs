using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using P42.Uno.Markup;
using System;

namespace P42.Uno.Controls
{
    public partial class MenuFlyoutSubItem : MenuFlyoutItemBase
    {
        #region Items Property
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            nameof(Items),
            typeof(IList<MenuFlyoutItemBase>),
            typeof(MenuFlyoutSubItem),
            new PropertyMetadata(default(IList<MenuFlyoutItemBase>))
        );
        public IList<MenuFlyoutItemBase> Items
        {
            get => (IList<MenuFlyoutItemBase>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }
        #endregion Items Property


        protected IconElement _chevron;

        public MenuFlyoutSubItem()
        {
            Content = new Grid()
                .Assign(out _grid)
                .Columns(0, 0, 20, 20)
                .Margin(10, 5)
                .ColumnSpacing(0);

            _grid.Children.Add(new FontIcon()
                .Assign(out _chevron)
                .FontFamily("Segoe MDL2 Assets")
                .Glyph("\uE76C"));
        }

    }
}