﻿using System;
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
using P42.Uno.Popups;
using Windows.UI;
using System.Threading.Tasks;
using P42.Utils.Uno;

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
        /*
        BasePopup _BasePopup = new BasePopup
        {
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(4),
            Margin = new Thickness(10),
            Padding = new Thickness(20),
            Background = new SolidColorBrush(Colors.White),
            BorderBrush = new SolidColorBrush(Colors.Blue)
        };
        */


        private void OnAltBorderTapped(object sender, TappedRoutedEventArgs e)
        {
            BorderTapped(sender, e);
        }

        void BorderTapped(object sender, TappedRoutedEventArgs e)
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

        HorizontalAlignment _lastHorizontalAlignment = HorizontalAlignment.Stretch;
        async void _button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                /*
                if (!_grid.Children.Contains(_BasePopup))
                    _grid.Children.Add(_BasePopup);
                if (!_grid.Children.Contains(_bubblePopup))
                    _grid.Children.Add(_bubblePopup);

                //var point = ScreenCoorder(element);
                var frame = element.GetFrame();
                System.Diagnostics.Debug.WriteLine(GetType() + $".BorderTapped  sender:[{sender.GetType()}] args:[{e}] frame:[{frame}]");
                var content = $"frame:[{frame.X.ToString("0.##")}, {frame.Y.ToString("0.##")}, {frame.Width.ToString("0.##")}, {frame.Height.ToString("0.##")}]";
                _bubblePopup.Content = content;
                _BasePopup.Content = content;

                //Canvas.SetLeft(_popup, frame.X);
                //Canvas.SetTop(_popup, frame.Y);

                if (_BasePopup.Visibility == Visibility.Visible)
                {
                    await _BasePopup.PopAsync();
                    await _bubblePopup.PushAsync();
                }
                else
                {
                    if (_bubblePopup.Visibility == Visibility.Visible)
                    await _bubblePopup.PopAsync();
                    await _BasePopup.PushAsync();
                }
                */
                var frame = element.GetFrame();
                var content = $"frame:[{frame.X.ToString("0.##")}, {frame.Y.ToString("0.##")}, {frame.Width.ToString("0.##")}, {frame.Height.ToString("0.##")}]";

                _BasePopup.Margin = new Thickness(5);
                _BasePopup.Padding = new Thickness(10);
                _BasePopup.BorderThickness = new Thickness(1);
                _BasePopup.CornerRadius = new CornerRadius(4);
                _BasePopup.Background = new SolidColorBrush(Colors.White);
                _BasePopup.BorderBrush = new SolidColorBrush(Colors.Blue);

                _BasePopup.Content = new TextBlock { Text = content };

                //_BasePopup.Padding = new Thickness( _BasePopup.Padding.Left+1);

                if (_BasePopup.Parent is Grid grid)
                    grid.Children.Remove(_BasePopup);

                _lastHorizontalAlignment++;
                if (_lastHorizontalAlignment > HorizontalAlignment.Stretch)
                    _lastHorizontalAlignment = HorizontalAlignment.Left;

                _BasePopup.HorizontalAlignment = _lastHorizontalAlignment;
                _bubbleBorder.HorizontalAlignment = _lastHorizontalAlignment;

               await _BasePopup.PushAsync();
            }
        }


    }
}
