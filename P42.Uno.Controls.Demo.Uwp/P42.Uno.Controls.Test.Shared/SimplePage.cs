using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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