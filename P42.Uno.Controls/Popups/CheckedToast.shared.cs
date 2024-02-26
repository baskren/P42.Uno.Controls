using Microsoft.UI.Xaml.Controls;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;
using Windows.UI;

namespace P42.Uno.Controls
{
    /// <summary>
    /// Alert with "Do not notify me again" check box
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class CheckedToast : Alert
    {
        #region Properties
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            nameof(IsChecked),
            typeof(bool),
            typeof(CheckedToast),
            new PropertyMetadata(default(bool))
            );
        /// <summary>
        /// Is the "Do not alert me again" check box checked?
        /// </summary>
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public static readonly DependencyProperty CheckContentProperty = DependencyProperty.Register(
            nameof(CheckContent),
            typeof(object),
            typeof(CheckedToast),
            new PropertyMetadata("Got it.  Do not alert me again.")
            );
        /// <summary>
        /// Alternative content for "Do not alert me again" check box
        /// </summary>
        public object CheckContent
        {
            get => (object)GetValue(CheckContentProperty);
            set => SetValue(CheckContentProperty, value);
        }
        #endregion


        #region Construction / Initialization
        /// <summary>
        /// Create the CheckedToast
        /// </summary>
        /// <param name="titleText"></param>
        /// <param name="messageText"></param>
        /// <param name="okButtonText"></param>
        /// <param name="okButtonColor"></param>
        /// <param name="okTextColor"></param>
        /// <returns></returns>
        public static new async Task<CheckedToast> CreateAsync(string titleText, string messageText, string okButtonText = null, Color okButtonColor = default, Color okTextColor = default, Effect effect = Effect.Alarm, EffectMode effectMode = EffectMode.Default)
        {
            var popup = new CheckedToast() 
            { 
                TitleContent = titleText, 
                Message = messageText, 
                OkButtonContent = okButtonText ?? "OK" ,
                PushEffect = effect,
                PushEffectMode = effectMode
            };
            if (okTextColor != default)
                popup.OkButtonForeground = okTextColor.ToBrush();
            if (okButtonColor != default)
                popup.OkButtonBackground = okButtonColor.ToBrush();
            await popup.PushAsync();
            return popup;
        }

        /// <summary>
        /// Creates the CheckedToast
        /// </summary>
        /// <param name="target"></param>
        /// <param name="titleContent"></param>
        /// <param name="messageContent"></param>
        /// <param name="okButtonContent"></param>
        /// <param name="okButtonColor"></param>
        /// <param name="okTextColor"></param>
        /// <returns></returns>
        public static new async Task<CheckedToast> CreateAsync(UIElement target, object titleContent, object messageContent, object okButtonContent = null, Color okButtonColor = default, Color okTextColor = default, Effect effect = Effect.Alarm, EffectMode effectMode = EffectMode.Default)
        {
            var popup = new CheckedToast(target) 
            { 
                TitleContent = titleContent, 
                Message = messageContent, 
                OkButtonContent = okButtonContent ?? "OK" ,
                PushEffect = effect,
                PushEffectMode = effectMode
            };
            if (okTextColor != default)
                popup.OkButtonForeground = okTextColor.ToBrush();
            if (okButtonColor != default)
                popup.OkButtonBackground = okButtonColor.ToBrush();
            await popup.PushAsync();
            return popup;
        }

        /// <summary>
        /// Construction
        /// </summary>
        public CheckedToast(UIElement target = null) : base(target)
        {
            Build();
        }

        #endregion


        #region Event Handlers
        protected override void OnOkButtonClickedAsync(object sender, RoutedEventArgs e)
        {
            base.OnOkButtonClickedAsync(sender, e);
        }
        #endregion
    }
}
