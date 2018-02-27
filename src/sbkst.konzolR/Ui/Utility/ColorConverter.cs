using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Utility
{
    internal static class ColorConverter
    {
        /// <summary>
        /// convert predefined colors to constants
        /// </summary>
        /// <param name="color">predefined konzolr colors</param>
        /// <returns></returns>
        public static ushort ConvertColor(this ConsoleWindowBackgroundColor color)
        {
            switch (color)
            {
                case ConsoleWindowBackgroundColor.Blue:
                    return Internals.W32ConsoleConstants.BACKGROUND_BLUE;

                case ConsoleWindowBackgroundColor.Red:
                    return Internals.W32ConsoleConstants.BACKGROUND_RED;

                case ConsoleWindowBackgroundColor.Green:
                    return Internals.W32ConsoleConstants.BACKGROUND_GREEN;
                default:
                    return 0;
            }
        }

        public static ushort ColorToForegroundDWORD(this ConsoleColor color)
        {
            return (ushort)color;
        }

        public static ushort ColorToBackgroundDWORD(this ConsoleColor color)
        {
            return (ushort)((ushort)color << 4);
        }

        /// <summary>
        /// Convert system colo to dword
        /// </summary>
        /// <param name="color">system color</param>
        /// <returns></returns>
        //remove dependency on system.drawing
        //public static ushort ColorDWORDFromSystemColor(this System.Drawing.Color color)
        //{
        //    return (ushort)(color.R + (((ushort)color.G) << 8) + (((ushort)color.B) << 16));
        //}
    }
}
