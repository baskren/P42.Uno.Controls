using System;
using System.Linq;
using UIKit;

namespace P42.Uno.Controls;

class NativeHapticPlayer : INativeHapticPlayer
{
    private static readonly AudioToolbox.SystemSound Vibrate = new AudioToolbox.SystemSound(4095);

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
                using var impact = CreateImpactGenerator(UIImpactFeedbackStyle.Medium);
                impact?.Prepare();
                impact?.ImpactOccurred();
            }
                break;
            case Effect.Info:
            {
                using var impact = CreateImpactGenerator(UIImpactFeedbackStyle.Heavy);
                impact?.Prepare();
                impact?.ImpactOccurred();
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
                Vibrate.PlaySystemSound();
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

    static UIImpactFeedbackGenerator CreateImpactGenerator(UIImpactFeedbackStyle style)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(17, 5) && !OperatingSystem.IsMacCatalystVersionAtLeast(17, 5))
            return new UIImpactFeedbackGenerator(style);

        // iOS 17.5+: generators are attached to a view; use the key window.
        var view = GetKeyWindow();
        return view is null ? null : UIImpactFeedbackGenerator.GetFeedbackGenerator(style, view);
    }

    static UIWindow GetKeyWindow()
        => UIApplication.SharedApplication.ConnectedScenes
            .OfType<UIWindowScene>()
            .SelectMany(scene => scene.Windows)
            .FirstOrDefault(window => window.IsKeyWindow);

}
