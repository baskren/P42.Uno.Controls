using P42.Uno.Controls.AnimateBar;

namespace P42.Uno.Controls;

[Bindable]
internal partial class LoopingFlipViewItem : Grid, IEventSubscriber
{
    #region Properties

    #region Foreground Property
    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
        nameof(Foreground),
        typeof(Brush),
        typeof(LoopingFlipViewItem),
        new PropertyMetadata(SystemColors.BaseHigh.ToBrush())
    );
    public Brush Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }
    #endregion Foreground Property

    public bool AreEventsEnabled 
        => Child is IEventSubscriber subscriber ? subscriber.AreEventsEnabled : false;

    #endregion Properties


    #region Fields
    internal UIElement Child;
    private readonly Left LeftBar = new();
    private readonly Right RightBar = new();
    #endregion Fields


    #region Constructors
    public LoopingFlipViewItem(UIElement child)
    {
        RightBar.AltBind(Base.ForegroundProperty, this, ForegroundProperty);
        LeftBar.AltBind(Base.ForegroundProperty, this, ForegroundProperty);

        Child = child;
        Children.Add(child);
        Children.Add(RightBar);
        Children.Add(LeftBar);

        RightBar.Tapped += OnBarTapped;
        LeftBar.Tapped += OnBarTapped;
    }
    #endregion Constructors


    #region Methods
    private void OnBarTapped(object sender, TappedRoutedEventArgs e)
    {
        if (sender is not Base bar)
            return;

        if (Parent is not Grid _grid || _grid.Parent is not LoopingFlipView flipView)
            return;

        if (bar == LeftBar)
            flipView.SelectedIndex--;
        else if (bar == RightBar)
            flipView.SelectedIndex++;

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
    #endregion Methods

}
