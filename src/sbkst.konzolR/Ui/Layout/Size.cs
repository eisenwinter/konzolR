using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Layout
{
    /// <summary>
    /// defines a positive only size
    /// </summary>
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

        /// <summary>
        /// width
        /// </summary>
        public ushort Width { get; set; }

        /// <summary>
        /// height
        /// </summary>
        public ushort Height { get; set; }
    }
}
