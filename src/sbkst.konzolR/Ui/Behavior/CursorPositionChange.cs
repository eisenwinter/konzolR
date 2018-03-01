using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Behavior
{
    public class CursorPositionChange
    {
        public CursorPositionChange(ushort x, ushort y, bool visible = true)
        {
            this.X = x;
            this.Y = y;
            this.Visible = visible;
        }
        public ushort X { get; private set; }

        public ushort Y { get; private set; }

        public bool Visible { get; private set; }
    }
}
