
using Hopex.OSI.Information.Enums;
using Hopex.OSI.Information.Services;

using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;

using System.Windows.Forms;

namespace Hopex.OSI.Information
{
    /// <summary>
    /// Summary information about the operating system.
    /// </summary>
    public class SystemInformation
    {
        #region Constantes

        /// <summary>
        /// Registy subkey of windows OS current version.
        /// </summary>
        private const string REGISTRY_CURRENT_VERSION =
            @"Software\Microsoft\Windows NT\CurrentVersion";

        private const int PRODUCT_UNDEFINED = 0x00000000;
        private const int PRODUCT_ULTIMATE = 0x00000001;
        private const int PRODUCT_HOME_BASIC = 0x00000002;
        private const int PRODUCT_HOME_PREMIUM = 0x00000003;
        private const int PRODUCT_ENTERPRISE = 0x00000004;
        private const int PRODUCT_HOME_BASIC_N = 0x00000005;
        private const int PRODUCT_BUSINESS = 0x00000006;
        private const int PRODUCT_STANDARD_SERVER = 0x00000007;
        private const int PRODUCT_DATACENTER_SERVER = 0x00000008;
        private const int PRODUCT_SMALLBUSINESS_SERVER = 0x00000009;
        private const int PRODUCT_ENTERPRISE_SERVER = 0x0000000A;
        private const int PRODUCT_STARTER = 0x0000000B;
        private const int PRODUCT_DATACENTER_SERVER_CORE = 0x0000000C;
        private const int PRODUCT_STANDARD_SERVER_CORE = 0x0000000D;
        private const int PRODUCT_ENTERPRISE_SERVER_CORE = 0x0000000E;
        private const int PRODUCT_ENTERPRISE_SERVER_IA64 = 0x0000000F;
        private const int PRODUCT_BUSINESS_N = 0x00000010;
        private const int PRODUCT_WEB_SERVER = 0x00000011;
        private const int PRODUCT_CLUSTER_SERVER = 0x00000012;
        //private const int PRODUCT_HOME_SERVER = 0x00000013; // Unused.
        private const int PRODUCT_STORAGE_EXPRESS_SERVER = 0x00000014;
        private const int PRODUCT_STORAGE_STANDARD_SERVER = 0x00000015;
        private const int PRODUCT_STORAGE_WORKGROUP_SERVER = 0x00000016;
        private const int PRODUCT_STORAGE_ENTERPRISE_SERVER = 0x00000017;
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS = 0x00000018;
        private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = 0x00000019;
        private const int PRODUCT_HOME_PREMIUM_N = 0x0000001A;
        private const int PRODUCT_ENTERPRISE_N = 0x0000001B;
        private const int PRODUCT_ULTIMATE_N = 0x0000001C;
        private const int PRODUCT_WEB_SERVER_CORE = 0x0000001D;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 0x0000001E;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 0x0000001F;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 0x00000020;
        private const int PRODUCT_SERVER_FOUNDATION = 0x00000021;
        private const int PRODUCT_HOME_PREMIUM_SERVER = 0x00000022;
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 0x00000023;
        private const int PRODUCT_STANDARD_SERVER_V = 0x00000024;
        private const int PRODUCT_DATACENTER_SERVER_V = 0x00000025;
        private const int PRODUCT_ENTERPRISE_SERVER_V = 0x00000026;
        private const int PRODUCT_DATACENTER_SERVER_CORE_V = 0x00000027;
        private const int PRODUCT_STANDARD_SERVER_CORE_V = 0x00000028;
        private const int PRODUCT_ENTERPRISE_SERVER_CORE_V = 0x00000029;
        private const int PRODUCT_HYPERV = 0x0000002A;
        private const int PRODUCT_STORAGE_EXPRESS_SERVER_CORE = 0x0000002B;
        private const int PRODUCT_STORAGE_STANDARD_SERVER_CORE = 0x0000002C;
        private const int PRODUCT_STORAGE_WORKGROUP_SERVER_CORE = 0x0000002D;
        private const int PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE = 0x0000002E;
        private const int PRODUCT_STARTER_N = 0x0000002F;
        private const int PRODUCT_PROFESSIONAL = 0x00000030;
        private const int PRODUCT_PROFESSIONAL_N = 0x00000031;
        private const int PRODUCT_SB_SOLUTION_SERVER = 0x00000032;
        private const int PRODUCT_SERVER_FOR_SB_SOLUTIONS = 0x00000033;
        private const int PRODUCT_STANDARD_SERVER_SOLUTIONS = 0x00000034;
        private const int PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE = 0x00000035;
        private const int PRODUCT_SB_SOLUTION_SERVER_EM = 0x00000036;
        private const int PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM = 0x00000037;
        private const int PRODUCT_SOLUTION_EMBEDDEDSERVER = 0x00000038;
        private const int PRODUCT_SOLUTION_EMBEDDEDSERVER_CORE = 0x00000039;
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT = 0x0000003B;
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL = 0x0000003C;
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC = 0x0000003D;
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC = 0x0000003E;
        private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE = 0x0000003F;
        private const int PRODUCT_CLUSTER_SERVER_V = 0x00000040;
        private const int PRODUCT_EMBEDDED = 0x00000041;
        private const int PRODUCT_STARTER_E = 0x00000042;
        private const int PRODUCT_HOME_BASIC_E = 0x00000043;
        private const int PRODUCT_HOME_PREMIUM_E = 0x00000044;
        private const int PRODUCT_PROFESSIONAL_E = 0x00000045;
        private const int PRODUCT_ENTERPRISE_E = 0x00000046;
        private const int PRODUCT_ULTIMATE_E = 0x00000047;

