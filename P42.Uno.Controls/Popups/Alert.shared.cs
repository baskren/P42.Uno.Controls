using System.Runtime.CompilerServices;
using Windows.UI;

namespace P42.Uno.Controls;

/// <summary>
/// Alert popup: A Toast with an "OK" button
/// </summary>
[Bindable]
//[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
public partial class Alert : Toast
{
    #region Properties

    #region OkButtonContent Property
    public static readonly DependencyProperty OkButtonContentProperty = DependencyProperty.Register(
        nameof(OkButtonContent),
        typeof(object),
        typeof(Alert),
        new PropertyMetadata("OK", (d,e) => ((Alert)d).OnOkButtonContentChanged(e))
    );

    private void OnOkButtonContentChanged(DependencyPropertyChangedEventArgs args)
    {
        if (args.NewValue is TextBlock tb)
        {
            var t = tb.Text;
            if (string.IsNullOrWhiteSpace(t))
                t = tb.GetHtml();
            _okButton.Collapsed(string.IsNullOrWhiteSpace(t));
            _okButton.Content = tb;
        }
        else if (args.NewValue is string text)
        {
            _okButton.Collapsed(string.IsNullOrWhiteSpace(text));
            if (_okButton.IsVisible())
            {
                _okButton.Content = new TextBlock()
                    .WBindFont(_okButton)
                    .WrapWords()
                    .SetHtml(text);
            }
            else
                _okButton.Content = null;
        }
        else
        {
            _okButton.Content = args.NewValue;
            _okButton.Visible();
        }
    }

    /// <summary>
    /// Text or UIElement for "OK" button content
    /// </summary>
    public object OkButtonContent
    {
        get => (object)GetValue(OkButtonContentProperty);
        set => SetValue(OkButtonContentProperty, value);
    }
    #endregion OkText Property


    #region OkButtonForeground Property
    public static readonly DependencyProperty OkButtonForegroundProperty = DependencyProperty.Register(
        nameof(OkButtonForeground),
        typeof(Brush),
        typeof(Alert),
        new PropertyMetadata(default, OnButtonForegroundChanged)
    );

    private static void OnButtonForegroundChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        if (dependencyObject is Alert alert)
            alert._okButton.Foreground = alert.OkButtonForeground;
    }

    /// <summary>
    /// Foreground for OK Button
    /// </summary>
    public Brush OkButtonForeground
    {
        get => (Brush)GetValue(OkButtonForegroundProperty);
        set => SetValue(OkButtonForegroundProperty, value);
    }
    #endregion OkButtonForeground Property


    #region OkButtonBackground Property
    public static readonly DependencyProperty OkButtonBackgroundProperty = DependencyProperty.Register(
        nameof(OkButtonBackground),
        typeof(Brush),
        typeof(Alert),
        new PropertyMetadata(default, OnButtonBackgroundChanged)
    );

    private static void OnButtonBackgroundChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        if (dependencyObject is Alert alert)
            alert._okButton.Background = alert.OkButtonBackground;
    }

    /// <summary>
    /// Background for OK button
    /// </summary>
    public Brush OkButtonBackground
    {
        get => (Brush)GetValue(OkButtonBackgroundProperty);
        set => SetValue(OkButtonBackgroundProperty, value);
    }
    #endregion OkButtonBackgroundBrush Property


    #endregion


    #region Construction / Initialization
    /// <summary>
    /// Creates the Alert (simpler)
    /// </summary>
    /// <param name="titleText"></param>
    /// <param name="messageText"></param>
    /// <param name="okButtonText"></param>
    /// <param name="okButtonColor"></param>
    /// <param name="okTextColor"></param>
    /// <returns></returns>
    public static async Task<Alert> CreateAsync(string titleText, string messageText, string okButtonText = null, Color okButtonColor = default, Color okTextColor = default, Effect effect = Effect.Alarm, EffectMode effectMode = EffectMode.Default)
    {
        var popup = new Alert
        { 
            Target = null, 
            TitleContent = titleText, 
            Message = messageText, 
            OkButtonContent = okButtonText ?? "OK",
            PushEffect = effect,
            PushEffectMode = effectMode
        };
        if (okTextColor != default)
            popup.OkButtonForeground = okTextColor.ToBrush();
        if (okButtonColor != default)
            popup.OkButtonBackground = okButtonColor.ToBrush();
        await popup.PushAsync();
        return popup;
    }

    /// <summary>
    /// Creates the Alert
    /// </summary>
    /// <param name="target"></param>
    /// <param name="titleContent"></param>
    /// <param name="messageContent"></param>
    /// <param name="okButtonContent"></param>
    /// <param name="okButtonColor"></param>
    /// <param name="okTextColor"></param>
    /// <returns></returns>
    public static async Task<Alert> CreateAsync(UIElement target, object titleContent, object messageContent, object okButtonContent = null, Color okButtonColor = default, Color okTextColor = default, Effect effect = Effect.Alarm, EffectMode effectMode = EffectMode.Default)
    {
        var popup = new Alert(target) 
        {
            TitleContent = titleContent, 
            Message = messageContent, 
            OkButtonContent = okButtonContent ?? "OK",
            PushEffect = effect,
            PushEffectMode = effectMode
        };
        if (okTextColor != default)
            popup.OkButtonForeground = okTextColor.ToBrush();
        if (okButtonColor != default)
            popup.OkButtonBackground = okButtonColor.ToBrush();
        await popup.PushAsync();
        return popup;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public Alert() : this(null) {}
        
    public Alert(UIElement target) : base(target)
    {
        Build();
    }

    #endregion


    #region Event Handlers
    protected virtual async void OnOkButtonClickedAsync(object sender, RoutedEventArgs e)
    {
        await PopAsync(PopupPoppedCause.ButtonTapped, true, sender);
    }
    #endregion


    #region Push / Pop
    /// <summary>
    /// Push Alert
    /// </summary>
    /// <param name="animated"></param>
    /// <returns></returns>
    public override async Task PushAsync(bool animated = true)
    {
        await base.PushAsync(animated);
        _okButton.Click += OnOkButtonClickedAsync;
    }

    /// <summary>
    /// Pop alert
    /// </summary>
    /// <param name="cause"></param>
    /// <param name="animated"></param>
    /// <param name="trigger"></param>
    /// <returns></returns>
    public override async Task PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, bool animated = true, [CallerMemberName] object trigger = null)
    {
        _okButton.Click -= OnOkButtonClickedAsync;
        await base.PopAsync(cause, animated, trigger);
    }
    #endregion

}
