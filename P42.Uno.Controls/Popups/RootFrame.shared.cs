using Microsoft.UI.Xaml.Media.Animation;

namespace P42.Uno.Controls;

[Bindable]
public class RootFrame : Frame
{
    #region Static Properties
    internal static bool Initiated { get; private set; }
    #endregion


    #region Static Fields
    private static RootFrame? m_current = null;
    private static Grid? m_grid = null;
    #endregion


    #region Static Methods 
    internal static async Task<Grid> GetPopupGridAsync()
        => m_grid ??= (Grid)((await GetCurrentAsync()).FindChildByName("PopupGrid") ?? throw new Exception("Failure to access PopupGrid"));

    public static async Task<RootFrame> GetCurrentAsync()
    {
        if (m_current is not null)
            return m_current;

        Frame? targetFrame;
        do
        {
            targetFrame = P42.Utils.Uno.Platform.MainWindow.Content as Frame;
        } while (targetFrame is not null);

        while (targetFrame is not null)
        {
            if (targetFrame.Parent is RootFrame current)
                return current;
            if (targetFrame.Parent is Frame parentFrame)
                targetFrame = parentFrame;
            else
                break;
        }

        var rootFrame = new RootFrame();
        Utils.Uno.Platform.MainWindow.Content = null;
        Utils.Uno.Platform.MainWindow.Content = rootFrame;
        rootFrame.InnerFrame = targetFrame;

        return rootFrame;
    }

    public static bool TryGoBack()
    {
        if (m_current != null && m_current.CanGoBack)
        {
            m_current.GoBack();
            return true;
        }
        return false;
    }
    #endregion


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


    #region Fields
    private Frame? InnerFrame;
    #endregion


    #region Construction
    public RootFrame()
    {
        DefaultStyleKey = typeof(RootFrame);
        m_current = this;
        SizeChanged += Popups.OnRootFrameSizeChanged;
        Initiated = true;
    }

    private RootFrame(Frame innerFrame) : this()
    {
        InnerFrame = innerFrame;
    }

    #endregion


    #region INavigate Methods
    public new string GetNavigationState()
        => InnerFrame?.GetNavigationState() ?? base.GetNavigationState();

    public new void GoBack()
    {
        if (InnerFrame is null)
            base.GoBack();
        else
            InnerFrame.GoBack();
    }

    public new void GoBack(NavigationTransitionInfo transitionInfoOverride)
    {
        if (InnerFrame is null)
            base.GoBack(transitionInfoOverride);
        else
            InnerFrame.GoBack(transitionInfoOverride);
    }

    public new void GoForward()
    {
        if (InnerFrame is null)
            base.GoForward();
        else
            InnerFrame.GoForward();
    }

    public new bool Navigate(Type sourcePageType)
        => InnerFrame?.Navigate(sourcePageType) ?? base.Navigate(sourcePageType);
         
    public new bool Navigate(Type sourcePageType, object parameter)
        => InnerFrame?.Navigate(sourcePageType, parameter) ?? base.Navigate(sourcePageType, parameter);

    public new bool Navigate(Type sourcePageType, object parameter, NavigationTransitionInfo infoOverride)
        => InnerFrame?.Navigate(sourcePageType, parameter, infoOverride) ?? base.Navigate(sourcePageType, parameter, infoOverride);

    public new void SetNavigationState(string navigationState)
    {
        if (InnerFrame is null)
            base.SetNavigationState(navigationState);
        else
            InnerFrame.SetNavigationState(navigationState);
    }
    #endregion



}
