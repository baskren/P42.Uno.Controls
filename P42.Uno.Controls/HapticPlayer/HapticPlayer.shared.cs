using Windows.Devices.Haptics;

namespace P42.Uno.Controls;

public static class HapticPlayer 
{
    private static INativeHapticPlayer NativeHapticPlayer => field ??= new NativeHapticPlayer();

    public static EffectMode DefaultEffectMode { get; set; }

    public static async Task PlayAsync(Effect effect, EffectMode mode = default)
    {
        if (mode == EffectMode.Default)
            mode = DefaultEffectMode;

        if (mode == EffectMode.Off)
            return;

        await NativeHapticPlayer.PlayAsync(effect, mode);

    }

}
