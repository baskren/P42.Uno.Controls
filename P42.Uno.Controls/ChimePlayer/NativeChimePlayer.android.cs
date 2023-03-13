using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Media;
using Java.Interop;
using Java.Security.Cert;
using Windows.Storage;
using Windows.UI.Notifications;

namespace P42.Uno.Controls
{
    class NativeChimePlayer : Java.Lang.Object, INativeChimePlayer, MediaPlayer.IOnPreparedListener
    {
        static AudioManager _audio;
        //static SoundPool _soundPool;
        static MediaPlayer _mediaPlayer;

        static string infoPath;
        static string warnPath;
        static string errorPath;

        static string alarmPath;
        static string inquiryPath;
        static string progressPath;

        /*
        static int infoId;
        static int warnId;
        static int errorId;

        static int alarmId;
        static int inquiryId;
        static int progressId;
        */

        public void OnPrepared(MediaPlayer mp)
        {
            _mediaPlayer.Start();

        }

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

                infoPath = await ChimePlayer.GetPathAsync(Effect.Info);
                warnPath = await ChimePlayer.GetPathAsync(Effect.Warning);
                errorPath = await ChimePlayer.GetPathAsync(Effect.Error);

                alarmPath = await ChimePlayer.GetPathAsync(Effect.Alarm);
                inquiryPath = await ChimePlayer.GetPathAsync(Effect.Inquiry);
                progressPath = await ChimePlayer.GetPathAsync(Effect.Progress);

                /*
                _soundPool = new SoundPool.Builder()
                     .SetMaxStreams(6)
                     .SetAudioAttributes(audioAttributes)
                     .Build();

                infoId = _soundPool.Load(infoPath, 1);
                warnId = _soundPool.Load(warnPath, 1);
                errorId = _soundPool.Load(errorPath, 1);

                alarmId = _soundPool.Load(alarmPath, 1);
                inquiryId = _soundPool.Load(inquiryPath, 1);
                progressId = _soundPool.Load(progressPath, 1);
                */

                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.SetAudioAttributes(new AudioAttributes.Builder().SetContentType(AudioContentType.Music).Build());
                _mediaPlayer.SetOnPreparedListener(this);
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
                    //_soundPool.Play(infoId, 1, 1, 0, 0, 1);
                    _mediaPlayer.Reset();
                    _mediaPlayer.SetDataSource(infoPath);
                    _mediaPlayer.Prepare();
                    _mediaPlayer.Start();
                    break;
                case Effect.Warning:
                    //_soundPool.Play(warnId, 1, 1, 0, 0, 1);
                    _mediaPlayer.Reset();
                    _mediaPlayer.SetDataSource(warnPath);
                    _mediaPlayer.Prepare();
                    _mediaPlayer.Start();
                    break;
                case Effect.Error:
                    //_soundPool.Play(errorId, 1, 1, 0, 0, 1);
                    _mediaPlayer.Reset();
                    _mediaPlayer.SetDataSource(errorPath);
                    _mediaPlayer.Prepare();
                    _mediaPlayer.Start();
                    break;
                case Effect.Alarm:
                    //_soundPool.Play(alarmId, 1, 1, 0, 0, 1);
                    _mediaPlayer.Reset();
                    _mediaPlayer.SetDataSource(alarmPath);
                    _mediaPlayer.Prepare();
                    _mediaPlayer.Start();
                    break;
                case Effect.Inquiry:
                    //_soundPool.Play(inquiryId, 1, 1, 0, 0, 1);
                    _mediaPlayer.Reset();
                    _mediaPlayer.SetDataSource(inquiryPath);
                    _mediaPlayer.Prepare();
                    _mediaPlayer.Start();
                    break;
                case Effect.Progress:
                    // _soundPool.Play(progressId, 1, 1, 0, 0, 1);
                    _mediaPlayer.Reset();
                    _mediaPlayer.SetDataSource(progressPath);
                    _mediaPlayer.Prepare();
                    _mediaPlayer.Start();
                    break;
            }

        }

    }
}