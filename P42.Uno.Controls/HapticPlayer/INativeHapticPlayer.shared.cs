using System;
using System.IO;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    interface INativeHapticPlayer
    {
        void Play(Effect effect, EffectMode mode);
    }
}