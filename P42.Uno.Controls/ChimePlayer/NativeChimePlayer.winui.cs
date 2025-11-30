using Windows.Media.Core;
using Windows.Media.Playback;

namespace P42.Uno.Controls 
{
    class NativeChimePlayer : INativeChimePlayer 
    {
        static MediaPlayer MediaPlayer;

        static NativeChimePlayer() 
        { 
            MediaPlayer = new MediaPlayer();
        }


#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task PlayAsync(Effect chime, EffectMode mode)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (mode == EffectMode.Off)
                return;

            MediaPlayer.Source = MediaSource.CreateFromUri(ChimePlayer.GetAssetUri(chime));
            MediaPlayer.Play();
        }
    }
}
