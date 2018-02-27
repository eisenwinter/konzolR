using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Rendering
{
    abstract class RenderEngine : IRenderProvider
    {
        protected IRenderable _renderable;

        protected RenderEngine(IRenderable renderable)
        {
            _renderable = renderable;
        }

        protected void CheckBounds(ushort x, ushort y)
        {
            if (x < 0 || y < 0 || x > _renderable.Size.Width || y > _renderable.Size.Height)
            {
                throw new Exceptions.OutOfBoundsException("Coordinates are out of bounds.", 0, _renderable.Size.Width, 0, _renderable.Size.Height, x, y);
            }
        }

        public abstract Tuple<char, ushort> GetRelative(ushort x, ushort y);
    }
}
