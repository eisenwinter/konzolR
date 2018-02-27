using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Layout
{
    public class Position
    {
        public Position()
        {

        }

        public Position(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }
        public ushort X { get; set; }

        public ushort Y { get; set; }
    }
}
