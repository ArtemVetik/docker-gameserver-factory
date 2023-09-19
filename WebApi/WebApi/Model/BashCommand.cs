using System.Diagnostics;

namespace WebApi.Model
{
    public class BashCommand
    {
        private readonly string Command;

        public BashCommand(string command)
        {
            Command = command;
        }

        public string Execute()
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{Command}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return result;
        }
    }
}