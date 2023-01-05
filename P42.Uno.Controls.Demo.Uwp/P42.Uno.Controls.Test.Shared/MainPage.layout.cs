using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.UI;
using System.Threading.Tasks;
using P42.Utils.Uno;
using P42.Uno.Markup;
using P42.Uno.Controls;
using System.Reflection.Emit;
using Microsoft.UI.Xaml.Shapes;

#if !HAS_UNO
#else
using Uno.Foundation;
#endif

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace P42.Uno.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Grid _grid;
        Border _altBorder;
        TextBox _marginTextBox, _paddingTextBox;
        ComboBox _pointerDirectionCombo, _hzAlignCombo, _vtAlignCombo;
        ToggleSwitch _indexOthogonal;
        Button _button;
        ListView _listView;
        BubbleBorder _bubbleBorder;

        void Build()
        {
            this.Padding(0)
                .Margin(0);

            Content = new Grid()
                .Assign(out _grid)
                .Margin(0)
                .Padding(0)
                .Background(Colors.Yellow)
                .Rows(150, 50, "*")
                .Children
                (
                    new Border()
                        .Assign(out _altBorder)
                        .Row(1)
                        .Size(50, 50)
                        .CenterHorizontal()
                        .Background(Colors.Pink)
                        .AddTapHandler(OnAltBorderTapped),
                    new StackPanel()
                        .Row(1)
                        .Horizontal()
                        .Children
                        (
                            new TextBlock().Text("Margin:"),
                            new TextBox().Assign(out _marginTextBox).Text("0"),
                            new TextBlock().Text("Padding:"),
                            new TextBox().Assign(out _paddingTextBox).Text("0"),
                            new TextBlock().Text("Pointer:"),
                            new ComboBox().Assign(out _pointerDirectionCombo).Text("PointerDir"),
                            new TextBlock().Text("HzAlign:"),
                            new ComboBox().Assign(out _hzAlignCombo).Text("HzAlign"),
                            new TextBlock().Text("VtAlign:"),
                            new ComboBox().Assign(out _vtAlignCombo).Text("VtAlign"),
                            new TextBlock().Text("Index Orthoganal:"),
                            new ToggleSwitch().Assign(out _indexOthogonal),
                            new Button().Assign(out _button)
                                .Content("Show Popup")
                                .AddTapHandler(_button_Click)
                        ),
                    new ListView()
                        .Assign(out _listView)
                        .Row(2)
                        .ItemTemplate(typeof(MainPageButtonRowTemplate), typeof(Type)),
                    new Rectangle()
                        .Stretch()
                        .Fill(Colors.Beige),
                    new BubbleBorder()
                        .Assign(out _bubbleBorder)
                        .Content(new TextBlock().Text("TEST TEST TEST"))
                );

        }

    }

    [Bindable]
    public partial class MainPageButtonRowTemplate : Button
    {
        public MainPageButtonRowTemplate()
        {
            this.Padding(20, 2)
                .StretchHorizontal()
                .HorizontalContentAlignment(HorizontalAlignment.Right)
                .Background(Colors.Beige)
                .BorderBrush(Colors.Green)
                .BorderThickness(1)
                .CornerRadius(5)
                //.AddTapHandler(BorderTapped)
                .Content(new TextBlock { Text = "ZAP" });
#if !HAS_UNO

            DataContextChanged += CellTemplate_DataContextChanged;
#endif
        }

#if !HAS_UNO

        private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
#else
            protected override void OnDataContextChanged(DependencyPropertyChangedEventArgs e)
#endif
        {
            Content = DataContext?.ToString() ?? string.Empty;
        }
    }

}