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
using Windows.UI;

namespace P42.Uno.Controls
{
    public partial class MenuFlyoutItemBase : DependencyObject 
    {
        #region Properties 

        #region Text Property
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(MenuFlyoutItemBase),
            new PropertyMetadata(default(string))
        );
        /*
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MenuFlyoutItemBase item)
            {
                if (e.NewValue == null)
                {
                    if (item._label != null && item._grid.Children.Contains(item._label))
                        item._grid.Children.Remove(item._label);
                }
                else
                {
                    item._label = item._label ?? new TextBlock()
                                                        .Column(1)
                                                        .CenterVertical()
                                                        .Left()
                                                        .Margin(0, 5, 20, 5);

                    item._label.Foreground(Colors.Black);
                    //item._label.Background(Colors.Pink);


                    if (!item._grid.Children.Contains(item._label))
                        item._grid.Children.Add(item._label);

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
            typeof(MenuFlyoutItemBase),
            new PropertyMetadata(default(IconElement))
        );
        /*
        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MenuFlyoutItemBase item)
            {
                if (e.OldValue is IconElement oldIcon)
                {
                    item._grid.Children.Remove(oldIcon);
                }
                if (e.NewValue is IconElement newIcon)
                {
                    newIcon
                        .Column(0)
                        .Center()
                        .Margin(0,0,10,0);
                    item._grid.Children.Add(newIcon);
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
            typeof(MenuFlyoutItemBase),
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
            typeof(MenuFlyoutItemBase),
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

#if __ANDROID__
        public new event RoutedEventHandler Click;
#else
        public event RoutedEventHandler Click;
#endif
        #endregion


        #region Fields
        protected TextBlock _label;
        protected Grid _grid;
#endregion


        public MenuFlyoutItemBase()
        {
        }

        public void OnItemClicked()
        {
            Click?.Invoke(this, new RoutedEventArgs());
            Command?.Execute(CommandParameter);
        }
    }
}