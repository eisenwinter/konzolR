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

        public static ConsoleColor Highlight(this ConsoleColor color)
        {
            if (color.IsDark()) return color.Brighten();
            else return color.Darken();
        }

        public static bool IsDark(this ConsoleColor color)
        {
            return new ConsoleColor[] { ConsoleColor.DarkBlue, ConsoleColor.DarkCyan, ConsoleColor.DarkGray, ConsoleColor.DarkGreen, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.Black }
            .Contains(color);
        }

        public static bool IsBright(this ConsoleColor color)
        {
            return !color.IsDark();
        }

        public static ConsoleColor Brighten(this ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black:
                    return ConsoleColor.DarkGray;
                case ConsoleColor.DarkBlue:
                    return ConsoleColor.Blue;
                case ConsoleColor.DarkCyan:
                    return ConsoleColor.Cyan;
                case ConsoleColor.DarkGray:
                    return ConsoleColor.Gray;
                case ConsoleColor.DarkGreen:
                    return ConsoleColor.Green;
                case ConsoleColor.DarkMagenta:
                    return ConsoleColor.Magenta;
                case ConsoleColor.DarkRed:
                    return ConsoleColor.Red;
                case ConsoleColor.DarkYellow:
                    return ConsoleColor.Yellow;
                case ConsoleColor.Gray:
                    return ConsoleColor.White;
                default:
                    return color;
            }
        }

        public static ConsoleColor Darken(this ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.DarkGray:
                    return ConsoleColor.Black;
                case ConsoleColor.Blue:
                    return ConsoleColor.DarkBlue;
                case ConsoleColor.Cyan:
                    return ConsoleColor.DarkCyan;
                case ConsoleColor.Gray:
                    return ConsoleColor.DarkGray;
                case ConsoleColor.Green:
                    return ConsoleColor.DarkGreen;
                case ConsoleColor.Magenta:
                    return ConsoleColor.DarkMagenta;
                case ConsoleColor.Red:
                    return ConsoleColor.DarkRed;
                case ConsoleColor.Yellow:
                    return ConsoleColor.DarkYellow;
                case ConsoleColor.White:
                    return ConsoleColor.Gray;
                default:
                    return color;
            }
        }

        public static ushort RgbToForegroundDWORD(uint r, uint g, uint b)
        {
            return (ushort)(r + (g << 8) + (b << 16));
        }


        public static ushort RgbToBackgroundDWORD(uint r, uint g, uint b)
        {
            return (ushort)(RgbToForegroundDWORD(r,g,b) << 4);
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
