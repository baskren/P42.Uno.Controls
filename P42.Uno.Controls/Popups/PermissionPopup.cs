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
    [TemplatePart(Name = CancelButtonName, Type = typeof(Button))]
    public partial class PermissionPopup : Alert
    {
        #region Properties

        #region CancelText Property
        public static readonly DependencyProperty CancelButtonTextProperty = DependencyProperty.Register(
            nameof(CancelButtonText),
            typeof(string),
            typeof(PermissionPopup),
            new PropertyMetadata(default(string))
        );
        public string CancelButtonText
        {
            get => (string)GetValue(CancelButtonTextProperty);
            set => SetValue(CancelButtonTextProperty, value);
        }
        #endregion CancelText Property

        #region CancelButtonForeground Property
        public static readonly DependencyProperty CancelButtonForegroundProperty = DependencyProperty.Register(
            nameof(CancelButtonForeground),
            typeof(Brush),
            typeof(PermissionPopup),
            new PropertyMetadata(default(Brush))
        );
        public Brush CancelButtonForeground
        {
            get => (Brush)GetValue(CancelButtonForegroundProperty);
            set => SetValue(CancelButtonForegroundProperty, value);
        }
        #endregion CancelButtonForeground Property


        #region CancelButtonBackground Property
        public static readonly DependencyProperty CancelButtonBackgroundProperty = DependencyProperty.Register(
            nameof(CancelButtonBackground),
            typeof(Brush),
            typeof(PermissionPopup),
            new PropertyMetadata(default(Brush))
        );
        public Brush CancelButtonBackground
        {
            get => (Brush)GetValue(CancelButtonBackgroundProperty);
            set => SetValue(CancelButtonBackgroundProperty, value);
        }
        #endregion CancelButtonBackgroundBrush Property


        #endregion


        #region Fields
        const string CancelButtonName = "_cancelButton";
        Button _cancelButton;
        #endregion


        #region Construction / Initialization
        public PermissionPopup() : base()
        {
            DefaultStyleKey = typeof(PermissionPopup);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _cancelButton = GetTemplateChild(CancelButtonName) as Button;
        }
        #endregion


        #region Event Handlers
        async void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            await PopAsync(PopupPoppedCause.ButtonTapped, sender);
        }
        #endregion


        #region Push / Pop
        public override async Task PushAsync()
        {
            await base.PushAsync();
            _cancelButton.Click += OnCancelButtonClicked;
        }


        public override async Task PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, [CallerMemberName] object trigger = null)
        {
            _cancelButton.Click -= OnCancelButtonClicked;
            await base.PopAsync(cause, trigger);
        }
        #endregion

    }
}
