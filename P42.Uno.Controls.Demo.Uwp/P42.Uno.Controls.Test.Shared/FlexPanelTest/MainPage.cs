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
using Windows.UI;
using P42.Uno.AsyncNavigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            /*
            this.InitializeComponent();

            _wrapCombo.ItemsSource = Enum.GetNames(typeof(FlexWrap));
            _wrapCombo.SelectedItem = FlexWrap.NoWrap.ToString();

            _positionCombo.ItemsSource = Enum.GetNames(typeof(FlexPosition));
            _positionCombo.SelectedItem = FlexPosition.Relative.ToString();

            _alignItemsCombo.ItemsSource = Enum.GetNames(typeof(FlexAlignItems));
            _alignItemsCombo.SelectedItem = FlexAlignItems.Stretch.ToString();

            _alignContentCombo.ItemsSource = Enum.GetNames(typeof(FlexAlignContent));
            _alignContentCombo.SelectedItem = FlexAlignContent.Stretch.ToString();

            _justifyContentCombo.ItemsSource = Enum.GetNames(typeof(FlexJustify));
            _justifyContentCombo.SelectedItem = FlexJustify.Start.ToString();

            _dirCombo.ItemsSource = Enum.GetNames(typeof(FlexDirection));
            _dirCombo.SelectedItem = FlexDirection.Row.ToString();
            */

           // Frame rootFrame = Microsoft.UI.Xaml.Window.Current.Content as Frame;
            //rootFrame.Navigate(typeof(FlexDemoHomePage));
            this.PushAsync(new FlexDemoHomePage());
        }
        
    }
}
