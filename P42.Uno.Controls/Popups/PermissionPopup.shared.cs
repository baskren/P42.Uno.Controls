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
    [Windows.UI.Xaml.Data.Bindable]
    [System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class PermissionPopup : Alert
    {
        #region Properties

        #region CancelButtonContent Property
        public static readonly DependencyProperty CancelButtonContentProperty = DependencyProperty.Register(
            nameof(CancelButtonContent),
            typeof(object),
            typeof(PermissionPopup),
            new PropertyMetadata("Cancel", OnCancelButtonContentChanged)
        );

        private static void OnCancelButtonContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is PermissionPopup popup)
                popup._cancelButton.Content = args.NewValue;
        }

        public object CancelButtonContent
        {
            get => (object)GetValue(CancelButtonContentProperty);
            set => SetValue(CancelButtonContentProperty, value);
        }
        #endregion CancelText Property


        #region CancelButtonForeground Property
        public static readonly DependencyProperty CancelButtonForegroundProperty = DependencyProperty.Register(
            nameof(CancelButtonForeground),
            typeof(Brush),
            typeof(PermissionPopup),
            new PropertyMetadata(((Color)Application.Current.Resources["SystemColorButtonTextColor"]).ToBrush())
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
            new PropertyMetadata(((Color)Application.Current.Resources["SystemColorButtonFaceColor"]).ToBrush())
        );
        public Brush CancelButtonBackground
        {
            get => (Brush)GetValue(CancelButtonBackgroundProperty);
            set => SetValue(CancelButtonBackgroundProperty, value);
        }
        #endregion CancelButtonBackgroundBrush Property


        #region PermissionState Property
        public static readonly DependencyProperty PermissionStateProperty = DependencyProperty.Register(
            nameof(PermissionState),
            typeof(PermissionState),
            typeof(PermissionPopup),
            new PropertyMetadata(default(PermissionState))
        );
        public PermissionState PermissionState
        {
            get => (PermissionState)GetValue(PermissionStateProperty);
            private set => SetValue(PermissionStateProperty, value);
        }
        #endregion PermissionState Property


        #endregion


        #region Construction / Initialization
        public PermissionPopup()
        {
            Build();
        }
        #endregion


        #region Factories
        /// <summary>
        /// Create the specified title, text, okText, cancelText, okButtonColor, cancelButtonColor, OkButtonContent and cancelTextColor.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Text.</param>
        /// <param name="okButtonContent">Ok text.</param>
        /// <param name="cancelButtonContent">Cancel text.</param>
        /// <param name="okButtonColor">Ok button color.</param>
        /// <param name="cancelButtonColor">Cancel button color.</param>
        /// <param name="okButtonTextColor">Ok text color.</param>
        /// <param name="cancelTextColor">Cancel text color.</param>
        public static async Task<PermissionPopup> CreateAsync(object title, object message, string okButtonContent = null, object cancelButtonContent = null, Color okButtonColor = default, Color cancelButtonColor = default, Color okButtonTextColor = default, Color cancelTextColor = default)
        {
            var popup = new PermissionPopup { TitleContent = title, Message = message, OkButtonContent = okButtonContent ?? "OK", CancelButtonContent = cancelButtonContent ?? "Cancel" };
            if (okButtonTextColor != default)
                popup.OkButtonForeground = okButtonTextColor.ToBrush();
            if (okButtonColor != default)
                popup.OkButtonBackground = okButtonColor.ToBrush();
            if (cancelTextColor != default)
                popup.CancelButtonForeground = cancelTextColor.ToBrush();
            if (cancelButtonColor != default)
                popup.CancelButtonBackground = cancelButtonColor.ToBrush();
            await popup.PushAsync();
            return popup;
        }

        /// <summary>
        /// Create the specified target, title, text, okText, cancelText, okButtonColor, cancelButtonColor, OkButtonContent and cancelTextColor.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="target">Target.</param>
        /// <param name="title">Title.</param>
        /// <param name="message">Text.</param>
        /// <param name="okButtonContent">Ok text.</param>
        /// <param name="cancelButtonContent">Cancel text.</param>
        /// <param name="okButtonColor">Ok button color.</param>
        /// <param name="cancelButtonColor">Cancel button color.</param>
        /// <param name="okButtonTextColor">Ok text color.</param>
        /// <param name="cancelTextColor">Cancel text color.</param>
        public static async Task<PermissionPopup> CreateAsync(UIElement target, object title, object message, string okButtonContent = null, object cancelButtonContent = null, Color okButtonColor = default, Color cancelButtonColor = default, Color okButtonTextColor = default, Color cancelTextColor = default)
        {
            var popup = new PermissionPopup() { Target = target, TitleContent = title, Message = message, OkButtonContent = okButtonContent ?? "OK", CancelButtonContent = cancelButtonContent ?? "Cancel" };
            if (okButtonTextColor != default)
                popup.OkButtonForeground = okButtonTextColor.ToBrush();
            if (okButtonColor != default)
                popup.OkButtonBackground = okButtonColor.ToBrush();
            if (cancelTextColor != default)
                popup.CancelButtonForeground = cancelTextColor.ToBrush();
            if (cancelButtonColor != default)
                popup.CancelButtonBackground = cancelButtonColor.ToBrush();
            await popup.PushAsync();
            return popup;
        }
        #endregion


        #region Event Handlers
        async void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            await PopAsync(PopupPoppedCause.ButtonTapped, true, sender);
        }
        #endregion


        #region Push / Pop
        public override async Task PushAsync(bool animated = true)
        {
            PermissionState = PermissionState.Pending;
            await base.PushAsync(animated);
            _cancelButton.Click += OnCancelButtonClicked;
        }


        public override async Task PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, bool animated = true, [CallerMemberName] object trigger = null)
        {
            _cancelButton.Click -= OnCancelButtonClicked;

            if (cause == PopupPoppedCause.ButtonTapped)
            {
                if (trigger == _cancelButton)
                    PermissionState = PermissionState.Rejected;
                else if (trigger == _okButton)
                    PermissionState = PermissionState.Ok;
            }
            if (PermissionState == PermissionState.Pending)
                PermissionState = PermissionState.Cancelled;

            await base.PopAsync(cause, animated, trigger);
        }
        #endregion

    }
}
