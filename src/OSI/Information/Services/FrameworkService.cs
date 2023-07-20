using System.Collections.Generic;
using Microsoft.Win32;

namespace Hopex.OSI.Information.Services
{
    /// <summary>
    /// Service for obtaining data about frameworks.
    /// </summary>
    public class FrameworkService
    {
        /// <summary>
        /// Registry subkey versions younger than 4.5.
        /// </summary>
        private const string SUBKEY_NDP = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\";

        /// <summary>
        /// Registry subkey versions older than 4.5
        /// </summary>
        private const string SUBKEY_NDP_V4 = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

        /// <summary>
        /// List of installed versions .NET Framework.
        /// </summary>
        public List<string> Versions = new List<string>();

        /// <summary>
        /// A service for obtaining data about bits.
        /// </summary>
        public FrameworkService()
        {
            Get1To45VersionFromRegistry();
            Get45PlusFromRegistry();
        }

        /// <summary>
        /// Writing data about installed versions to the local property .NET Framework.
        /// </summary>
        /// <param name="version">Framework version.</param>
        /// <param name="spLevel">Service pack version.</param>
        private void WriteVersion(string version, string spLevel = "")
        {
            version = version.Trim();
            if (string.IsNullOrEmpty(version))
                return;

            string spLevelString = "";
            if (!string.IsNullOrEmpty(spLevel))
                spLevelString = " Service Pack " + spLevel;

            Versions.Add($"{version}{spLevelString}");
        }

        /// <summary>
        /// Getting versions younger than 4.5.
        /// </summary>
        private void Get1To45VersionFromRegistry()
        {
            // Open the registry key for writing .NET Framework.
            using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(SUBKEY_NDP))
            {
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    // We skip the information about version 4.5.
                    if (versionKeyName == "v4")
                        continue;

                    if (versionKeyName.StartsWith("v"))
                    {
                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);

                        // We get the value .NET Framework versions.
                        string name = (string)versionKey.GetValue("Version", "");

                        // We get the number of the service package (SP).
                        string sp = versionKey.GetValue("SP", "").ToString();

                        //We get the installation flag or an empty string if there is none.
                        string install = versionKey.GetValue("Install", "").ToString();

                        // There is no information about the installation, it must be in a child unit.
                        if (string.IsNullOrEmpty(install))
                            WriteVersion(name);
                        else
                        {
                            if (!(string.IsNullOrEmpty(sp)) && install == "1")
                                WriteVersion(name, sp);
                        }
                        if (!string.IsNullOrEmpty(name))
                            continue;

                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", "");
                            if (!string.IsNullOrEmpty(name))
                                sp = subKey.GetValue("SP", "").ToString();

                            install = subKey.GetValue("Install", "").ToString();

                            // If there is no installation information.
                            if (string.IsNullOrEmpty(install))
                                WriteVersion(name);
                            else
                            {
                                if (!(string.IsNullOrEmpty(sp)) && install == "1")
                                    WriteVersion(name, sp);
                                else if (install == "1")
                                    WriteVersion(name);

                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Getting versions older than 4.5.
        /// </summary>
        private void Get45PlusFromRegistry()
        {
            using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(SUBKEY_NDP_V4))
            {
                if (ndpKey.Equals(null))
                    return;

                // First, we check whether a specific version is specified.
                if (!ndpKey.GetValue("Version").Equals(null))
                    WriteVersion(ndpKey.GetValue("Version").ToString());
                else
                {
                    if (!ndpKey.Equals(null) && !ndpKey.GetValue("Release").Equals(null))
                        WriteVersion(CheckFor45PlusVersion((int)ndpKey.GetValue("Release")));
                }
            }
        }

        /// <summary>
        /// Check for 4.5+ versions.
        /// </summary>
        /// <param name="releaseKey">Release key.</param>
        /// <returns>Version 4.5+</returns>
        private string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 528040)
                return "4.8";
            if (releaseKey >= 461808)
                return "4.7.2";
            if (releaseKey >= 461308)
                return "4.7.1";
            if (releaseKey >= 460798)
                return "4.7";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
                return "4.6.1";
            if (releaseKey >= 393295)
                return "4.6";
            if (releaseKey >= 379893)
                return "4.5.2";
            if (releaseKey >= 378675)
                return "4.5.1";
            if (releaseKey >= 378389)
                return "4.5";

            // This code should never be executed.
            // The non-zero release key should mean that version 4.5 or later is already installed.
            return "Unknown";
        }
    }
}
