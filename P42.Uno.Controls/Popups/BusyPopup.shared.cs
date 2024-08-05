using System;
using System.Threading.Tasks;
using P42.Uno.Markup;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    /// <summary>
    /// BusyPopup: popup with spinner, title and messageContent
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class BusyPopup : Toast
    {
        #region Properties

        #region SpinnerBrush Property
        internal static readonly DependencyProperty SpinnerBrushProperty = DependencyProperty.Register(
            nameof(SpinnerBrush),
            typeof(Brush),
            typeof(BusyPopup),
            new PropertyMetadata(new SolidColorBrush(P42.Uno.Markup.SystemColors.Accent), new PropertyChangedCallback(OnSpinnerBrushChanged))
        );
        private static void OnSpinnerBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BusyPopup popup && popup.ProgressRing is ProgressRing ring)
            {
                if (e.NewValue is null)
                    ring.Foreground = new SolidColorBrush(P42.Uno.Markup.SystemColors.Accent);
                else
                    ring.Foreground = (Brush)e.NewValue;
            }
        }
        /// <summary>
        /// Brush for Spinner
        /// </summary>
        internal Brush SpinnerBrush
        {
            get => (Brush)GetValue(SpinnerBrushProperty);
            set => SetValue(SpinnerBrushProperty, value);
        }
        #endregion SpinnerBrush Property

        #endregion


        #region Factory
        /// <summary>
        /// Create and present with messageContent
        /// </summary>
        /// <param name="messageContent"></param>
        /// <param name="popAfter"></param>
        /// <returns></returns>
        public static async Task<BusyPopup> CreateAsync(object messageContent, TimeSpan popAfter = default)
        {
            var result = new BusyPopup {Message = messageContent, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        /// <summary>
        /// Create and present the specified title and text.
        /// </summary>
        /// <param name="titleContent"></param>
        /// <param name="messageContent"></param>
        /// <param name="popAfter">Will disappear after popAfter TimeSpan</param>
        /// <returns></returns>
        public static async Task<BusyPopup> CreateAsync(object titleContent, object messageContent, TimeSpan popAfter = default)
        {
            var result = new BusyPopup { TitleContent = titleContent, Message = messageContent, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        /// <summary>
        /// Create and present, pointing towards target, with title and messageContent
        /// </summary>
        /// <param name="target"></param>
        /// <param name="title"></param>
        /// <param name="messageContent"></param>
        /// <param name="popAfter"></param>
        /// <returns></returns>
        public static async Task<BusyPopup> CreateAsync(UIElement target, object title,  object messageContent, TimeSpan popAfter = default)
        {
            var result = new BusyPopup(target) 
            { 
                TitleContent = title, 
                Message = messageContent, 
                PopAfter = popAfter
            };
            await result.PushAsync();
            return result;
        }
        #endregion


        #region Construction / Initialization
        /// <summary>
        /// Construction
        /// </summary>
        public BusyPopup() : this(null) {}

        public BusyPopup(UIElement target) : base(target)
        {
            Build();
        }
        #endregion



        protected override void OnPushPopStateChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPushPopStateChanged(e);
            ProgressRing.IsActive = PushPopState == PushPopState.Pushed;
        }

        protected override void OnTitleChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnTitleChanged(args);
            UpdateProgressRingLocation();
        }

        protected override void OnMessageChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnMessageChanged(args);
            UpdateProgressRingLocation();
        }

        protected virtual void UpdateProgressRingLocation()
        {
            if (_titleBlock.IsVisible())
            {
                ProgressRing.RowCol(0, 0);
                ProgressRing.RowSpan(_messageBlock.IsVisible() ? 2 : 1);
            }
            else if (_messageBlock.IsVisible())
            {
                ProgressRing.RowCol(1, 0);
                ProgressRing.RowSpan(1);
            }
            else
            {
                ProgressRing.RowCol(0, 0);
                ProgressRing.RowSpan(1);
            }

        }
    }
}
