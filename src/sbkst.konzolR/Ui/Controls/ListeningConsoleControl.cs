using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;
using System.ComponentModel;
using sbkst.konzolR.Ui.Behavior;
using sbkst.konzolR.Ui.Layout;
using sbkst.konzolR.Ui.Utility;

namespace sbkst.konzolR.Ui.Controls
{
    public abstract class ListeningConsoleControl : FocusableConsoleControl, IListeningControl
    {
        private Position _cursorPosition = new Position(0, 0);
        protected long _currentSize = 0;


        protected ListeningConsoleControl(string id) : base(id)
        {
            this.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == "HasFocus")
                {
                    if (this.HasFocus)
                    {
                        _cursorPosition.X = (ushort)_currentSize.Clamp(0,this.Size.Width-1);
                        _cursorPosition.Y = 0;
                        _cursorPosition.SetRelativeTo(this.Position.GetAbsolutePosition());
                    }
                }
                
            };
            if(_cursorPosition != null)
            {
                _cursorPosition.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
                {
                    if (new string[] { "X", "Y" }.Any(a => a == e.PropertyName))
                    {
                        _cursorPosition.SetRelativeTo(this.Position.GetAbsolutePosition());
                    }
                };
            }            
        }

        protected bool HookStandardKeys(ControlKeyReceived controlKey, Action delete, Action backspace, Action newLine, Action tab)
        {
            if (controlKey.Key == ConsoleKey.LeftArrow && CursorPosition.X > 0)
            {
                CursorPosition.X--;
                return true;
            }
            else if (controlKey.Key == ConsoleKey.RightArrow && CursorPosition.X <= _currentSize && CursorPosition.X < this.Size.Width - 1)
            {
                CursorPosition.X++;
                return true;
            }
            else if (controlKey.Key == ConsoleKey.Backspace)
            {
                if (CursorPosition.X > 0)
                {
                    CursorPosition.X--;
                    backspace();
                }
                return true;
            }
            else if (controlKey.Key == ConsoleKey.Delete)
            {
                delete();
                return true;
            }else if (controlKey.Key == ConsoleKey.Enter)
            {
                if(this.Size.Height-1 > CursorPosition.Y)
                {
                    CursorPosition.Y++;
                }
                newLine();
                return true;
            }else if(controlKey.Key == ConsoleKey.Tab)
            {
                tab();
                return true;
            }
            return false;
        }

        public Position CursorPosition
        {
            get
            {
                return _cursorPosition;
            }
            protected set
            {
                _cursorPosition = value;
                _cursorPosition.SetRelativeTo(this.Position.GetAbsolutePosition());
                _cursorPosition.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
                {
                    if (new string[] { "X", "Y" }.Any(a => a == e.PropertyName))
                    {
                        _cursorPosition.SetRelativeTo(this.Position.GetAbsolutePosition());
                    }
                };
            }
        }

   

        public abstract bool KeyReceived(ControlKeyReceived controlKey);
    }
}
