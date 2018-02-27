using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;

namespace sbkst.konzolR.Ui.Controls
{
    class ConsoleButton : ConsoleControl
    {
        public ConsoleButton(string text, string id) : base(id)
        {

        }

        public override IRenderProvider GetProvider()
        {
            throw new NotImplementedException();
        }
    }
}
