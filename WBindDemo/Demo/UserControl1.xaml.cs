using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Demo;
public sealed partial class UserControl1 : UserControl
{


    public UserControl1()
    {
        this.InitializeComponent();
    }

    async void OnButtonClick(object sender, RoutedEventArgs e)
    {
        var popup = new P42.Uno.Controls.PermissionPopup(sender as Button)
        {
            TitleContent = "Happy now?",
            Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tincidunt nunc pulvinar sapien et ligula ullamcorper malesuada proin. Sollicitudin aliquam ultrices sagittis orci a scelerisque purus. Molestie a iaculis at erat pellentesque adipiscing commodo. Sociis natoque penatibus et magnis dis parturient montes nascetur. Mi proin sed libero enim sed faucibus turpis in. Sem et tortor consequat id porta nibh venenatis cras sed. Lectus mauris ultrices eros in cursus turpis massa tincidunt dui. Semper risus in hendrerit gravida rutrum quisque non. Fames ac turpis egestas maecenas pharetra convallis posuere morbi leo. Lacus luctus accumsan tortor posuere ac. Dui faucibus in ornare quam viverra. Dolor sit amet consectetur adipiscing elit. Cras semper auctor neque vitae tempus quam. Tristique senectus et netus et.",
        };

        popup.PreferredPointerDirection = P42.Uno.Controls.PointerDirection.Vertical;
        popup.PageOverlayBrush = new SolidColorBrush(Colors.Black);
        popup.PopOnPageOverlayTouch = false;


        await popup.PushAsync();

    }
}
