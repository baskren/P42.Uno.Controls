using System;
using System.Threading.Tasks;
using Foundation;
using P42.Utils;
using Windows.Storage;

namespace P42.Uno.Controls
{
    class NativeChimePlayer : INativeChimePlayer
    {
        static readonly AudioToolbox.SystemSound Click = new AudioToolbox.SystemSound(1104);
        static readonly AudioToolbox.SystemSound Modifier = new AudioToolbox.SystemSound(1156);
        static readonly AudioToolbox.SystemSound Delete = new AudioToolbox.SystemSound(1155);
        //static readonly AudioToolbox.SystemSound message = new AudioToolbox.SystemSound(1007);
        //static readonly AudioToolbox.SystemSound alarm = new AudioToolbox.SystemSound(1005);
        //static readonly AudioToolbox.SystemSound alert = new AudioToolbox.SystemSound(1033);
        //static readonly AudioToolbox.SystemSound error = new AudioToolbox.SystemSound(1073);

        static AVFoundation.AVAudioPlayer InfoPlayer;
        static AVFoundation.AVAudioPlayer WarningPlayer;
        static AVFoundation.AVAudioPlayer ErrorPlayer;

        static AVFoundation.AVAudioPlayer AlarmPlayer;
        static AVFoundation.AVAudioPlayer InquiryPlayer;
        static AVFoundation.AVAudioPlayer ProgressPlayer;

        public async Task PlayAsync(Effect chime, EffectMode mode)
        {
            if (mode == EffectMode.Off)
                return;

            switch (chime)
            {
                case Effect.Select:
                    Click.PlaySystemSound();
                    break;
                case Effect.Modify:
                    Modifier.PlaySystemSound();
                    break;
                case Effect.Delete:
                    Delete.PlaySystemSound();
                    break;
                case Effect.Info:
                    InfoPlayer ??= new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Info)), "mp3", out NSError nSError0);
                    InfoPlayer.Play();
                    break;
                case Effect.Warning:
                    WarningPlayer ??= new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Warning)), "mp3", out NSError nSError1);
                    WarningPlayer.Play();
                    break;
                case Effect.Error:
                    ErrorPlayer ??= new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Error)), "mp3", out NSError nSError2);
                    ErrorPlayer.Play();
                    break;
                case Effect.Alarm:
                    AlarmPlayer ??= new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Alarm)), "mp3", out NSError nSError3);
                    AlarmPlayer.Play();
                    break;
                case Effect.Inquiry:
                    InquiryPlayer ??= new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Inquiry)), "mp3", out NSError nSError4);
                    InquiryPlayer.Play();
                    break;
                case Effect.Progress:
                    ProgressPlayer ??= new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Progress)), "mp3", out NSError nSError5);
                    ProgressPlayer.Play();
                    break;
            }
        }
    }
}