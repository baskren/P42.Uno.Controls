using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    [TemplatePart(Name = TitleBlockName, Type = typeof(TextBlock))]
    public partial class Toast : TargetedPopup
    {
        #region Title Property
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(TitleContent),
            typeof(object),
            typeof(Toast),
            new PropertyMetadata(null, new PropertyChangedCallback(OnTitleContentChanged))
        );
        private static void OnTitleContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            /*
            if (d is BusyPopup popup)
            {
                if (e.NewValue is null)
                    popup._titleBlock.Visibility = Visibility.Collapsed;
                else if (e.NewValue is string str)
                    popup._titleBlock.Visibility = string.IsNullOrEmpty(str) ? Visibility.Collapsed : Visibility.Visible;
                else
                    popup._titleBlock.Visibility = Visibility.Visible;
            }
            */
        }
        public object TitleContent
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        #endregion Title Property


        #region Fields
        const string TitleBlockName = "_titleBlock";
        ContentPresenter _titleBlock;
        #endregion


        #region Factory
        public static async Task<Toast> CreateAsync(object content, TimeSpan popAfter = default)
        {
            var result = new Toast { Content = content, PopAfter = popAfter, };
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
        public static async Task<Toast> CreateAsync(object titleContent, object content, TimeSpan popAfter = default)
        {
            var result = new Toast { TitleContent = titleContent, Content = content, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        public static async Task<Toast> CreateAsync(UIElement target,object content, TimeSpan popAfter = default)
        {
            var result = new Toast { Target = target, Content = content, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        public static async Task<Toast> CreateAsync(UIElement target, object titleContent, object content, TimeSpan popAfter = default)
        {
            var result = new Toast { Target = target, TitleContent = titleContent, Content = content, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }
        #endregion


        #region Construction / Initialization
        public Toast()
        {
            DefaultStyleKey = typeof(Toast);
        }

        protected override void OnApplyTemplate()
        {
            _titleBlock = (ContentPresenter)GetTemplateChild(TitleBlockName);
            // must call base.OnApplyTemplate last so completion handler won't be called until template loading is truly complete
            base.OnApplyTemplate();
        }
        #endregion


    }
}
