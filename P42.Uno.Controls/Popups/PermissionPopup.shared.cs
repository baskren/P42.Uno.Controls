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
            new PropertyMetadata("Cancel", (d,e) => ((PermissionPopup)d).OnCancelButtonContentChanged(e))
        );

        private void OnCancelButtonContentChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is TextBlock tb)
            {
                var t = tb.Text;
                if (string.IsNullOrWhiteSpace(t))
                    t = tb.GetHtml();
                _cancelButton.Collapsed(string.IsNullOrWhiteSpace(t));
                _cancelButton.Content = tb;
            }
            else if (args.NewValue is string text)
            {
                _cancelButton.Collapsed(string.IsNullOrWhiteSpace(text));
                if (_cancelButton.IsVisible())
                {
                    _cancelButton.Content = new TextBlock()
                        .BindFont(_cancelButton)
                        .WrapWords()
                        .SetHtml(text);
                }
                else
                    _cancelButton.Content = null;
            }
            else
            {
                _cancelButton.Content = args.NewValue;
                _cancelButton.Visible();
            }
        }

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
        /// Create the specified titleText, messageText, okText, cancelText, okButtonColor, cancelButtonColor, OkButtonContent and cancelTextColor.
        /// </summary>
        /// <param name="titleText"></param>
        /// <param name="messageText"></param>
        /// <param name="okButtonText"></param>
        /// <param name="cancelButtonText"></param>
        /// <param name="okButtonColor"></param>
        /// <param name="cancelButtonColor"></param>
        /// <param name="okButtonTextColor"></param>
        /// <param name="cancelTextColor"></param>
        /// <returns></returns>
        public static async Task<PermissionPopup> CreateAsync(string titleText, string messageText, string okButtonText = null, string cancelButtonText = null, Color okButtonColor = default, Color cancelButtonColor = default, Color okButtonTextColor = default, Color cancelTextColor = default, Effect effect = Effect.Inquiry, EffectMode effectMode = EffectMode.Default)
        {
            var popup = new PermissionPopup() 
            { 
                Target = null, 
                TitleContent = titleText, 
                Message = messageText, 
                OkButtonContent = okButtonText ?? "OK", 
                CancelButtonContent = cancelButtonText ?? "Cancel",
                PushEffect = effect,
                PushEffectMode = effectMode
            };
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
        /// Create the specified target, titleContent, text, okText, cancelText, okButtonColor, cancelButtonColor, OkButtonContent and cancelTextColor.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="target">Target.</param>
        /// <param name="titleContent">Title.</param>
        /// <param name="messageContent">Text.</param>
        /// <param name="okButtonContent">Ok text.</param>
        /// <param name="cancelButtonContent">Cancel text.</param>
        /// <param name="okButtonColor">Ok button color.</param>
        /// <param name="cancelButtonColor">Cancel button color.</param>
        /// <param name="okButtonTextColor">Ok text color.</param>
        /// <param name="cancelTextColor">Cancel text color.</param>
        public static async Task<PermissionPopup> CreateAsync(UIElement target, object titleContent, object messageContent, object okButtonContent = null, object cancelButtonContent = null, Color okButtonColor = default, Color cancelButtonColor = default, Color okButtonTextColor = default, Color cancelTextColor = default, Effect effect = Effect.Inquiry, EffectMode effectMode = EffectMode.Default)
        {
            var popup = new PermissionPopup() 
            { 
                Target = target, 
                TitleContent = titleContent, 
                Message = messageContent, 
                OkButtonContent = okButtonContent ?? "OK", 
                CancelButtonContent = cancelButtonContent ?? "Cancel",
                PushEffect = effect,
                PushEffectMode = effectMode
            };
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
        async void OnCancelButtonClickedAsync(object sender, RoutedEventArgs e)
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
            _cancelButton.Click += OnCancelButtonClickedAsync;
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
            _cancelButton.Click -= OnCancelButtonClickedAsync;

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
