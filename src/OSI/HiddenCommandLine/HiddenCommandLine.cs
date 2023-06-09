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
    }
}
