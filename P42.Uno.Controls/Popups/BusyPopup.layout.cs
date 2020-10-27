using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
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
                .Margin(10);

            _iconPresenter.Collapsed();

            _bubbleContentGrid.Children.Add(_progressRing);
        }
    }
}
