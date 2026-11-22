namespace P42.Uno.Controls;

/// <summary>
/// Interface used by parent objects to communicate that a class instance is either "Active" or "Inactive".
/// This is meant to eliminate the need for events (thus IDisposal and GC) to manage instance's event 
/// subscriptions.  In other words, a better - and more situational - way to manage event subscriptions
/// and their potential memory leaks.
/// </summary>
public interface IEventSubscriber

{
    void EnableEvents();

    void DisableEvents();

    bool AreEventsEnabled { get; }
}