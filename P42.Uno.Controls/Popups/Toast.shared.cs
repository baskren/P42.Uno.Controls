using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Windows.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;

namespace P42.Uno.Controls
{
    /// <summary>
    /// A simple toast
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    [ContentProperty(Name = "Message")]
    public partial class Toast : TargetedPopup
    {
        #region Title Property
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(TitleContent),
            typeof(object),
            typeof(Toast),
            new PropertyMetadata(null, OnTitleChanged)
        );

        private static void OnTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is Toast t)
                t.OnTitleChanged(args);
        }
        /// <summary>
        /// the title's string or UIElement
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnTitleChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is TextBlock tb)
            {
                var t = tb.Text;
                if (string.IsNullOrWhiteSpace(t))
                    t = tb.GetHtml();
                _titleBlock.Collapsed(string.IsNullOrWhiteSpace(t));
                _titleBlock.Content = tb;
            }
            else if (args.NewValue is string text)
            {
                _titleBlock.Collapsed(string.IsNullOrWhiteSpace(text));
                _titleBlock.Content = new TextBlock { Text = text }.BindFont(_titleBlock);
            }
            else
            {
                _titleBlock.Content = args.NewValue;
                _titleBlock.Visible();
            }
        }

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
            new PropertyMetadata(default(object), OnMessageChanged)
        );

        private static void OnMessageChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is Toast t)
                t.OnMessageChanged(args);
        }
        /// <summary>
        /// The messageContent's string or UIElement
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnMessageChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is TextBlock tb)
            {
                var t = tb.Text;
                if (string.IsNullOrWhiteSpace(t))
                    t = tb.GetHtml();
                _messageBlock.Collapsed(string.IsNullOrWhiteSpace(t));
                _messageBlock.Content = tb;
            }
            else if (args.NewValue is string text)
            {
                _messageBlock.Collapsed(string.IsNullOrWhiteSpace(text));
                _messageBlock.Content = new TextBlock { Text = text }.BindFont(_messageBlock);
            }
            else
            {
                _messageBlock.Content = args.NewValue;
                _messageBlock.Visible();
            }
        }

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
            new PropertyMetadata(default(IconElement), OnIconElementChanged)
        );

        private static void OnIconElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is Toast toast)
                toast._iconPresenter.Content = args.NewValue;
        }
        /// <summary>
        /// The icon
        /// </summary>
        public IconElement IconElement
        {
            get => (IconElement)GetValue(IconElementProperty);
            set => SetValue(IconElementProperty, value);
        }
        #endregion IconElement Property


        #region IsMessageScrollable Property
        public static readonly DependencyProperty IsMessageScrollableProperty = DependencyProperty.Register(
            nameof(IsMessageScrollable),
            typeof(bool),
            typeof(Toast),
            new PropertyMetadata(default(bool), OnIsScrollableChanged)
        );

        private static void OnIsScrollableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Toast toast)
            {
                if (toast.IsMessageScrollable)
                {
                    if (!toast._bubbleContentGrid.Children.Any(c => c is ScrollViewer))
                    {
                        toast._bubbleContentGrid.Children.Add(new ScrollViewer()
                            .RowCol(1, 1)
                            .Content(toast._messageBlock)
                            );
                    }
                }
                else if (toast._bubbleContentGrid.Children.FirstOrDefault(c=>c is ScrollViewer) is ScrollViewer scroll)
                {
                    toast._bubbleContentGrid.Children.Remove(scroll);
                    toast._bubbleContentGrid.Children.Add(toast._messageBlock);
                }
            }
        }
        /// <summary>
        /// Can we scroll if the messageContent exceeds the available space?
        /// </summary>
        public bool IsMessageScrollable
        {
            get => (bool)GetValue(IsMessageScrollableProperty);
            set => SetValue(IsMessageScrollableProperty, value);
        }
        #endregion IsMessageScrollable Property


        #region Factory
        /// <summary>
        /// Create and present the Toast
        /// </summary>
        /// <param name="messageContent"></param>
        /// <param name="popAfter"></param>
        /// <returns></returns>
        public static async Task<Toast> CreateAsync(object messageContent, TimeSpan popAfter = default, Effect effect = Effect.Info, EffectMode effectMode = EffectMode.Default)
        {
            var result = new Toast 
            { 
                Message = messageContent,
                PopAfter = popAfter,
                PushEffect = effect,
                PushEffectMode = effectMode
            };
            await result.PushAsync();
            return result;
        }

        /// <summary>
        /// Create and present the specified title and text.
        /// </summary>
        /// <param name="titleContent"></param>
        /// <param name="messageContent"></param>
        /// <param name="popAfter">Will dissappear after popAfter TimeSpan</param>
        /// <returns></returns>
        public static async Task<Toast> CreateAsync(object titleContent, object messageContent, TimeSpan popAfter = default, Effect effect = Effect.Info, EffectMode effectMode = EffectMode.Default)
        {
            var result = new Toast 
            { 
                TitleContent = titleContent, 
                Message = messageContent,
                PopAfter = popAfter,
                PushEffect = effect,
                PushEffectMode = effectMode
            };
            await result.PushAsync();
            return result;
        }

        /// <summary>
        /// Create and present the Toast
        /// </summary>
        /// <param name="target"></param>
        /// <param name="titleContent"></param>
        /// <param name="messageContent"></param>
        /// <param name="popAfter"></param>
        /// <returns></returns>
        public static async Task<Toast> CreateAsync(UIElement target, object titleContent, object messageContent, TimeSpan popAfter = default, Effect effect = Effect.Info, EffectMode effectMode = EffectMode.Default)
        {
            var result = new Toast 
            { 
                Target = target, 
                TitleContent = titleContent, 
                Message = messageContent,
                PopAfter = popAfter,
                PushEffect = effect,
                PushEffectMode = effectMode
            };
            await result.PushAsync();
            return result;
        }
        #endregion


        #region Construction / Initialization
        /// <summary>
        /// Constructor
        /// </summary>
        public Toast()
        {
            Build();

            Loaded += OnLoaded;
        }

        protected override Task OnPushBeginAsync()
        {
            UpdateScrollMaxHeight();
            P42.Utils.Uno.Platform.Window.SizeChanged += OnCurrentWindow_SizeChanged;
            return base.OnPushBeginAsync();
        }

        protected override Task OnPopBeginAsync()
        {
            P42.Utils.Uno.Platform.Window.SizeChanged -= OnCurrentWindow_SizeChanged;
            return base.OnPopBeginAsync();
        }
        #endregion

        /*
        protected override void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
        {
            base.OnBorderSizeChanged(sender, args);

            var size = _messageBlock.DesiredSize;
            _messageBlock.Measure(new Windows.Foundation.Size(size.Width, 2 * size.Height));
            var desiredSize = _messageBlock.DesiredSize;

            IsMessageScrollable = desiredSize.Height > size.Height;
            System.Diagnostics.Debug.WriteLine($"Toast.OnBorderSizeChanged height[{size.Height}] desiredHeight[{desiredSize.Height}]");
        }
        */

        private void OnCurrentWindow_SizeChanged(object sender, Microsoft.UI.Xaml.WindowSizeChangedEventArgs e)
        {
            UpdateScrollMaxHeight();
        }

        protected void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateScrollMaxHeight();
        }
        void UpdateScrollMaxHeight()
        {
            //scrollViewer.MaxHeight = AppWindow.Size(this).Height - Margin.Vertical() - Padding.Vertical() - _titleRowDefinition.ActualHeight - 200;
        }
    }
}
