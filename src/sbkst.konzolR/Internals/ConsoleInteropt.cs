using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace sbkst.konzolR.Internals
{
    /// <summary>
    /// Calls used for interopt with console, according to https://docs.microsoft.com/en-us/windows/console/console-functions
    /// </summary>
    partial class ConsoleInteropt
    {

        /// <summary>
        /// Retrieves the window handle used by the console associated with the calling process.
        /// https://docs.microsoft.com/en-us/windows/console/getconsolewindow
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// Sets the specified screen buffer to be the currently displayed console screen buffer.
        /// https://docs.microsoft.com/en-us/windows/console/setconsoleactivescreenbuffer
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleActiveScreenBuffer(IntPtr hConsoleOutput);

        /// <summary>
        /// Creates a console screen buffer.
        /// https://docs.microsoft.com/en-us/windows/console/createconsolescreenbuffer
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="dwShareMode"></param>
        /// <param name="lpSecurityAttributes"></param>
        /// <param name="dwFlags"></param>
        /// <param name="lpScreenBufferData"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateConsoleScreenBuffer(uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwFlags, IntPtr lpScreenBufferData);

        /// <summary>
        /// Sets the cursor position in the specified console screen buffer.
        /// https://docs.microsoft.com/en-us/windows/console/setconsolecursorposition
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="dwCursorPosition"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleCursorPosition(IntPtr hConsoleOutput, COORD dwCursorPosition);

        /// <summary>
        /// Moves a block of data in a screen buffer. The effects of the move can be limited by specifying a clipping rectangle, so the contents of the console screen buffer outside the clipping rectangle are unchanged.
        /// https://docs.microsoft.com/en-us/windows/console/scrollconsolescreenbuffer
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="lpScrollRectangle"></param>
        /// <param name="lpClipRectangle"></param>
        /// <param name="dwDestinationOrigin"></param>
        /// <param name="lpFill"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ScrollConsoleScreenBuffer(IntPtr hConsoleOutput, [In] ref SMALL_RECT lpScrollRectangle, IntPtr lpClipRectangle, COORD dwDestinationOrigin, [In] ref CHAR_INFO lpFill);

        /// <summary>
        /// Reads data from the specified console input buffer without removing it from the buffer.
        /// https://docs.microsoft.com/en-us/windows/console/peekconsoleinput
        /// </summary>
        /// <param name="hConsoleInput"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nLength"></param>
        /// <param name="lpNumberOfEventsRead"></param>
        /// <returns></returns>

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool PeekConsoleInput(IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, uint nLength, out uint lpNumberOfEventsRead);

        /// <summary>
        /// Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
        /// https://docs.microsoft.com/en-us/windows/console/getstdhandle
        /// </summary>
        /// <param name="nStdHandle"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        /// <summary>
        /// Retrieves information about the specified console screen buffer.
        /// https://docs.microsoft.com/en-us/windows/console/getconsolescreenbufferinfo
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="lpConsoleScreenBufferInfo"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleScreenBufferInfo(IntPtr hConsoleOutput, out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

        /// <summary>
        /// Retrieves information about the size and visibility of the cursor for the specified console screen buffer.
        /// https://docs.microsoft.com/en-us/windows/console/getconsolecursorinfo
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="lpConsoleCursorInfo"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleCursorInfo(IntPtr hConsoleOutput, out CONSOLE_CURSOR_INFO lpConsoleCursorInfo);

        /// <summary>
        /// Flushes the console input buffer. All input records currently in the input buffer are discarded.
        /// https://docs.microsoft.com/en-us/windows/console/flushconsoleinputbuffer
        /// </summary>
        /// <param name="hConsoleInput"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FlushConsoleInputBuffer(IntPtr hConsoleInput);

        /// <summary>
        /// Writes a character to the console screen buffer a specified number of times, beginning at the specified coordinates.
        /// https://docs.microsoft.com/en-us/windows/console/fillconsoleoutputcharacter
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="cCharacter"></param>
        /// <param name="nLength"></param>
        /// <param name="dwWriteCoord"></param>
        /// <param name="lpNumberOfCharsWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FillConsoleOutputCharacter(IntPtr hConsoleOutput, char cCharacter, uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

        /// <summary>
        /// Sets the character attributes for a specified number of character cells, beginning at the specified coordinates in a screen buffer.
        /// https://docs.microsoft.com/en-us/windows/console/fillconsoleoutputattribute
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="wAttribute"></param>
        /// <param name="nLength"></param>
        /// <param name="dwWriteCoord"></param>
        /// <param name="lpNumberOfAttrsWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FillConsoleOutputAttribute(IntPtr hConsoleOutput, ushort wAttribute, uint nLength, COORD dwWriteCoord, out uint lpNumberOfAttrsWritten);

        /// <summary>
        /// Reads character input from the console input buffer and removes it from the buffer.
        /// https://docs.microsoft.com/en-us/windows/console/readconsole
        /// </summary>
        /// <param name="hConsoleInput"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nNumberOfCharsToRead"></param>
        /// <param name="lpNumberOfCharsRead"></param>
        /// <param name="lpReserved"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadConsole(IntPtr hConsoleInput, [Out] StringBuilder lpBuffer, uint nNumberOfCharsToRead, out uint lpNumberOfCharsRead, IntPtr lpReserved);

        /// <summary>
        /// Reads data from a console input buffer and removes it from the buffer.
        /// https://docs.microsoft.com/en-us/windows/console/readconsoleinput
        /// </summary>
        /// <param name="hConsoleInput"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nLength"></param>
        /// <param name="lpNumberOfEventsRead"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)]
        public static extern bool ReadConsoleInput(IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, uint nLength, out uint lpNumberOfEventsRead);


        /// <summary>
        /// Sets the size and visibility of the cursor for the specified console screen buffer.
        /// https://docs.microsoft.com/en-us/windows/console/setconsolecursorinfo
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="lpConsoleCursorInfo"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput, [In] ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo);

        /// <summary>
        /// Changes the size of the specified console screen buffer.
        /// https://docs.microsoft.com/en-us/windows/console/setconsolescreenbuffersize
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="dwSize"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD dwSize);

        /// <summary>
        /// Sets the attributes of characters written to the console screen buffer by the WriteFile or WriteConsole function, or echoed by the ReadFile or ReadConsole function. This function affects text written after the function call.
        /// https://docs.microsoft.com/en-us/windows/console/setconsoletextattribute
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="wAttributes"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, ushort wAttributes);

        /// <summary>
        /// Sets the handle for the specified standard device (standard input, standard output, or standard error).
        /// https://docs.microsoft.com/en-us/windows/console/setstdhandle
        /// </summary>
        /// <param name="nStdHandle"></param>
        /// <param name="hHandle"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetStdHandle(uint nStdHandle, IntPtr hHandle);

        /// <summary>
        /// Sets the current size and position of a console screen buffer's window.
        /// https://docs.microsoft.com/en-us/windows/console/setconsolewindowinfo
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="bAbsolute"></param>
        /// <param name="lpConsoleWindow"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleWindowInfo(IntPtr hConsoleOutput, bool bAbsolute, [In] ref SMALL_RECT lpConsoleWindow);

        /// <summary>
        /// Writes a character string to a console screen buffer beginning at the current cursor location.
        /// https://docs.microsoft.com/en-us/windows/console/writeconsole
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nNumberOfCharsToWrite"></param>
        /// <param name="lpNumberOfCharsWritten"></param>
        /// <param name="lpReserved"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsole(IntPtr hConsoleOutput, string lpBuffer, uint nNumberOfCharsToWrite, out uint lpNumberOfCharsWritten, IntPtr lpReserved);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hConsoleInput"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nLength"></param>
        /// <param name="lpNumberOfEventsWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleInput(IntPtr hConsoleInput, INPUT_RECORD[] lpBuffer, uint nLength, out uint lpNumberOfEventsWritten);

        /// <summary>
        /// Writes character and color attribute data to a specified rectangular block of character cells in a console screen buffer. The data to be written is taken from a correspondingly sized rectangular block at a specified location in the source buffer.
        /// https://docs.microsoft.com/en-us/windows/console/writeconsoleoutput
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="dwBufferSize"></param>
        /// <param name="dwBufferCoord"></param>
        /// <param name="lpWriteRegion"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, CHAR_INFO[] lpBuffer, COORD dwBufferSize, COORD dwBufferCoord, ref SMALL_RECT lpWriteRegion);

        /// <summary>
        /// Copies a number of character attributes to consecutive cells of a console screen buffer, beginning at a specified location.
        /// https://docs.microsoft.com/en-us/windows/console/writeconsoleoutputattribute
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="lpAttribute"></param>
        /// <param name="nLength"></param>
        /// <param name="dwWriteCoord"></param>
        /// <param name="lpNumberOfAttrsWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutputAttribute(IntPtr hConsoleOutput, ushort[] lpAttribute, uint nLength, COORD dwWriteCoord, out uint lpNumberOfAttrsWritten);

        /// <summary>
        /// Copies a number of characters to consecutive cells of a console screen buffer, beginning at a specified location.
        /// https://docs.microsoft.com/en-us/windows/console/writeconsoleoutputcharacter
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="lpCharacter"></param>
        /// <param name="nLength"></param>
        /// <param name="dwWriteCoord"></param>
        /// <param name="lpNumberOfCharsWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, string lpCharacter, uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

    }
}
