using System;
using System.Runtime.InteropServices;

namespace Hopex.OSI.OperationSystem.Enums
{
    /// <summary>
    /// System information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SystemInformation
    {
        /// <summary>
        /// Processor information.
        /// </summary>
        internal ProcessorInfoUnion uProcessorInfo;

        /// <summary>
        /// Page size.
        /// </summary>
        public uint dwPageSize;

        /// <summary>
        /// Minimum application address.
        /// </summary>
        public IntPtr lpMinimumApplicationAddress;

        /// <summary>
        /// Maximum application address.
        /// </summary>
        public IntPtr lpMaximumApplicationAddress;

        /// <summary>
        /// Active processor mask.
        /// </summary>
        public IntPtr dwActiveProcessorMask;

        /// <summary>
        /// Number of processors.
        /// </summary>
        public uint dwNumberOfProcessors;

        /// <summary>
        /// Processor type.
        /// </summary>
        public uint dwProcessorType;

        /// <summary>
        /// Allocation granularity.
        /// </summary>
        public uint dwAllocationGranularity;

        /// <summary>
        /// Processor level.
        /// </summary>
        public ushort dwProcessorLevel;

        /// <summary>
        /// Processor revision.
        /// </summary>
        public ushort dwProcessorRevision;
    }
}
