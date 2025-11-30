using P42.Uno.Controls.AnimateBar;

namespace P42.Uno.Controls;

[Bindable]
internal class LoopingFlipViewItem : Grid, IEventSubscriber
{
    #region Foreground Property
    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
        nameof(Foreground),
        typeof(Brush),
        typeof(LoopingFlipViewItem),
        new PropertyMetadata(SystemColors.BaseHigh.ToBrush())
    );
#if ANDROID
        public new Brush Foreground 
#else
    public Brush Foreground
#endif
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }
    #endregion Foreground Property



    internal UIElement Child;

    private Left LeftBar = new();


    private Right RightBar = new();

    public LoopingFlipViewItem(UIElement child)
    {
        RightBar.WBind(Base.ForegroundProperty, this, ForegroundProperty);
        LeftBar.WBind(Base.ForegroundProperty, this, ForegroundProperty);

        Child = child;
        Children.Add(child);
        Children.Add(RightBar);
        Children.Add(LeftBar);

        RightBar.Tapped += OnBarTapped;
        LeftBar.Tapped += OnBarTapped;
    }



    private void OnBarTapped(object sender, TappedRoutedEventArgs e)
    {
        if (Parent is Grid _grid && _grid.Parent is LoopingFlipView flipView)
        {
            if (sender is Base bar)
            {
                if (bar == LeftBar)
                    flipView.SelectedIndex--;
                else if (bar == RightBar)
                    flipView.SelectedIndex++;
            }
        }
    }

    public void EnableEvents()
    {
        if (Child is IEventSubscriber eventSubscriber)
            eventSubscriber.EnableEvents();
    }

    public void DisableEvents()
    {
        if (Child is IEventSubscriber eventSubscriber)
            eventSubscriber.DisableEvents();
    }

    public bool AreEventsEnabled
    {
        get
        {
            if (Child is IEventSubscriber subscriber)
                return subscriber.AreEventsEnabled;
            return false;
        }
    }
}
