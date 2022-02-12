using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

namespace P42.Uno.Controls
{
    [Windows.UI.Xaml.Data.Bindable]
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

        protected virtual void OnTitleChanged(DependencyPropertyChangedEventArgs args)
        {
            _titleBlock.Content = args.NewValue;
            if (args.NewValue is TextBlock tb)
            {
                var t = tb.Text;
                if (string.IsNullOrWhiteSpace(t))
                    t = tb.GetHtml();
                _titleBlock.Collapsed(string.IsNullOrWhiteSpace(t));
            }
            else if (args.NewValue is string text)
                _titleBlock.Collapsed(string.IsNullOrWhiteSpace(text));
            else
                _titleBlock.Visible();
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

        protected virtual void OnMessageChanged(DependencyPropertyChangedEventArgs args)
        {
            _messageBlock.Content = args.NewValue;
            if (args.NewValue is TextBlock tb)
            {
                var t = tb.Text;
                if (string.IsNullOrWhiteSpace(t))
                    t = tb.GetHtml();
                _messageBlock.Collapsed(string.IsNullOrWhiteSpace(t));
            }
            else if (args.NewValue is string text)
                _messageBlock.Collapsed(string.IsNullOrWhiteSpace(text));
            else
                _messageBlock.Visible();
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

        public bool IsMessageScrollable
        {
            get => (bool)GetValue(IsMessageScrollableProperty);
            set => SetValue(IsMessageScrollableProperty, value);
        }
        #endregion IsMessageScrollable Property


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

            Loaded += OnLoaded;
        }

        protected override Task OnPushBeginAsync()
        {
            UpdateScrollMaxHeight();
            Windows.UI.Xaml.Window.Current.SizeChanged += OnCurrentWindow_SizeChanged;
            return base.OnPushBeginAsync();
        }

        protected override Task OnPopBeginAsync()
        {
            Windows.UI.Xaml.Window.Current.SizeChanged -= OnCurrentWindow_SizeChanged;
            return base.OnPopBeginAsync();
        }
        #endregion

        protected override void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
        {
            base.OnBorderSizeChanged(sender, args);

            var size = _messageBlock.DesiredSize;
            _messageBlock.Measure(new Windows.Foundation.Size(size.Width, 2 * size.Height));
            var desiredSize = _messageBlock.DesiredSize;

            IsMessageScrollable = desiredSize.Height > size.Height;
            System.Diagnostics.Debug.WriteLine($"Toast.OnBorderSizeChanged height[{size.Height}] desiredHeight[{desiredSize.Height}]");
        }


        private void OnCurrentWindow_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
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
