using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using P42.Utils;
using P42.Utils.Uno;
using P42.Uno.Markup;
using P42.Uno.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class FlexDemoHomePage : Page
    {
        NavigationView NavView;
        Frame ContentFrame;

        void Build()
        {
            Content = new Grid()
                .Children
                (
                    new NavigationView()
                        .Assign(out NavView)
                        .MenuItems
                        (
                            new NavigationViewItem { Content = "Simple Stack Page", Icon = new SymbolIcon(Symbol.Page), Tag = "simpleStack" },
                            new NavigationViewItem { Content = "Photo Wrapping Page", Icon = new SymbolIcon(Symbol.Page), Tag = "photoWrapping" },
                            new NavigationViewItem { Content = "Holy Grail Page", Icon = new SymbolIcon(Symbol.Page), Tag = "holyGrail" },
                            new NavigationViewItem { Content = "Catalog Items Page", Icon = new SymbolIcon(Symbol.Page), Tag = "catalogItems" },
                            new NavigationViewItem { Content = "Experiment Page", Icon = new SymbolIcon(Symbol.Page), Tag = "experiment" },
                            new NavigationViewItem { Content = "Basis Experiment Page", Icon = new SymbolIcon(Symbol.Page), Tag = "basisExperiment" },
                            new NavigationViewItem { Content = "Grow Experiment Page", Icon = new SymbolIcon(Symbol.Page), Tag = "growExperiment" },
                            new NavigationViewItem { Content = "Shrink Experiment Page", Icon = new SymbolIcon(Symbol.Page), Tag = "shrinkExperiment" }
                        )
                        .AddOnBackRequested(NavView_BackRequested)
                        .AddOnLoaded(NavView_Loaded)
                        .AddOnSelectionChanged(NavView_SelectionChanged)
                        .Content
                        (
                            new ScrollViewer()
                                .VerticalScrollBarVisibility(ScrollBarVisibility.Auto)
                                .VerticalScrollMode(ScrollMode.Auto)
                                .Content
                                (
                                    new Frame()
                                        .Assign(out ContentFrame)
                                        .Padding(0,30,0,0)
                                        .TabStop()
                                        .AddOnNavigationFailed(ContentFrame_NavigationFailed)
                                )
                        )
                );

        }
    }
}