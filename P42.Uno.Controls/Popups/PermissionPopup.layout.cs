using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using P42.Uno.Markup;

namespace P42.Uno.Controls
{
    public partial class PermissionPopup : Alert
    {
        protected Button _cancelButton;
        protected ContentPresenter _cancelButtonContentPresenter;
        protected Grid _buttonBar;

        void Build()
        {
            _cancelButtonContentPresenter = new ContentPresenter()
                .TextWrapping(TextWrapping.WrapWholeWords)
                .Bind(ContentPresenter.FontFamilyProperty, this, nameof(FontFamily))
                .Bind(ContentPresenter.FontSizeProperty, this, nameof(FontSize))
                .Bind(ContentPresenter.FontStretchProperty, this, nameof(FontStretch))
                .Bind(ContentPresenter.FontStyleProperty, this, nameof(FontStyle))
                .Bind(ContentPresenter.FontWeightProperty, this, nameof(FontWeight))
                .Bind(ContentPresenter.ForegroundProperty, this, nameof(CancelButtonForeground))
                .Bind(ContentPresenter.ContentProperty, this, nameof(CancelButtonContent));

            _cancelButton = new Button()
                .Column(1)
                .StretchHorizontal()
                .CornerRadius(2)
                .Bind(ContentPresenter.ForegroundProperty, this, nameof(CancelButtonForeground))
                .Bind(ContentPresenter.BackgroundProperty, this, nameof(CancelButtonBackground));

            _bubbleContentGrid.Children.Remove(_okButton);

            _okButton.Row(0);

            _buttonBar = new Grid()
                .Row(2)
                .ColumnSpacing(5)
                .Columns(
                    GridRowsColumns.Star, GridRowsColumns.Star
                )
                .Children(
                    _okButton, _cancelButton
                );

            _bubbleContentGrid.Children.Add(_buttonBar);
        }
    }
}
