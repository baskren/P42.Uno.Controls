using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    [TemplatePart(Name = ProgressRingName, Type = typeof(ProgressRing))]
    public partial class BusyPopup : Toast
    {
        #region Properties

        #region SpinnerBrush Property
        internal static readonly DependencyProperty SpinnerBrushProperty = DependencyProperty.Register(
            nameof(SpinnerBrush),
            typeof(Brush),
            typeof(BusyPopup),
            new PropertyMetadata(new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]), new PropertyChangedCallback(OnSpinnerBrushChanged))
        );
        private static void OnSpinnerBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BusyPopup popup && popup._progressRing is ProgressRing ring)
            {
                if (e.NewValue is null)
                    ring.Foreground = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);
                else
                    ring.Foreground = (Brush)e.NewValue;
            }
        }
        internal Brush SpinnerBrush
        {
            get => (Brush)GetValue(SpinnerBrushProperty);
            set => SetValue(SpinnerBrushProperty, value);
        }
        #endregion SpinnerBrush Property

        #endregion


        #region Factory
        public static new async Task<BusyPopup> CreateAsync(object content, TimeSpan popAfter = default)
        {
            var result = new BusyPopup {Content = content, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        /// <summary>
        /// Create and present the specified title and text.
        /// </summary>
        /// <param name="titleContent"></param>
        /// <param name="content"></param>
        /// <param name="popAfter">Will dissappear after popAfter TimeSpan</param>
        /// <returns></returns>
        public static new async Task<BusyPopup> CreateAsync(object titleContent, object content, TimeSpan popAfter = default)
        {
            var result = new BusyPopup { TitleContent = titleContent, Content = content, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        public static new async Task<BusyPopup> CreateAsync(UIElement target, object content, TimeSpan popAfter = default)
        {
            var result = new BusyPopup { Target = target, Content = content, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        public static new async Task<BusyPopup> CreateAsync(UIElement target, object titleContent, object content, TimeSpan popAfter = default)
        {
            var result = new BusyPopup { Target = target, TitleContent = titleContent, Content = content, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }
        #endregion


        #region Fields
        const string ProgressRingName = "_progressRing";
        ProgressRing _progressRing;
        #endregion


        #region Construction / Initialization
        public BusyPopup()
        {
            DefaultStyleKey = typeof(BusyPopup);
        }

        protected override void OnApplyTemplate()
        {
            _progressRing = (ProgressRing)GetTemplateChild(ProgressRingName);
            // must call base.OnApplyTemplate last so completion handler won't be called until template loading is truly complete
            base.OnApplyTemplate();
        }
        #endregion



        protected override void OnPushPopStateChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPushPopStateChanged(e);
            _progressRing.IsActive = PushPopState == PushPopState.Pushed;
        }
    }
}
