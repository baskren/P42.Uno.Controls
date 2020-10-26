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
                .Bind(ContentPresenter.FontFamilyProperty, this, nameof(FontFamily))
                .Bind(ContentPresenter.FontSizeProperty, this, nameof(FontSize))
                .Bind(ContentPresenter.FontStretchProperty, this, nameof(FontStretch))
                .Bind(ContentPresenter.FontStyleProperty, this, nameof(FontStyle))
                .Bind(ContentPresenter.FontWeightProperty, this, nameof(FontWeight))
                .Bind(ContentPresenter.ForegroundProperty, this, nameof(OkButtonForeground))
                .Bind(ContentPresenter.ContentProperty, this, nameof(OkButtonContent));

            _okButton = new Button()
                .Row(2)
                .StretchHorizontal()
                .CornerRadius(2)
                .Bind(BackgroundProperty, this, nameof(OkButtonBackground))
                .Bind(ForegroundProperty, this, nameof(OkButtonBackground))
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
