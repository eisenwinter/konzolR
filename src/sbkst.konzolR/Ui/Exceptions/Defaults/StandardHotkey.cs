using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Defaults
{
    class StandardHotkey : IHotkeyDefinition
    {
        public StandardHotkey(ConsoleKey key, ConsoleModifiers? mod = null)
        {
            this.Key = key;
            this.Modifier = mod;
        }
        public ConsoleKey Key { get; set; }

        public ConsoleModifiers? Modifier { get; set; }
    }
}
