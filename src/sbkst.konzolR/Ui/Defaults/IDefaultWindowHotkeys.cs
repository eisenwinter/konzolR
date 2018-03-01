using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Defaults
{
    public interface IDefaultWindowHotkeys
    {
        IHotkeyDefinition WINDOW_NEXT_CONTROL
        {
            get;
        }

        IHotkeyDefinition WINDOW_PREVIOUS_CONTROL
        {
            get;
        }
    }
}
