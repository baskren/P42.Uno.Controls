namespace P42.Uno.Controls;

internal class NativeChimePlayer : INativeChimePlayer
{
    public Task PlayAsync(Effect chime, EffectMode mode)
    {
        return Task.CompletedTask;
    }
}
