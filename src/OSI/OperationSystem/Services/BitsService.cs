
using Hopex.OSI.OperationSystem.Enums;

using System;
using System.Diagnostics;
using System.Linq;

using System.Runtime.InteropServices;

namespace Hopex.OSI.OperationSystem.Services
{
    /// <summary>
    /// A service for obtaining data about bits.
    /// </summary>
    public class BitsService
    {
#pragma warning disable CS1591
        [DllImport("kernel32.dll")]
        public static extern void GetNativeSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SystemInformation lpSystemInfo);

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public extern static IntPtr GetProcAddress(IntPtr hwnd, string procedureName);

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public extern static IntPtr LoadLibrary(string libraryName);
#pragma warning restore CS1591

        private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, [Out] out bool isWow64Process);

        /// <summary>
        /// A service for obtaining data about bits.
        /// </summary>
        public BitsService()
        {

        }

        /// <summary>
        /// Is the delegate of the Wow 64 process.
        /// </summary>
        private IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
        {
            IntPtr handle = LoadLibrary("kernel32");

            if (handle != IntPtr.Zero)
            {
                IntPtr fnPtr = GetProcAddress(handle, "IsWow64Process");

                if (fnPtr != IntPtr.Zero)
                {
                    return (IsWow64ProcessDelegate)Marshal.GetDelegateForFunctionPointer(
                        fnPtr,
                        typeof(IsWow64ProcessDelegate)
                    );
                }
            }

            return null;
        }

        /// <summary>
        /// Is a 32-bit processor on a 64-bit processor.
        /// </summary>
        private bool Is32BitProcessOn64BitProcessor()
        {
            IsWow64ProcessDelegate fnDelegate = GetIsWow64ProcessDelegate();

            if (fnDelegate.Equals(null))
            {
                return false;
            }

            bool retVal = fnDelegate.Invoke(Process.GetCurrentProcess().Handle, out bool isWow64);

            return retVal
                ? isWow64
                : retVal;
        }

        /// <summary>
        /// Program (software) architecture.
        /// </summary>
        public SoftwareArchitecture ProgramBits
        {
            get
            {
                switch (IntPtr.Size * 8)
                {
                    case 64:
                        return SoftwareArchitecture.Bit64;
                    case 32:
                        return SoftwareArchitecture.Bit32;
                    default:
                        return SoftwareArchitecture.Unknown;
                }
            }
        }

        /// <summary>
        /// Operation system bits.
        /// </summary>
        public SoftwareArchitecture OperationSystemBits
        {
            get
            {
                switch (IntPtr.Size * 8)
                {
                    case 64:
                        return SoftwareArchitecture.Bit64;
                    case 32:
                        if (Is32BitProcessOn64BitProcessor())
                            return SoftwareArchitecture.Bit64;
                        else
                            return SoftwareArchitecture.Bit32;
                    default:
                        return SoftwareArchitecture.Unknown;
                }
            }
        }

        /// <summary>
        /// Processor bits.
        /// </summary>
        public ProcessorArchitecture ProcessorBits
        {
            get
            {
                try
                {
                    SystemInformation nativeSystemInfo = new SystemInformation();
                    GetNativeSystemInfo(ref nativeSystemInfo);

                    switch (nativeSystemInfo.uProcessorInfo.wProcessorArchitecture)
                    {
                        case 9: // PROCESSOR_ARCHITECTURE_AMD64
                            return ProcessorArchitecture.Bit64;
                        case 6: // PROCESSOR_ARCHITECTURE_IA64
                            return ProcessorArchitecture.Itanium64;
                        case 0: // PROCESSOR_ARCHITECTURE_INTEL
                            return ProcessorArchitecture.Bit32;
                        default: // PROCESSOR_ARCHITECTURE_UNKNOWN
                            return ProcessorArchitecture.Unknown;
                    }
                }
                catch
                {
                    return ProcessorArchitecture.Unknown;
                }
            }
        }

    }
}
