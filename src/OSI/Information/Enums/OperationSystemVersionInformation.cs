using System.Runtime.InteropServices;

namespace Hopex.OSI.Information.Enums
{
    /// <summary>
    /// Operating system version informations.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct InformationVersionInformation
    {
        /// <summary>
        /// Information of size, operating system version.
        /// </summary>
        public int dwOSVersionInfoSize;

        /// <summary>
        /// Major version.
        /// </summary>
        public int dwMajorVersion;

        /// <summary>
        /// Minor version.
        /// </summary>
        public int dwMinorVersion;

        /// <summary>
        /// Number of build.
        /// </summary>
        public int dwBuildNumber;

        /// <summary>
        /// Platform ID.
        /// </summary>
        public int dwPlatformId;

        /// <summary>
        /// Version of CSD.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;

        /// <summary>
        /// Service pack major version.
        /// </summary>
        public short wServicePackMajor;

        /// <summary>
        /// Service pack minor version.
        /// </summary>
        public short wServicePackMinor;

        /// <summary>
        /// Suite mask.
        /// </summary>
        public short wSuiteMask;

        /// <summary>
        /// Product type.
        /// </summary>
        public byte wProductType;

        /// <summary>
        /// Reserved.
        /// </summary>
        public byte wReserved;
    }
}
