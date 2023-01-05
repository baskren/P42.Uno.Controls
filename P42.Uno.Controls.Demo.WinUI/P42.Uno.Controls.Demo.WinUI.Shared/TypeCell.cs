using System;
using P42.Uno.Markup;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace P42.Uno.Controls.Demo
{
    [Bindable]
    public partial class TypeCell : Grid
    {
        TextBlock _textBlock = new TextBlock
        {
            HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch,
            HorizontalTextAlignment = Microsoft.UI.Xaml.TextAlignment.Left,
            VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Stretch
        };

        public TypeCell()
        {
            Children.Add(_textBlock);
            DataContextChanged += OnDataContextChanged;
        }

#if !HAS_UNO

        private void OnDataContextChanged(Microsoft.UI.Xaml.FrameworkElement sender, Microsoft.UI.Xaml.DataContextChangedEventArgs args)
#else
        private void OnDataContextChanged(Microsoft.UI.Xaml.DependencyObject sender, Microsoft.UI.Xaml.DataContextChangedEventArgs args)
#endif
        {
            if (DataContext is Type type)
                _textBlock.Text = type.Namespace + "." + type.Name;
        }
    }
}