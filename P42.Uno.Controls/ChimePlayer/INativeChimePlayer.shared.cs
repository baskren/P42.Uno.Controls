using System;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    interface INativeChimePlayer
    {
        void Play(Effect chime, EffectMode mode);
    }
}