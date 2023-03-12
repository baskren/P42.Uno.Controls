using System;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    interface INativeChimePlayer
    {
        Task PlayAsync(Effect chime, EffectMode mode);
    }
}