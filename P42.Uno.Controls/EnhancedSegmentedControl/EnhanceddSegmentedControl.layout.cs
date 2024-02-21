using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using P42.Uno.Markup;
using P42.Utils.Uno;

namespace P42.Uno.Controls;

[Bindable]
public partial class EnhancedSegmentedControl : SegmentedControl
{
    void Build()
    {
        var baseContent = Content as Grid;
        Content = new Grid()
            .Children
            (
                baseContent
                    .BindCollapsed(this, nameof(IsOverflowed)),
                new Grid()
                    .BindVisible(this, nameof(IsOverflowed))
                    .Columns('*', 'a')
                    .Bind(Grid.PaddingProperty, this, nameof(Padding))
                    .BindBorder<Grid>(this)
                    .AddTappedHandler(OnValueLabelTapped)
                    .Children
                    (
                        new TextBlock()
                            .Bind(TextBlock.HorizontalAlignmentProperty, this, nameof(HorizontalContentAlignment))
                            .Bind(TextBlock.VerticalAlignmentProperty, this, nameof(VerticalContentAlignment))
                            .BindTextProperties(this)
                            .BindHtml(this, nameof(SelectedLabel)),
                        new Button()
                            
                    )
            );
    }

    private void OnValueLabelTapped(object sender, TappedRoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}
