using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    public partial class Alert : Toast
    {
        #region Properties

        #region OkButtonContent Property
        public static readonly DependencyProperty OkButtonContentProperty = DependencyProperty.Register(
            nameof(OkButtonContent),
            typeof(object),
            typeof(Alert),
            new PropertyMetadata(default(string))
        );
        public object OkButtonContent
        {
            get => (object)GetValue(OkButtonContentProperty);
            set => SetValue(OkButtonContentProperty, value);
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


        #region Construction / Initialization
        /// <summary>
        /// Create and present the specified title, text, okText, cancelText, okButtonColor, cancelButtonColor, okTextColor and cancelTextColor.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Text.</param>
        /// <param name="okContent">Ok text.</param>
        /// <param name="okButtonColor">Ok button color.</param>
        /// <param name="okTextColor">Ok text color.</param>
        public static async Task<Alert> CreateAsync(object title, object message, object okContent = null, Color okButtonColor = default, Color okTextColor = default)
        {
            var popup = new Alert { TitleContent = title, Message = message, OkButtonContent = okContent ?? "OK" };
            if (okTextColor != default)
                popup.OkButtonForeground = okTextColor.ToBrush();
            if (okButtonColor != default)
                popup.OkButtonBackground = okButtonColor.ToBrush();
            await popup.PushAsync();
            return popup;
        }

        /// <summary>
        /// Create and present the specified target, title, text, okText, cancelText, okButtonColor, cancelButtonColor, okTextColor and cancelTextColor.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="target">Target.</param>
        /// <param name="title">Title.</param>
        /// <param name="text">Text.</param>
        /// <param name="okText">Ok text.</param>
        /// <param name="okButtonColor">Ok button color.</param>
        /// <param name="okTextColor">Ok text color.</param>
        public static async Task<Alert> CreateAsync(UIElement target, object title, object message, object okContent = null, Color okButtonColor = default, Color okTextColor = default)
        {
            var popup = new Alert() {Target = target, TitleContent = title, Message = message, OkButtonContent = okContent ?? "OK" };
            if (okTextColor != default)
                popup.OkButtonForeground = okTextColor.ToBrush();
            if (okButtonColor != default)
                popup.OkButtonBackground = okButtonColor.ToBrush();
            await popup.PushAsync();
            return popup;
        }


        public Alert() : base()
        {
            Build();
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
