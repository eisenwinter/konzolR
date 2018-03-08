using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
namespace sbkst.konzolR.Ui.Controls
{
    /// <summary>
    /// inherit if the control can receive focus 
    /// </summary>
    public interface IFocusableControl
    {
        bool CursorVisible { get; }
        Boolean HasFocus { get; }
        void Focus();
        void Blur();
    }
}
