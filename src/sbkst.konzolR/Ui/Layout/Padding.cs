using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Utility;
namespace sbkst.konzolR.Ui.Layout
{
    public class Padding
    {

        public Padding(ushort all)
        {
            this.Top = all;
            this.Bottom = all;
            this.Left = all;
            this.Right = all;
        }

        public Padding(ushort tb, ushort lr)
        {
            this.Top = tb;
            this.Bottom = tb;
            this.Left = lr;
            this.Right = lr;
        }

        public Padding(ushort top, ushort bottom, ushort left, ushort right)
        {
            this.Top = top;
            this.Bottom = bottom;
            this.Left = left;
            this.Right = right;
        }
        public ushort Top { get; protected set; }

        public ushort Bottom { get; protected set; }

        public ushort Left { get; protected set; }

        public ushort Right { get; protected set; }

        public static Size operator -(Size size, Padding padding)
        {
            var ret = new Size(
                (ushort)(size.Width - (padding.Right + padding.Left)).Clamp(1, UInt16.MaxValue),
                (ushort)(size.Height - padding.Bottom).Clamp(1, UInt16.MaxValue));
            return ret;
        }

        public static Size operator +(Size size, Padding padding)
        {
            var ret = new Size(
                (ushort)(size.Width + (padding.Right + padding.Left)).Clamp(1, UInt16.MaxValue),
                (ushort)(size.Height + padding.Bottom).Clamp(1, UInt16.MaxValue));
            return ret;
        }
    }
}
