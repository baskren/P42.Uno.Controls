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
using UserControlTest.Popups;
using Windows.UI;

#if NETFX_CORE
#else
using Uno.Foundation;
#endif

#if __ANDROID__
using Android.Content;
using Android.Content.Res;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
#endif

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UserControlTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [TemplateVisualState(GroupName = "State", Name = "Collapsed")]
    [TemplateVisualState(GroupName = "State", Name = "Visible")]
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            _listView.ItemsSource = new List<int> { 1, 2, 3, 4 };

        }

        ModalPopup _popup = new ModalPopup
        {
            HasShadow = true,
            BorderThickness = new Thickness(1, 2, 3, 4),
            CornerRadius = new CornerRadius(4,8,12,16)
        };

        private void OnAltBorderTapped(object sender, TappedRoutedEventArgs e)
        {
            BorderTapped(sender, e);
        }

        async void BorderTapped(object sender, TappedRoutedEventArgs e)
        {
            _button_Click(sender, e);
        }

        private void OnCanvasTapped(object sender, TappedRoutedEventArgs e)
        {
            _button_Click(sender, e);
        }

        private void OnContentControlTapped(object sender, TappedRoutedEventArgs e)
        {
            _button_Click(sender, e);
        }

        async void _button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                if (!_grid.Children.Contains(_popup))
                    _grid.Children.Add(_popup);

                //var point = ScreenCoorder(element);
                var frame = element.GetFrame();
                System.Diagnostics.Debug.WriteLine(GetType() + $".BorderTapped  sender:[{sender.GetType()}] args:[{e}] frame:[{frame}]");
                _popup.Content = $"frame:[{frame.X.ToString("0.##")}, {frame.Y.ToString("0.##")}, {frame.Width.ToString("0.##")}, {frame.Height.ToString("0.##")}]";

                //Canvas.SetLeft(_popup, frame.X);
                //Canvas.SetTop(_popup, frame.Y);

                await _popup.PushAsync();
            }
        }


    }
}
