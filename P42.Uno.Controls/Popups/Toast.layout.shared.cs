using P42.Uno.Markup;
using P42.Utils.Uno;
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
            Padding = new Windows.UI.Xaml.Thickness(5);
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
                        //.Bind(ContentPresenter.ContentProperty, this, nameof(IconElement))
                        .BindNullCollapse(),

                    new ContentPresenter()
                        .Assign(out _titleBlock)
                        .Row(0)
                        .Column(1)
                        .TextWrapping(Windows.UI.Xaml.TextWrapping.WrapWholeWords)
                        .FontFamily("ms-appx:///Assets/Fonts/Ubuntu-Bold.ttf#Ubuntu-Bold")
                        //.BindFont(this, except: nameof(FontWeight))
                        .BindNullCollapse(),

                    new ScrollViewer()
                        .RowCol(1,1)
                        .Content(
                    new ContentPresenter()
                        .Assign(out _messageBlock)
                        .TextWrapping(Windows.UI.Xaml.TextWrapping.WrapWholeWords)
                        .BindFont(this)
                        .BindNullCollapse()
                        )
                );
        }
    }
}
