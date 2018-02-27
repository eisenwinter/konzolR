using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Exceptions
{
    public class OutOfBoundsException : Exception
    {
        public ushort MinX { get; private set; }

        public ushort MinY { get; private set; }

        public ushort MaxX { get; private set; }

        public ushort MaxY { get; private set; }

        public ushort AttemptedX { get; private set; }

        public ushort AttemptedY { get; private set; }

        public OutOfBoundsException(string message) : base(message)
        {

        }

        public OutOfBoundsException(string message, ushort minx, ushort maxx, ushort miny, ushort maxy, ushort attemptedx,ushort attemptedy) : base(message)
        {
            MinX = minx;
            MinY = miny;
            MaxX = maxx;
            MaxY = maxy;
            AttemptedX = attemptedx;
            AttemptedY = attemptedy;
        }
    }
}
