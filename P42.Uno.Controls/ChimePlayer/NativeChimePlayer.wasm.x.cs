using System;
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
                Console.WriteLine($"NativeAudioPlayer.Initialize: === ENTER ====");
                if (await ChimePlayer.GetPathAsync(Effect.Alarm) is string path)
                {
                    System.Console.WriteLine($"NativeAudioPlayer.Initialize : path=[{path}]");
                    var ac = "/local/.assetsCache";
                    SoundAssetsPath = path.Replace(ac, ".").Replace(Effect.Alarm + ".mp3", "");
                    System.Console.WriteLine($"NativeAudioPlayer.Initialize : path=[{SoundAssetsPath}]");
                }
                Console.WriteLine($"NativeAudioPlayer.Initialize: === EXIT ====");
            }

            if (string.IsNullOrWhiteSpace(SoundAssetsPath))
            {
                System.Console.WriteLine($"NativeChimePlayer.Could not find P42.Uno.Controls sound assets");
                return;
            }

            string fileName = chime + ".mp3";

            var javascript = @"
function play() {
    var audio = new Audio('" + SoundAssetsPath + fileName + @"');
    audio.play();
}

play();
                ";

            //global::Uno.Foundation.WebAssemblyRuntime.InvokeJS(javascript);

            System.Console.WriteLine($"ChimePlayerScript: [{javascript}]");
        }
    }
}
