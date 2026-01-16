using Windows.Media.Core;
using Windows.Media.Playback;

namespace P42.Uno.Controls; 

internal class NativeChimePlayer : INativeChimePlayer 
{
    static readonly MediaPlayer MediaPlayer = new();

    /*
    static NativeChimePlayer() 
    { 
        MediaPlayer = new MediaPlayer();
    }
    */


#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task PlayAsync(Effect chime, EffectMode mode)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        if (mode == EffectMode.Off)
            return;

        MediaPlayer.Source = MediaSource.CreateFromUri(chime.ChimeAssetUri);
        MediaPlayer.Play();
    }
}
