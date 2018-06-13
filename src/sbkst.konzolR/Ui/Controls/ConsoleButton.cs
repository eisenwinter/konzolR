using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Behavior;
using sbkst.konzolR.Ui.Rendering;

namespace sbkst.konzolR.Ui.Controls
{
    public class ConsoleButton : ListeningConsoleControl
    {

        private readonly string _buttonText = string.Empty;

        public ConsoleButton(string id, string text, Action onClick) : base(id)
        {
            OnClick = onClick;
            _buttonText = text;
            CursorVisible = false;
        }

        private ConsoleColor _backgroundColor = ConsoleColor.Red;
        public ConsoleColor BackgroundColor { get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
            }
        }


        public Action OnClick { get; private set; }

        public override IRenderProvider GetProvider()
        {
            this.Valid = true;
            return new ControlRenderEngine(this, _buttonText, _backgroundColor,this.HasFocus);
        }

        public override void Blur()
        {
            base.Blur();
            this.Valid = false;
        }

        public override void Focus()
        {
            base.Focus();
            this.Valid = false;
        }

        public override bool KeyReceived(ControlKeyReceived controlKey)
        {
            if(controlKey.Key == ConsoleKey.Enter)
            {
                OnClick();
                return true;
            }
            return false;
        }
    }
}
