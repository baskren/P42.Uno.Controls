using System;
using System.Collections.Generic;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;

namespace P42.Uno.Controls
{
    public partial class RootFrame : Frame
    {


        #region Frame Properties

        public new int BackStackDepth => InnerFrame?.BackStackDepth ?? base.BackStackDepth;

        public new IList<PageStackEntry> BackStack => InnerFrame?.BackStack ?? base.BackStack;

        public new int CacheSize
        {
            get => InnerFrame?.CacheSize ?? base.CacheSize;
            set
            {
                if (InnerFrame is null)
                    base.CacheSize = value;
                else
                    InnerFrame.CacheSize = value;
            }
        }

        public new bool CanGoBack => InnerFrame?.CanGoBack ?? base.CanGoBack;

        public new bool CanGoForward => InnerFrame?.CanGoForward ?? base.CanGoForward;

        public new Type CurrentSourcePageType => InnerFrame?.CurrentSourcePageType ?? base.CurrentSourcePageType;

        public new IList<PageStackEntry> ForwardStack => InnerFrame?.ForwardStack ?? base.ForwardStack;

        public new bool IsNavigationStackEnabled
        {
            get => InnerFrame?.IsNavigationStackEnabled ?? base.IsNavigationStackEnabled;
            set
            {
                if (InnerFrame is null)
                    base.IsNavigationStackEnabled = value;
                else
                    InnerFrame.IsNavigationStackEnabled = value;
            }
        }

        public new Type SourcePageType
        {
            get => InnerFrame?.SourcePageType ?? base.SourcePageType;
            set
            {
                if (InnerFrame is null)
                    base.SourcePageType = value;
                else
                    InnerFrame.SourcePageType = value;
            }
        }
        #endregion


        #region Frame Events

        public new event NavigatedEventHandler Navigated
        {
            add
            {
                if (InnerFrame is null)
                    base.Navigated += value;
                else
                    InnerFrame.Navigated += value;
            }
            remove
            {
                if (InnerFrame is null)
                    base.Navigated -= value;
                else
                    InnerFrame.Navigated -= value;
            }
        }

        public new event NavigatingCancelEventHandler Navigating
        {
            add
            {
                if (InnerFrame is null)
                    base.Navigating += value;
                else
                    InnerFrame.Navigating += value;
            }
            remove
            {
                if (InnerFrame is null)
                    base.Navigating -= value;
                else
                    InnerFrame.Navigating -= value;
            }
        }

        public new event NavigationFailedEventHandler NavigationFailed
        {
            add
            {
                if (InnerFrame is null)
                    base.NavigationFailed += value;
                else
                    InnerFrame.NavigationFailed += value;
            }
            remove
            {
                if (InnerFrame is null)
                    base.NavigationFailed -= value;
                else
                    InnerFrame.NavigationFailed -= value;
            }
        }

        public new event NavigationStoppedEventHandler NavigationStopped
        {
            add
            {
                if (InnerFrame is null)
                    base.NavigationStopped += value;
                else
                    InnerFrame.NavigationStopped += value;
            }
            remove
            {
                if (InnerFrame is null)
                    base.NavigationStopped -= value;
                else
                    InnerFrame.NavigationStopped -= value;
            }
        }
        #endregion


        #region Private Properties
        static Grid _grid;
        static Grid Grid => _grid ??= (Grid)Current.FindChildByName("PopupGrid");

        static RootFrame _current;
        internal static RootFrame Current => _current ??= Inject();
        #endregion


        #region Fields
        Frame InnerFrame;
        #endregion


        #region Construction
        public RootFrame()
        {
            DefaultStyleKey = typeof(RootFrame);
            _current = this;
        }

        public RootFrame(Frame innerFrame) : this()
        {
            InnerFrame = innerFrame;
        }
        #endregion


        #region INavigate Methods
        public new string GetNavigationState()
            => InnerFrame.GetNavigationState();

        public new void GoBack()
            => InnerFrame.GoBack();

        public new void GoBack(NavigationTransitionInfo transitionInfoOverride)
            => InnerFrame.GoBack(transitionInfoOverride);

        public new void GoForward()
            => InnerFrame.GoForward();

        public new bool Navigate(Type sourcePageType)
            => InnerFrame.Navigate(sourcePageType);

        public new bool Navigate(Type sourcePageType, object parameter)
            => InnerFrame.Navigate(sourcePageType, parameter);

        public new bool Navigate(Type sourcePageType, object parameter, NavigationTransitionInfo infoOverride)
            => InnerFrame.Navigate(sourcePageType, parameter, infoOverride);

        public new void SetNavigationState(string navigationState)
            => InnerFrame.SetNavigationState(navigationState);
        #endregion


        #region Apply
        static RootFrame Inject()
        {
            var targetFrame = P42.Utils.Uno.Platform.Window.Content as Frame;

            while (targetFrame is Frame)
            {
                if (targetFrame.Parent is RootFrame current)
                    return current;
                if (targetFrame.Parent is Frame parentFrame)
                    targetFrame = parentFrame;
                else
                    break;
            }

            var rootFrame = new RootFrame();
            P42.Utils.Uno.Platform.Window.Content = null;
            P42.Utils.Uno.Platform.Window.Content = rootFrame;
            rootFrame.InnerFrame = targetFrame;

            return rootFrame;
        }

        public static void HidePopups()
            => Grid.Visibility = Visibility.Collapsed;

        public static void ShowPopups()
            => Grid.Visibility = Visibility.Visible;

        internal static void Add(TargetedPopup popup)
        {
            if (!Grid.Children.Contains(popup.ContentBorder))
            {
                Grid.Children.Add(popup.PageOverlay);
                Grid.Children.Add(popup.ShadowBorder);
                Grid.Children.Add(popup.ContentBorder);
            }
        }

        internal static void Remove(TargetedPopup popup)
        {
            if (Grid.Children.Contains(popup.ContentBorder))
            {
                Grid.Children.Remove(popup.PageOverlay);
                Grid.Children.Remove(popup.ShadowBorder);
                Grid.Children.Remove(popup.ContentBorder);
            }
        }

        #endregion



    }
}