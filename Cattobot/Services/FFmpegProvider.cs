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
                "-reconnect_delay_max", "3",
                "-re",
                "-i", input,
                "-vn", "-sn", "-dn",
                "-ar", "48000",
                "-ac", "2",
                "-c:a", "pcm_s16le",
                "-f", "s16le",
                "-bufsize", "256K",
                "-probesize", "16K",
                "-analyzeduration", "500000",
                "-avioflags", "direct",
                "-loglevel", "quiet",
                "-flush_packets", "1",
                "pipe:1"
            ]),
        };

        var proc = Process.Start(startInfo)!;
        
        proc.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
        
        return proc;
    }
}