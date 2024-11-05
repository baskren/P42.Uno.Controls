using P42.Uno.Markup;
using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Text;

namespace P42.Uno.Controls
{
    public partial class Toast : TargetedPopup
    {
        protected readonly ContentPresenter _titleBlock = new();
        protected readonly Grid _bubbleContentGrid = new();
        protected readonly ContentPresenter _iconPresenter = new();
        protected readonly ContentPresenter _messageBlock = new();
        protected readonly ScrollViewer scrollViewer = new();

        void Build()
        {
            Content = _bubbleContentGrid
                .RowSpacing(3)
                .ColumnSpacing(0)
                .Margin(0)
                .Padding(0)
                .Rows('a', '*')
                .Columns('a', '*')
                .Children(
                    _iconPresenter
                        .RowSpan(2)
                        .Margin(3)
                        .Center()
                        .WBind(ContentPresenter.ContentProperty, this, IconElementProperty)
                        .WBindNullCollapse(),

                    _titleBlock
                        .RowCol(0,1)
                        .CenterVertical()
                        .VerticalContentAlignment(VerticalAlignment.Center)
                        .TextWrapping(Microsoft.UI.Xaml.TextWrapping.WrapWholeWords)
                        .WBindFont(this, except: nameof(FontWeight))
                        .FontWeight(FontWeights.Bold)
                        .WBindNullCollapse()
                        .WBind(ContentPresenter.BackgroundProperty, this, TitleBackgroundProperty)
                        .WBind(ContentPresenter.PaddingProperty, this, PaddingProperty)
                        ,

                    scrollViewer
                        .RowCol(1,1)
                        .MaxHeight(300)
                        .Content(
                            _messageBlock
                                .RowCol(1,1)
                                .CenterVertical()
                                .VerticalContentAlignment(VerticalAlignment.Center)
                                .TextWrapping(Microsoft.UI.Xaml.TextWrapping.WrapWholeWords)
                                .WBindFont(this)
                                .WBindNullCollapse()
                                .WBind(ContentPresenter.PaddingProperty, this, PaddingProperty)
                        )

                //.Bind(ScrollViewer.MaxHeightProperty, _contentRowDefinition, nameof(ActualHeight))
                );
            //Padding = new Microsoft.UI.Xaml.Thickness(5);

            this.RegisterPropertyChangedCallback(PaddingProperty, OnPaddingPropertyChanged);

        }


        private void OnPaddingPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            _iconPresenter.Margin(Math.Min(Padding.Left, _bubbleContentGrid.RowSpacing), Math.Min(Padding.Top, _bubbleContentGrid.RowSpacing), Math.Min(Padding.Left, _bubbleContentGrid.RowSpacing), Math.Min(Padding.Bottom, _bubbleContentGrid.RowSpacing));
        }
    }
}
