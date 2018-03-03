using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Defaults
{
    public interface IHotkeyDefinition
    {
        ConsoleKey Key { get; }
        ConsoleModifiers? Modifier { get; }
    }
}
