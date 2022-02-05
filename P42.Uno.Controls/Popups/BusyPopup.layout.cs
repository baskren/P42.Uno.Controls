using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using P42.Uno.Markup;
#if __WASM__
using ProgressRing = P42.Uno.Controls.Spinner;
#endif

namespace P42.Uno.Controls
{
    public partial class BusyPopup : Toast
    {
        protected ProgressRing _progressRing;

        void Build()
        {
            _progressRing = new ProgressRing()
                .RowSpan(2)
                .Width(40)
                .Height(40)
                .Margin(10);

//#if NETSTANDARD
//            _progressRing.Collapsed();
//#endif

            _iconPresenter.Collapsed();

            _bubbleContentGrid.Children.Add(_progressRing);
        }
    }
}
