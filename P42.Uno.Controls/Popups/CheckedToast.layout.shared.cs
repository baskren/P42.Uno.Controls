using Microsoft.UI.Xaml.Controls;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Microsoft.UI.Xaml;
using System;

namespace P42.Uno.Controls
{
    public partial class CheckedToast : Alert
    {
        protected CheckBox _checkBox;

        void Build()
        {
            _bubbleContentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            _okButton.Row(3);

            _checkBox = new CheckBox()
                .Row(2).Column(1).Margin(0)
                .StretchHorizontal()
                .HorizontalContentAlignment(HorizontalAlignment.Left)
                .Bind(CheckBox.IsCheckedProperty, this, nameof(IsChecked), Microsoft.UI.Xaml.Data.BindingMode.TwoWay)
                .Bind(CheckBox.ContentProperty, this, nameof(CheckContent));

            _bubbleContentGrid.Children.Add(_checkBox);

            this.DisableAlternativeCancel(false);
        }

    }
}