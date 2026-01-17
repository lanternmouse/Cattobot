using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Cattobot.Extensions;

public static class ProcessExtensions
{
    [DllImport("libc.so.6", SetLastError = true)]
    private static extern int kill(int pid, int signal);

    extension(Process process)
    {
        private void SendSignal(int signal)
        {
            var result = kill(process.Id, signal);
            if (result != 0)
                throw new Exception($"Failed to send signal {signal} to process with id {process.Id}");
        }

        public void SendSignalStop() => process.SendSignal(19);
        public void SendSignalContinue() => process.SendSignal(18);
    }
}