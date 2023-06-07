using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Windows.UI;
using Windows.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;



namespace P42.Uno.Controls
{
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class MenuFlyout : DependencyObject
    {
        #region Properties

        #region Items Property
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            nameof(Items),
            typeof(IList<MenuItemBase>),
            typeof(MenuFlyout),
            new PropertyMetadata(default(IList<MenuItemBase>), OnItemsChanged)
        );


        public IList<MenuItemBase> Items
        {
            get => (IList<MenuItemBase>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
            /*
            {
                if (Items != null)
                {
                    Items.Clear();
                    if (value?.Any() ?? false)
                    {
                        foreach (var item in value)
                            Items.Add(item);
                    }
                }
            }
            */
        }
        #endregion Items Property

        #region Target Property
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(UIElement),
            typeof(MenuFlyout),
            new PropertyMetadata(default(UIElement), OnTargetChanged)
        );

        public UIElement Target
        {
            get => (UIElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        #endregion Target Property

        #endregion


        #region Fields
        //internal ObservableCollection<MenuItemBase> ObsvItems;
        #endregion


        #region Constructor
        public MenuFlyout(FrameworkElement target= null) 
        {
            //ObsvItems = new ObservableCollection<MenuItemBase>();
            //SetValue(ItemsProperty, ObsvItems);
            Build();
            Target = target;
        }
        #endregion
    }
}