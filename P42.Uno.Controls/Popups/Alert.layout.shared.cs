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
            new Button()
                .Assign(out _okButton)
                .Row(2)
                .Column(1)
                .StretchHorizontal()
                .CornerRadius(2)
                .Bind(BackgroundProperty, this, nameof(OkButtonBackground))
                .Bind(ForegroundProperty, this, nameof(OkButtonForeground));
                //.Bind(Button.ContentProperty, this, nameof(OkButtonContent));

            _bubbleContentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            _bubbleContentGrid.Children.Add(_okButton);
        }

    }
}
