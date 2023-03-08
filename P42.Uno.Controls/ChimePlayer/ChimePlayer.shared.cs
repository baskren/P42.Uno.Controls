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

        public static void Play(Effect chime, EffectMode mode = default)
        {
            if (mode == EffectMode.Default)
                mode = DefaultEffectMode;

            NativeChimePlayer.Play(chime, mode);
        }

        internal static async Task<string> GetPathAsync(Effect chime)
            => (await GetStorageFileAsync(chime)).Path;

        internal static async Task<StorageFile> GetStorageFileAsync(Effect chime)
        {
            if (await StorageFile.GetFileFromApplicationUriAsync(GetAssetUri(chime)) is StorageFile file)
                return file;
            return null;
        }

        internal static Uri GetAssetUri(Effect chime)
            => new("ms-appx:///" + GetAssetRelativePath(chime));

        internal static string GetAssetRelativePath(Effect chime)
            => new("P42.Uno.Controls/Assets/Sounds/" + chime + ".mp3");


    }
}