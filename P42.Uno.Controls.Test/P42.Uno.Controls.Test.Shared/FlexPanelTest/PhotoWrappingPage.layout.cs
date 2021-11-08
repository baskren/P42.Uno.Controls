using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
    public partial class PhotoWrappingPage : Page
    {
        FlexPanel flexPanel;
        ProgressRing activityIndicator;

        void Build()
        {
            Content = new Grid()
                .Children
                (
                    new ScrollViewer()
                        .Content
                        (
                            new FlexPanel()
                                .Assign(out flexPanel)
                                .Wrap(FlexWrap.Wrap)
                                .JustifyContent(FlexJustify.SpaceAround)
                        ),
                    new ProgressRing()
                        .Assign(out activityIndicator)
                        .Center()
                );
        }
    }
}
 