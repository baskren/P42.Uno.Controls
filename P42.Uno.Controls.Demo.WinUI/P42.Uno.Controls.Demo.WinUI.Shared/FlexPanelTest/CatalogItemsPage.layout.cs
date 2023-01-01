using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using P42.Uno.Controls;
using P42.Uno.Markup;
using P42.Utils.Uno;
using P42.Utils;
using Microsoft.UI;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CatalogItemsPage : Page
    {
        void Build()
        {
            this.AddStyle("ItemStyle", typeof(Border),
                    (Border.BackgroundProperty, new SolidColorBrush(Colors.LightYellow)),
                    (Border.BorderBrushProperty, Colors.Blue),
                    (Border.BorderThicknessProperty, 1),
                    (Border.MarginProperty, 5),
                    (Border.PaddingProperty, 20),
                    (Border.CornerRadiusProperty, 20),
                    (Border.WidthProperty, 350),
                    (Border.HeightProperty, 500)
                )
                .AddStyle(typeof(TextBlock),
                    (TextBlock.MarginProperty, new Thickness(0, 2, 0, 2)),
                    (TextBlock.TextWrappingProperty, TextWrapping.WrapWholeWords),
                    (TextBlock.ForegroundProperty, Colors.Black)
                )
                .AddStyle("headerTextBlock", typeof(TextBlock),
                    (TextBlock.MarginProperty, new Thickness(0, 8, 0, 8)),
                    (TextBlock.FontSizeProperty, 30),
                    (TextBlock.ForegroundProperty, Colors.Blue)
                )
                .AddStyle(typeof(Image),
                    (FlexPanel.OrderProperty, -1),
                    (FlexPanel.AlignSelfProperty, FlexAlignSelf.Center)
                )
                .AddStyle(typeof(Button),
                    (Button.ContentProperty, "LEARN MORE"),
                    (Button.FontSizeProperty, 30),
                    (Button.ForegroundProperty, Colors.White),
                    (Button.BackgroundProperty, Colors.Green),
                    (Button.CornerRadiusProperty, 10),
                    (FlexPanel.AlignSelfProperty, FlexAlignSelf.Center)
                );

            Content = new ScrollViewer()
                .Margin(0)
                .Padding(0)
                .Stretch()
                .Background(Colors.Pink)
                .HorizontalScrollBarVisibility(ScrollBarVisibility.Visible)
                .HorizontalScrollMode(ScrollMode.Enabled)
                .VerticalScrollBarVisibility(ScrollBarVisibility.Auto)
                .VerticalScrollMode(ScrollMode.Disabled)
                .Content
                (
                    new FlexPanel()
                        .Background(Colors.Cyan)
                        .Children
                        (
                            new Border()
                                .Style((Style)Resources["ItemStyle"])
                                .Child
                                (
                                    new FlexPanel()
                                        .Direction(FlexDirection.Column)
                                        .Children
                                        (
                                            new TextBlock().Text("Seated Monkey").Style((Style)Resources["headerTextBlock"]),
                                            new TextBlock().Text("This monkey is laid back and relaxed, and likes to watch the world go by."),
                                            new TextBlock().Text("  &#x2022; Doesn't make a lot of noise"),
                                            new TextBlock().Text("  &#x2022; Often smiles mysteriously"),
                                            new TextBlock().Text("  &#x2022; Sleeps sitting up"),
                                            new Image()
                                                .Size(180,180)
                                                .Source("/Assets/SeatedMonkey.jpg"),
                                            new TextBlock().FlexPanelGrow(1),
                                            new Button()
                                        )
                                ),
                            new Border()
                                .Style((Style)Resources["ItemStyle"])
                                .Child
                                (
                                    new FlexPanel()
                                        .Direction(FlexDirection.Column)
                                        .Children
                                        (
                                            new TextBlock().Text("Banana Monkey").Style((Style)Resources["headerTextBlock"]),
                                            new TextBlock().Text("Watch this monkey eat a giant banana."),
                                            new TextBlock().Text("  &#x2022; More fun than a barrel of monkeys"),
                                            new TextBlock().Text("  &#x2022; Banana not included"),
                                            new Image()
                                                .Size(240, 180)
                                                .Source("/Assets/Banana.jpg"),
                                            new TextBlock().FlexPanelGrow(1),
                                            new Button()
                                        )
                                ),
                            new Border()
                                .Style((Style)Resources["ItemStyle"])
                                .Child
                                (
                                    new FlexPanel()
                                        .Direction(FlexDirection.Column)
                                        .Children
                                        (
                                            new TextBlock().Text("Face-Palm Monkey").Style((Style)Resources["headerTextBlock"]),
                                            new TextBlock().Text("This monkey reacts appropriately to ridiculous assertions and actions."),
                                            new TextBlock().Text("  &#x2022; Cynical but not unfriendly"),
                                            new TextBlock().Text("  &#x2022; Seven varieties of grimaces"),
                                            new TextBlock().Text("  &#x2022; Doesn't laugh at your jokes"),
                                            new Image()
                                                .Size(180, 180)
                                                .Source("/Assets/FacePalm.jpg"),
                                            new TextBlock().FlexPanelGrow(1),
                                            new Button()
                                        )
                                )
                        )
                );
        }
    }
}