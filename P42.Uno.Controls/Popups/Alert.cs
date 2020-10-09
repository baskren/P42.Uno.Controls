using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    [TemplatePart(Name = OkButtonName, Type = typeof(Button))]
    public partial class Alert : Toast
    {
        #region Properties
        #region OkText Property
        public static readonly DependencyProperty OkButtonTextProperty = DependencyProperty.Register(
            nameof(OkButtonText),
            typeof(string),
            typeof(Alert),
            new PropertyMetadata(default(string))
        );
        public string OkButtonText
        {
            get => (string)GetValue(OkButtonTextProperty);
            set => SetValue(OkButtonTextProperty, value);
        }
        #endregion OkText Property

        #region OkButtonForeground Property
        public static readonly DependencyProperty OkButtonForegroundProperty = DependencyProperty.Register(
            nameof(OkButtonForeground),
            typeof(Brush),
            typeof(Alert),
            new PropertyMetadata(default(Brush))
        );
        public Brush OkButtonForeground
        {
            get => (Brush)GetValue(OkButtonForegroundProperty);
            set => SetValue(OkButtonForegroundProperty, value);
        }
        #endregion OkButtonForeground Property


        #region OkButtonBackground Property
        public static readonly DependencyProperty OkButtonBackgroundProperty = DependencyProperty.Register(
            nameof(OkButtonBackground),
            typeof(Brush),
            typeof(Alert),
            new PropertyMetadata(default(Brush))
        );
        public Brush OkButtonBackground
        {
            get => (Brush)GetValue(OkButtonBackgroundProperty);
            set => SetValue(OkButtonBackgroundProperty, value);
        }
        #endregion OkButtonBackgroundBrush Property


        #endregion


        #region Fields
        const string OkButtonName = "_okButton";
        Button _okButton;
        #endregion


        #region Construction / Initialization
        public Alert() : base()
        {
            DefaultStyleKey = typeof(Alert);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _okButton = GetTemplateChild(OkButtonName) as Button;
        }
        #endregion


        #region Event Handlers
        async void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            await PopAsync(PopupPoppedCause.ButtonTapped, sender);
        }
        #endregion


        #region Push / Pop
        public override async Task PushAsync()
        {
            await base.PushAsync();
            _okButton.Click += OnOkButtonClicked;
        }

        public override async Task PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, [CallerMemberName] object trigger = null)
        {
            _okButton.Click -= OnOkButtonClicked;
            await base.PopAsync(cause, trigger);
        }
        #endregion

    }
}