        private const int VER_NT_WORKSTATION = 1;
        //private const int VER_NT_DOMAIN_CONTROLLER = 2; // Unused.
        private const int VER_NT_SERVER = 3;
        //private const int VER_SUITE_SMALLBUSINESS = 1; // Unused.
        private const int VER_SUITE_ENTERPRISE = 2;
        //private const int VER_SUITE_TERMINAL = 16; // Unused.
        private const int VER_SUITE_DATACENTER = 128;
        //private const int VER_SUITE_SINGLEUSERTS = 256; // Unused.
        private const int VER_SUITE_PERSONAL = 512;
        private const int VER_SUITE_BLADE = 1024;

        #endregion Constantes

#pragma warning disable CS1591
        [DllImport("Kernel32.dll")]
        internal static extern bool GetProductInfo(
            int osMajorVersion,
            int osMinorVersion,
            int spMajorVersion,
            int spMinorVersion,
            out int edition
        );

        [DllImport("kernel32.dll")]
        private static extern bool GetVersionEx(ref InformationVersionInformation osVersionInfo);

        [DllImport("user32")]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("kernel32.dll")]
        public static extern void GetSystemInfo([MarshalAs(UnmanagedType.Struct)] ref Enums.SystemInformation lpSystemInfo);

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public extern static IntPtr LoadLibrary(string libraryName);

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public extern static IntPtr GetProcAddress(IntPtr hwnd, string procedureName);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
#pragma warning restore CS1591

