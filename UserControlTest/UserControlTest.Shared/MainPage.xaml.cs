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
        TargetedPopup _TargetedPopup = new TargetedPopup
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

        VerticalAlignment _lastAlignment = VerticalAlignment.Stretch;
        async void _button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                var frame = element.GetBounds();
                var content = $"frame:[{frame.X.ToString("0.##")}, {frame.Y.ToString("0.##")}, {frame.Width.ToString("0.##")}, {frame.Height.ToString("0.##")}]";

                _TargetedPopup.Target = _altBorder;
                _TargetedPopup.PreferredPointerDirection = PointerDirection.None;

                _TargetedPopup.VerticalAlignment = VerticalAlignment.Center;

                _TargetedPopup.Margin = new Thickness(5);
                _TargetedPopup.Padding = new Thickness(10);
                _TargetedPopup.BorderThickness = new Thickness(1);
                _TargetedPopup.CornerRadius = new CornerRadius(4);
                _TargetedPopup.Background = new SolidColorBrush(Colors.White);
                _TargetedPopup.BorderBrush = new SolidColorBrush(Colors.Blue);

                _TargetedPopup.Content = new TextBlock { Text = content };

                if (_TargetedPopup.Parent is Grid grid)
                    grid.Children.Remove(_TargetedPopup);

                _lastAlignment++;
                if (_lastAlignment > VerticalAlignment.Stretch)
                    _lastAlignment = VerticalAlignment.Top;

                _TargetedPopup.HorizontalAlignment = (HorizontalAlignment) _lastAlignment;
                _bubbleBorder.HorizontalAlignment = (HorizontalAlignment)_lastAlignment;

               await _TargetedPopup.PushAsync();
            }
        }


    }
}
