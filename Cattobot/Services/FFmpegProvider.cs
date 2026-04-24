using System.Diagnostics;

namespace Cattobot.Services;

public static class FFmpegProvider
{
    public static Process StartEncodeProcess(string input)
    {
        ProcessStartInfo startInfo = new("ffmpeg")
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = false,
            CreateNoWindow = true,
            Arguments = string.Join(" ", [
                "-reconnect", "1",
                "-reconnect_streamed", "1",
                "-reconnect_delay_max", "5",
                "-i", input,
                "-ar", "48000",
                "-ac", "2",
                "-f", "s16le",
                "-loglevel", "8",
                "pipe:1"
            ]),
        };

        var proc = Process.Start(startInfo)!;
        
        proc.ErrorDataReceived += (_, e) => Console.WriteLine(e.Data);
        
        return proc;
    }
}