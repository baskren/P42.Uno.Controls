using System;
using System.IO;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    class NativeChimePlayer : INativeChimePlayer
    {
        static string SoundAssetsPath = string.Empty;

        static NativeChimePlayer()
        {
            Task.Run(Initialize).Wait();
        }

        static async Task Initialize()
        {
            if (await ChimePlayer.GetPathAsync(Effect.Alarm) is string path)
            {
                var ac = "/local/.assetsCache";
                SoundAssetsPath = path.Replace(ac, ".").Replace(Effect.Alarm + ".mp3", "");
                System.Console.WriteLine($"NativeAudioPlaer.Initialize : path=[{SoundAssetsPath}]");
                return;
            }

            throw new Exception("Could not find P42.Uno.Controls sound assets");
        }



        public void Play(Effect chime, EffectMode mode)
        {
            if (mode == EffectMode.Off)
                return;

            if (string.IsNullOrWhiteSpace(SoundAssetsPath))
                return;
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