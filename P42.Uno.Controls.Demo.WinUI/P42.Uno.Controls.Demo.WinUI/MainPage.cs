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
using Microsoft.UI.Text;


#if __ANDROID__
using Android.Content;
using Android.Content.Res;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
#endif

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace P42.Uno.Controls.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [TemplateVisualState(GroupName = "State", Name = "Collapsed")]
    [TemplateVisualState(GroupName = "State", Name = "Visible")]
    public sealed partial class MainPage : Page
    {
        string[] _hzSource;
        string[] _vtSource;


        TargetedPopup _targetedPopup;
        TargetedPopup TargetedPopup => _targetedPopup ??= MakePopup();

        TargetedPopup MakePopup()
        {
            var result = new TargetedPopup()
            //.Background(Colors.Transparent)
            .PushEffect(Effect.Info, EffectMode.On)
            .Content(new StackPanel()
                .Stretch()
                .Children
                (
                    new TextBlock()
                        .Text("TITLE")
                        .BoldFontWeight()
                        .Margin(10, 5),
                    new Grid()
                        .Background(Colors.White)
                        .BorderBrush(Colors.Gray)
                        .Padding(10)
                        .Children
                        (
                            new TextBlock()
                                .WrapWords()
                                .Bind(TextBlock.TextProperty, _textBox, nameof(TextBox.Text))
                        ),
                    new Button()
                        .Content("CANCEL")
                        .AddClickHandler(OnTargetedPopupCancelButtonClicked)
                        .Center()
                        .Margin(10)
                )
            );
            //result.BackgroundColor = Colors.Transparent;
            return result;
        }

        async void OnTargetedPopupCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            await TargetedPopup.PopAsync();
        }


        /*
        Toast _toast = new Toast
        {
            TitleContent = new Border
            {
                Child = new TextBlock
                {
                    Text = "TOAST TITLE 2 - THIS IS TO BE LONGER THAN THE CONTENT",
                },
            },
            Message = new Border { Child = new TextBlock { Text = "Toast message content" } },
            IconElement = new SymbolIcon { Symbol=Symbol.AddFriend }
        };
        
        Alert _alert = new Alert
        {
            TitleContent = "ALERT TITLE",
            Message = new TextBlock { Text = "alert text" },
            IconElement = new SymbolIcon { Symbol = Symbol.Important },
            OkButtonContent = "GO FOR IT!"
        };
        
        
        PermissionPopup _permission = new PermissionPopup
        {
            TitleContent =  new TextBlock { Text = "PERMISSION TITLE" },
            
            Message =  new TextBlock  { Text = "PERMISSION TEXT"  },
            IconElement = new SymbolIcon { Symbol = Symbol.Audio },
            OkButtonContent = "YUP",
            CancelButtonContent = "NOPE!",
            CancelButtonBackground = new SolidColorBrush(Colors.Red),
            //IsAnimated = true
        };
        */

        public MainPage()
        {
            //this.InitializeComponent();
            Build();


            _marginTextBox.TextChanged += _marginTextBox_TextChanged;
            _paddingTextBox.TextChanged += _paddingTextBox_TextChanged;
            /*
            _bubbleBorder = new BubbleBorder()
                .Margin(5)
                .Padding(5)
                .Left()
                .Bottom()
                .ContentCenter()
                .Background(Colors.Orange)
                .BorderBrush(Colors.Black)
                .BorderThickness(1)
                .CornerRadius(5)
                .HasShadow()
                .PointerDirection(PointerDirection.Down)
                .Content(
                    new Border()
                        .Background("#40FF")
                        .Child(new TextBlock { Text = "TEST TEST TEST" })
                );
            _grid.Children.Add(_bubbleBorder);
            */

        }

        private void _paddingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(_paddingTextBox.Text, out double result))
            {
                //_bubble.Padding(result);
                TargetedPopup.Padding(result);
            }
        }

        private void _marginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(_marginTextBox.Text, out double result))
            {
                //_bubble.Margin(result);
                TargetedPopup.Margin(result);
            }
        }


        private void OnElementTapped(object sender, TappedRoutedEventArgs e)
        {
            _button_Click(sender, e);
        }

        async void _button_Click(object sender, RoutedEventArgs e)
        {
            TargetedPopup.Target = sender as UIElement;
            if (sender == _button)
                TargetedPopup.Target = null;
            await TargetedPopup.PushAsync();

            /*

                var popup = _targetedPopup;
    #if __WASM__
                    var prefDir = (PointerDirection) Enum.Parse(typeof(PointerDirection), _pointerDirectionCombo.SelectedItem as string);
                    var hzAlign = Enum.Parse(typeof(HorizontalAlignment),_hzAlignCombo.SelectedItem as string, true);
                    var vtAlign = Enum.Parse(typeof(VerticalAlignment),_vtAlignCombo.SelectedItem as string, true);
    #else
                var prefDir = Enum.Parse<PointerDirection>(_pointerDirectionCombo.SelectedItem as string, true);
                var hzAlign = Enum.Parse<HorizontalAlignment>(_hzAlignCombo.SelectedItem as string, true);
                var vtAlign = Enum.Parse<VerticalAlignment>(_vtAlignCombo.SelectedItem as string, true);
    #endif
                _bubbleBorder.HorizontalAlignment = (HorizontalAlignment)hzAlign;


                popup.VerticalAlignment = (VerticalAlignment)vtAlign;
                popup.HorizontalAlignment = (HorizontalAlignment)hzAlign;
                popup.PreferredPointerDirection = prefDir;
                popup.Target = sender as UIElement;

                //popup.PopupContent = new TextBlock { Text =  "We are going to the supermarket" };
                //popup.Content(new TextBlock { Text = "Green Car" });
                popup.Background(Colors.Yellow);
                popup.BorderBrush(Colors.Red);

                var margin = 0.0;
                if (double.TryParse(_marginTextBox.Text, out double m))
                    margin = m;
                var padding = 0.0;
                if (double.TryParse(_paddingTextBox.Text, out double p))
                    padding = p;

                popup.Margin(m);
                popup.Padding(p);

                await popup.PushAsync();

                /*
                _toast.Content = "I like pizza";



                /*
                if (sender is FrameworkElement element)
                {
                    var frame = element.GetBounds();
                    var content = $"frame:[{frame.X.ToString("0.##")}, {frame.Y.ToString("0.##")}, {frame.Width.ToString("0.##")}, {frame.Height.ToString("0.##")}]";

                    _TargetedPopup.Target = element;

                    _TargetedPopup.Margin = new Thickness(5);
                    _TargetedPopup.Padding = new Thickness(10);
                    _TargetedPopup.BorderThickness = new Thickness(1);
                    _TargetedPopup.CornerRadius = new CornerRadius(4);
                    _TargetedPopup.Background = new SolidColorBrush(Colors.White);
                    _TargetedPopup.BorderBrush = new SolidColorBrush(Colors.Blue);

                    _TargetedPopup.Content = new TextBlock { Text = content };

                    if (_TargetedPopup.Parent is Grid grid)
                        grid.Children.Remove(_TargetedPopup);

    #if __WASM__
                    _TargetedPopup.PreferredPointerDirection = (PointerDirection) Enum.Parse(typeof(PointerDirection), _pointerDirectionCombo.SelectedItem as string);
                    var hzAlign = Enum.Parse(typeof(HorizontalAlignment),_hzAlignCombo.SelectedItem as string, true);
                    var vtAlign = Enum.Parse(typeof(VerticalAlignment),_vtAlignCombo.SelectedItem as string, true);
    #else
                    _TargetedPopup.PreferredPointerDirection = Enum.Parse<PointerDirection>(_pointerDirectionCombo.SelectedItem as string, true);
                    var hzAlign = Enum.Parse<HorizontalAlignment>(_hzAlignCombo.SelectedItem as string, true);
                    var vtAlign = Enum.Parse<VerticalAlignment>(_vtAlignCombo.SelectedItem as string, true);
    #endif

                    _bubbleBorder.HorizontalAlignment = (HorizontalAlignment) hzAlign;
                    _TargetedPopup.VerticalAlignment = (VerticalAlignment) vtAlign;
                    _TargetedPopup.HorizontalAlignment = (HorizontalAlignment) hzAlign;

                    if (_indexOthogonal.IsOn)
                    {
                        if (_TargetedPopup.PreferredPointerDirection.IsHorizontal())
                        {
                            var newIndex = _vtAlignCombo.SelectedIndex+1;
                            if (newIndex >= _vtSource.Length)
                                newIndex = 0;
                            _vtAlignCombo.SelectedIndex = newIndex;
                        }
                        else
                        {
                            var newIndex = _hzAlignCombo.SelectedIndex+1;
                            if (newIndex >= _hzSource.Length)
                                newIndex = 0;
                            _hzAlignCombo.SelectedIndex = newIndex;
                        }
                    }

                    await _TargetedPopup.PushAsync();
                }
                */
            }


        }
    }
