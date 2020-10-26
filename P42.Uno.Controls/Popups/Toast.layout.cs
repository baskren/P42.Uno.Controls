using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    public partial class Toast : TargetedPopup
    {
        protected ContentPresenter _titleBlock;
        protected Grid _bubbleContentGrid;

        void Build()
        {
            _titleBlock = new ContentPresenter()
                .Row(0)
                .Bold()
                .TextWrapping(Windows.UI.Xaml.TextWrapping.WrapWholeWords)
                //.Bind(ContentPresenter.FontFamilyProperty, FontFamily)
                //.Bind(ContentPresenter.FontSizeProperty, FontSize)
                //.Bind(ContentPresenter.FontStretchProperty, FontStretch)
                //.Bind(ContentPresenter.FontStyleProperty, FontStyle)
                //.Bind(ContentPresenter.VisibilityProperty, TitleContent, convert: (object content) => (content != null).ToVisibility());
                ;

            _contentPresenter.Row(1).CenterVertical().Bind(ContentPresenter.ContentProperty, Message);

            PopupContent = _bubbleContentGrid = new Grid()
                .Rows(
                    GridRowsColumns.Auto,
                    GridRowsColumns.Star
                )
                .Children(
                    _titleBlock,
                    _contentPresenter
                );
        }
    }
}
