using System;
using System.IO;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    class NativeHapticPlayer : INativeHapticPlayer
    {

        const string Select = @"navigator.vibrate(50);";
        const string Modify = @"navigator.vibrate(200);";
        const string Delete = @"navigator.vibrate(100);";

        const string Info = @"navigator.vibrate(200);";
        const string Warning = @"navigator.vibrate([200, 100, 200]);";
        const string Error = @"navigator.vibrate([200, 100, 200, 100, 200]);";

        const string Alarm = @"navigator.vibrate(800);";
        const string Inquiry = @"navigator.vibrate([200, 100, 200]);";


        public void Play(Effect effect, EffectMode mode)
        {
            if (mode == EffectMode.Off)
                return;

            var command = string.Empty;
            switch (effect)
            {
                case Effect.Select:
                    command = Select; break;
                case Effect.Modify:
                    command = Modify; break;
                case Effect.Delete:
                    command = Delete; break;
                case Effect.Info:
                    command = Info; break;
                case Effect.Warning:
                    command = Warning; break;
                case Effect.Error:
                    command = Error; break;
                case Effect.Alarm:
                    command = Alarm; break;
                case Effect.Inquiry:
                    command = Inquiry; break;
                default:
                    return;
            }

            var javascript = @"
navigator.vibrate = navigator.vibrate || navigator.webkitVibrate || navigator.mozVibrate || navigator.msVibrate;

if (navigator.vibrate) {
	" + command + @"
}";
            Uno.Foundation.WebAssemblyRuntime.InvokeJS(javascript);
        }
    }
}