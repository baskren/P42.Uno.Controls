namespace P42.Uno.Controls;

internal class NativeHapticPlayer : INativeHapticPlayer
{
    private const string Select = "navigator.vibrate(50);";
    private const string Modify = "navigator.vibrate(200);";
    private const string Delete = "navigator.vibrate(100);";

    private const string Info = "navigator.vibrate(200);";
    private const string Warning = "navigator.vibrate([200, 100, 200]);";
    private const string Error = "navigator.vibrate([200, 100, 200, 100, 200]);";

    private const string Alarm = "navigator.vibrate(800);";
    private const string Inquiry = "navigator.vibrate([200, 100, 200]);";


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

        var javascript = $@"
navigator.vibrate = navigator.vibrate || navigator.webkitVibrate || navigator.mozVibrate || navigator.msVibrate;

if (navigator.vibrate) {{
	{command}
}}";
        global::Uno.Foundation.WebAssemblyRuntime.InvokeJS(javascript);
    }
}