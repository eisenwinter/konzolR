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
        private ConsoleColor _bg = ConsoleColor.White;
        private bool _hasFocus;
        private string _display;
        private bool _centerText = false;
        public ControlRenderEngine(IRenderable ctrl, string display) : base(ctrl)
        {
            _display = display;
        }

        public ControlRenderEngine(IRenderable ctrl, string display, ConsoleColor background, bool hasFocus = false, bool centerText = true) : base(ctrl)
        {
            _display = display;
            _bg = background;
            _hasFocus = hasFocus;
            _centerText = centerText;
        }

        private int CenterOffset()
        {
            if (!_centerText || String.IsNullOrEmpty(_display) || _renderable.Size.Width <= _display.Length) return 0;
            return (ushort)Math.Floor((_renderable.Size.Width - _display.Length) * 0.5);
        }

        public override Tuple<char, ushort> GetRelative(ushort x, ushort y)
        {
            CheckBounds(x, y);
            var bgToUse = (_hasFocus) ? _bg.Highlight().ColorToBackgroundDWORD() : _bg.ColorToBackgroundDWORD();
            int offset = CenterOffset();
            if (!string.IsNullOrEmpty(_display) && x >= offset && x < (_display.Length + offset))
            {
                if(_display.Length > _renderable.Size.Width && x == _renderable.Size.Width-1)
                {
                    return new Tuple<char, ushort>(AsciiArtIndex.THERES_MORE, bgToUse);
                }
                return new Tuple<char, ushort>(_display[x-offset], bgToUse);
            }
            return new Tuple<char, ushort>(' ', bgToUse);
        }
    }
}
