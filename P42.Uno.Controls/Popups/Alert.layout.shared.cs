using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using P42.Uno.Markup;

namespace P42.Uno.Controls
{
    public partial class Alert : Toast
    {
        protected Button _okButton;
        protected ContentPresenter _okButtonContentPresenter;

        void Build()
        {
            Width = 300;

            new Button()
                .Assign(out _okButton)
                .Row(2)
                .Column(1)
                .Margin(0)
                .Stretch()
                .CornerRadius(2)
                .Height(40)
                .Style(StaticResources.TryGetAs<Style>(Application.Current.Resources, "AccentButtonStyle"))
                .WBind(Button.ContentProperty, this, OkButtonContentProperty);

            _bubbleContentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            _bubbleContentGrid.Children.Add(_okButton);

            this.DisableAlternativeCancel(); 
        }

    }
}
