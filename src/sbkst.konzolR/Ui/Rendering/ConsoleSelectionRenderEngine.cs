using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Utility;
namespace sbkst.konzolR.Ui.Rendering
{
    class ConsoleSelectionRenderEngine : RenderEngine
    {
        private ConsoleColor _bg;
        private int _index = 0;
        private string[] _items;
   
        public ConsoleSelectionRenderEngine(IRenderable ctrl, string[] items,int focusedIndex, ConsoleColor backgroundColor = ConsoleColor.White) : base(ctrl)
        {
            _bg = backgroundColor;
            _index = focusedIndex;
            _items = items;
        }
        

      
        public override Tuple<char, ushort> GetRelative(ushort x, ushort y)
        {
            CheckBounds(x, y);
            var bgToUse = (_index == y) ? _bg.Highlight().ColorToBackgroundDWORD() : _bg.ColorToBackgroundDWORD();
            if (y < _items.Length)
            {
                if (x < _items[y].Length)
                {
                    if(_index == y)
                    {
                        return new Tuple<char, ushort>(_items[y][x],bgToUse);
                    }
                    else
                    {
                        return new Tuple<char, ushort>(_items[y][x],bgToUse);
                    }
                }
            }
            return new Tuple<char, ushort>(' ', bgToUse);
        }
    }
}
