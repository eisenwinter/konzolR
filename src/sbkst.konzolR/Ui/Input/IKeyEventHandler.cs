using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Input
{
    /// <summary>
    /// used to register keys to events
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKeyEventHandler<T> where T : class
    {
        /// <summary>
        /// register a key to execute the defined function
        /// </summary>
        /// <param name="key"></param>
        /// <param name="exec"></param>
        void On(ConsoleKey key, Func<T, bool> exec);

        /// <summary>
        /// register a key + key modifier to listen to the defined function
        /// </summary>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        /// <param name="exec"></param>
        void On(ConsoleKey key, ConsoleModifiers? modifier, Func<T, bool> exec);

        /// <summary>
        /// register a key + key modifier to listen to the defined function if the object has currently focus
        /// </summary>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        /// <param name="exec"></param>
        void WithFocusOn(ConsoleKey key, ConsoleModifiers? modifier, Func<T, bool> exec);


        /// <summary>
        /// register a key to listen to the defined function if the object has currently focus
        /// </summary>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        /// <param name="exec"></param>
        void WithFocusOn(ConsoleKey key, Func<T, bool> exec);

        /// <summary>
        /// remove a global key handler key
        /// </summary>
        /// <param name="key"></param>
        void Off(ConsoleKey key);

        /// <summary>
        /// remove a global key + modifier handler 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        void Off(ConsoleKey key, ConsoleModifiers? modifier);

        /// <summary>
        /// remove a focused handler key
        /// </summary>
        /// <param name="key"></param>
        void WithFocusOff(ConsoleKey key);

        /// <summary>
        /// remove a focused handler key + modifier
        /// </summary>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        void WithFocusOff(ConsoleKey key, ConsoleModifiers? modifier);
    }
}
