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
                    new Grid()
                        .Background(P42.Uno.Markup.SystemColors.AltHigh)
                        .BorderBrush(Colors.Gray)
                        .Padding(10)
                        .Children
                        (
                            new TextBlock()
                                .Text("TITLE")
                                .BoldFontWeight()
                        ),
                    new TextBlock()
                        .WrapWords()
                        .Margin(10)
                        .Bind(TextBlock.TextProperty, _textBox, nameof(TextBox.Text)),
                    new Grid()
                        .Background(P42.Uno.Markup.SystemColors.AltHigh)
                        .BorderBrush(Colors.Gray)
                        .Padding(10)
                        .Children
                        (
                            new Button()
                                .Content("CANCEL")
                                .AddClickHandler(OnTargetedPopupCancelButtonClicked)
                                .Center()
                        )
                )
            );
            //result.BackgroundColor = Colors.Transparent;
            return result;
        }

        async void OnTargetedPopupCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            await TargetedPopup.PopAsync();
        }


        public MainPage()
        {
            Build();

            _marginTextBox.TextChanged += _marginTextBox_TextChanged;
            _paddingTextBox.TextChanged += _paddingTextBox_TextChanged;
        }

        private void _paddingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(_paddingTextBox.Text, out double result))
            {
                _bubble.Padding(result);
                TargetedPopup.Padding(result);
            }
        }

        private void _marginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(_marginTextBox.Text, out double result))
            {
                _bubble.Margin(result);
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
            if ((sender as Button) == _button)
                TargetedPopup.Target = null;
            await TargetedPopup.PushAsync();
        }


    }
}
