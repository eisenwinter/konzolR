using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Utility
{
    internal static class ColorConverter
    {
       
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
