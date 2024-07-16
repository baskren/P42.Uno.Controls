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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace P42.Uno.Controls.Demo;

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
        //this.InitializeComponent();
        Build();
    }




    private void OnDataContextChanged(DependencyObject sender, DependencyProperty dp)
    {
        // works in Uno but not UWP?!?!
        //System.Diagnostics.Debug.WriteLine(GetType() + ".OnDataContextChanged sender:" + sender + " dp:" + dp);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    async void BorderTapped(object sender, TappedRoutedEventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        System.Diagnostics.Debug.WriteLine(GetType() + ".BorderTapped: sender:" + sender + " e.PointerDeviceType:" + e.PointerDeviceType);

    }


    public void CellTapped()
    {
        //System.Diagnostics.Debug.WriteLine("StringButtonUserControl.");
    }
}
