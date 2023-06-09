using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;

namespace Hopex.OSI.ProcessManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessManager
    {
        /// <summary>
        /// Список имен всех запущенных процессов.
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
        /// Возвращает список описаний всех запущенных процессов.
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
        /// 
        /// </summary>
        public ProcessManager()
        {

        }

        /// <summary>
        /// Указывает на то, существует ли хотя бы один запущеный процесс с указанным именем.
        /// </summary>
        /// <param name="processName">Имя процесса.</param>
        /// <returns>Наличие процесса с указанным именем в списке запущенных процессов.</returns>
        public bool ProcessExistsByName(string processName) => Process.GetProcessesByName(processName).Any();

        /// <summary>
        /// Уничтожает все процессы с указанным именем.
        /// </summary>
        /// <param name="processName">Имя процесса.</param>
        public void ProcessKillByName(string processName) => Process
            .GetProcessesByName(processName)
            .ToList()
            .ForEach(process => process.Kill());

        /// <summary>
        /// Возвращает имя процесса по его идентификатору.
        /// </summary>
        /// <param name="processId">Идентификатор процесса.</param>
        public string GetProcessNameById(int processId) => Process.GetProcessById(processId).ProcessName;

        /// <summary>
        /// Возвращает идентификатор процесса по его имени.
        /// </summary>
        /// <param name="processName">Имя процесса.</param>
        public string GetProcessIdByName(string processName)
        {
            foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT ProcessId, Name FROM Win32_Process").Get())
                if (managementObject["Name"].ToString().Equals(processName))
                    return managementObject["ProcessId"].ToString();

            return string.Empty;
        }

        /// <summary>
        /// Путь до файла процесса по его идентификатору.
        /// </summary>
        /// <param name="processId">Идентификатор процесса.</param>
        public string GetProcessPathById(int processId)
        {
            foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT ProcessId, ExecutablePath FROM Win32_Process").Get())
                if (int.Parse(managementObject["ProcessId"].ToString()).Equals(processId))
                    return managementObject["ExecutablePath"].ToString();

            return string.Empty;
        }

        /// <summary>
        /// Путь до файла процесса по его имени.
        /// </summary>
        /// <param name="processName">Имя процесса.</param>
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
        /// Список подключенных дисков и их объем в гигабайтах.
        /// </summary>
        public Dictionary<string, string> GetDrives()
        {
            Dictionary<string, string> drives = new Dictionary<string, string>();
            Environment
                .GetLogicalDrives()
                .ToList()
                .ForEach(drive =>
            {
                DriveInfo driveInfo = new DriveInfo(drive);

                long totalSize = driveInfo.TotalSize;
                long availableFreeSpace = driveInfo.AvailableFreeSpace;

                double occupiedSizeRounded = Math.Round((double)(totalSize - availableFreeSpace) / 1000000000, 0);
                double totalSizeRounded = Math.Round((double)totalSize / 1000000000, 0);

                drives.Add(drive.Replace(":\\", ""), $@"{occupiedSizeRounded}/{totalSizeRounded}");
            });

            return drives;
        }
    }
}
