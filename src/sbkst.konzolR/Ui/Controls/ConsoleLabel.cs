using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;

namespace sbkst.konzolR.Ui.Controls
{
    public class ConsoleLabel : ConsoleControl
    {
        private string _value;
        public ConsoleLabel(string id, string text, ConsoleColor backgroundColor = ConsoleColor.White) : base(id)
        {
            this._value = text;
            this.BackgroundColor = backgroundColor;
        }


        public ConsoleColor BackgroundColor
        {
            get; private set;
        }

        public override IRenderProvider GetProvider()
        {
            return new ControlRenderEngine(this, _value, BackgroundColor, centerText: false);
        }
    }
}
