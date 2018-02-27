using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;

namespace sbkst.konzolR.Ui.Utility
{
    internal static class BoundingBoxFilter
    {
        public static Func<IRenderable,bool> Filter(uint x, uint y)
        {
            return a => a.Position.X <= x && a.Position.Y <= y && (a.Position.X + a.Size.Width) > x && (a.Position.Y + a.Size.Height) > y;
        }
    }
}
