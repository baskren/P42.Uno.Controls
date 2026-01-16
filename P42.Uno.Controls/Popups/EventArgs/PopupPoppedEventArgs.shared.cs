namespace P42.Uno.Controls;

/// <summary>
/// Event arguments passed by Popped event and WaitUntilPoppedAsyc() method in P42.Uno.Controls popups.
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="cause">What was the cause of the Popup being popped?</param>
/// <param name="trigger">What object triggered the popup being popped</param>
public class PopupPoppedEventArgs(PopupPoppedCause cause, object? trigger) : EventArgs
{
    /// <summary>
    /// What was the cause of the Popup being popped?
    /// </summary>
    public PopupPoppedCause Cause { get; private set; } = cause;

    /// <summary>
    /// What object triggered the popup being popped
    /// </summary>
    public object? Trigger { get; private set; } = trigger;
}
