using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Behavior;
using sbkst.konzolR.Ui.Layout;
namespace sbkst.konzolR.Ui.Controls
{
    /// <summary>
    /// inherit if the control will listen to key events once focused
    /// </summary>
    public interface IListeningControl : IFocusableControl
    {
        bool KeyReceived(ControlKeyReceived controlKey);
        Position CursorPosition { get; }
    }
}
