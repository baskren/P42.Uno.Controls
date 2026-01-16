
namespace P42.Uno.Controls;

public static class Feedback
{
    public static async Task PlayAsync(Effect effect, EffectMode mode = default) 
    {
        await Task.WhenAll
        (
            HapticPlayer.PlayAsync(effect, mode),
            ChimePlayer.PlayAsync(effect, mode)
        );
    }
}
