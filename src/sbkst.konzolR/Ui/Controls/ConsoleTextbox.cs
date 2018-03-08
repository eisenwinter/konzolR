using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;
using sbkst.konzolR.Ui.Behavior;
namespace sbkst.konzolR.Ui.Controls
{
    public class ConsoleTextbox : ListeningConsoleControl
    {
        private string _value;

        public virtual string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (value != null)
                {
                    this._currentSize = Value.Length;
                }
            }
        }

        public ConsoleTextbox(string id, string value) : base(id)
        {
            _value = value;
            if (!String.IsNullOrEmpty(_value))
            {
                this._currentSize = Value.Length;
            }
        }

        public override IRenderProvider GetProvider()
        {
            return new ControlRenderEngine(this, _value);
        }

        public override bool KeyReceived(ControlKeyReceived controlKey)
        {
#if DEBUG
            System.Diagnostics.Trace.WriteLine(String.Format("Control {0} [{1},{2}|{3},{4}] received key {5}", this.Id, this.Position.X, this.Position.Y, this.Size.Width, this.Size.Height, controlKey.Key));
#endif

            var standardArgs = new StandardKeyArgs
            {
                OnBackspacePressed = () =>
                {
                    if(this.Value.Length > CursorPosition.X)
                    {
                        var sb = new StringBuilder(this.Value);
                        sb.Remove(CursorPosition.X, 1);
                        this.Value = sb.ToString();
                    }
                },
                OnDeletePressed = () =>
                {
                    if (CursorPosition.X >= 0 && CursorPosition.X < Value.Length)
                    {
                        var sb = new StringBuilder(this.Value);
                        sb.Remove(CursorPosition.X, 1);
                        this.Value = sb.ToString();
                    }
                },
                OnEnterPressed = () =>
                {
                    this.Blur();
                },
                OnTabPressed = () =>
                {

                }
            };

            if (!HookStandardKeys(controlKey, standardArgs))
            {

                if (CursorPosition.X < Value.Length)
                {
                    var sb = new StringBuilder(this.Value);
                    sb[CursorPosition.X] = controlKey.Character;
                    this.Value = sb.ToString();
                    if (CursorPosition.X < this.Size.Width - 1)
                    {
                        CursorPosition.X++;
                    }
                }
                else
                {
                    this.Value = Value + controlKey.Character;
                    if (CursorPosition.X < this.Size.Width - 1)
                    {
                        CursorPosition.X++;
                    }
                }

            }

            return true;
        }
    }
}
