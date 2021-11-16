using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    public partial class FlyoutMenu : TargetedPopup
    {
        SimpleListView _listView;

        void Build()
        {
            XamlContent = new SimpleListView()
                .Assign(out _listView)
                .ItemTemplate(typeof(FlyoutMenuItemTemplate).AsDataTemplate())
                .Bind(SimpleListView.ItemsSourceProperty, this, nameof(Items));
        }
    }

    [Windows.UI.Xaml.Data.Bindable]
    [System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    internal partial class FlyoutMenuItemTemplate : Grid
    {
        public FlyoutMenuItemTemplate()
        {
#if WINDOWS_UWP
            DataContextChanged += FlyoutMenuItemTemplate_DataContextChanged;
#endif
        }


#if WINDOWS_UWP
        private void FlyoutMenuItemTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var value = args.NewValue;
#else
        protected override void OnDataContextChanged(DependencyPropertyChangedEventArgs e)
        {
            var value = e.NewValue;
#endif
            Children.Clear();
            if (value is MenuFlyoutItemBase item)
                Children.Add(item);
        }
    }
}