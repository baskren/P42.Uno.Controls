using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    public static class ContentAndDetailPresenterMarkupExtensions
    {
        public static ContentAndDetailPresenter Content(this ContentAndDetailPresenter presenter, FrameworkElement element = null)
        { presenter.Content = element; return presenter; }

        public static ContentAndDetailPresenter Footer(this ContentAndDetailPresenter presenter, FrameworkElement element = null)
        { presenter.Footer = element; return presenter; }

        public static ContentAndDetailPresenter Detail(this ContentAndDetailPresenter presenter, FrameworkElement element = null)
        { presenter.Detail = element; return presenter; }

        public static ContentAndDetailPresenter DetailBackground(this ContentAndDetailPresenter presenter, Brush brush)
        { presenter.DetailBackground = brush; return presenter; }

        public static ContentAndDetailPresenter DetailBackground(this ContentAndDetailPresenter presenter, Windows.UI.Color color)
        { presenter.DetailBackground = new SolidColorBrush(color); return presenter; }

        public static ContentAndDetailPresenter DetailAspectRatio(this ContentAndDetailPresenter presenter, double aspect)
        { presenter.DetailAspectRatio = aspect; return presenter; }

        public static ContentAndDetailPresenter Target(this ContentAndDetailPresenter presenter, FrameworkElement element)
        { presenter.Target = element; return presenter; }

        public static ContentAndDetailPresenter PopupWidth(this ContentAndDetailPresenter presenter, double width)
        { presenter.PopupWidth = width; return presenter; }

        public static ContentAndDetailPresenter PopupHeight(this ContentAndDetailPresenter presenter, double height)
        { presenter.PopupHeight = height; return presenter; }

        public static ContentAndDetailPresenter PopupHorizontalAlignment(this ContentAndDetailPresenter presenter, HorizontalAlignment alignment)
        { presenter.PopupHorizontalAlignment = alignment; return presenter; }

        public static ContentAndDetailPresenter PopupVerticalAlignment(this ContentAndDetailPresenter presenter, VerticalAlignment alignment)
        { presenter.PopupVerticalAlignment = alignment; return presenter; }

        public static ContentAndDetailPresenter IsAnimated(this ContentAndDetailPresenter presenter, bool isAnimated)
        { presenter.IsAnimated = isAnimated; return presenter; }

        public static ContentAndDetailPresenter IsLightDismissEnabled(this ContentAndDetailPresenter presenter, bool isLightDismissEnabled)
        { presenter.IsLightDismissEnabled = isLightDismissEnabled; return presenter; }

        public static ContentAndDetailPresenter LightDismissOverlayMode(this ContentAndDetailPresenter presenter, LightDismissOverlayMode lightDismissMode)
        { presenter.LightDismissOverlayMode = lightDismissMode; return presenter; }

        public static ContentAndDetailPresenter LightDismissOverlay(this ContentAndDetailPresenter presenter, Brush lightDismissBrush)
        { presenter.LightDismissOverlayBrush = lightDismissBrush; return presenter; }

        public static ContentAndDetailPresenter LightDismissOverlay(this ContentAndDetailPresenter presenter, Windows.UI.Color color)
        { presenter.LightDismissOverlayBrush = new SolidColorBrush(color); return presenter; }

    }
}
