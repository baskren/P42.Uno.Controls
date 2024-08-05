

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class BubblePointerAdjustPage : Page
{
    public BubblePointerAdjustPage()
    {
        //this.InitializeComponent();
        Build();
    }

    private void _slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        _bubble.PointerAxialPosition = _slider.Value;
    }

    private void _borderThicknessSliderChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        _bubble.BorderThickness = new Thickness( _borderThicknessSlider.Value);
    }

}
