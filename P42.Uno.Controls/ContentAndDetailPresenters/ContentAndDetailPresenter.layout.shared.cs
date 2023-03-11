﻿using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml.Input;

namespace P42.Uno.Controls
{
    public partial class ContentAndDetailPresenter : Grid
    {
        TargetedPopup _targetedPopup;
        Border _detailDrawer;
        Rectangle _overlay;

        ColumnDefinition _drawerColumnDefinition = new ColumnDefinition().Auto();
        RowDefinition _drawerRowDefinition = new RowDefinition().Auto();

        const double popupMargin = 30;

        void Build()
        {
            // r0,c0 : Content
            // r1,c0 : Footer
            this.Rows("*", _drawerRowDefinition);
            this.Columns("*", _drawerColumnDefinition);
            _overlay = new Rectangle()
                .Row(0)
                .RowSpan(2)
                .Bind(Rectangle.FillProperty, this, nameof(PageOverlayBrush))
                .Bind(Rectangle.IsHitTestVisibleProperty, this, nameof(IsPageOverlayHitTestVisible))
                .Bind(Rectangle.VisibilityProperty, this, nameof(PageOverlayBrush), converter: P42.Utils.Uno.VisibilityExtensions.VisibilityConverter)
                .AddTappedHandler(OnDismissPointerPressed);

            _detailDrawer = new Border()
                .Bind(Border.BorderBrushProperty, this, nameof(DetailBorderBrush))
                .Bind(Border.BackgroundProperty, this, nameof(DetailBackground));

            _targetedPopup = new TargetedPopup()
                .Padding(0)
                .Opacity(0)
                .Margin(popupMargin)
                .ContentStretch()
                .HasShadow()
                .PreferredPointerDirection(PointerDirection.Up)
                .FallbackPointerDirection(PointerDirection.Any)
                .PageOverlay(Colors.Transparent)
                .PageOverlayHitTestVisible(false)
                .Bind(TargetedPopup.TargetProperty, this, nameof(Target))
                .Bind(TargetedPopup.WidthProperty, this, nameof(PopupWidth))
                .Bind(TargetedPopup.HeightProperty, this, nameof(PopupHeight))
                .Bind(TargetedPopup.BorderBrushProperty, this, nameof(DetailBorderBrush))
                .Bind(TargetedPopup.BackgroundProperty, this, nameof(DetailBackground))
                .AddPoppedHandler(OnTargetedPopupPopped)
                ;

            PageOverlayBrush = Colors.Black.WithAlpha(0.01).ToBrush();
            DetailBorderThickness = new Thickness(1);
            DetailBackground = SystemTeachingTipBrushes.Background;
            DetailBorderBrush = SystemTeachingTipBrushes.Border;

        }


    }
}
