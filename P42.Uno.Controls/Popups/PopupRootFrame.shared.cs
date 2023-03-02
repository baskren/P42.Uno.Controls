using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;
#if !HAS_UNO
using System;
using System.Collections.Generic;
#endif

namespace P42.Uno.Controls
{
    public partial class PopupRootFrame : Grid, INavigate
    {

        static PopupRootFrame _current;
        public static PopupRootFrame Current => _current = _current ?? Attach();


        #region Frame Properties

        public int BackStackDepth => InnerFrame.BackStackDepth;

        public IList<PageStackEntry> BackStack => InnerFrame.BackStack;

        public int CacheSize
        {
            get => InnerFrame.CacheSize;
            set => InnerFrame.CacheSize = value;
        }

        public bool CanGoBack => InnerFrame.CanGoBack;

        public bool CanGoForward => InnerFrame.CanGoForward;

        public Type CurrentSourcePageType => InnerFrame.CurrentSourcePageType;

        public IList<PageStackEntry> ForwardStack => InnerFrame.ForwardStack;

        public bool IsNavigationStackEnabled
        {
            get => InnerFrame.IsNavigationStackEnabled;
            set => InnerFrame.IsNavigationStackEnabled= value;
        }

        public Type SourcePageType
        {
            get => InnerFrame.SourcePageType;
            set => InnerFrame.SourcePageType = value;
        }
        #endregion


        #region Frame Events

        public event NavigatedEventHandler Navigated
        {
            add => InnerFrame.Navigated += value;
            remove => InnerFrame.Navigated -= value;
        }

        public event NavigatingCancelEventHandler Navigating
        {
            add => InnerFrame.Navigating += value;
            remove => InnerFrame.Navigating -= value;
        }

        public event NavigationFailedEventHandler NavigationFailed
        {
            add => InnerFrame.NavigationFailed+= value;
            remove => InnerFrame.NavigationFailed -= value;
        }

        public event NavigationStoppedEventHandler NavigationStopped
        {
            add => InnerFrame.NavigationStopped += value;
            remove => InnerFrame.NavigationStopped -= value;
        }
        #endregion


        #region Fields
        Frame InnerFrame;
        #endregion


        #region Construction
        internal PopupRootFrame()
        {
            //Children.Add(InnerFrame);
        }
        #endregion


        #region INavigate Methods
        public string GetNavigationState()
            => InnerFrame.GetNavigationState();

        public void GoBack()
            => InnerFrame.GoBack();

        public void GoBack(NavigationTransitionInfo transitionInfoOverride)
            => InnerFrame.GoBack(transitionInfoOverride);

        public void GoForward()
            => InnerFrame.GoForward();

        public bool Navigate(Type sourcePageType)
            => InnerFrame.Navigate(sourcePageType);

        public bool Navigate(Type sourcePageType, object parameter)
            => InnerFrame.Navigate(sourcePageType, parameter);

        public bool Navigate(Type sourcePageType, object parameter, NavigationTransitionInfo infoOverride)
            => InnerFrame.Navigate(sourcePageType, parameter, infoOverride);

        public void SetNavigationState(string navigationState)
            => InnerFrame.SetNavigationState(navigationState);
        #endregion


        #region Apply
        static PopupRootFrame Attach()
        {
            var targetFrame = P42.Utils.Uno.Platform.Window.Content as Frame;

            while (targetFrame is Frame)
            {
                if (targetFrame.Parent is PopupRootFrame current)
                    return current;
                if (targetFrame.Parent is null)
                    break;
                if (targetFrame.Parent is Frame parentFrame)
                    targetFrame = parentFrame;
                else
                    throw new Exception("Unexpected visual tree");
            }

            var popupRootFrame = new PopupRootFrame();
            P42.Utils.Uno.Platform.Window.Content = popupRootFrame;
            popupRootFrame.Children.Add(targetFrame);

            return popupRootFrame;
        }

        #endregion



    }
}