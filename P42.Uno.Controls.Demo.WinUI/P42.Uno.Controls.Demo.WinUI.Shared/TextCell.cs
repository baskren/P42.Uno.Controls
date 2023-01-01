using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace P42.Uno.Controls.Demo
{
    [Bindable]
    public partial class TextCell : Grid
    {
        TextBlock _valueLabel;
        TextBlock _titleLabel;
        public  TextCell()
        {
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            _titleLabel = new TextBlock();
            Children.Add(_titleLabel);

            _valueLabel = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            Grid.SetRow(_valueLabel, 1);
            Children.Add(_valueLabel);

            DataContextChanged += TextCell_DataContextChanged;
            Tapped += TextCell_Tapped;
        }

        async void TextCell_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            await ListEditPage.Current.OnCellClicked(_valueLabel);
        }

#if NET7_0_WINDOWS10_0_19041_0
        private void TextCell_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
#else
        private void TextCell_DataContextChanged(DependencyObject sender, DataContextChangedEventArgs args)
#endif
        {
            System.Diagnostics.Debug.WriteLine("TextCell.DataContextChanged!!!");
            if (DataContext is string value)
            {
                _titleLabel.Text = value ?? string.Empty;
                _valueLabel.Text = value ?? string.Empty;
            }
            else if (DataContext is Type type)
            {
                _titleLabel.Text = type.FullName ?? string.Empty;
                _valueLabel.Text = type.Name ?? string.Empty;
            }
            else
            {
                _titleLabel.Text = "Huh?";
                _valueLabel.Text = "doh!";
            }
        }
        
        /*
        public void ChangeValue(string newValue)
        {
            _valueLabel.Text = newValue ?? string.Empty;
        }
        */
    }
}
