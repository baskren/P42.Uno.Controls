using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Views;
using Application = Android.App.Application;

//#pragma warning disable CA1422 // Validate platform compatibility
namespace P42.Uno.Controls;

internal class NativeHapticPlayer : INativeHapticPlayer
{
    private static Vibrator? Vibrator 
    { 
        get 
        { 
            if (field is not null)
                return field;

            if (Android.OS.BuildVersionCodes.S >= Build.VERSION.SdkInt)
            {
#pragma warning disable CA1416 // Validate platform compatibility
                var vibratorManager = (VibratorManager?)Application.Context.GetSystemService(Context.VibratorManagerService);
                field = vibratorManager?.DefaultVibrator;
#pragma warning restore CA1416 // Validate platform compatibility
            }
            else
            {
#pragma warning disable CA1422 // Validate platform compatibility
                field = (Vibrator?)Application.Context.GetSystemService(Context.VibratorService);
#pragma warning restore CA1422 // Validate platform compatibility
            }
            
            return field;
        }
    }

    private static bool _appEnabledTested;
    private static bool AppEnabled
    {
        get
        {
            if (!_appEnabledTested)
            {
                field = Application.Context.CheckCallingOrSelfPermission("android.permission.VIBRATE") == Permission.Granted;
                _appEnabledTested = true;
            }
            return field;
        }
    }

    private static bool _audioAttributesTested;
    private static AudioAttributes? Attributes
    {
        get
        {
            if (!_audioAttributesTested)
            {
                using var builder = new AudioAttributes.Builder();
                builder.SetContentType(AudioContentType.Sonification);
                field = builder.Build();
                _audioAttributesTested = true;
            }
            return field;
        }
    }

    public async Task PlayAsync(Effect effect, EffectMode mode)
    {
        if (!AppEnabled)
            return;

        if (mode == EffectMode.Off)
            return;

        bool enabled;
#pragma warning disable CA1422 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
        if (mode == EffectMode.Default)
        {
            // Fix CA1422: Use Settings.Secure for Android 33+ (API level 33)
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // Android 13 (API 33)
                enabled = Settings.Secure.GetInt(Application.Context.ContentResolver, "haptic_feedback_enabled", 1) != 0;
            else
                enabled = Settings.System.GetInt(Application.Context.ContentResolver, Settings.System.HapticFeedbackEnabled, 1) != 0;

            if (!enabled)
                return;
        }

        await Task.Run(() =>
        {
            if (effect == Effect.Press)
            {
                if (ContextHelper.Current is not Activity currentActivity
                    || currentActivity.IsFinishing
                    || currentActivity.Window is not Android.Views.Window window)
                    return;
                window.DecorView.PerformHapticFeedback(FeedbackConstants.KeyboardPress);
            }
            else if (effect == Effect.Select)
            {
                if (ContextHelper.Current is not Activity currentActivity
                    || currentActivity.IsFinishing
                    || currentActivity.Window is not Android.Views.Window window)
                    return;
                window.DecorView.PerformHapticFeedback(FeedbackConstants.KeyboardTap);
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                VibrationEffect? droidEffect = null;
                switch (effect)
                {

                    case Effect.Delete:
                        droidEffect = VibrationEffect.CreateOneShot(200, 196);
                        break;
                    case Effect.Info:
                        droidEffect = VibrationEffect.CreateOneShot(200, 255);
                        break;
                    case Effect.Warning:
                        droidEffect = VibrationEffect.CreateWaveform([0, 200, 100, 200], [0, 196, 0, 255], -1);
                        break;
                    case Effect.Error:
                        droidEffect = VibrationEffect.CreateWaveform([0, 200, 100, 200, 100, 200], [0, 196, 0, 196, 0, 255], -1);
                        break;
                    case Effect.Alarm:
                        droidEffect = VibrationEffect.CreateOneShot(800, 255);
                        break;
                    case Effect.Inquiry:
                        droidEffect = VibrationEffect.CreateWaveform([0, 200, 100, 200], [0, 255, 0, 196], -1);
                        break;
                }
                if (droidEffect != null)
                    Vibrator?.Vibrate(droidEffect);
            }
            else
            {
                long[]? pattern = null;
                switch (effect)
                {
                    case Effect.Delete:
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                            Vibrator?.Vibrate(VibrationEffect.CreateOneShot(300, VibrationEffect.DefaultAmplitude), Attributes);
                        else
                            Vibrator?.Vibrate(300);
                        break;
                    case Effect.Info:
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                            Vibrator?.Vibrate(VibrationEffect.CreateOneShot(400, VibrationEffect.DefaultAmplitude), Attributes);
                        else
                            Vibrator?.Vibrate(400);
                        break;
                    case Effect.Warning:
                        pattern = [0, 200, 100, 200];
                        break;
                    case Effect.Error:
                        pattern = [0, 200, 100, 200, 100, 200];
                        break;
                    case Effect.Alarm:
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                            Vibrator?.Vibrate(VibrationEffect.CreateOneShot(800, VibrationEffect.DefaultAmplitude), Attributes);
                        else
                            Vibrator?.Vibrate(800);
                        break;
                    case Effect.Inquiry:
                        pattern = [0, 200, 100, 200];
                        break;
                }
                if (pattern != null)
                {
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                        Vibrator?.Vibrate(VibrationEffect.CreateWaveform(pattern, -1), Attributes);
                    else
                        Vibrator?.Vibrate(pattern, -1);

                }

            }
        });
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning restore CA1422 // Validate platform compatibility
    }
}
