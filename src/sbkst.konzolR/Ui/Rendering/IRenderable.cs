using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Layout;
namespace sbkst.konzolR.Ui.Rendering
{
    public interface IRenderable
    {
        Size Size { get; }
        Position Position { get; }

        IRenderProvider GetProvider();

       
    }
}
