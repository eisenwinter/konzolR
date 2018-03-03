using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Behavior
{
    public class ControlKeyReceived
    {
        public ControlKeyReceived(ConsoleKey key)
        {
            this.Key = key;   
        }

        public ControlKeyReceived(ConsoleKey key, char character)
        {
            this.Key = key;
            this.Character = character;
        }

        public ControlKeyReceived(ConsoleKey key, ConsoleModifiers modifier)
        {
            this.Modifier = modifier;
            this.Key = key;
        }

        public ControlKeyReceived(ConsoleKey key, ConsoleModifiers modifier, char character)
        {
            this.Modifier = modifier;
            this.Key = key;
            this.Character = character;
        }

        public char Character { get; private set; }
        public ConsoleModifiers? Modifier { get; private set; }
        public ConsoleKey Key { get; private set; }

        public string GenerateDictionaryKey(string flag)
        {
            return flag + "-" + this.Key.ToString() + "-" + ((Modifier.HasValue) ? Modifier.Value.ToString() : "none");
        }
    }
}
