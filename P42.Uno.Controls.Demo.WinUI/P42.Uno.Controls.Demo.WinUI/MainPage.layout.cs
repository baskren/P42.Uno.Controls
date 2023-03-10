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
using Microsoft.UI;
using System.Threading.Tasks;
using P42.Utils.Uno;
using P42.Uno.Markup;
using P42.Uno.Controls;
using System.Reflection.Emit;
using Microsoft.UI.Xaml.Shapes;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace P42.Uno.Controls.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    public sealed partial class MainPage : Page
    {
        Grid _grid;
        Border _altBorder;
        TextBox _marginTextBox, _paddingTextBox;
        ComboBox _pointerDirectionCombo, _hzAlignCombo, _vtAlignCombo;
        Button _button;
        ListView _listView;
        TextBox _textBox;
        TextBlock _textBlock;
        ToggleSwitch _shadowToggleSwitch, _overlayToggleSwitch, _hitTransparentOverlayToggleSwitch;
        Rectangle _targetRectangle, _bubbleTestRectangle;
        BubbleBorder _bubble;

        void Build()
        {
            this.Padding(0)
                .Margin(0);

            Content = new Grid()
                .Assign(out _grid)
                .Margin(0)
                .Padding(0)
                .Background(Colors.Yellow)
                .Rows(150, 50, 50, "*", 50, 50)
                .Children
                (
                    new Rectangle()
                        .Assign(out _bubbleTestRectangle)
                        .Row(0)
                        .Stretch()
                        .Fill(Colors.Beige),
                    new StackPanel()
                        .Row(1)
                        .Horizontal()
                        .Children
                        (
                            new TextBlock().Text("Margin:").Foreground(Colors.Black),
                            new TextBox().Assign(out _marginTextBox).Text("0").Foreground(Colors.Black),
                            new TextBlock().Text("Padding:").Foreground(Colors.Black),
                            new TextBox().Assign(out _paddingTextBox).Text("0").Foreground(Colors.Black),
                            new TextBlock().Text("Pointer:").Foreground(Colors.Black),
                            new ComboBox()
                                .Assign(out _pointerDirectionCombo)
                                .Text("PointerDir")
                                .Foreground(Colors.Black)
                                .ItemTemplate(typeof(EnumItemTemplate), typeof(Type))
                                .ItemsSource(Enum.GetValues(typeof(P42.Uno.Controls.PointerDirection)))
                                .SelectedIndex(0)
                                .AddSelectionChangedHandler(OnPointerDirChanged),
                            new TextBlock().Text("HzAlign:").Foreground(Colors.Black),
                            new ComboBox()
                                .Assign(out _hzAlignCombo)
                                .Text("HzAlign")
                                .Foreground(Colors.Black)
                                .ItemTemplate(typeof(EnumItemTemplate), typeof(Type))
                                .ItemsSource(Enum.GetValues(typeof(HorizontalAlignment)))
                                .SelectedIndex(0)
                                .AddSelectionChangedHandler(OnHzAlignChanged),
                            new TextBlock().Text("VtAlign:").Foreground(Colors.Black),
                            new ComboBox()
                                .Assign(out _vtAlignCombo)
                                .Text("VtAlign")
                                .Foreground(Colors.Black)
                                .ItemTemplate(typeof(EnumItemTemplate), typeof(Type))
                                .ItemsSource(Enum.GetValues(typeof(VerticalAlignment)))
                                .SelectedIndex(0)
                                .AddSelectionChangedHandler(OnVtAlignChanged),
                            new Button().Assign(out _button)
                                .Content("Show Popup")
                                .AddTapHandler(_button_Click)
                                .Foreground(Colors.Black)
                        ),
                    new StackPanel()
                        .Row(2)
                        .Horizontal()
                        .Children(
                            new TextBlock().Text("Shadow:").Foreground(Colors.Black),
                            new ToggleSwitch().Assign(out _shadowToggleSwitch).On().AddToggledHandler(OnShadowToggleButtonChanged),
                            new TextBlock().Text("Overlay:").Foreground(Colors.Black),
                            new ToggleSwitch().Assign(out _overlayToggleSwitch).On().AddToggledHandler(OnPageOverlayToggleSwitchChanged),
                            new TextBlock().Text("Hitable:").Foreground(Colors.Black),
                            new ToggleSwitch().Assign(out _hitTransparentOverlayToggleSwitch).On().AddToggledHandler(OnHitTransparentToggleButtonChanged)
                        ),
                    new ListView()
                        .Assign(out _listView)
                        .Row(3)
                        .ItemTemplate(typeof(MainPageButtonRowTemplate), typeof(Type))
                        .ItemsSource(new List<int> { 1, 2, 3, 4 }),
                    new TextBox()
                        .Row(5)
                        .Assign(out _textBox)
                        .Text("My Text Test Here!"),
                    new Border()
                        .Assign(out _altBorder)
                        .Row(3)
                        .Size(50, 50)
                        .CenterHorizontal()
                        .Child(new TextBlock { Text = "TARGET", VerticalAlignment = VerticalAlignment.Center })
                        .Background(Colors.Pink)
                        .AddTapHandler(OnElementTapped),
                    new BubbleBorder()
                        .Assign(out _bubble)
                        .Content(new Border()
                            .Child(new TextBlock()
                                .Assign(out _textBlock)
                                .Margin(10)
                                .Bind(TextBlock.TextProperty, _textBox, nameof(TextBox.Text))
                            )
                        )
                );

            OnPointerDirChanged(null, null);
            OnVtAlignChanged(null, null);
            OnHzAlignChanged(null, null);
        }

        private void OnHitTransparentToggleButtonChanged(object sender, RoutedEventArgs e)
        {
            TargetedPopup.IsPageOverlayHitTestVisible = _hitTransparentOverlayToggleSwitch.IsOn;
        }

        private void OnShadowToggleButtonChanged(object sender, RoutedEventArgs e)
        {
            TargetedPopup.HasShadow = _shadowToggleSwitch.IsOn;
        }

        private void OnPageOverlayToggleSwitchChanged(object sender, RoutedEventArgs e)
        {
            TargetedPopup.PageOverlayBrush = _overlayToggleSwitch.IsOn
                ? new SolidColorBrush(Colors.Black.WithAlpha(0.25))
                : null;
        }

        private void OnPointerDirChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_pointerDirectionCombo.SelectedItem is not P42.Uno.Controls.PointerDirection dir)
                dir = PointerDirection.None;
            TargetedPopup.PreferredPointerDirection = dir;

            
            if (dir == PointerDirection.Horizontal ||  dir == PointerDirection.Vertical || dir == PointerDirection.Any)
                _bubble.PointerDirection = PointerDirection.None;
            else
                _bubble.PointerDirection = dir;
            
        }

        private void OnVtAlignChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vtAlignCombo.SelectedItem is not VerticalAlignment align)
                align = VerticalAlignment.Top;

            //TargetedPopup.VerticalAlignment = align;
            _bubble.VerticalAlignment = align;
        }

        private void OnHzAlignChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_hzAlignCombo.SelectedItem is not HorizontalAlignment align)
                align = HorizontalAlignment.Left;

            //TargetedPopup.HorizontalAlignment = align;
            _bubble.HorizontalAlignment = align;
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

            DataContextChanged += CellTemplate_DataContextChanged;
        }

        private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Content = DataContext?.ToString() ?? string.Empty;
        }
    }

    [Bindable]
    public partial class EnumItemTemplate : Grid
    {
        TextBlock textBlock;

        public EnumItemTemplate()
        {
            this.Padding(20, 2)
                .Children(
                    new TextBlock()
                    .Assign(out textBlock)
                );
            DataContextChanged += CellTemplate_DataContextChanged;
        }

        private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            textBlock.Text = DataContext?.ToString() ?? string.Empty;
        }
    }
}