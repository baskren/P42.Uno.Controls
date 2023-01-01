using P42.Uno.Markup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace P42.Uno.Controls.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoopingFlipViewTestPage : Page
    {
        ObservableCollection<UIElement> Elements;


        public LoopingFlipViewTestPage()
        {
            //this.InitializeComponent();
            Build();

            Elements = new ObservableCollection<UIElement>
            {
                new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    },
                    Children =
                    {
                        new Item("1"),
                        new Item("2").Column(1),
                        new Item("3").Column(2),
                        new Item("4").Column(3),
                    }
                },
                new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    },
                    Children =
                    {
                        new Item("5"),
                        new Item("6").Column(1),
                        new Item("7").Column(2),
                        new Item("8").Column(3),
                    }
                },
                new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    },
                    Children =
                    {
                        new Item("9"),
                        new Item("A").Column(1),
                        new Item("B").Column(2),
                        new Item("C").Column(3),
                    }
                },
                /*
                new Item("4"),
                new Item("5"),
                new Item("6"),
                new Item("7"),
                new Item("8"),
                new Item("9"),
                new Item("10"),
                */
            };


            var flipView = new P42.Uno.Controls.LoopingFlipView();
            flipView.ItemsSource = Elements;

            _innerGrid.Children.Add(flipView);
        }

        public partial class Item : Grid
        {
            #region Text Property
            public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(Item),
                new PropertyMetadata(default(string))
            );
            public string Text
            {
                get => (string)GetValue(TextProperty);
                set => SetValue(TextProperty, value);
            }
            #endregion Text Property

            static Random rand = new Random();

            public Item(string text)
            {
                Children.Add(new TextBlock
                {
                    Text = text,
                    FontSize = 50,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Colors.Red)
                });
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, (byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255)));
            }
        }
    }
}
