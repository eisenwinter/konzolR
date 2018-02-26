using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace sbkst.konzolR.Ui
{
    class ConsoleCanvas
    {
        Color _backgroundColor = Color.AliceBlue;
        private List<ConsoleWindow> _windows = new List<ConsoleWindow>();
        private ConsoleMenu _menu = null;
        private ConsoleStatusStrip _statusStrip = null;

        private Size _viewport;

        public ConsoleCanvas(uint width, uint height)
        {

        }

        public void DrawTo(IntPtr consoleHandle)
        {
            
        }
    }
}
