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


        public void Play(Effect chime, EffectMode mode) 
        {
            if (mode == EffectMode.Off)
                return;

            MediaPlayer.Source = MediaSource.CreateFromUri(ChimePlayer.GetAssetUri(chime));
            MediaPlayer.Play();
        }
    }
}
