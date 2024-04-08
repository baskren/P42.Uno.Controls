using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using Windows.UI;
using Windows.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Text;

namespace P42.Uno.Controls
{
    public partial class Toast : TargetedPopup
    {
        protected ContentPresenter _titleBlock;
        protected Grid _bubbleContentGrid;
        protected ContentPresenter _iconPresenter;
        protected ContentPresenter _messageBlock;
        protected ScrollViewer scrollViewer;

        void Build()
        {
            Content = _bubbleContentGrid = new Grid()
                .RowSpacing(3)
                .ColumnSpacing(0)
                .Margin(0)
                .Padding(0)
                .Rows('a', '*')
                .Columns('a', '*')
                .Children(
                    new ContentPresenter()
                        .Assign(out _iconPresenter)
                        .RowSpan(2)
                        .Margin(3)
                        .Center()
                        .WBind(ContentPresenter.ContentProperty, this, IconElementProperty)
                        .WBindNullCollapse(),

                    new ContentPresenter()
                        .Assign(out _titleBlock)
                        .RowCol(0,1)
                        .CenterVertical()
                        .VerticalContentAlignment(VerticalAlignment.Center)
                        .TextWrapping(Microsoft.UI.Xaml.TextWrapping.WrapWholeWords)
                        .WBindFont(this, except: nameof(FontWeight))
                        .FontWeight(FontWeights.Bold)
                        .WBindNullCollapse()
                        ,

                    new ScrollViewer()
                        .RowCol(1,1)
                        .Assign(out scrollViewer)
                        .MaxHeight(300)
                        .Content(
                        new ContentPresenter()
                            .Assign(out _messageBlock)
                            .RowCol(1,1)
                            .CenterVertical()
                            .VerticalContentAlignment(VerticalAlignment.Center)
                            .TextWrapping(Microsoft.UI.Xaml.TextWrapping.WrapWholeWords)
                            .WBindFont(this)
                            .WBindNullCollapse()
                        )
                        //.Bind(ScrollViewer.MaxHeightProperty, _contentRowDefinition, nameof(ActualHeight))
                );
            Padding = new Microsoft.UI.Xaml.Thickness(5);

            this.RegisterPropertyChangedCallback(PaddingProperty, OnPaddingPropertyChanged);

        }


        private void OnPaddingPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            _iconPresenter.Margin(Math.Min(Padding.Left, _bubbleContentGrid.RowSpacing), Math.Min(Padding.Top, _bubbleContentGrid.RowSpacing), Math.Min(Padding.Left, _bubbleContentGrid.RowSpacing), Math.Min(Padding.Bottom, _bubbleContentGrid.RowSpacing));
        }
    }
}
