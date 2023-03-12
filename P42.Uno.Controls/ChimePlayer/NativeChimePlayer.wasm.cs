using System;
using System.IO;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    class NativeChimePlayer : INativeChimePlayer
    {
        static string SoundAssetsPath = string.Empty;

        public async Task PlayAsync(Effect chime, EffectMode mode)
        {
            if (mode == EffectMode.Off)
                return;

            if (string.IsNullOrWhiteSpace(SoundAssetsPath))
            {
                if (await ChimePlayer.GetPathAsync(Effect.Alarm) is string path)
                {
                    var ac = "/local/.assetsCache";
                    SoundAssetsPath = path.Replace(ac, ".").Replace(Effect.Alarm + ".mp3", "");
                    System.Console.WriteLine($"NativeAudioPlaer.Initialize : path=[{SoundAssetsPath}]");
                }
            }

            if (string.IsNullOrWhiteSpace(SoundAssetsPath))
                throw new Exception("Could not find P42.Uno.Controls sound assets");

            string fileName = chime + ".mp3";

            var javascript = @"
function play() {
    var audio = new Audio('" + SoundAssetsPath + fileName + @"');
    audio.play();
}

play();
                ";

            global::Uno.Foundation.WebAssemblyRuntime.InvokeJS(javascript);
        }
    }
}