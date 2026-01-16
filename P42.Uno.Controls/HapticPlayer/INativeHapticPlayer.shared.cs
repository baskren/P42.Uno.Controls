namespace P42.Uno.Controls;

internal interface INativeHapticPlayer
{
    Task PlayAsync(Effect effect, EffectMode mode);
}
