using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace Hopex.OSI.ProcessManager
{
    /// <summary>
    /// Windows process manager.
    /// </summary>
    public class ProcessManager
    {
        /// <summary>
        /// List of names of all running processes.
        /// </summary>
        public List<string> ProcessNames
        {
            get
            {
                List<string> names = new List<string>();
                names.AddRange(new ManagementObjectSearcher("root\\CIMV2", "SELECT Name FROM Win32_Process")
                    .Get()
                    .OfType<ManagementObject>()
                    .Select(process => process["Name"].ToString()).ToArray());

                return names;
            }
        }

        /// <summary>
        /// A list of descriptions of all running processes.
        /// </summary>
        public List<string> ProcessCaptions
        {
            get
            {
                List<string> captions = new List<string>();
                captions.AddRange(new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption FROM Win32_Process")
                    .Get()
                    .OfType<ManagementObject>()
                    .Select(process => process["Caption"].ToString()).ToArray());

                //foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption FROM Win32_Process").Get())
                //    captions.Add(managementObject["Caption"].ToString());

                return captions;

                //string response = null;
                //string queryString = "SELECT Name, ProcessId, Caption, ExecutablePath FROM Win32_Process";

                //SelectQuery query = new SelectQuery(queryString);
                //ManagementScope scope = new ManagementScope(@"\\.\root\CIMV2");

                //ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                //ManagementObjectCollection processes = searcher.Get();

                //foreach (ManagementObject mo in processes)
                //    response += mo["Caption"].ToString() + "~";
                //return response;
            }
        }

        /// <summary>
        /// Windows process manager.
        /// </summary>
        public ProcessManager()
        {

        }

        /// <summary>
        /// Indicates whether there is at least one running process with the specified name.
        /// </summary>
        /// <param name="processName">The name of the process.</param>
        /// <returns><see langword="true"/>, if the process with the specified name is in the list of running processes.</returns>
        public bool ProcessExistsByName(string processName) => Process.GetProcessesByName(processName).Any();


        /// <summary>
        /// Searches for a user-defined process.
        /// </summary>
        /// <param name="processName">The desired process (extension is optional).</param>
        /// <returns><see langword="true"/>, if the process with the specified name is in the list of running processes.</returns>
        public async Task<bool> AsyncProcessExistsByName(string processName)
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

        /// <summary>
        /// Destroys all processes with the specified name.
        /// </summary>
        /// <param name="processName">The name of the process.</param>
        public void ProcessKillByName(string processName) => Process
            .GetProcessesByName(processName)
            .ToList()
            .ForEach(process => process.Kill());

        /// <summary>
        /// Returns the process name by its ID.
        /// </summary>
        /// <param name="processId">Process ID.</param>
        public string GetProcessNameById(int processId) => Process.GetProcessById(processId).ProcessName;

        /// <summary>
        /// Returns the process ID by its name.
        /// </summary>
        /// <param name="processName">The name of the process.</param>
        public string GetProcessIdByName(string processName)
        {
            foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT ProcessId, Name FROM Win32_Process").Get())
                if (managementObject["Name"].ToString().Equals(processName))
                    return managementObject["ProcessId"].ToString();

            return string.Empty;
        }

        /// <summary>
        /// The path to the process file by its ID.
        /// </summary>
        /// <param name="processId">Process ID.</param>
        public string GetProcessPathById(int processId)
        {
            foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT ProcessId, ExecutablePath FROM Win32_Process").Get())
                if (int.Parse(managementObject["ProcessId"].ToString()).Equals(processId))
                    return managementObject["ExecutablePath"].ToString();

            return string.Empty;
        }

        /// <summary>
        /// The path to the process file by its name.
        /// </summary>
        /// <param name="processName">The name of the process.</param>
        public string GetProcessPathName(string processName)
        {
            if (!processName.Equals("0x01"))
            {
                foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT Name, ExecutablePath FROM Win32_Process").Get())
                    if (managementObject["Name"].ToString().Contains(processName))
                        return managementObject["ExecutablePath"].ToString();
            }
            
            return string.Empty;
        }

        /// <summary>
        /// Destroys a user-defined process if it is available.
        /// </summary>
        /// <param name="processName">The process being destroyed (extension is optional).</param>
        public void KillProcessByName(string processName)
        {
            if (!processName.Contains(".exe"))
                processName = $@"{processName}.exe";

            new HiddenCommandLine.HiddenCommandLine().Exec("taskkill /F /IM " + processName);
        }

    }
}
