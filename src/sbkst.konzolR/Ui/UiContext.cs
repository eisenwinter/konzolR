using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Internals;
namespace sbkst.konzolR.Ui
{
    public class UiContext : IDisposable
    {
        private IntPtr _currentConsoleHandle = IntPtr.Zero;
        private IntPtr _screenBuffer = IntPtr.Zero;

        private ConsoleCanvas _canvas;

        public UiContext()
        {
            _currentConsoleHandle = ConsoleInteropt.GetConsoleWindow();
            if(_currentConsoleHandle == IntPtr.Zero)
            {
                throw new Exceptions.ConsoleHandleException("Couldnt retrive console handle, are you using a console application?");
            }
   
            _screenBuffer = ConsoleInteropt.CreateConsoleScreenBuffer(W32ConsoleConstants.GENERIC_WRITE, 0, IntPtr.Zero, W32ConsoleConstants.CONSOLE_TEXTMODE_BUFFER, IntPtr.Zero);
            if (_screenBuffer == IntPtr.Zero)
            {
                throw new Exceptions.ScreenBufferException("Couldnt create screen buffer");
            }
            _canvas = new ConsoleCanvas(_screenBuffer, (ushort)Console.BufferWidth, (ushort)Console.WindowHeight);
        }

        public void Initialize(ConsoleColor backgroundColor)
        {
            _canvas.Initiliaze(backgroundColor);
            ConsoleInteropt.SetConsoleActiveScreenBuffer(_screenBuffer);
        }

        public void AddWindow(ConsoleWindow window)
        {
          
            _canvas[window.Id] = window;
            _canvas.Redraw();
        }

        public ConsoleWindow GetWindow(string id)
        {
            return _canvas[id];
        }

        public ConsoleWindow RemoveWindow(string id)
        {
            return _canvas.RemoveWindow(id);
        }

        public void Dispose()
        {
            ConsoleInteropt.CloseHandle(_screenBuffer);
        }
    }
}
