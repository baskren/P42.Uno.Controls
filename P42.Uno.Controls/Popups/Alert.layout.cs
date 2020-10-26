using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using P42.Uno.Markup;

namespace P42.Uno.Controls
{
    public partial class Alert : Toast
    {
        protected Button _okButton;
        protected ContentPresenter _okButtonContentPresenter;

        void Build()
        {
            _okButtonContentPresenter = new ContentPresenter()
                .TextWrapping(TextWrapping.WrapWholeWords)
                .Bind(ContentPresenter.FontFamilyProperty, FontFamily)
                .Bind(ContentPresenter.FontSizeProperty, FontSize)
                .Bind(ContentPresenter.FontStretchProperty, FontStretch)
                .Bind(ContentPresenter.FontStyleProperty, FontStyle)
                .Bind(ContentPresenter.FontWeightProperty, FontWeight)
                .Bind(ContentPresenter.ForegroundProperty, OkButtonForeground)
                .Bind(ContentPresenter.ContentProperty, OkButtonContent);

            _okButton = new Button()
                .Row(2)
                .StretchHorizontal()
                .CornerRadius(2)
                .Bind(BackgroundProperty, OkButtonBackground)
                .Bind(ForegroundProperty, OkButtonBackground)
                .Content(_okButtonContentPresenter);

            _bubbleContentGrid
                .Rows(
                    GridLength.Auto,
                    GridRowsColumns.Star,
                    GridLength.Auto
                )
                .Children(
                    _titleBlock,
                    _contentPresenter,
                    _okButton
                );
        }

    }
}
