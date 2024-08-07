using System;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    // TODO: Obsolete P42.Uno.Control.MenuFlyout after 2025.01.01
    
    /// <summary>
    /// Item for Popup Menu
    /// </summary>
    [Obsolete("Use Microsoft.UI.Xaml.Controls.MenuFlyoutItem")]
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class MenuItemBase : DependencyObject 
    {
        #region Properties 

        #region Text Property
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(MenuItemBase),
            new PropertyMetadata(default(string))
        );
        /*
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MenuFlyoutItemBase item)
            {
                if (e.NewValue == null)
                {
                    if (item._label != null && item.ContentGrid.Children.Contains(item._label))
                        item.ContentGrid.Children.Remove(item._label);
                }
                else
                {
                    item._label = item._label ?? new TextBlock()
                                                        .Column(1)
                                                        .CenterVertical()
                                                        .Left()
                                                        .Margin(0, 5, 20, 5);

                    item._label.Foreground(Microsoft.UI.Colors.Black);
                    //item._label.Background(Microsoft.UI.Colors.Pink);


                    if (!item.ContentGrid.Children.Contains(item._label))
                        item.ContentGrid.Children.Add(item._label);

                    item._label.Text = item.Text;
                }
            }
        }
        */
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion Text Property

        #region Icon Property
        public static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register(
            nameof(IconSource),
            typeof(IconSource),
            typeof(MenuItemBase),
            new PropertyMetadata(default(IconElement))
        );
        /*
        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MenuFlyoutItemBase item)
            {
                if (e.OldValue is IconElement oldIcon)
                {
                    item.ContentGrid.Children.Remove(oldIcon);
                }
                if (e.NewValue is IconElement newIcon)
                {
                    newIcon
                        .Column(0)
                        .Center()
                        .Margin(0,0,10,0);
                    item.ContentGrid.Children.Add(newIcon);
                }
            }
        }
        */
        public IconSource IconSource
        {
            get => (IconSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }
        #endregion Icon Property

        #region CommandParameter Property
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter),
            typeof(object),
            typeof(MenuItemBase),
            new PropertyMetadata(default(object))
        );
        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        #endregion CommandParameter Property

        #region Command Property
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(MenuItemBase),
            new PropertyMetadata(default(ICommand))
        );
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        #endregion Command Property

        #endregion


        #region Events
        public event RoutedEventHandler Click;
        #endregion


        public object Tag;
#if !HAS_UNO
        //internal WeakReference<MenuFlyout> MenuFlyoutWeakRef;
        internal WeakReference<MenuFlyoutCell> MenuFlyoutCellWeakRef;
#endif

        public MenuItemBase() 
        {
        }

        public void OnItemClicked()
        {
            Click?.Invoke(this, new RoutedEventArgs());
            Command?.Execute(CommandParameter);
        }
    }
}
