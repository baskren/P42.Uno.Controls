using System;
using System.Threading.Tasks;
using Foundation;
using Windows.Storage;

namespace P42.Uno.Controls
{
    class NativeChimePlayer : INativeChimePlayer
    {
        static readonly AudioToolbox.SystemSound click = new AudioToolbox.SystemSound(1104);
        static readonly AudioToolbox.SystemSound modifier = new AudioToolbox.SystemSound(1156);
        static readonly AudioToolbox.SystemSound delete = new AudioToolbox.SystemSound(1155);
        //static readonly AudioToolbox.SystemSound message = new AudioToolbox.SystemSound(1007);
        //static readonly AudioToolbox.SystemSound alarm = new AudioToolbox.SystemSound(1005);
        //static readonly AudioToolbox.SystemSound alert = new AudioToolbox.SystemSound(1033);
        //static readonly AudioToolbox.SystemSound error = new AudioToolbox.SystemSound(1073);

        static AVFoundation.AVAudioPlayer infoPlayer;
        static AVFoundation.AVAudioPlayer warningPlayer;
        static AVFoundation.AVAudioPlayer errorPlayer;

        static AVFoundation.AVAudioPlayer alarmPlayer;
        static AVFoundation.AVAudioPlayer inquiryPlayer;
        static AVFoundation.AVAudioPlayer progressPlayer;


        static NativeChimePlayer()
        {
            Task.Run(Initialize).Wait();
        }

        static async Task Initialize()
        { 
            infoPlayer = new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Info)), "mp3", out NSError nSError0);
            warningPlayer = new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Warning)), "mp3", out NSError nSError1);
            errorPlayer = new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Error)), "mp3", out NSError nSError2);

            alarmPlayer = new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Alarm)), "mp3", out NSError nSError3);
            inquiryPlayer = new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Inquiry)), "mp3", out NSError nSError4);
            progressPlayer = new AVFoundation.AVAudioPlayer(NSUrl.FromFilename(await ChimePlayer.GetPathAsync(Effect.Progress)), "mp3", out NSError nSError5);
        }


        public void Play(Effect chime, EffectMode mode)
        {
            if (mode == EffectMode.Off)
                return;

            switch (chime)
            {
                case Effect.Select:
                    click.PlaySystemSound();
                    break;
                case Effect.Modify:
                    modifier.PlaySystemSound();
                    break;
                case Effect.Delete:
                    delete.PlaySystemSound();
                    break;
                case Effect.Info:
                    infoPlayer.Play();
                    break;
                case Effect.Warning:
                    warningPlayer.Play();
                    break;
                case Effect.Error:
                    errorPlayer.Play();
                    break;
                case Effect.Alarm:
                    alarmPlayer.Play();
                    break;
                case Effect.Inquiry:
                    inquiryPlayer.Play();
                    break;
                case Effect.Progress:
                    progressPlayer.Play();
                    break;
            }
        }
    }
}