using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    public partial class Toast : TargetedPopup
    {
        #region Title Property
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(Toast),
            new PropertyMetadata(default(string))
        );
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        #endregion Title Property


        #region Factory
        /// <summary>
        /// Create and present the specified title and text.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="popAfter">Will dissappear after popAfter TimeSpan</param>
        /// <returns></returns>
        public static async Task<Toast> Create(string title, object content, TimeSpan popAfter = default)
        {
            var result = new Toast { Title = title, Content = content, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }
        #endregion


        #region Construction
        public Toast()
        {
            DefaultStyleKey = typeof(Toast);
        }
        #endregion


    }
}
