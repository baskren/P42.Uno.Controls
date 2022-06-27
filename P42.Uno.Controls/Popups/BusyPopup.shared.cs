using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    /// <summary>
    /// BusyPopup: popup with spinner, title and message
    /// </summary>
    [Windows.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class BusyPopup : Toast
    {
        #region Properties

        #region SpinnerBrush Property
        internal static readonly DependencyProperty SpinnerBrushProperty = DependencyProperty.Register(
            nameof(SpinnerBrush),
            typeof(Brush),
            typeof(BusyPopup),
            new PropertyMetadata(new SolidColorBrush(Utils.Uno.SystemColors.Accent), new PropertyChangedCallback(OnSpinnerBrushChanged))
        );
        private static void OnSpinnerBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BusyPopup popup && popup._progressRing is ProgressRing ring)
            {
                if (e.NewValue is null)
                    ring.Foreground = new SolidColorBrush(Utils.Uno.SystemColors.Accent);
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
        /// Create and present with message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="popAfter"></param>
        /// <returns></returns>
        public static new async Task<BusyPopup> CreateAsync(object message, TimeSpan popAfter = default)
        {
            var result = new BusyPopup {Message = message, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        /// <summary>
        /// Create and present the specified title and text.
        /// </summary>
        /// <param name="titleContent"></param>
        /// <param name="message"></param>
        /// <param name="popAfter">Will dissappear after popAfter TimeSpan</param>
        /// <returns></returns>
        public static new async Task<BusyPopup> CreateAsync(object titleContent, object message, TimeSpan popAfter = default)
        {
            var result = new BusyPopup { TitleContent = titleContent, Message = message, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        /// <summary>
        /// Create and present, pointing towards target, with title and message
        /// </summary>
        /// <param name="target"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="popAfter"></param>
        /// <returns></returns>
        public static new async Task<BusyPopup> CreateAsync(UIElement target, object title,  object message, TimeSpan popAfter = default)
        {
            var result = new BusyPopup { Target = target, TitleContent = title, Message = message, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }
        #endregion


        #region Construction / Initialization
        /// <summary>
        /// Construction
        /// </summary>
        public BusyPopup()
        {
            Build();
        }
        #endregion



        protected override void OnPushPopStateChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPushPopStateChanged(e);
            _progressRing.IsActive = PushPopState == PushPopState.Pushed;
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
                _progressRing.RowCol(0, 0);
                _progressRing.RowSpan(_messageBlock.IsVisible() ? 2 : 1);
            }
            else if (_messageBlock.IsVisible())
            {
                _progressRing.RowCol(1, 0);
                _progressRing.RowSpan(1);
            }
            else
            {
                _progressRing.RowCol(0, 0);
                _progressRing.RowSpan(1);
            }

        }
    }
}
