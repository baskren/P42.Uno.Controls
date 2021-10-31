using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
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
            Content = _bubbleContentGrid = new Grid()
                .RowSpacing(3)
                .ColumnSpacing(0)
                .Margin(0)
                .Padding(0)
                .Rows(
                    GridRowsColumns.Auto,
                    GridRowsColumns.Star
                )
                .Columns(GridRowsColumns.Auto, GridRowsColumns.Star)
                .Children(
                    new ContentPresenter()
                        .Assign(out _iconPresenter)
                        .RowSpan(2)
                        .Margin(3)
                        .Center()
                        //.Bind(ContentPresenter.ContentProperty, this, nameof(IconElement))
                        .BindNullCollapse(),

                    new ContentPresenter()
                        .Assign(out _titleBlock)
                        .Row(0)
                        .Column(1)
                        .TextWrapping(Windows.UI.Xaml.TextWrapping.WrapWholeWords)
                        //.FontFamily("ms-appx:///Assets/Fonts/Ubuntu-Bold.ttf#Ubuntu-Bold")
                        .BindFont(this, except: nameof(FontWeight))
                        .FontWeight(FontWeights.Bold)
                        .BindNullCollapse(),

                    //new ScrollViewer()
                    //    .RowCol(1,1)
                    //    .Content(
                        new ContentPresenter()
                            .Assign(out _messageBlock)
                            .RowCol(1,1)
                            .TextWrapping(Windows.UI.Xaml.TextWrapping.WrapWholeWords)
                            .BindFont(this)
                            .BindNullCollapse()
                    //    )
                );
            Padding = new Windows.UI.Xaml.Thickness(5);

            this.RegisterPropertyChangedCallback(PaddingProperty, OnPaddingPropertyChanged);
        }

        private void OnPaddingPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            _iconPresenter.Margin(Math.Min(Padding.Left, _bubbleContentGrid.RowSpacing), Math.Min(Padding.Top, _bubbleContentGrid.RowSpacing), Math.Min(Padding.Left, _bubbleContentGrid.RowSpacing), Math.Min(Padding.Bottom, _bubbleContentGrid.RowSpacing));
        }
    }
}
