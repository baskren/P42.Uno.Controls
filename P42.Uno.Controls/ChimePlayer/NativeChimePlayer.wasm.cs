namespace P42.Uno.Controls;

internal class NativeChimePlayer : INativeChimePlayer
{
    private static string SoundAssetsPath = string.Empty;

    public async Task PlayAsync(Effect chime, EffectMode mode)
    {
        if (mode == EffectMode.Off)
            return;


        if (string.IsNullOrWhiteSpace(SoundAssetsPath))
        {
            Console.WriteLine("NativeAudioPlayer.Initialize: === ENTER ====");
            if (await ChimePlayer.GetPathAsync(Effect.Alarm) is { } path)
            {
                Console.WriteLine($"NativeAudioPlayer.Initialize : path=[{path}]");
                var ac = "/local/.assetsCache";
                SoundAssetsPath = path.Replace(ac, ".").Replace($"{Effect.Alarm}.mp3", "");
                Console.WriteLine($"NativeAudioPlayer.Initialize : path=[{SoundAssetsPath}]");
            }
            Console.WriteLine("NativeAudioPlayer.Initialize: === EXIT ====");
        }

        if (string.IsNullOrWhiteSpace(SoundAssetsPath))
        {
            Console.WriteLine("NativeChimePlayer.Could not find P42.Uno.Controls sound assets");
            return;
        }

        var fileName = $"{chime}.mp3";

        var javascript = $@"
function play() {{
    var audio = new Audio('{SoundAssetsPath}{fileName}');
    audio.play();
}}

play();
                ";

        //global::Uno.Foundation.WebAssemblyRuntime.InvokeJS(javascript);

        Console.WriteLine($"ChimePlayerScript: [{javascript}]");
    }
}
