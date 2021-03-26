using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#if __WASM__
using Uno.UI.Runtime.WebAssembly;
using Uno.Foundation;

namespace P42.Uno.Controls
{
    [Windows.UI.Xaml.Data.Bindable]
    [System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    [HtmlElement("img")]
    public partial class Spinner : Control
    {
        static readonly string PackageLocation;

        static Spinner()
        {
            PackageLocation = WebAssemblyRuntime.InvokeJS("window.scriptDirectory");
        }

#region IsActive Property
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            nameof(IsActive),
            typeof(bool),
            typeof(Spinner),
            new PropertyMetadata(default(bool))
        );
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }
#endregion IsActive Property



        public Spinner()
        {
            Width = 100;
            Height = 100;
            this.SetHtmlAttribute("src", $"{PackageLocation}Assets/loading_apple.gif");
        }
    }
}
#else
namespace P42.Uno.Controls
{
    public partial class Spinner : FrameworkElement
    {

    }
}
#endif
