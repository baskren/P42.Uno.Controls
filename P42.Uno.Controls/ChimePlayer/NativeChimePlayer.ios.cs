using System.Media;
using AudioToolbox;
using AVFoundation;
using Foundation;

namespace P42.Uno.Controls;

internal class NativeChimePlayer : INativeChimePlayer
{
    private static readonly SystemSound Click = new(1104);
    private static readonly SystemSound Modifier = new(1156);

    private static readonly SystemSound Delete = new(1155);
    //static readonly AudioToolbox.SystemSound message = new AudioToolbox.SystemSound(1007);
    //static readonly AudioToolbox.SystemSound alarm = new AudioToolbox.SystemSound(1005);
    //static readonly AudioToolbox.SystemSound alert = new AudioToolbox.SystemSound(1033);
    //static readonly AudioToolbox.SystemSound error = new AudioToolbox.SystemSound(1073);

    private static AVAudioPlayer? InfoPlayer;
    private static AVAudioPlayer? WarningPlayer;
    private static AVAudioPlayer? ErrorPlayer;

    private static AVAudioPlayer? AlarmPlayer;
    private static AVAudioPlayer? InquiryPlayer;
    private static AVAudioPlayer? ProgressPlayer;

    public async Task PlayAsync(Effect chime, EffectMode mode)
    {
        if (mode == EffectMode.Off)
            return;

        switch (chime)
        {
            case Effect.Select:
                await Click.PlaySystemSoundAsync();
                break;
            case Effect.Modify:
                await Modifier.PlaySystemSoundAsync();
                break;
            case Effect.Delete:
                await Delete.PlaySystemSoundAsync();
                break;
            case Effect.Info:
                InfoPlayer ??= new AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Info)), "mp3", out var nSError0);
                InfoPlayer.Play();
                break;
            case Effect.Warning:
                WarningPlayer ??= new AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Warning)), "mp3", out var nSError1);
                WarningPlayer.Play();
                break;
            case Effect.Error:
                ErrorPlayer ??= new AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Error)), "mp3", out var nSError2);
                ErrorPlayer.Play();
                break;
            case Effect.Alarm:
                AlarmPlayer ??= new AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Alarm)), "mp3", out var nSError3);
                AlarmPlayer.Play();
                break;
            case Effect.Inquiry:
                InquiryPlayer ??= new AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Inquiry)), "mp3", out var nSError4);
                InquiryPlayer.Play();
                break;
            case Effect.Progress:
                ProgressPlayer ??= new AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Progress)), "mp3", out var nSError5);
                ProgressPlayer.Play();
                break;
        }
    }
}
