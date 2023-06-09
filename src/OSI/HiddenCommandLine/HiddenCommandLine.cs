using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hopex.OSI.HiddenCommandLine
{
    /// <summary>
    /// Hidden command line interpreter.
    /// </summary>
    public class HiddenCommandLine
    {
        /// <summary>
        /// Hidden command line interpreter.
        /// </summary>
        public HiddenCommandLine()
        {

        }

        /// <summary>
        /// Executes the user's entered command.
        /// </summary>
        /// <param name="command">The executed command.</param>
        /// <param name="waitForExit">Wait for the command to finish.</param>
        public void Exec(string command, bool waitForExit = true)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(@"cmd.exe", @"/C " + command)
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process procCommand = Process.Start(processStartInfo);

            if (waitForExit)
                procCommand.WaitForExit();
        }

        /// <summary>
        /// Destroys a user-defined process if it is available.
        /// </summary>
        /// <param name="processName">The process being destroyed (extension is optional).</param>
        public void KillProcessByName(string processName)
        {
            if (!processName.Contains(".exe"))
                processName = $@"{processName}.exe";

            Exec("taskkill /F /IM " + processName);
        }

        /// <summary>
        /// Searches for a user-defined process.
        /// </summary>
        /// <param name="processName">The desired process (extension is optional).</param>
        public async Task<bool> ExistsProcessByName(string processName)
        {
            bool isExists = false;
            await Task.Run(() =>
            {
                foreach (Process process in Process.GetProcesses())
                    if (process.ProcessName.ToLower().Equals(processName.Replace(".exe", "").ToLower()))
                    {
                        isExists = true;
                        break;
                    }
            });

            return isExists;
        }
    }
}
