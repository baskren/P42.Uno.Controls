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
            _bubbleContentGrid.Children.Remove(_okButton);
            _okButton.Row(0).Column(0);

            new Button()
                .Assign(out _cancelButton)
                .Column(1)
                .StretchHorizontal()
                .CornerRadius(2)
                .Bind(Button.ForegroundProperty, this, nameof(CancelButtonForeground))
                .Bind(Button.BackgroundProperty, this, nameof(CancelButtonBackground))
                .Bind(Button.ContentProperty, this, nameof(CancelButtonContent));

            new Grid()
                .Assign(out _buttonBar)
                .Row(2)
                .Column(1)
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
