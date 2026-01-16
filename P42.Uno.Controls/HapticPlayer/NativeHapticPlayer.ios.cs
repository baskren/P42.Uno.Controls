using AudioToolbox;
using UIKit;

namespace P42.Uno.Controls;

internal class NativeHapticPlayer : INativeHapticPlayer
{
    private static readonly SystemSound vibrate = new(4095);

    internal static UIWindow? IOSWindow
    {
        get
        {
            if (field is null)
            {
                var window = UIApplication.SharedApplication.Delegate.Window;
                field = window;
            }
            return field;
        }
    }

    public async Task PlayAsync(Effect effect, EffectMode mode)
    {
        if (mode == EffectMode.Off || !UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            return;

        await Task.Run(() =>
        {
            UIImpactFeedbackGenerator? generator;

            UIImpactFeedbackStyle? style = effect switch
            {
                Effect.Press => UIImpactFeedbackStyle.Light,
                Effect.Select => UIImpactFeedbackStyle.Medium,
                Effect.Delete => UIImpactFeedbackStyle.Medium,
                Effect.Info => UIImpactFeedbackStyle.Heavy,
                _ => null,
            };

            if (style is not null)
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(17, 5) && IOSWindow is not null)
                {
                    generator = UIImpactFeedbackGenerator.GetFeedbackGenerator(style.Value, IOSWindow);
                }
                else
                {
#pragma warning disable CA1422 // Validate platform compatibility
                    generator = new UIImpactFeedbackGenerator(style.Value);
#pragma warning restore CA1422 // Validate platform compatibility
                }
                generator.Prepare();
                generator.ImpactOccurred();
                generator.Dispose();
                return;
            }

            UINotificationFeedbackType? notification = effect switch
            {
                Effect.Error => UINotificationFeedbackType.Error,
                Effect.Warning => UINotificationFeedbackType.Warning,
                Effect.Inquiry => UINotificationFeedbackType.Success,
                _ => null,
            };

            if (notification is not null)
            {
                UINotificationFeedbackGenerator? notifGenerator;
                if (UIDevice.CurrentDevice.CheckSystemVersion(17, 5) && IOSWindow is not null)
                {
                    notifGenerator = UINotificationFeedbackGenerator.GetFeedbackGenerator(IOSWindow);
                }
                else
                {
                    notifGenerator = new UINotificationFeedbackGenerator();
                }
                notifGenerator.Prepare();
                notifGenerator.NotificationOccurred(notification.Value);
                notifGenerator.Dispose();
                return;
            }

            if (effect == Effect.Alarm)
            {
                vibrate.PlaySystemSound();
                return;
            }
        });

    }

}
