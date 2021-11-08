using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using P42.Utils;
using P42.Utils.Uno;
using P42.Uno.Markup;
using P42.Uno.Controls;
using Windows.UI;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class HolyGrailLayoutPage : Page
    {
        void Build()
        {
            Content = new Grid()
                .Children
                (
                    new FlexPanel()
                        .Direction(FlexDirection.Column)
                        .AddStyle(typeof(TextBlock),
                            (TextBlock.FontSizeProperty, 40),
                            (TextBlock.TextAlignmentProperty, TextAlignment.Center)
                        )
                        .Children
                        (
                            new Border()
                                .Background(Colors.Aqua)
                                .Child(new TextBlock().Text("HEADER")),

                            new FlexPanel()
                                .Direction(FlexDirection.Row)
                                .Background(Colors.AliceBlue)
                                .FlexPanelGrow(1)
                                .Children
                                (
                                    new Rectangle()
                                        .FlexPanelOrder(-1)
                                        .FlexPanelBasis(50)
                                        .Fill(Colors.Blue),
                                    new Border()
                                        .Background(Colors.Gray)
                                        .FlexPanelGrow(1)
                                        .Child(new TextBlock().Text("CONTENT").StretchHorizontal().CenterVertical()),
                                    new Rectangle()
                                        .FlexPanelBasis(50)
                                        .Fill(Colors.Green)
                                ),

                            new Border()
                                .Background(Colors.Pink)
                                .Child(new TextBlock().Text("FOOTER"))
                        )
                );
        }
    }
}