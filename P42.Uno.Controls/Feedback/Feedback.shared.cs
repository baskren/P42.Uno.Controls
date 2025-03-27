
using System.Threading.Tasks;

namespace P42.Uno.Controls;

public static class Feedback
{
    public static async Task PlayAsync(Effect effect, EffectMode mode = default) 
    {
        HapticPlayer.Play(effect, mode);
        await ChimePlayer.PlayAsync(effect, mode);
    }
}