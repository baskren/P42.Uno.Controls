using System.Media;
using AudioToolbox;
using AVFoundation;
using Foundation;
using P42.Serilog.QuickLog;

namespace P42.Uno.Controls;

internal class NativeChimePlayer : INativeChimePlayer
{
    public async Task PlayAsync(Effect chime, EffectMode mode)
    {
        if (mode == EffectMode.Off)
            return;

        chime.AVAudioPlayer?.Play();
    }
}


internal static partial class EffectExtensions
{
    private static readonly Dictionary<Effect, AVAudioPlayer> Players = new();

    extension(Effect effect)
    {
        public AVAudioPlayer?  AVAudioPlayer
        {
            get
            {
                if (Players.TryGetValue(effect, out var player))
                    return player;

                if (effect.ChimeAssetAbsolutePath is not string path)
                    return null;

                player = AVAudioPlayer.FromUrl(NSUrl.FromFilename(path), AVFileTypes.MpegLayer3, out var nSError);

                if (nSError is not null)
                    QLog.Error(path + " AVAudioPlayer error: " + nSError.LocalizedDescription);

                if (player is not null)
                    Players[effect] = player;

                return player;
            }
        }
    }

}
