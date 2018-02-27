using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Rendering
{
    public interface IRenderProvider
    {
        Tuple<char, ushort> GetRelative(ushort x, ushort y);
    }
}
