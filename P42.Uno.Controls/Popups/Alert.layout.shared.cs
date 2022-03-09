using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using P42.Uno.Markup;
using P42.Utils.Uno;

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
                .StretchHorizontal()
                .CornerRadius(2)
                .Height(40)
                .Bind(Button.ContentProperty, this, nameof(OkButtonContent))
                .Bind(Button.BackgroundProperty, this, nameof(OkButtonBackground))
                .Bind(Button.ForegroundProperty, this, nameof(OkButtonForeground));

            _bubbleContentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            _bubbleContentGrid.Children.Add(_okButton);
        }

    }
}
