namespace P42.Uno.Controls;

public static class HapticPlayer 
{
    static INativeHapticPlayer nativeHapticPlayer;
    static INativeHapticPlayer NativeHapticPlayer => nativeHapticPlayer ??= new NativeHapticPlayer();

    public static EffectMode DefaultEffectMode { get; set; }

    public static void Play(Effect effect, EffectMode mode = default)
    {
        if (mode == EffectMode.Default)
            mode = DefaultEffectMode;

        if (mode == EffectMode.Off)
            return;

        NativeHapticPlayer.Play(effect, mode);
    }
}
