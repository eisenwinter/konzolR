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
                if(x == 0 && (_renderable as ConsoleWindow).HasFocus)
                {
                    return new Tuple<char, ushort>('*', (_renderable as ConsoleWindow).BackgroundColor.ColorToBackgroundDWORD());
                }
                string title = (_renderable as ConsoleWindow).Title;
                if(x > 0 && x-1 < title.Length)
                {
                   return new Tuple<char, ushort>(title[x-1], (_renderable as ConsoleWindow).BackgroundColor.ColorToBackgroundDWORD());
                }
                return new Tuple<char, ushort>(' ', (_renderable as ConsoleWindow).BackgroundColor.ColorToBackgroundDWORD());
            }
            else
            {
                var ctrl = (_renderable as ConsoleWindow).Controls.FirstOrDefault(BoundingBoxFilter.Filter(x, y));
                if(ctrl != null)
                {
                    var provider = ctrl.GetProvider();
                    var relative = ctrl.Position.RelativePositionTo(new Layout.Position(x,y));
                    return provider.GetRelative(relative.X,relative.Y);
                }
                else
                {
                    return new Tuple<char, ushort>(' ', (_renderable as ConsoleWindow).BackgroundColor.ColorToBackgroundDWORD());
                }
            }
        }
    }
}
