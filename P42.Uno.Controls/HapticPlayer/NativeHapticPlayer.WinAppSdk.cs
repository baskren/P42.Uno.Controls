using Windows.Devices.Haptics;

namespace P42.Uno.Controls;

class NativeHapticPlayer : INativeHapticPlayer
{
    public async Task PlayAsync(Effect effect, EffectMode mode)
    {
        ushort? waveform = effect switch
        {
            Effect.Press => KnownSimpleHapticsControllerWaveforms.Press,
            Effect.Select => KnownSimpleHapticsControllerWaveforms.Click,
            Effect.Modify => KnownSimpleHapticsControllerWaveforms.PencilContinuous,
            Effect.Delete => KnownSimpleHapticsControllerWaveforms.EraserContinuous,
            Effect.Info => KnownSimpleHapticsControllerWaveforms.Success,
            Effect.Warning => KnownSimpleHapticsControllerWaveforms.RumbleContinuous,
            Effect.Error => KnownSimpleHapticsControllerWaveforms.Error,
            Effect.Alarm => KnownSimpleHapticsControllerWaveforms.BuzzContinuous,
            Effect.Inquiry => KnownSimpleHapticsControllerWaveforms.GalaxyPenContinuous,
            Effect.Progress => KnownSimpleHapticsControllerWaveforms.BrushContinuous,
            _ => null,
        };

        if (waveform is null)
            return;
        

        var result = await VibrationDevice.RequestAccessAsync();
        if (result == VibrationAccessStatus.Allowed)
        {
            if (await VibrationDevice.GetDefaultAsync() is not { } vibrationDevice)
                return;

            var simpleHapticsController = vibrationDevice.SimpleHapticsController;
            if (simpleHapticsController.SupportedFeedback.FirstOrDefault(feedback => feedback.Waveform == waveform) is not { } feedbackType)
                return;
                    
            simpleHapticsController.SendHapticFeedback(feedbackType);

        }

    }
}
