using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;

namespace sbkst.konzolR.Ui.Controls
{
    public class ConsoleTextbox : ConsoleControl, IFocusableControl
    {
        private string _value;

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

        public ConsoleTextbox(string id, string value) : base(id)
        {
            _value = value;
        }
        public override IRenderProvider GetProvider()
        {
            return new ControlRenderEngine(this, _value);
        }
    }
}
