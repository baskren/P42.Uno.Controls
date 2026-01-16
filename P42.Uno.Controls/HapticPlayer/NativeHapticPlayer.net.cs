namespace P42.Uno.Controls;

internal class NativeHapticPlayer : INativeHapticPlayer
{
    public Task PlayAsync(Effect effect, EffectMode mode)
    {
        return Task.CompletedTask;
    }
}
