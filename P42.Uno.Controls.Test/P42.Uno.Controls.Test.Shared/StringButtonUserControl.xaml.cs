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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace P42.Uno.Controls.Test
{
    public partial class StringButtonUserControl : UserControl
    {
        #region Properties

        #region BindingContext Property
        public static readonly DependencyProperty BindingContextProperty = DependencyProperty.Register(
            nameof(BindingContext),
            typeof(object),
            typeof(StringButtonUserControl),
            new PropertyMetadata(default(object), new PropertyChangedCallback((d, e) => ((StringButtonUserControl)d).OnBindingContextChanged(e)))
        );
        protected virtual void OnBindingContextChanged(DependencyPropertyChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".OnBindingContextChanged e.NewValue:" + e.NewValue);
            //System.Diagnostics.Debug.WriteLine("\t DataContext:" + DataContext );
        }
        public object BindingContext
        {
            get => (object)GetValue(BindingContextProperty);
            set => SetValue(BindingContextProperty, value);
        }
        #endregion BindingContext Property


        #endregion

        public StringButtonUserControl()
        {
            //System.Diagnostics.Debug.WriteLine("StringButtonUserControl.ctr DataContext:" + DataContext);
            var x = this.RegisterPropertyChangedCallback(DataContextProperty, OnDataContextChanged);
            this.InitializeComponent();
        }

        private void OnContentChanged(DependencyObject sender, DependencyProperty dp)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".OnContentChanged sender:" + sender + " dp:" + dp);
        }

#if !NETFX_CORE
        protected override void OnContentChanged(object oldValue, object newValue)
        {
            base.OnContentChanged(oldValue, newValue);
        }
#else
#endif


        private void OnDataContextChanged(DependencyObject sender, DependencyProperty dp)
        {
            // works in Uno but not UWP?!?!
            //System.Diagnostics.Debug.WriteLine(GetType() + ".OnDataContextChanged sender:" + sender + " dp:" + dp);
        }

        async void BorderTapped(object sender, TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(GetType() + ".BorderTapped: sender:" + sender + " e.PointerDeviceType:" + e.PointerDeviceType);

        }


        public void CellTapped()
        {
            //System.Diagnostics.Debug.WriteLine("StringButtonUserControl.");
        }
    }
}
