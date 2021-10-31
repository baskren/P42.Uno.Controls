using System;
using P42.Uno.Markup;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls.Test
{
    public partial class TypeCell : Grid
    {
        TextBlock _textBlock = new TextBlock
        {
            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch,
            HorizontalTextAlignment = Windows.UI.Xaml.TextAlignment.Left,
            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch
        };

        public TypeCell()
        {
            Children.Add(_textBlock);
            DataContextChanged += OnDataContextChanged;
        }

#if WINDOWS_UWP
        private void OnDataContextChanged(Windows.UI.Xaml.FrameworkElement sender, Windows.UI.Xaml.DataContextChangedEventArgs args)
#else
        private void OnDataContextChanged(Windows.UI.Xaml.DependencyObject sender, Windows.UI.Xaml.DataContextChangedEventArgs args)
#endif
        {
            if (DataContext is Type type)
                _textBlock.Text = type.Namespace + "." + type.Name;
        }
    }
}