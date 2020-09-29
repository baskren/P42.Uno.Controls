using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UserControlTest.Popups
{
    public sealed partial class UwpPopup : UserControl
    {
        public UwpPopup()
        {
            this.InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(UwpPopup),
                        new PropertyMetadata("", OnTextChanged));


        public void OpenPopup()
        {
            this.ParentPopup.IsOpen = true;
        }

        public void ClosePopup()
        {
            this.ParentPopup.IsOpen = false;
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as UwpPopup;
            var newValue = e.NewValue as string;
            if (instance != null && newValue != null)
            {
                instance.CustomTextBlock.Text = newValue;
            }
        }

        private void OnPopupLoaded(object sender, RoutedEventArgs e)
        {
            this.ParentPopup.HorizontalOffset = (Window.Current.Bounds.Width - gdChild.ActualWidth) / 2;
            this.ParentPopup.VerticalOffset = (Window.Current.Bounds.Height - gdChild.ActualHeight) / 2;
        }
    }
}
