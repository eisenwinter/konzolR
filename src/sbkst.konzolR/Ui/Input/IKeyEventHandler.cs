using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Input
{
    public interface IKeyEventHandler<T> where T : class
    {
        void On(ConsoleKey key, Func<T, bool> exec);
        void On(ConsoleKey key, ConsoleModifiers modifier, Func<T, bool> exec);
        void WithFocusOn(ConsoleKey key, ConsoleModifiers modifier, Func<T, bool> exec);
        void WithFocusOn(ConsoleKey key, Func<T, bool> exec);
        void Off(ConsoleKey key);
        void Off(ConsoleKey key, ConsoleModifiers modifier);
        void WithFocusOff(ConsoleKey key);
        void WithFocusOff(ConsoleKey key, ConsoleModifiers modifier);
    }
}
