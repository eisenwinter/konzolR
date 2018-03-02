using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Layout
{
    /// <summary>
    /// defines a positive-only two dimensional position
    /// </summary>
    public class Position
    {
        public Position()
        {

        }

        public Position(Position position)
        {
            X = position.X;
            Y = position.Y;
        }

        public Position(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// x coordinate
        /// </summary>
        public ushort X { get; set; }

        /// <summary>
        /// y coordinate
        /// </summary>
        public ushort Y { get; set; }
    }
}
