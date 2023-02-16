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
    public partial class PopupHostFrame : Grid, INavigate
    {

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
        Frame InnerFrame = new Frame();
        #endregion


        #region Construction
        public PopupFrame()
        {
            Children.Add(InnerFrame);
        }
        #endregion


        #region Frame Methods
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





    }
}