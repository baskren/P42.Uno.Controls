namespace P42.Uno.Controls;

public static class ChimePlayer
{
    //private static INativeChimePlayer nativeChimePlayer;
    private static INativeChimePlayer NativeChimePlayer => field ??= new NativeChimePlayer();

    public static EffectMode DefaultEffectMode { get; set; }

    public static async Task PlayAsync(Effect chime, EffectMode mode = default)
    {
        if (mode == EffectMode.Default)
            mode = DefaultEffectMode;

        await NativeChimePlayer.PlayAsync(chime, mode);
    }

    /*
    internal static async Task<string> GetPathAsync(Effect chime)
        => AssetExtensions.AssetPath(chime.ChimeAssetPath);

    internal static async Task<StorageFile?> GetStorageFileAsync(Effect chime)
    {
        Console.WriteLine($"ChimePlayer.GetStorageFileAsync({chime})  ==== ENTER ====");
        var uri = chime.ChimeAssetUri;
        Console.WriteLine($"ChimePlayer.GetStorageFileAsync  uri=[{uri}]");
        if (await StorageFile.GetFileFromApplicationUriAsync(uri) is { } file)
        {
            Console.WriteLine($"ChimePlayer.GetStorageFileAsync({chime}) = [{file.Path}]  ==== EXIT ====");
            return file;
        }
        Console.WriteLine($"ChimePlayer.GetStorageFileAsync({chime}) = [null]  ==== EXIT ====");
        return null;
    }

    */
}
