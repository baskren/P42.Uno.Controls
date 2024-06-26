using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using P42.Uno.Markup;

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
                .Margin(10)
                .WBind(ProgressRing.IsActiveProperty, this, IsPushedProperty);

//#if NETSTANDARD
//            _progressRing.Collapsed();
//#endif

            _iconPresenter.Collapsed();

            _bubbleContentGrid.Children.Add(_progressRing);
        }
    }
}
