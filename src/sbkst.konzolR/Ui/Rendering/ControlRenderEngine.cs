using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Utility;
namespace sbkst.konzolR.Ui.Rendering
{
    class ControlRenderEngine : RenderEngine
    {
        private string _display;
        public ControlRenderEngine(IRenderable ctrl, string display) : base(ctrl)
        {
            _display = display;
        }


        public override Tuple<char, ushort> GetRelative(ushort x, ushort y)
        {
            CheckBounds(x, y);
            if(!string.IsNullOrEmpty(_display) &&  x < _display.Length)
            {
                return new Tuple<char, ushort>(_display[x], ConsoleColor.White.ColorToBackgroundDWORD());
            }
            return new Tuple<char, ushort>(' ', ConsoleColor.White.ColorToBackgroundDWORD());
        }
    }
}
