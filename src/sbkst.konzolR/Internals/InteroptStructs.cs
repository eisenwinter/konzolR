using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace sbkst.konzolR.Internals
{
    partial class ConsoleInteropt
    {

#pragma warning disable 0649
        /// <summary>
        /// Defines the coordinates of a character cell in a console screen buffer. The origin of the coordinate system (0,0) is at the top, left cell of the buffer
        /// https://docs.microsoft.com/en-us/windows/console/coord-str
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {

            public short X;
            public short Y;

        }
        /// <summary>
        /// Defines the coordinates of the upper left and lower right corners of a rectangle.
        /// https://docs.microsoft.com/en-us/windows/console/small-rect-str
        /// </summary>
        public struct SMALL_RECT
        {

            public short Left;
            public short Top;
            public short Right;
            public short Bottom;

        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/console/char-info-str
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct CHAR_INFO
        {
            [FieldOffset(0)]
#pragma warning disable IDE0044 // Add readonly modifier
            char UnicodeChar;
#pragma warning restore IDE0044 // Add readonly modifier
            [FieldOffset(0)]
#pragma warning disable IDE0044 // Add readonly modifier
            char AsciiChar;
#pragma warning restore IDE0044 // Add readonly modifier
            [FieldOffset(2)]
#pragma warning disable IDE0044 // Add readonly modifier
            UInt16 Attributes;
#pragma warning restore IDE0044 // Add readonly modifier
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FOCUS_EVENT_RECORD
        {
            public uint bSetFocus;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct MENU_EVENT_RECORD
        {
            public uint dwCommandId;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct KEY_EVENT_RECORD
        {
            [FieldOffset(0), MarshalAs(UnmanagedType.Bool)]
            public bool bKeyDown;
            [FieldOffset(4), MarshalAs(UnmanagedType.U2)]
            public ushort wRepeatCount;
            [FieldOffset(6), MarshalAs(UnmanagedType.U2)]
            //public VirtualKeys wVirtualKeyCode;
            public ushort wVirtualKeyCode;
            [FieldOffset(8), MarshalAs(UnmanagedType.U2)]
            public ushort wVirtualScanCode;
            [FieldOffset(10)]
            public char UnicodeChar;
            [FieldOffset(12), MarshalAs(UnmanagedType.U4)]
            //public ControlKeyState dwControlKeyState;
            public uint dwControlKeyState;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSE_EVENT_RECORD
        {
            public COORD dwMousePosition;
            public uint dwButtonState;
            public uint dwControlKeyState;
            public uint dwEventFlags;
        }


        public struct WINDOW_BUFFER_SIZE_RECORD
        {
            public COORD dwSize;
            public WINDOW_BUFFER_SIZE_RECORD(short x, short y)
            {
                dwSize = new COORD
                {
                    X = x,
                    Y = y
                };
            }
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/console/input-record-str
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT_RECORD
        {
            [FieldOffset(0)]
            public ushort EventType;
            [FieldOffset(4)]
            public KEY_EVENT_RECORD KeyEvent;
            [FieldOffset(4)]
            public MOUSE_EVENT_RECORD MouseEvent;
            [FieldOffset(4)]
            public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;
            [FieldOffset(4)]
            public MENU_EVENT_RECORD MenuEvent;
            [FieldOffset(4)]
            public FOCUS_EVENT_RECORD FocusEvent;
        }

        public struct CONSOLE_SCREEN_BUFFER_INFO
        {

            public COORD dwSize;
            public COORD dwCursorPosition;
            public short wAttributes;
            public SMALL_RECT srWindow;
            public COORD dwMaximumWindowSize;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_CURSOR_INFO
        {
            public uint Size;
            public bool Visible;
        }
#pragma warning restore 0649


    }
}
