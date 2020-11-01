using P42.Uno.Markup;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    public partial class Toast : TargetedPopup
    {
        protected ContentPresenter _titleBlock;
        protected Grid _bubbleContentGrid;
        protected ContentPresenter _iconPresenter;
        protected ContentPresenter _messageBlock;

        void Build()
        {
            PopupContent = _bubbleContentGrid = new Grid()
                .RowSpacing(3)
                .Rows(
                    GridRowsColumns.Auto,
                    GridRowsColumns.Star
                )
                .Columns(GridRowsColumns.Auto, GridRowsColumns.Star)
                .Children(
                    new ContentPresenter()
                        .Assign(out _iconPresenter)
                        .RowSpan(2)
                        .Margin(10)
                        .Bind(ContentPresenter.ContentProperty, this, nameof(IconElement))
                        .BindNullCollapse(),

                    new ContentPresenter()
                        .Assign(out _titleBlock)
                        .Row(0)
                        .Column(1)
                        .Bold()
                        .TextWrapping(Windows.UI.Xaml.TextWrapping.WrapWholeWords)
                        .Bind(ContentPresenter.FontFamilyProperty, this, nameof(FontFamily))
                        .Bind(ContentPresenter.FontSizeProperty, this, nameof(FontSize))
                        .Bind(ContentPresenter.FontStretchProperty, this, nameof(FontStretch))
                        .Bind(ContentPresenter.FontStyleProperty, this, nameof(FontStyle))
                        .Bind(ContentPresenter.ContentProperty, this, nameof(TitleContent))
                        .BindNullCollapse(),

                    new ContentPresenter()
                        .Assign(out _messageBlock)
                        .Row(1)
                        .Column(1)
                        .TextWrapping(Windows.UI.Xaml.TextWrapping.WrapWholeWords)
                        .Bind(ContentPresenter.FontFamilyProperty, this, nameof(FontFamily))
                        .Bind(ContentPresenter.FontSizeProperty, this, nameof(FontSize))
                        .Bind(ContentPresenter.FontStretchProperty, this, nameof(FontStretch))
                        .Bind(ContentPresenter.FontStyleProperty, this, nameof(FontStyle))
                        .Bind(ContentPresenter.FontWeightProperty, this, nameof(FontWeight))
                        .Bind(ContentPresenter.ContentProperty, this, nameof(Message))
                        .BindNullCollapse()

                );
        }
    }
}
