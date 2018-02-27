using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Layout;

namespace sbkst.konzolR.Ui.Utility
{
    static class RelativeUtility
    {
        public static Position RelativePositionTo(this Position x, Position to)
        {
            return new Position
            {
                X = (ushort)(to.X - x.X),
                Y = (ushort)(to.Y - x.Y)
            };
        }
    }
}
