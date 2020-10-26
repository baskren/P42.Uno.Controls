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
        protected Grid _borderContent;

        void Build()
        {
            _progressRing = new ProgressRing()
                .RowSpan(2)
                .Width(40)
                .Height(40)
                .Margin(10);

            _titleBlock.Column(1);

            _contentPresenter.Row(1).Column(1);

            _border.Content = null;

            _borderContent = new Grid()
                .Rows(
                    GridLength.Auto, GridRowsColumns.Star
                )
                .Columns(
                    GridLength.Auto, GridRowsColumns.Star
                )
                .Children(
                    _progressRing,
                    _titleBlock,
                    _contentPresenter
                );
        }
    }
}
