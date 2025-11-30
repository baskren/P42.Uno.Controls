

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using System.Collections.ObjectModel;
using Windows.UI;

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public partial class LoopingFlipViewTestPage 
{
    private readonly ObservableCollection<UIElement> _elements = new();

    public LoopingFlipViewTestPage()
    {
        Build();

        _elements.AddRange([
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
        ]);


        var flipView = new LoopingFlipView
        {
            ItemsSource = _elements
        };

        _innerGrid.Children.Add(flipView);
    }

    public class Item : Grid
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

        private static readonly Random Rand = new Random();

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
            Background = new SolidColorBrush(Color.FromArgb(255, (byte)Rand.Next(255), (byte)Rand.Next(255), (byte)Rand.Next(255)));
        }
    }
}
