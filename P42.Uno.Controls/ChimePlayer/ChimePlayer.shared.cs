using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace P42.Uno.Controls
{
    public static class ChimePlayer
    {

        static INativeChimePlayer nativeChimePlayer;
        static INativeChimePlayer NativeChimePlayer => nativeChimePlayer = nativeChimePlayer ?? new NativeChimePlayer();

        public static EffectMode DefaultEffectMode { get; set; }

        public static async Task PlayAsync(Effect chime, EffectMode mode = default)
        {
            if (mode == EffectMode.Default)
                mode = DefaultEffectMode;

            await NativeChimePlayer.PlayAsync(chime, mode);
        }

        internal static async Task<string> GetPathAsync(Effect chime)
            => (await GetStorageFileAsync(chime)).Path;

        internal static async Task<StorageFile> GetStorageFileAsync(Effect chime)
        {
            Console.WriteLine($"ChimePlayer.GetStorageFileAsync({chime})  ==== ENTER ====");
            var uri = GetAssetUri(chime);
            Console.WriteLine($"ChimePlayer.GetStorageFileAsync  uri=[{uri}]");
            if (await StorageFile.GetFileFromApplicationUriAsync(uri) is StorageFile file)
            {
                Console.WriteLine($"ChimePlayer.GetStorageFileAsync({chime}) = [{file.Path}]  ==== EXIT ====");
                return file;
            }
            Console.WriteLine($"ChimePlayer.GetStorageFileAsync({chime}) = [null]  ==== EXIT ====");
            return null;
        }

        internal static Uri GetAssetUri(Effect chime)
            => new("ms-appx:///" + GetAssetRelativePath(chime));

        internal static string GetAssetRelativePath(Effect chime)
            => new("P42.Uno.Controls/Assets/Sounds/" + chime + ".mp3");


    }
}