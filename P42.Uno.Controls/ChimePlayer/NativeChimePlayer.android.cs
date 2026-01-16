using Android.Content;
using Android.Media;
using Android.Provider;
using Application = Android.App.Application;
using Object = Java.Lang.Object;

namespace P42.Uno.Controls;

internal class NativeChimePlayer : Object, INativeChimePlayer, MediaPlayer.IOnPreparedListener
{
    private static AudioManager? _audio;
    //static SoundPool _soundPool;
    private static MediaPlayer? _mediaPlayer;

    public void OnPrepared(MediaPlayer? mp)
    {
        _mediaPlayer = mp;
        _mediaPlayer?.Start();
    }

    public async Task PlayAsync(Effect chime, EffectMode mode)
    {
        if (mode == EffectMode.Off)
            return;

        if (mode == EffectMode.Default)
        {
            var enabled = Settings.System.GetInt(Application.Context.ContentResolver, Settings.System.SoundEffectsEnabled) != 0;
            if (!enabled)
                return;
        }

        if (_audio is null)
        {
            if (Application.Context?.GetSystemService(Context.AudioService) is not AudioManager am)
                return;

            _audio = am;

            if (new AudioAttributes.Builder() is not AudioAttributes.Builder builder)
                return;

            builder.SetContentType(AudioContentType.Music);

            if (builder.Build() is not AudioAttributes audioAttributes)
                return;

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.SetAudioAttributes(audioAttributes);
            _mediaPlayer.SetOnPreparedListener(this);
        }

        if (_mediaPlayer is null)
            return;

        _mediaPlayer.PlayChime(chime);
    }

}

internal static class MediaPlayerExtensions
{
    public static void PlayChime(this MediaPlayer mediaPlayer, Effect effect)
    {
        if (effect.ChimeAssetAbsolutePath is not string path)
            return;
        mediaPlayer.SetDataSource(path);
        mediaPlayer.Prepare();
        mediaPlayer.Start();
    }
}
