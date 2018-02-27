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

        private string _value = string.Empty;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public InfotextControl(string id, string initialText) : base(id)
        {
            _value = initialText;
        }

        public override IRenderProvider GetProvider()
        {
            return new ControlRenderEngine(this, _value);
        }
    }
}
