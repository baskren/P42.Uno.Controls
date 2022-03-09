using Windows.UI.Xaml.Controls;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.UI;

namespace P42.Uno.Controls
{
    public partial class CheckedToast : Alert
    {
        #region Properties
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            nameof(IsChecked),
            typeof(bool),
            typeof(CheckedToast),
            new PropertyMetadata(default(bool))
            );
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
        public object CheckContent
        {
            get => (object)GetValue(CheckContentProperty);
            set => SetValue(CheckContentProperty, value);
        }
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
        public static new async Task<CheckedToast> CreateAsync(object title, object message, object okContent = null, Color okButtonColor = default, Color okTextColor = default)
        {
            var popup = new CheckedToast { TitleContent = title, Message = message, OkButtonContent = okContent ?? "OK" };
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
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="okContent"></param>
        /// <param name="okButtonColor"></param>
        /// <param name="okTextColor"></param>
        /// <returns></returns>
        public static new async Task<CheckedToast> CreateAsync(UIElement target, object title, object message, object okContent = null, Color okButtonColor = default, Color okTextColor = default)
        {
            var popup = new CheckedToast() { Target = target, TitleContent = title, Message = message, OkButtonContent = okContent ?? "OK" };
            if (okTextColor != default)
                popup.OkButtonForeground = okTextColor.ToBrush();
            if (okButtonColor != default)
                popup.OkButtonBackground = okButtonColor.ToBrush();
            await popup.PushAsync();
            return popup;
        }


        public CheckedToast() : base()
        {
            Build();
        }

        #endregion


        #region Event Handlers
        protected override void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            base.OnOkButtonClicked(sender, e);
        }
        #endregion
    }
}