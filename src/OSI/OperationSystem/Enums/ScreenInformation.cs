using System;
using System.Drawing;
using System.Linq;

namespace Hopex.OSI.OperationSystem.Enums
{
    /// <summary>
    /// Basic information about the user's display.
    /// </summary>
    public struct ScreenInformation
    {
        /// <summary>
        /// Display name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Indicates whether the display is the primary one.
        /// </summary>
        public bool IsPrimary { get; internal set; }

        /// <summary>
        /// Display resolution.
        /// </summary>
        public Size Size { get; internal set; }
    }
}
