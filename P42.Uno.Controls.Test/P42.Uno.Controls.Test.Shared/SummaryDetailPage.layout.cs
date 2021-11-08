using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SummaryDetailPage : Page
    {
        ContentAndDetailPresenter _ContentAndDetailPresenter;
        ListView _listView;

        void Build()
        {
            Content = new Grid()
                .Children
                (
                    new ContentAndDetailPresenter()
                        .Assign(out _ContentAndDetailPresenter)
                        .Content
                        (
                            new ListView()
                                .Assign(out _listView)
                                .ItemTemplate(typeof(SummaryDetailPageCellTemplate))
                        )
                );

        }

    }

    [Bindable]
    public class SummaryDetailPageCellTemplate : Button
    {
        public SummaryDetailPageCellTemplate()
        {
            this.Padding(20, 2)
                .StretchHorizontal()
                .ContentRight()
                .Background(Colors.Beige)
                .BorderBrush(Colors.Green)
                .BorderThickness(1)
                //.AddOnTapped(BorderTapped)
                .CornerRadius(5);
#if WINDOWS_UWP
            DataContextChanged += CellTemplate_DataContextChanged;
#endif
        }

#if WINDOWS_UWP
        private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
#else
        protected override void OnDataContextChanged(DependencyPropertyChangedEventArgs e)
#endif
        {
            Content = DataContext?.ToString() ?? string.Empty;
        }
    }
}