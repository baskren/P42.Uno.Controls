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
using P42.Utils;
using P42.Utils.Uno;
using P42.Uno.Markup;
using P42.Uno.Controls;
using Microsoft.UI;
using Microsoft.UI.Xaml.Shapes;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class SimpleStackPage : Page
    {
        void Build()
        {
            Content = new FlexPanel()
                .Direction(FlexDirection.Column)
                .AlignItems(FlexAlignItems.Center)
                .JustifyContent(FlexJustify.SpaceEvenly)
                .Children
                (
                    new TextBlock().Text("FlexPanel in Action").FontSize(40),
                    new Image().Stretch(Stretch.None).Source("/Assets/OIP.jpg"),
                    new Button().Content("Do-Nothing Button"),
                    new TextBlock().Text("Another Label")
                );
        }
    }
}