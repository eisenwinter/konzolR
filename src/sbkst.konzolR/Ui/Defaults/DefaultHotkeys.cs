using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Defaults
{
    class DefaultHotkeys : IDefaultHotkeys
    {
        public DefaultHotkeys()
        {
            this.WINDOW_PREVIOUS_CONTROL = new StandardHotkey(ConsoleKey.UpArrow);
            this.WINDOW_NEXT_CONTROL = new StandardHotkey(ConsoleKey.DownArrow);
            this.CONTEXT_FOCUS_NEXT_WINDOW = new StandardHotkey(ConsoleKey.Tab);
        }

        public IHotkeyDefinition WINDOW_NEXT_CONTROL { get; private set; }
        public IHotkeyDefinition WINDOW_PREVIOUS_CONTROL { get; private set; }

        public IHotkeyDefinition CONTEXT_FOCUS_NEXT_WINDOW { get; private set; }
    }
}
