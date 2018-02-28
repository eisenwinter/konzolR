using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Layout
{
    public class Size
    {
        public Size()
        {

        }
        public Size(Size size)
        {
            this.Width = size.Width;
            this.Height = size.Height;
        }
        public Size(ushort w, ushort h)
        {
            this.Width = w;
            this.Height = h;
        }
        public ushort Width { get; set; }
        public ushort Height { get; set; }
    }
}
