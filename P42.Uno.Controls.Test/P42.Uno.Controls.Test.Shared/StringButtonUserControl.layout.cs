using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace P42.Uno.Controls.Test
{
    public partial class StringButtonUserControl : UserControl
    {
        Grid _grid;
        Rectangle _rectangle;
        Button _button;

        void Build()
        {
            Content = new Grid()
                        .Assign(out _grid)
                        .Stretch()
                        .Children
                        (
                            new Rectangle()
                                .Assign(out _rectangle)
                                .Stretch()
                                .Fill(Colors.Gray),
                            new Button()
                                .Assign(out _button)
                                .Padding(20,2)
                                .StretchHorizontal()
                                .CenterVertical()
                                .ContentCenter()
                                .Background(Colors.Beige)
                                .BorderBrush(Colors.Green)
                                .BorderThickness(1)
                                .CornerRadius(5)
                                .Opacity(0)
                                .AddOnTap(BorderTapped)
                                .Bind(Button.ContentProperty, this, nameof(Content))
                        );
        }

    }
}