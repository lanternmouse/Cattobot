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
            CreateNoWindow = true
        };
        var arguments = startInfo.ArgumentList;
        
        arguments.Add("-vn");
        
        arguments.Add("-reconnect");
        arguments.Add("1");
        
        arguments.Add("-reconnect_streamed");
        arguments.Add("1");
        
        arguments.Add("-reconnect_delay_max");
        arguments.Add("5");
        
        arguments.Add("-i");
        arguments.Add(input);
        // Set the logging level to quiet mode
        arguments.Add("-loglevel");
        arguments.Add("-8");

        arguments.Add("-ac");
        arguments.Add("2");

        arguments.Add("-f");
        arguments.Add("s16le");

        arguments.Add("-ar");
        arguments.Add("48000");

        arguments.Add("pipe:1");

        return Process.Start(startInfo)!;
    }
}