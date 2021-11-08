using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace App1
{
    public sealed partial class SimplePage : Page
    {
        public SimplePage()
        {
            Content = new Grid()
            {
                Children =
                { 
                    new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = "SIMPLE PAGE",
                        Foreground = new SolidColorBrush(Colors.Red)
                    }
                }
            };
        }
    }
}