using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Layout;
using sbkst.konzolR.Ui.Rendering;
namespace sbkst.konzolR.Ui.Utility
{
    static class RelativeUtility
    {
        public static Position RelativePositionTo(this Position x, Position to)
        {
            return new Position
            {
                X = (ushort)(to.X - x.X).Clamp(0,UInt16.MaxValue),
                Y = (ushort)(to.Y - x.Y).Clamp(0,UInt16.MaxValue)
            };
        }

        public static char Border(this ConsoleWindow window, ushort x, ushort y)
        {
            if (window.Border)
            {
                return (window as IRenderable).Border(x, y);
            }
            return ' ';
        }

        public static char Border(this IRenderable renderable, ushort x, ushort y)
        {
            
            if (y == 0 && x == 0)
            {
                return AsciiArtIndex.BOX_TOP_LEFT_CORNER;
            }
            if (y == 0 && x == renderable.Size.Width - 1)
            {
                return AsciiArtIndex.BOX_TOP_RIGHT_CORNER;
            }
            if (y == renderable.Size.Height - 1 && x == 0)
            {
                return AsciiArtIndex.BOX_BOTTOM_LEFT_CORNER;
            }
            if (y == renderable.Size.Height - 1 && x == renderable.Size.Width - 1)
            {
                return AsciiArtIndex.BOX_BOTTOM_RIGHT_CORNER;
            }
            if(x == 0 || x == renderable.Size.Width - 1)
            {
                return AsciiArtIndex.BOX_VERTICAL;
            }
            if(y == 0 || y == renderable.Size.Height - 1)
            {
                return AsciiArtIndex.BOX_HORIZONTAL;
            }
            return ' ';
        }
    }
}
