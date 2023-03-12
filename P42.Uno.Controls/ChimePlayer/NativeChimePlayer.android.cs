using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Media;
using Java.Security.Cert;
using Windows.Storage;
using Windows.UI.Notifications;

namespace P42.Uno.Controls
{
    class NativeChimePlayer : INativeChimePlayer
    {
        static AudioManager _audio;
        static SoundPool _soundPool;
        static int infoId;
        static int warnId;
        static int errorId;

        static int alarmId;
        static int inquiryId;
        static int progressId;

        public async Task PlayAsync(Effect chime, EffectMode mode)
        {
            if (mode == EffectMode.Off)
                return;

            if (mode == EffectMode.Default)
            {
                var enabled = Android.Provider.Settings.System.GetInt(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.System.SoundEffectsEnabled) != 0;
                if (!enabled)
                    return;
            }

            if (_audio is null)
            {
                _audio = (Android.Media.AudioManager)Android.App.Application.Context.GetSystemService(Context.AudioService);

                var audioAttributes = new AudioAttributes.Builder()
                     .SetUsage(AudioUsageKind.NotificationEvent)
                     .SetContentType(AudioContentType.Sonification)
                     .Build();

                _soundPool = new SoundPool.Builder()
                     .SetMaxStreams(6)
                     .SetAudioAttributes(audioAttributes)
                     .Build();

                infoId = _soundPool.Load(await ChimePlayer.GetPathAsync(Effect.Info), 1);
                warnId = _soundPool.Load(await ChimePlayer.GetPathAsync(Effect.Warning), 1);
                errorId = _soundPool.Load(await ChimePlayer.GetPathAsync(Effect.Error), 1);

                alarmId = _soundPool.Load(await ChimePlayer.GetPathAsync(Effect.Alarm), 1);
                inquiryId = _soundPool.Load(await ChimePlayer.GetPathAsync(Effect.Inquiry), 1);
                progressId = _soundPool.Load(await ChimePlayer.GetPathAsync(Effect.Progress), 1);

            }

            switch (chime)
            {
                case Effect.None:
                    return;
                case Effect.Select:
                    _audio.PlaySoundEffect(Android.Media.SoundEffect.KeyClick);
                    break;
                case Effect.Modify:
                    _audio.PlaySoundEffect(Android.Media.SoundEffect.Return);
                    break;
                case Effect.Delete:
                    _audio.PlaySoundEffect(Android.Media.SoundEffect.Delete);
                    break;
                case Effect.Info:
                    _soundPool.Play(infoId, 1, 1, 0, 0, 1);
                    break;
                case Effect.Warning:
                    _soundPool.Play(warnId, 1, 1, 0, 0, 1);
                    break;
                case Effect.Error:
                    _soundPool.Play(errorId, 1, 1, 0, 0, 1);
                    break;
                case Effect.Alarm:
                    _soundPool.Play(alarmId, 1, 1, 0, 0, 1);
                    break;
                case Effect.Inquiry:
                    _soundPool.Play(inquiryId, 1, 1, 0, 0, 1);
                    break;
                case Effect.Progress:
                    _soundPool.Play(progressId, 1, 1, 0, 0, 1);
                    break;
            }

        }

    }
}