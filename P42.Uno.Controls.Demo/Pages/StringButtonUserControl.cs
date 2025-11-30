

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace P42.Uno.Controls.Demo;

public partial class StringButtonUserControl 
{
    #region Properties

    #region BindingContext Property
    public static readonly DependencyProperty BindingContextProperty = DependencyProperty.Register(
        nameof(BindingContext),
        typeof(object),
        typeof(StringButtonUserControl),
        new PropertyMetadata(default(object), (d, e) => ((StringButtonUserControl)d).OnBindingContextChanged(e))
    );
    protected virtual void OnBindingContextChanged(DependencyPropertyChangedEventArgs e)
    {
        //System.Diagnostics.Debug.WriteLine(GetType() + ".OnBindingContextChanged e.NewValue:" + e.NewValue);
        //System.Diagnostics.Debug.WriteLine("\t DataContext:" + DataContext );
    }
    public object BindingContext
    {
        get => GetValue(BindingContextProperty);
        set => SetValue(BindingContextProperty, value);
    }
    #endregion BindingContext Property


    #endregion

    public StringButtonUserControl()
    {
        //System.Diagnostics.Debug.WriteLine("StringButtonUserControl.ctr DataContext:" + DataContext);
        var x = RegisterPropertyChangedCallback(DataContextProperty, OnDataContextChanged);
        //this.InitializeComponent();
        Build();
    }




    private void OnDataContextChanged(DependencyObject sender, DependencyProperty dp)
    {
        // works in Uno but not UWP?!?!
        Debug.WriteLine(GetType() + ".OnDataContextChanged sender:" + sender + " dp:" + dp);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async void BorderTapped(object sender, TappedRoutedEventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        Debug.WriteLine(GetType() + ".BorderTapped: sender:" + sender + " e.PointerDeviceType:" + e.PointerDeviceType);
    }


    public void OnCellTapped()
    {
        Debug.WriteLine("StringButtonUserControl.");
    }
}
