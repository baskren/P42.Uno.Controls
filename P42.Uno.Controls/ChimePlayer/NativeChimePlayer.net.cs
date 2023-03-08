using System;
using System.IO;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    class NativeChimePlayer : INativeChimePlayer
    {
        public void Play(Effect chime, EffectMode mode)
        {
            throw new PlatformNotSupportedException();
        }
    }
}