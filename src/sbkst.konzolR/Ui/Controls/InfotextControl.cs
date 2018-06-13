using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;

namespace sbkst.konzolR.Ui.Controls
{
    public class InfotextControl : ConsoleControl
    {
        public InfotextControl(string id) : base(id)
        {

        }
        public string Value { get; set; } = string.Empty;

        public InfotextControl(string id, string initialText) : base(id)
        {
            Value = initialText;
        }

        public override IRenderProvider GetProvider()
        {
            return new ControlRenderEngine(this, Value);
        }
    }
}
