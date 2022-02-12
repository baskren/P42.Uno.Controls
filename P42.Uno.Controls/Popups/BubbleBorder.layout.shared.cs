using System;
using System.IO;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using SkiaSharp;
using Windows.UI;
using P42.Utils.Uno;
using Uno.UI.Toolkit;
using P42.Uno.Markup;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace P42.Uno.Controls
{
    public partial class BubbleBorder
    {
        //Grid _innerContent;

        void Build()
        {
            this
                .Left()
                .Top()
                .Background(SystemColors.ChromeMedium)
                .BorderBrush(SystemColors.BaseLow)
                .BorderThickness(2)
                .FontSize(16)
                .Foreground(SystemColors.BaseHigh);

            _path = new Windows.UI.Xaml.Shapes.Path();
            _path.Bind(Windows.UI.Xaml.Shapes.Path.FillProperty, this, nameof(Background));
            _path.Bind(Windows.UI.Xaml.Shapes.Path.StrokeProperty, this, nameof(BorderBrush));

            _contentPresenter = new ContentPresenter
            {
                TextWrapping = TextWrapping.WrapWholeWords
            }
                //.Bind(ContentPresenter.MarginProperty, ContentPresenterMargin)
                .Bind(ContentPresenter.HorizontalContentAlignmentProperty, this, nameof(HorizontalContentAlignment))
                .Bind(ContentPresenter.VerticalContentAlignmentProperty, this, nameof(VerticalContentAlignment))
                .BindFont(this);

            base.Content = new Grid()
                .ColumnSpacing(0)
                .RowSpacing(0)
                .AddOnSizeChanged(OnSizeChanged)
                //.Background(Colors.Blue)
                .Children
                (
                    //new ElevatedView()
                    //{
                    //    ElevatedContent = _path,
                    //    Elevation = 10
                    //}
                    //    .Assign(out _dropShadow),
                    //new Windows.UI.Xaml.Shapes.Rectangle { Fill=Colors.Pink.ToBrush()}.Stretch(),
                    _path,
                    _contentPresenter
                )
                .Bind(Grid.MarginProperty, this, nameof(Margin));

        }

    }
}