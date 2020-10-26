﻿using System;
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
            nameof(TitleContent),
            typeof(object),
            typeof(Toast),
            new PropertyMetadata(null)
        );
        public object TitleContent
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        #endregion Title Property


        #region Message Property
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            nameof(Message),
            typeof(object),
            typeof(Toast),
            new PropertyMetadata(default(object))
        );
        public object Message
        {
            get => (object)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
        #endregion Message Property


        #region IconElement Property
        public static readonly DependencyProperty IconElementProperty = DependencyProperty.Register(
            nameof(IconElement),
            typeof(IconElement),
            typeof(Toast),
            new PropertyMetadata(default(IconElement))
        );
        public IconElement IconElement
        {
            get => (IconElement)GetValue(IconElementProperty);
            set => SetValue(IconElementProperty, value);
        }
        #endregion IconElement Property


        #region Factory
        public static async Task<Toast> CreateAsync(object message, TimeSpan popAfter = default)
        {
            var result = new Toast { Message = message, PopAfter = popAfter, };
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
        public static async Task<Toast> CreateAsync(object titleContent, object message, TimeSpan popAfter = default)
        {
            var result = new Toast { TitleContent = titleContent, Message = message, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }

        public static async Task<Toast> CreateAsync(UIElement target, object titleContent, object message, TimeSpan popAfter = default)
        {
            var result = new Toast { Target = target, TitleContent = titleContent, Message = message, PopAfter = popAfter, };
            await result.PushAsync();
            return result;
        }
        #endregion


        #region Construction / Initialization
        public Toast()
        {
            Build();
        }
        #endregion


    }
}
