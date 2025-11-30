using Windows.UI;
using AsyncAwaitBestPractices;

namespace P42.Uno.Controls.AnimateBar;

[Bindable]
public class Base : Grid, IDisposable
{
    private static readonly Brush DefaultBrush = SystemColors.BaseHigh.ToBrush();

    #region Properties

    #region Brush Property
    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
        nameof(Foreground),
        typeof(Brush),
        typeof(Base),
        new PropertyMetadata(DefaultBrush, OnBrushPropertyChanged)
    );

    private static void OnBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Base a)
            return;
        
        if (e.NewValue is Brush brush)
            a.DynamicRect.Fill =  brush;
        else
            a.DynamicRect.Fill = null;
    }

    public Brush Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }
    #endregion Brush Property

    #endregion


    #region Fields
    protected readonly Rectangle DynamicRect = new()
    {
        StrokeThickness = 0
    };
    
    protected int Dir = 1;
    protected TimeSpan ActionTime = TimeSpan.FromSeconds(0.5);
    protected TimeSpan LullTime = TimeSpan.FromSeconds(1);
    protected bool Looping;
    protected bool Failed;
    protected bool Disposed;
    
    #endregion


    #region Construction / Disposal
    public Base()
    {
        DynamicRect.Fill = DefaultBrush; // SystemColors.BaseHigh.ToBrush();
        Children.Add(DynamicRect);

        Loaded += Base_Loaded;
        Background = new SolidColorBrush(Color.FromArgb(1, 1, 1, 1));
    }



#if WINDOWS
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                }

                Disposed = true;
            }
        }

        public virtual void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

#endif

    #endregion


    #region vitual methods
    private void Base_Loaded(object sender, RoutedEventArgs e)
    {
        if (Looping)
            return;

        Looping = true;
        Loop(Dir).SafeFireAndForget();
    }


#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    protected virtual async Task Loop(int dir)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        throw new NotImplementedException();
    }
    #endregion

}
