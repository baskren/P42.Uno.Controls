

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
[Microsoft.UI.Xaml.Data.Bindable]
public sealed partial class BubblePointerAdjustPage : Page
{
    private readonly BubbleBorder _bubble = new();
    private readonly Slider _slider = new();
    private readonly Slider _borderThicknessSlider = new();

    void Build()
    {
        Content = new Grid()
            .Rows("*", 30, 30)
            .Children
            (
                _bubble
                    .Stretch()
                    .Background(Microsoft.UI.Colors.Blue)
                    .BorderBrush(Microsoft.UI.Colors.Green)
                    .BorderThickness(5)
                    .CornerRadius(5)
                    .PointerDown()
                    .PointerLength(20)
                    .PointerTipRadius(5)
                    .Content(new TextBlock { Text = "THIS IS THE CONTENT " }),
                _slider
                    .Row(1)
                    .MinMaxStep(0, 1, 0.001)
                    .Value(0.5)
                    .AddValueChangedHandler(_slider_ValueChanged),
                _borderThicknessSlider
                    .Row(2)
                    .MinMaxStep(0, 20, 1)
                    .AddValueChangedHandler(_borderThicknessSliderChanged)
                    .Value(1)
            );
    }
}
