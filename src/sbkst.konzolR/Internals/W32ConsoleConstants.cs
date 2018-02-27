using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Internals
{
    /// <summary>
    /// w32 constants
    /// </summary>
    class W32ConsoleConstants
    {
        public const ushort BACKGROUND_BLUE = 0x0010;
        public const ushort BACKGROUND_GREEN = 0x0020;
        public const ushort BACKGROUND_RED = 0x0040;
        public const ushort BACKGROUND_INTENSITY = 0x0080;

        public const ushort FOREGROUND_RED = 0x0004;
        public const ushort FOREGROUND_GREEN = 0x0002;
        public const ushort FOREGROUND_BLUE = 0x0001;
        public const ushort FOREGROUND_INTENSITY = 0x0008;

        public const uint CONSOLE_TEXTMODE_BUFFER = 0x00000001;

        public const uint FILE_SHARE_READ = 0x00000001;
        public const uint FILE_SHARE_WRITE = 0x00000002;

        public const uint GENERIC_WRITE = 0x40000000;
        public const uint GENERIC_READ = 0x80000000;

        public const uint STD_INPUT_HANDLE = 0xFFFFFFF6;
        public const uint STD_OUTPUT_HANDLE = 0xFFFFFFF5;
        public const uint STD_ERROR_HANDLE = 0xFFFFFFF4;
        public const uint INVALID_HANDLE_VALUE = 0xFFFFFFFF;

        public const uint ENABLE_PROCESSED_INPUT = 0x0001;
        public const uint ENABLE_LINE_INPUT = 0x0002;
        public const uint ENABLE_ECHO_INPUT = 0x0004;
        public const uint ENABLE_WINDOW_INPUT = 0x0008;
        public const uint ENABLE_MOUSE_INPUT = 0x0010;
        public const uint ENABLE_PROCESSED_OUTPUT = 0x0001;
        public const uint ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002;


        public const uint KEY_EVENT = 0x0001;
        public const uint MOUSE_EVENT = 0x0002;
        public const uint WINDOW_BUFFER_SIZE_EVENT = 0x0004;
        public const uint MENU_EVENT = 0x0008;
        public const uint FOCUS_EVENT = 0x0010;

        public const uint CAPSLOCK_ON = 0x0080;
        public const uint ENHANCED_KEY = 0x0100;
        public const uint NUMLOCK_ON = 0x0020;
        public const uint SHIFT_PRESSED = 0x0010;
        public const uint LEFT_CTRL_PRESSED = 0x0008;
        public const uint RIGHT_CTRL_PRESSED = 0x0004;
        public const uint LEFT_ALT_PRESSED = 0x0002;
        public const uint RIGHT_ALT_PRESSED = 0x0001;
        public const uint SCROLLLOCK_ON = 0x0040;

        public const uint MOUSE_WHEELED = 0x0004;
        public const uint DOUBLE_CLICK = 0x0002;
        public const uint MOUSE_MOVED = 0x0001;
        public const uint FROM_LEFT_1ST_BUTTON_PRESSED = 0x0001;
        public const uint FROM_LEFT_2ND_BUTTON_PRESSED = 0x0004;
        public const uint FROM_LEFT_3RD_BUTTON_PRESSED = 0x0008;
        public const uint FROM_LEFT_4TH_BUTTON_PRESSED = 0x0010;
        public const uint RIGHTMOST_BUTTON_PRESSED = 0x0002;

        public const uint CTRL_C_EVENT = 0x0000;
        public const uint CTRL_BREAK_EVENT = 0x0001;
        public const uint CTRL_CLOSE_EVENT = 0x0002;
        public const uint CTRL_LOGOFF_EVENT = 0x0005;
        public const uint CTRL_SHUTDOWN_EVENT = 0x0006;

        public const uint COMMON_LVB_REVERSE_VIDEO = 0x16384;

    }
}
