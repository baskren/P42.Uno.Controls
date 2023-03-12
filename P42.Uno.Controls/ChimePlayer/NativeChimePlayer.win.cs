using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Storage;

namespace P42.Uno.Controls 
{
    class NativeChimePlayer : INativeChimePlayer 
    {
        static Windows.Media.Playback.MediaPlayer MediaPlayer;

        static NativeChimePlayer() 
        { 
            MediaPlayer = new Windows.Media.Playback.MediaPlayer();
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
