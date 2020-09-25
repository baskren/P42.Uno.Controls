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

        private void OnAltBorderTapped(object sender, TappedRoutedEventArgs e)
        {
            BorderTapped(sender, e);
        }

        private void BorderTapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is UIElement element)
            {
                var point = ScreenCoorder(element);
                System.Diagnostics.Debug.WriteLine(GetType() + $".BorderTapped  sender:[{sender.GetType()}] args:[{e}] point:[{point}]");
                _popup.Content = $"point:[{point}]";

                Canvas.SetLeft(_popup, point.X);
                Canvas.SetTop(_popup, point.Y);
            }
        }

        private void OnCanvasTapped(object sender, TappedRoutedEventArgs e)
        {
            _button_Click(null, null);
        }

        private void OnContentControlTapped(object sender, TappedRoutedEventArgs e)
        {
            _button_Click(null, null);
        }

        bool _collapsing;
        bool _appearing;
        private void _button_Click(object sender, RoutedEventArgs e)
        {
            if (_popup.Parent is Grid grid)
                Grid.SetRowSpan(_popup, grid.RowDefinitions.Count);

            if (_collapsing || _appearing)
                return;

            //var location = _popup.ActualOffset;
            var location = ScreenCoorder(_popup);
            System.Diagnostics.Debug.WriteLine(GetType() + ". Visibility=" + _popup.Visibility + "  x:" + location.X + " y:" + location.Y);
            _popup.Content = ". Visibility=" + _popup.Visibility + "  x:" + location.X + " y:" + location.Y;
            if (_popup.Visibility == Visibility.Visible)
            {
                /*
                _collapsing = true;
                VisualStateManager.GoToState(_popup, "Collapsed", true);
                DispatcherTimerSetup(TimeSpan.FromSeconds(0.2), () => 
                {
                    _popup.Visibility = Visibility.Collapsed;
                    _collapsing = false;
                    return false;
                });
                */
                _popup.Visibility = Visibility.Collapsed;

            }
            else
            {
                //_appearing = true;
                _popup.Visibility = Visibility.Visible;
                /*
                VisualStateManager.GoToState(_popup, "Visible", true);
                DispatcherTimerSetup(TimeSpan.FromSeconds(0.2), () =>
                {
                    _appearing = false;
                   
                    //DispatcherTimerSetup(TimeSpan.FromSeconds(2), () => 
                    //{
                    //    _button_Click(sender, e);
                    //    return false;
                    //});
                    
                    return false;
                });
                */
            }

        }

        DispatcherTimer dispatcherTimer;

        Func<bool> Func;
        public void DispatcherTimerSetup(TimeSpan span, Func<bool> func)
        {
            Func = func;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //IsEnabled defaults to false
            dispatcherTimer.Start();
            //IsEnabled should now be true after calling start
        }

        void DispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            //Time since last tick should be very very close to Interval
            if (!Func.Invoke())
            {
                dispatcherTimer.Stop();
            }
        }

        Point ScreenCoorder(UIElement element)
        {
            var ttv = element.TransformToVisual(Windows.UI.Xaml.Window.Current.Content);
            var screenCoords = ttv.TransformPoint(new Point(0, 0));
            return screenCoords;
        }
    }
}
