using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Layout;
using sbkst.konzolR.Ui.Rendering;
using sbkst.konzolR.Ui.Behavior;
namespace sbkst.konzolR.Ui.Controls
{
    public class ConsoleTextbox : ListeningConsoleControl
    {
        private int _viewboxOffset = 0;

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

        protected string ViewboxValue
        {
            get
            {
                if (_viewboxOffset > 0)
                {
                    var sb = new StringBuilder(Value.Substring(_viewboxOffset));
                    sb[0] = AsciiArtIndex.THERES_MORE;
                    return sb.ToString();
                }
                return Value;
            }
        }

        public ConsoleTextbox(string id, string value) : base(id)
        {
            _value = value;
            if (!String.IsNullOrEmpty(_value))
            {
                this._currentSize = Value.Length;
            }
            this.Size.PropertyChanged += (s, e) =>
            {
                if(e.PropertyName == "Width")
                {
                    _viewboxOffset = 0;
                }
            };
        }

        public override IRenderProvider GetProvider()
        {
            return new ControlRenderEngine(this, ViewboxValue);
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
                        sb.Remove((CursorPosition.X + _viewboxOffset), 1);
                        this.Value = sb.ToString();
                    }
                },
                OnDeletePressed = () =>
                {
                    if (CursorPosition.X >= 0 && CursorPosition.X < Value.Length)
                    {
                        var sb = new StringBuilder(this.Value);
                        sb.Remove((CursorPosition.X + _viewboxOffset), 1);
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
                if(controlKey.Key == ConsoleKey.LeftArrow)
                {
                    if(_viewboxOffset > 0)
                    {
                        _viewboxOffset--;
                    }
                    return true;
                }
                if (controlKey.Key == ConsoleKey.RightArrow)
                {
                    if ((CursorPosition.X + _viewboxOffset) < Value.Length)
                    {
                        _viewboxOffset++;
                    }
                    return true;
                }

                if ((CursorPosition.X + _viewboxOffset) < Value.Length)
                {
                    var sb = new StringBuilder(this.Value);
                    sb[CursorPosition.X+_viewboxOffset] = controlKey.Character;
                    this.Value = sb.ToString();
                    if (CursorPosition.X < this.Size.Width - 1)
                    {
                        CursorPosition.X++;
                    }else if((CursorPosition.X + _viewboxOffset) < Value.Length)
                    {
                        _viewboxOffset++;
                    }
                }
                else
                {
                    this.Value = Value + controlKey.Character;
                    if (CursorPosition.X < this.Size.Width - 1)
                    {
                        CursorPosition.X++;
                    }
                    else if((CursorPosition.X + _viewboxOffset) <= Value.Length)
                    {
                        _viewboxOffset++;
                    }
                }

            }

            return true;
        }
    }
}
