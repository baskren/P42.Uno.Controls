using P42.Serilog.QuickLog;

namespace P42.Uno.Controls;

internal class NativeChimePlayer : INativeChimePlayer
{
    public async Task PlayAsync(Effect chime, EffectMode mode)
    {
        if (mode == EffectMode.Off)
            return;

        if (chime.ChimeAssetAbsolutePath is not string mp3Path)
            return;
        
        //var soundsPath = System.IO.Path.Combine(folderPath, "P42.Uno.Controls", "Assets", "Sounds");
        //var mp3Path = System.IO.Path.Combine(soundsPath, $"{chime}.mp3");
        (int code, string output, string error) result;

        if (OperatingSystem.IsWindows())
        {
            if (System.Reflection.Assembly.GetExecutingAssembly().Location is not string dllPath)
                throw new Exception("Unable to get path for executing assembly dll");

            if (System.IO.Path.GetDirectoryName(dllPath) is not string folderPath)
                throw new Exception("Unable to folder path for executing assembly");

            var playerPath = System.IO.Path.Combine(folderPath, "cmdmp3win.exe");
            result = await Shell.ExecuteCommandAsync(playerPath, mp3Path);
        }
        else if (OperatingSystem.IsMacOS())
            result = await Shell.ExecuteCommandAsync("afplay", mp3Path);
        else if (OperatingSystem.IsLinux())
            result = await Shell.ExecuteCommandAsync("ffplay", mp3Path + " -nodisp -autoexit");
        else
            throw new Exception("Unsupported OS");

        if (result.code != 0)
            QLog.Warning($"Chime play failed: {result.error}");

        return;

    }
}
