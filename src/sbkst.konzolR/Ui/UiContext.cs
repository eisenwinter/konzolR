using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Internals;
namespace sbkst.konzolR.Ui
{
    public class UiContext
    {
        private IntPtr _currentConsoleHandle = IntPtr.Zero;

        private ConsoleCanvas _canvas;

        public UiContext()
        {
            _currentConsoleHandle = ConsoleInteropt.GetConsoleWindow();
            if(_currentConsoleHandle == IntPtr.Zero)
            {
                throw new Exceptions.ConsoleHandleException("Couldnt retrive console handle, are you using a console application?");
            }
            _canvas = new ConsoleCanvas((uint)Console.BufferWidth, (uint)Console.BufferHeight);
        }

        public void Redraw()
        {
            
        }
    }
}
