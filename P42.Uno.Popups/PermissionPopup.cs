using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Popups
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

        public PermissionPopup() : base()
        {
            this.DefaultStyleKey = typeof(PermissionPopup);
            /*
            var style = Style;
            var template = Application.Current.Resources["AlertPopupTemplate"] as ControlTemplate;
            System.Diagnostics.Debug.WriteLine(GetType() + ".Alert ctr template:" + template + "  style:"+style);
            Template = template;
            */
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _cancelButton = GetTemplateChild(CancelButtonName) as Button;
        }


        public override async Task PushAsync()
        {
            _cancelButton.Click += OnCancelButtonClicked;
            await base.PushAsync();
        }

        protected override void OnPopupClosed(object sender, object e)
        {
            _cancelButton.Click -= OnCancelButtonClicked;
            base.OnPopupClosed(sender, e);
        }

        async void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            await PopAsync(PopupPoppedCause.ButtonTapped, sender);
        }


    }
}
