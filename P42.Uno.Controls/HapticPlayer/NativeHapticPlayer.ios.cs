using UIKit;

namespace P42.Uno.Controls;

internal class NativeHapticPlayer : INativeHapticPlayer
{
    private static readonly AudioToolbox.SystemSound vibrate = new(4095);

    public void Play(Effect effect, EffectMode mode)
    {
        if (mode == EffectMode.Off || !UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            return;

        switch (effect)
        {
            case Effect.Select:
            {
                using var selection = new UISelectionFeedbackGenerator();
                selection.Prepare();
                selection.SelectionChanged();
            }
                break;
            case Effect.Delete:
            {
                using var impact = new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Medium);
                impact.Prepare();
                impact.ImpactOccurred();
            }
                break;
            case Effect.Info:
            {
                using var impact = new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Heavy);
                impact.Prepare();
                impact.ImpactOccurred();
            }
                break;
            case Effect.Error:
            {
                // Initialize feedback
                using var notification = new UINotificationFeedbackGenerator();
                notification.Prepare();
                notification.NotificationOccurred(UINotificationFeedbackType.Error);
            }
                break;
            case Effect.Warning:
            {
                // Initialize feedback
                using var notification = new UINotificationFeedbackGenerator();
                notification.Prepare();
                notification.NotificationOccurred(UINotificationFeedbackType.Warning);
            }
                break;
            case Effect.Alarm:
                vibrate.PlaySystemSound();
                break;
            case Effect.Inquiry:
            {
                // Initialize feedback
                using var notification = new UINotificationFeedbackGenerator();
                notification.Prepare();
                notification.NotificationOccurred(UINotificationFeedbackType.Success);
            }
                break;

        }

    }

}
