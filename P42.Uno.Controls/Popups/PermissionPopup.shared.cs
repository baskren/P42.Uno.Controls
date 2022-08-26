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

namespace P42.Uno.Controls
{
    /// <summary>
    /// Permission Popup
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class PermissionPopup : Alert
    {
        #region Properties

        #region CancelButtonContent Property
        public static readonly DependencyProperty CancelButtonContentProperty = DependencyProperty.Register(
            nameof(CancelButtonContent),
            typeof(object),
            typeof(PermissionPopup),
            new PropertyMetadata("Cancel")
        );

        /// <summary>
        /// Content for Cancel button
        /// </summary>
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
            new PropertyMetadata(default(Brush), OnCancelButtonForegroundChanged)
        );

        private static void OnCancelButtonForegroundChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is PermissionPopup p)
                p._cancelButton.Foreground = args.NewValue as Brush;
        }

        /// <summary>
        /// Foreground Brush for Cancel button
        /// </summary>
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
            new PropertyMetadata(null, OnCancelButtonBackgroundChanged)
        );

        private static void OnCancelButtonBackgroundChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is PermissionPopup p)
                p._cancelButton.Background = args.NewValue as Brush;
        }
        /// <summary>
        /// Background Brush for Cancel button
        /// </summary>
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
        /// <summary>
        /// Resulting PermissionState after PermissionPopup has been popped
        /// </summary>
        public PermissionState PermissionState
        {
            get => (PermissionState)GetValue(PermissionStateProperty);
            private set => SetValue(PermissionStateProperty, value);
        }
        #endregion PermissionState Property


        #endregion


        #region Construction / Initialization
        /// <summary>
        /// Constructor
        /// </summary>
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
        /// <summary>
        /// Push the PermissionPopup
        /// </summary>
        /// <param name="animated"></param>
        /// <returns></returns>
        public override async Task PushAsync(bool animated = true)
        {
            PermissionState = PermissionState.Pending;
            await base.PushAsync(animated);
            _cancelButton.Click += OnCancelButtonClicked;
        }

        /// <summary>
        /// Pop the PermissionPopup 
        /// </summary>
        /// <param name="cause"></param>
        /// <param name="animated"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public override async Task PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, bool animated = true, [CallerMemberName] object trigger = null)
        {
            _cancelButton.Click -= OnCancelButtonClicked;

            if (cause == PopupPoppedCause.ButtonTapped)
            {
                if (trigger is Button button)
                {
                    if (button == _cancelButton)
                        PermissionState = PermissionState.Rejected;
                    else if (button == _okButton)
                        PermissionState = PermissionState.Ok;
                }
            }
            if (PermissionState == PermissionState.Pending)
                PermissionState = PermissionState.Cancelled;

            await base.PopAsync(cause, animated, trigger);
        }
        #endregion

    }
}
