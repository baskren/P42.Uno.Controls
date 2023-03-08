
namespace P42.Uno.Controls
{
    public static class Feedback
    {
        public static void Play(Effect effect, EffectMode mode = default) 
        {
            ChimePlayer.Play(effect, mode);
            HapticPlayer.Play(effect, mode);
        }
    }
}