        /// <summary>
        /// Summary information about all user screens.
        /// </summary>
        public List<ScreenInformation> Screens
        {
            get
            {
                List<ScreenInformation> screens = new List<ScreenInformation>();

                Screen.AllScreens
                    .ToList()
                    .ForEach(screen =>
                    {
                        screens.Add(new ScreenInformation()
                        {
                            Name = screen.DeviceName.Replace(@"\\.\", ""),
                            IsPrimary = screen.Primary,
                            Size = new Size(screen.Bounds.Width, screen.Bounds.Height)
                        });
                    });

                return screens;
            }
        }

        /// <summary>
        /// The nominal amount of RAM in gigabytes.
        /// </summary>
        public long RandomAccessMemorySize
        {
            get
            {
                GetPhysicallyInstalledSystemMemory(out long memKb);
                return (memKb / 1024 / 1024);
            }
        }

        /// <summary>
        /// The number of processor cores.
        /// </summary>
        public string CoreCounts => Environment.ProcessorCount.ToString();

        /// <summary>
        /// User name.
        /// </summary>
        public string UserName => Environment.UserName;

        /// <summary>
        /// Computer name.
        /// </summary>
        public string ComputerName => Environment.MachineName;

        /// <summary>
        /// Information about the bitness of the system.
        /// </summary>
        public BitsService Bits => new BitsService();

        /// <summary>
        /// The edition of the operating system running.
        /// </summary>
        public string Edition
        {
            get
            {
                OperatingSystem osVersion = Environment.OSVersion;
                InformationVersionInformation osVersionInfo = new InformationVersionInformation
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(InformationVersionInformation))
                };

                if (GetVersionEx(ref osVersionInfo))
                {
                    int majorVersion = osVersion.Version.Major;
                    int minorVersion = osVersion.Version.Minor;
                    byte productType = osVersionInfo.wProductType;
                    short suiteMask = osVersionInfo.wSuiteMask;

                    if (majorVersion == 4)
                    {
                        if (productType == VER_NT_WORKSTATION)
                        {
                            // Windows NT 4.0 Workstation
                            return "Workstation";
                        }
                        else if (productType == VER_NT_SERVER)
                        {
                            if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                            {
                                // Windows NT 4.0 Server Enterprise
                                return "Enterprise Server";
                            }
                            else
                            {
                                // Windows NT 4.0 Server
                                return "Standard Server";
                            }
                        }
                    }
                    else if (majorVersion == 5)
                    {
                        if (productType == VER_NT_WORKSTATION)
                        {
                            if ((suiteMask & VER_SUITE_PERSONAL) != 0)
                            {
                                return "Home";
                            }
                            else
                            {
                                if (GetSystemMetrics(86) == 0) // 86 == SM_TABLETPC
                                    return "Professional";
                                else
                                    return "Tablet Edition";
                            }
                        }
                        else if (productType == VER_NT_SERVER)
                        {
                            if (minorVersion == 0)
                            {
                                if ((suiteMask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows 2000 Datacenter Server
                                    return "Datacenter Server";
                                }
                                else if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows 2000 Advanced Server
                                    return "Advanced Server";
                                }
                                else
                                {
                                    // Windows 2000 Server
                                    return "Server";
                                }
                            }
                            else
                            {
                                if ((suiteMask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows Server 2003 Datacenter Edition
                                    return "Datacenter";
                                }
                                else if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows Server 2003 Enterprise Edition
                                    return "Enterprise";
                                }
                                else if ((suiteMask & VER_SUITE_BLADE) != 0)
                                {
                                    // Windows Server 2003 Web Edition
                                    return "Web Edition";
                                }
                                else
                                {
                                    // Windows Server 2003 Standard Edition
                                    return "Standard";
                                }
                            }
                        }
                    }
                    else if (majorVersion == 6)
                    {
                        if (GetProductInfo(
                            majorVersion,
                            minorVersion,
                            osVersionInfo.wServicePackMajor,
                            osVersionInfo.wServicePackMinor,
                            out int ed
                        ))
                        {
                            switch (ed)
                            {
                                case PRODUCT_BUSINESS:
                                    return "Business";
                                case PRODUCT_BUSINESS_N:
                                    return "Business N";
                                case PRODUCT_CLUSTER_SERVER:
                                    return "HPC Edition";
                                case PRODUCT_CLUSTER_SERVER_V:
                                    return "HPC Edition without Hyper-V";
                                case PRODUCT_DATACENTER_SERVER:
                                    return "Datacenter Server";
                                case PRODUCT_DATACENTER_SERVER_CORE:
                                    return "Datacenter Server (core installation)";
                                case PRODUCT_DATACENTER_SERVER_V:
                                    return "Datacenter Server without Hyper-V";
                                case PRODUCT_DATACENTER_SERVER_CORE_V:
                                    return "Datacenter Server without Hyper-V (core installation)";
                                case PRODUCT_EMBEDDED:
                                    return "Embedded";
                                case PRODUCT_ENTERPRISE:
                                    return "Enterprise";
                                case PRODUCT_ENTERPRISE_N:
                                    return "Enterprise N";
                                case PRODUCT_ENTERPRISE_E:
                                    return "Enterprise E";
                                case PRODUCT_ENTERPRISE_SERVER:
                                    return "Enterprise Server";
                                case PRODUCT_ENTERPRISE_SERVER_CORE:
                                    return "Enterprise Server (core installation)";
                                case PRODUCT_ENTERPRISE_SERVER_CORE_V:
                                    return "Enterprise Server without Hyper-V (core installation)";
                                case PRODUCT_ENTERPRISE_SERVER_IA64:
                                    return "Enterprise Server for Itanium-based Systems";
                                case PRODUCT_ENTERPRISE_SERVER_V:
                                    return "Enterprise Server without Hyper-V";
                                case PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT:
                                    return "Essential Business Server MGMT";
                                case PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL:
                                    return "Essential Business Server ADDL";
                                case PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC:
                                    return "Essential Business Server MGMTSVC";
                                case PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC:
                                    return "Essential Business Server ADDLSVC";
                                case PRODUCT_HOME_BASIC:
                                    return "Home Basic";
                                case PRODUCT_HOME_BASIC_N:
                                    return "Home Basic N";
                                case PRODUCT_HOME_BASIC_E:
                                    return "Home Basic E";
                                case PRODUCT_HOME_PREMIUM:
                                    return "Home Premium";
                                case PRODUCT_HOME_PREMIUM_N:
                                    return "Home Premium N";
                                case PRODUCT_HOME_PREMIUM_E:
                                    return "Home Premium E";
                                case PRODUCT_HOME_PREMIUM_SERVER:
                                    return "Home Premium Server";
                                case PRODUCT_HYPERV:
                                    return "Microsoft Hyper-V Server";
                                case PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT:
                                    return "Windows Essential Business Management Server";
                                case PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING:
                                    return "Windows Essential Business Messaging Server";
                                case PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY:
                                    return "Windows Essential Business Security Server";
                                case PRODUCT_PROFESSIONAL:
                                    return "Professional";
                                case PRODUCT_PROFESSIONAL_N:
                                    return "Professional N";
                                case PRODUCT_PROFESSIONAL_E:
                                    return "Professional E";
                                case PRODUCT_SB_SOLUTION_SERVER:
                                    return "SB Solution Server";
                                case PRODUCT_SB_SOLUTION_SERVER_EM:
                                    return "SB Solution Server EM";
                                case PRODUCT_SERVER_FOR_SB_SOLUTIONS:
                                    return "Server for SB Solutions";
                                case PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM:
                                    return "Server for SB Solutions EM";
                                case PRODUCT_SERVER_FOR_SMALLBUSINESS:
                                    return "Windows Essential Server Solutions";
                                case PRODUCT_SERVER_FOR_SMALLBUSINESS_V:
                                    return "Windows Essential Server Solutions without Hyper-V";
                                case PRODUCT_SERVER_FOUNDATION:
                                    return "Server Foundation";
                                case PRODUCT_SMALLBUSINESS_SERVER:
                                    return "Windows Small Business Server";
                                case PRODUCT_SMALLBUSINESS_SERVER_PREMIUM:
                                    return "Windows Small Business Server Premium";
                                case PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE:
                                    return "Windows Small Business Server Premium (core installation)";
                                case PRODUCT_SOLUTION_EMBEDDEDSERVER:
                                    return "Solution Embedded Server";
                                case PRODUCT_SOLUTION_EMBEDDEDSERVER_CORE:
                                    return "Solution Embedded Server (core installation)";
                                case PRODUCT_STANDARD_SERVER:
                                    return "Standard Server";
                                case PRODUCT_STANDARD_SERVER_CORE:
                                    return "Standard Server (core installation)";
                                case PRODUCT_STANDARD_SERVER_SOLUTIONS:
                                    return "Standard Server Solutions";
                                case PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE:
                                    return "Standard Server Solutions (core installation)";
                                case PRODUCT_STANDARD_SERVER_CORE_V:
                                    return "Standard Server without Hyper-V (core installation)";
                                case PRODUCT_STANDARD_SERVER_V:
                                    return "Standard Server without Hyper-V";
                                case PRODUCT_STARTER:
                                    return "Starter";
                                case PRODUCT_STARTER_N:
                                    return "Starter N";
                                case PRODUCT_STARTER_E:
                                    return "Starter E";
                                case PRODUCT_STORAGE_ENTERPRISE_SERVER:
                                    return "Enterprise Storage Server";
                                case PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE:
                                    return "Enterprise Storage Server (core installation)";
                                case PRODUCT_STORAGE_EXPRESS_SERVER:
                                    return "Express Storage Server";
                                case PRODUCT_STORAGE_EXPRESS_SERVER_CORE:
                                    return "Express Storage Server (core installation)";
                                case PRODUCT_STORAGE_STANDARD_SERVER:
                                    return "Standard Storage Server";
                                case PRODUCT_STORAGE_STANDARD_SERVER_CORE:
                                    return "Standard Storage Server (core installation)";
                                case PRODUCT_STORAGE_WORKGROUP_SERVER:
                                    return "Workgroup Storage Server";
                                case PRODUCT_STORAGE_WORKGROUP_SERVER_CORE:
                                    return "Workgroup Storage Server (core installation)";
                                case PRODUCT_UNDEFINED:
                                    return "Unknown product";
                                case PRODUCT_ULTIMATE:
                                    return "Ultimate";
                                case PRODUCT_ULTIMATE_N:
                                    return "Ultimate N";
                                case PRODUCT_ULTIMATE_E:
                                    return "Ultimate E";
                                case PRODUCT_WEB_SERVER:
                                    return "Web Server";
                                case PRODUCT_WEB_SERVER_CORE:
                                    return "Web Server (core installation)";
                            }
                        }
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// The name of the operating system running.
        /// </summary>
        public string Name
        {
            get
            {
                OperatingSystem osVersion = Environment.OSVersion;
                InformationVersionInformation osVersionInfo = new InformationVersionInformation
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(InformationVersionInformation))
                };

                if (GetVersionEx(ref osVersionInfo))
                {
                    int majorVersion = osVersion.Version.Major;
                    int minorVersion = osVersion.Version.Minor;

                    if (majorVersion == 6 && minorVersion == 2)
                    {
                        //The registry read workaround is by Scott Vickery. Thanks a lot for the help!

                        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms724832(v=vs.85).aspx

                        // For applications that have been manifested for Windows 8.1 & Windows 10.
                        // Applications not manifested for 8.1 or 10 will return the Windows 8 OS version value (6.2). 
                        // By reading the registry, we'll get the exact version - meaning we can even compare against
                        // Win 8 and Win 8.1.

                        //string exactVersion = RegistryRead(
                        //    REGISTRY_CURRENT_VERSION,
                        //    "CurrentVersion",
                        //    ""
                        //);

                        string exactVersion = new RegEdit.RegEdit(Registry.LocalMachine)
                            .Read(REGISTRY_CURRENT_VERSION, "CurrentVersion")
                            .ToString();

                        if (!string.IsNullOrEmpty(exactVersion))
                        {
                            string[] splitResult = exactVersion.Split('.');
                            majorVersion = Convert.ToInt32(splitResult[0]);
                            minorVersion = Convert.ToInt32(splitResult[1]);
                        }
                        if (IsWindows10())
                        {
                            majorVersion = 10;
                            minorVersion = 0;
                        }
                    }

                    switch (osVersion.Platform)
                    {
                        case PlatformID.Win32S:
                            return "Windows 3.1";
                        case PlatformID.WinCE:
                            return "Windows CE";
                        case PlatformID.Win32Windows:
                        {
                            if (majorVersion == 4)
                            {
                                string csdVersion = osVersionInfo.szCSDVersion;
                                switch (minorVersion)
                                {
                                    case 0:
                                        if (csdVersion == "B" || csdVersion == "C")
                                            return "Windows 95 OSR2";
                                        else
                                            return "Windows 95";
                                    case 10:
                                        if (csdVersion == "A")
                                            return "Windows 98 Second Edition";
                                        else
                                            return "Windows 98";
                                    case 90:
                                        return "Windows Me";
                                }
                            }
                            break;
                        }
                        case PlatformID.Win32NT:
                        {
                            byte productType = osVersionInfo.wProductType;

                            switch (majorVersion)
                            {
                                case 3:
                                    return "Windows NT 3.51";
                                case 4:
                                    switch (productType)
                                    {
                                        case 1:
                                            return "Windows NT 4.0";
                                        case 3:
                                            return "Windows NT 4.0 Server";
                                    }
                                    break;
                                case 5:
                                    switch (minorVersion)
                                    {
                                        case 0:
                                            return "Windows 2000";
                                        case 1:
                                            return "Windows XP";
                                        case 2:
                                            return "Windows Server 2003";
                                    }
                                    break;
                                case 6:
                                    switch (minorVersion)
                                    {
                                        case 0:
                                            switch (productType)
                                            {
                                                case 1:
                                                    return "Windows Vista";
                                                case 3:
                                                    return "Windows Server 2008";
                                            }
                                            break;

                                        case 1:
                                            switch (productType)
                                            {
                                                case 1:
                                                    return "Windows 7";
                                                case 3:
                                                    return "Windows Server 2008 R2";
                                            }
                                            break;
                                        case 2:
                                            switch (productType)
                                            {
                                                case 1:
                                                    return "Windows 8";
                                                case 3:
                                                    return "Windows Server 2012";
                                            }
                                            break;
                                        case 3:
                                            switch (productType)
                                            {
                                                case 1:
                                                    return "Windows 8.1";
                                                case 3:
                                                    return "Windows Server 2012 R2";
                                            }
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (minorVersion)
                                    {
                                        case 0:
                                            switch (productType)
                                            {
                                                case 1:
                                                    return "Windows 10";
                                                case 3:
                                                    return "Windows Server 2016";
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        }
                    }
                }

                return "unknown";
            }
        }

        /// <summary>
        /// The service pack information of the operating system running.
        /// </summary>
        public string ServicePack
        {
            get
            {
                InformationVersionInformation osVersionInfo = new InformationVersionInformation
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(InformationVersionInformation))
                };

                if (GetVersionEx(ref osVersionInfo))
                {
                    return osVersionInfo.szCSDVersion;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// The full version string of the operating system running.
        /// </summary>
        public string VersionString => string.Join(".", MajorVersion, MinorVersion, BuildVersion, RevisionVersion);

        /// <summary>
        /// The full version of the operating system running.
        /// </summary>
        public Version Version => new Version(MajorVersion, MinorVersion, BuildVersion, RevisionVersion);

        /// <summary>
        /// The major version number of the operating system running.
        /// </summary>
        public int MajorVersion
        {
            get
            {
                if (IsWindows10())
                {
                    return 10;
                }

                string exactVersion = new RegEdit.RegEdit(Registry.LocalMachine)
                    .Read(REGISTRY_CURRENT_VERSION, "CurrentVersion")
                    .ToString();

                if (!string.IsNullOrEmpty(exactVersion))
                {
                    string[] splitVersion = exactVersion.Split('.');
                    return int.Parse(splitVersion[0]);
                }
                return Environment.OSVersion.Version.Major;
            }
        }

        /// <summary>
        /// The minor version number of the operating system running.
        /// </summary>
        public int MinorVersion
        {
            get
            {
                if (IsWindows10())
                {
                    return 0;
                }

                string exactVersion = new RegEdit.RegEdit(Registry.LocalMachine)
                    .Read(REGISTRY_CURRENT_VERSION, "CurrentVersion")
                    .ToString();

                if (!string.IsNullOrEmpty(exactVersion))
                {
                    string[] splitVersion = exactVersion.Split('.');
                    return int.Parse(splitVersion[1]);
                }

                return Environment.OSVersion.Version.Minor;
            }
        }

        /// <summary>
        /// The build version number of the operating system running.
        /// </summary>
        public int BuildVersion => int.Parse(new RegEdit
                .RegEdit(Registry.LocalMachine)
                .Read(REGISTRY_CURRENT_VERSION, "CurrentBuildNumber")
                .ToString()
            );

        /// <summary>
        /// The revision version number of the operating system running.
        /// </summary>
        public int RevisionVersion
        {
            get => IsWindows10()
                ? 0
                : Environment.OSVersion.Version.Revision;
        }

        /// <summary>
        /// List of installed versions .NET Framework.
        /// </summary>
        public List<string> DotNetFrameworkVersions => new FrameworkService().Versions;

        /// <summary>
        /// The bitness of Windows OS (32/64).
        /// </summary>
        public int WindowsBit => Environment.Is64BitOperatingSystem ? 64 : 32;

        /// <summary>
        /// Summary information about the operating system.
        /// </summary>
        public SystemInformation()
        {

        }

        /// <summary>
        /// Determines whether the current operating system is Windows 10.
        /// </summary>
        /// <returns>System is Windows 10 if <see langword="true"/>.</returns>
        private bool IsWindows10()
        {
            return new RegEdit.RegEdit(Registry.LocalMachine)
                .Read(REGISTRY_CURRENT_VERSION, "ProductName")
                .ToString()
                .StartsWith("Windows 10", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns the name of the Windows release.
        /// </summary>
        public string WindowsVersion
        {
            get
            {
                switch ($"{MajorVersion}.{MinorVersion}")
                {
                    case "5.0":
                        return "Windows 2000";
                    case "5.1":
                        return "Windows XP";
                    case "5.2":
                        return "Windows XP (R2)";
                    case "6.0":
                        return "Windows Vista";
                    case "6.1":
                        return "Windows 7";
                    case "6.2":
                        return "Windows 8";
                    case "6.3":
                        return "Windows 8.1";
                    case "10.0":
                        return "Windows 10";
                    default:
                        return "Unknow";
                }
            }
        }

        /// <summary>
        /// A list of connected disks and their size in gigabytes.
        /// </summary>
        public Dictionary<string, string> Drives
        {
            get
            {
                Dictionary<string, string> drives = new Dictionary<string, string>();
                Environment
                    .GetLogicalDrives()
                    .ToList()
                    .ForEach(drive =>
                    {
                        DriveInfo driveInfo = new DriveInfo(drive);
                        if (driveInfo.IsReady)
                        {
                            long totalSize = driveInfo.TotalSize;
                            long availableFreeSpace = driveInfo.AvailableFreeSpace;

                            double occupiedSizeRounded = Math.Round((double)(totalSize - availableFreeSpace) / 1000000000, 0);
                            double totalSizeRounded = Math.Round((double)totalSize / 1000000000, 0);

                            drives.Add(drive.Replace(":\\", ""), $@"{occupiedSizeRounded}/{totalSizeRounded}");
                        }                        
                    });

                return drives;
            }
        }
    }
}
