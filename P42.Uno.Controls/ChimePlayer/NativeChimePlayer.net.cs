using System;
using System.IO;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    class NativeChimePlayer : INativeChimePlayer
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task PlayAsync(Effect chime, EffectMode mode)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            throw new PlatformNotSupportedException();
        }
    }
}