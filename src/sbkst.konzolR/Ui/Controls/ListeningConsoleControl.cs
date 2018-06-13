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
        protected int _currentSize = 0;

        protected ListeningConsoleControl(string id) : base(id)
        {
            this.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == "HasFocus")
                {
                    if (this.HasFocus)
                    {
                        CursorPosition.X = (ushort)_currentSize.Clamp(0, this.Size.Width - 1);
                        CursorPosition.Y = 0;
                    }
                }

            };
            if (CursorPosition != null)
            {
                CursorPosition.SetRelativeTo(this.Position);
            }
        }

        protected class StandardKeyArgs
        {
            public Action OnDeletePressed { get; set; }
            public Action OnBackspacePressed { get; set; }
            public Action OnEnterPressed { get; set; }
            public Action OnTabPressed { get; set; }
        }

        protected bool HookStandardKeys(ControlKeyReceived controlKey, StandardKeyArgs args)
        {
            if (controlKey.Key == ConsoleKey.LeftArrow)
            {
                if (CursorPosition.X > 0)
                {
                    CursorPosition.X--;
                    return true;
                }
                return false;
            }
            else if (controlKey.Key == ConsoleKey.RightArrow)
            {
                if (CursorPosition.X <= _currentSize && CursorPosition.X < this.Size.Width - 1)
                {
                    CursorPosition.X++;
                    return true;
                }
                return false;
            }
            else if (controlKey.Key == ConsoleKey.Backspace && args.OnBackspacePressed != null)
            {
                if (CursorPosition.X > 0)
                {
                    CursorPosition.X--;
                    args?.OnBackspacePressed();
                }
                return true;
            }
            else if (controlKey.Key == ConsoleKey.Delete && args.OnDeletePressed != null)
            {
                args?.OnDeletePressed();
                return true;
            }
            else if (controlKey.Key == ConsoleKey.Enter && args.OnEnterPressed != null)
            {
                if (this.Size.Height - 1 > CursorPosition.Y)
                {
                    CursorPosition.Y++;
                }
                args.OnEnterPressed();
                return true;
            }
            else if (controlKey.Key == ConsoleKey.Tab && args.OnTabPressed != null)
            {
                args.OnTabPressed();
                return true;
            }
            return false;
        }

        public Position CursorPosition { get; protected set; } = new Position(0, 0);

        public abstract bool KeyReceived(ControlKeyReceived controlKey);
    }
}
