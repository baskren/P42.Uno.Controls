using Android.Content;
using Android.Media;
using Android.OS;

#pragma warning disable CA1422 // Validate platform compatibility
namespace P42.Uno.Controls
{
    class NativeHapticPlayer : INativeHapticPlayer
    {
        static Vibrator _vibrator;
        static Vibrator Vibrator => _vibrator ??= (Vibrator)Android.App.Application.Context.GetSystemService(Context.VibratorService);

        static bool _appEnabledTested;
        static bool _appEnabled;
        static bool AppEnabled
        {
            get
            {
                if (!_appEnabledTested)
                {
                    _appEnabled = Android.App.Application.Context.CheckCallingOrSelfPermission("android.permission.VIBRATE") == Android.Content.PM.Permission.Granted;
                    _appEnabledTested = true;
                }
                return _appEnabled;
            }
        }

        Android.Media.AudioAttributes _attributes;
        Android.Media.AudioAttributes Attributes
        {
            get
            {
                if (_attributes == null)
                {
                    using (var builder = new Android.Media.AudioAttributes.Builder())
                    {
                        builder.SetContentType(AudioContentType.Sonification);
                        _attributes = builder.Build();
                    }
                }
                return _attributes;
            }
        }

        public void Play(Effect effect, EffectMode mode)
        {
            if (!AppEnabled)
                return;

            if (mode == EffectMode.Off)
                return;

            if (mode == EffectMode.Default)
            {
                var enabled = Android.Provider.Settings.System.GetInt(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.System.HapticFeedbackEnabled, 1) != 0;
                if (!enabled)
                    return;
            }

            var currentActivity = global::Uno.UI.ContextHelper.Current as Android.App.Activity;
            if (effect == Effect.Select)
                currentActivity.Window.DecorView.PerformHapticFeedback(Android.Views.FeedbackConstants.KeyboardTap);
            else if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                VibrationEffect droidEffect = null;
                switch (effect)
                {
                    case Effect.Delete:
                        droidEffect = VibrationEffect.CreateOneShot(200, 196);
                        break;
                    case Effect.Info: 
                        droidEffect = VibrationEffect.CreateOneShot(200, 255);
                        break;
                    case Effect.Warning:
                        droidEffect = VibrationEffect.CreateWaveform(new long[] { 0, 200, 100, 200 }, new int[] { 0, 196, 0, 255 }, -1);
                        break;
                    case Effect.Error:
                        droidEffect = VibrationEffect.CreateWaveform(new long[] { 0, 200, 100, 200, 100, 200 }, new int[] { 0, 196, 0, 196, 0, 255 }, -1);
                        break;
                    case Effect.Alarm:
                        droidEffect = VibrationEffect.CreateOneShot(800, 255);
                        break;
                    case Effect.Inquiry:
                        droidEffect = VibrationEffect.CreateWaveform(new long[] { 0, 200, 100, 200 }, new int[] { 0, 255, 0, 196 }, -1);
                        break;
                }
                if (droidEffect != null)
                    Vibrator.Vibrate(droidEffect);
            }
            else
            {
#pragma warning disable CS0618 // Type or member is obsolete
                long[] pattern = null;
                switch (effect)
                {
                    case Effect.Delete:
                        Vibrator.Vibrate(300, Attributes);
                        break;
                    case Effect.Info:
                        Vibrator.Vibrate(400, Attributes);
                        break;
                    case Effect.Warning:
                        pattern = new long[] { 0, 200, 100, 200 };
                        break;
                    case Effect.Error:
                        pattern = new long[] { 0, 200, 100, 200, 100, 200 };
                        break;
                    case Effect.Alarm:
                        Vibrator.Vibrate(800, Attributes);
                        break;
                    case Effect.Inquiry:
                        pattern = new long[] { 0, 200, 100, 200 };
                        break;
                }
                if (pattern != null)
                {
                    Vibrator.Vibrate(pattern, -1, Attributes);
                }
#pragma warning restore CS0618 // Type or member is obsolete

            }
        }
    }
}
#pragma warning restore CA1422 // Validate platform compatibility
