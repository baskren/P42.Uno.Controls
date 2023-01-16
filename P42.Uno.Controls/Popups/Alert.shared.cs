using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using P42.Uno.Markup;

namespace P42.Uno.Controls
{
    /// <summary>
    /// Alert popup: A Toast with an "OK" button
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class Alert : Toast
    {
        #region Properties

        #region OkButtonContent Property
        public static readonly DependencyProperty OkButtonContentProperty = DependencyProperty.Register(
            nameof(OkButtonContent),
            typeof(object),
            typeof(Alert),
            new PropertyMetadata("OK")
        );

        /// <summary>
        /// Text or UIElement for "OK" button content
        /// </summary>
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
            new PropertyMetadata(((Color)Application.Current.Resources["SystemColorHighlightTextColor"]).ToBrush())
        );
        /// <summary>
        /// Foreground for OK Button
        /// </summary>
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
            new PropertyMetadata(((Color)Application.Current.Resources["SystemColorHighlightColor"]).ToBrush())
        );
        /// <summary>
        /// Background for OK button
        /// </summary>
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
        /// Creates the Alert
        /// </summary>
        /// <param name="target"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="okContent"></param>
        /// <param name="okButtonColor"></param>
        /// <param name="okTextColor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Constructor
        /// </summary>
        public Alert() : base()
        {
            Build();
        }

        #endregion


        #region Event Handlers
        protected virtual async void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            await PopAsync(PopupPoppedCause.ButtonTapped, true, sender);
        }
        #endregion


        #region Push / Pop
        /// <summary>
        /// Push Alert
        /// </summary>
        /// <param name="animated"></param>
        /// <returns></returns>
        public override async Task PushAsync(bool animated = true)
        {
            await base.PushAsync(animated);
            _okButton.Click += OnOkButtonClicked;
        }

        /// <summary>
        /// Pop alert
        /// </summary>
        /// <param name="cause"></param>
        /// <param name="animated"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public override async Task PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, bool animated = true, [CallerMemberName] object trigger = null)
        {
            _okButton.Click -= OnOkButtonClicked;
            await base.PopAsync(cause, animated, trigger);
        }
        #endregion

    }
}
