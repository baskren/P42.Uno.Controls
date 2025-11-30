namespace P42.Uno.Controls;

internal interface INativeChimePlayer
{
    Task PlayAsync(Effect chime, EffectMode mode);
}
