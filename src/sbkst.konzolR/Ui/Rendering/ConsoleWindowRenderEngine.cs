using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Utility;

namespace sbkst.konzolR.Ui.Rendering
{
    class ConsoleWindowRenderEngine : RenderEngine
    {
    
        public ConsoleWindowRenderEngine(ConsoleWindow renderable) : base(renderable)
        {
            
        }

 

        public override Tuple<char, ushort> GetRelative(ushort x, ushort y)
        {
            CheckBounds(x, y);
            if(y == 0)
            {
                string title = (_renderable as ConsoleWindow).Title;
                if(x < title.Length)
                {
                   return new Tuple<char, ushort>(title[x], (_renderable as ConsoleWindow).BackgroundColor.ColorToBackgroundDWORD());
                }
                return new Tuple<char, ushort>(' ', (_renderable as ConsoleWindow).BackgroundColor.ColorToBackgroundDWORD());
            }
            else
            {
              
                return new Tuple<char, ushort>(' ', (_renderable as ConsoleWindow).BackgroundColor.ColorToBackgroundDWORD());
            }
        }
    }
}
