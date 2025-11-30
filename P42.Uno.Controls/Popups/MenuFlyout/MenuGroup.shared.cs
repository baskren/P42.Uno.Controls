using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Markup;

namespace P42.Uno.Controls;

[Bindable]
[Obsolete("Use Microsoft.UI.Xaml.Controls.MenuFlyoutSubItem")]
[ContentProperty(Name = "Items")]
public class MenuGroup : MenuItemBase
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
        Items = ObsvItems = [];
    }

}