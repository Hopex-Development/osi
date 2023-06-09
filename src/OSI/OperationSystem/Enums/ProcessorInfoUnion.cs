using System.Runtime.InteropServices;

namespace Hopex.OSI.OperationSystem.Enums
{
    /// <summary>
    /// Processor info union.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ProcessorInfoUnion
    {
        /// <summary>
        /// OEM ID.
        /// </summary>
        [FieldOffset(0)]
        internal uint dwOemId;

        /// <summary>
        /// Processor architecture
        /// </summary>
        [FieldOffset(0)]
        internal ushort wProcessorArchitecture;

        /// <summary>
        /// Reserved.
        /// </summary>
        [FieldOffset(2)]
        internal ushort wReserved;
    }
}
