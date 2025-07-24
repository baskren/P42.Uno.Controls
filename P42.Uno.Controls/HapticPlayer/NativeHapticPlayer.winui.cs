using System;
using System.IO;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    class NativeHapticPlayer : INativeHapticPlayer
    {
        const string vibrationDeviceApiType = "Windows.Devices.Haptics.VibrationDevice";

        public void Play(Effect effect, EffectMode mode)
        {

        }
    }
}