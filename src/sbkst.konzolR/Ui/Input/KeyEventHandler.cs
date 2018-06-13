using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
namespace sbkst.konzolR.Ui.Input
{
    class KeyEventHandler<T> : IKeyEventHandler<T> where T : class
    {
        ConcurrentDictionary<string, Func<T, bool>> _keyActions = new ConcurrentDictionary<string, Func<T, bool>>();
        readonly Lazy<Regex> _keyDefinition = new Lazy<Regex>(() => new Regex("(?<flag>[(a|f)])-(?<key>[A-z0-9]{1,})-(?<mod>none|Alt|Shift|Control)",RegexOptions.Compiled),true);

        private void Register(string key, Func<T, bool> exec)
        {
            if (!_keyActions.TryAdd(key, exec))
            {
                throw new Exceptions.RegisterKeyHandlerException("Could not register key handler", key);
            }
        }

        private void Unregister(string key)
        {
            if (!_keyActions.TryRemove(key, out Func<T, bool> val))
            {
                throw new Exceptions.UnregisterKeyHandlerException("Could not unregister key handler", key);
            }
        }

        public void Off(ConsoleKey key)
        {
            string k = "a-" + key.ToString() + "-none";
            Unregister(k);
        }

        public void Off(ConsoleKey key, ConsoleModifiers? modifier)
        {
            if (modifier.HasValue)
            {
                string k = "a-" + key.ToString() + "-" + modifier.ToString();
                Unregister(k);
            }
            else
            {
                Off(key);
            }
           
        }

        public void On(ConsoleKey key, Func<T, bool> exec)
        {
            string k = "a-" + key.ToString() + "-none";
            Register(k, exec);

        }

        public void On(ConsoleKey key, ConsoleModifiers? modifier, Func<T, bool> exec)
        {
            if (modifier.HasValue)
            {
                string k = "a-" + key.ToString() + "-" + modifier.ToString();
                Register(k, exec);
            }
            else
            {
                On(key, exec);
            }  
        }

        public void WithFocusOff(ConsoleKey key)
        {
            string k = "f-" + key.ToString() + "-none";
            Unregister(k);
        }

        public void WithFocusOff(ConsoleKey key, ConsoleModifiers? modifier)
        {
            if (modifier.HasValue)
            {
                string k = "a-" + key.ToString() + "-" + modifier.ToString();
                Unregister(k);
            }
            else
            {
                WithFocusOff(key);
            }
           
        }

        public void WithFocusOn(ConsoleKey key, ConsoleModifiers? modifier, Func<T, bool> exec)
        {
            if (modifier.HasValue)
            {
                string k = "f-" + key.ToString() + "-" + modifier.Value.ToString();
                Register(k, exec);
            }
            else
            {
                WithFocusOn(key, exec);
            }
            
        }

        public void WithFocusOn(ConsoleKey key, Func<T, bool> exec)
        {
            string k = "f-" + key.ToString() + "-none";
            Register(k, exec);
        }

        public bool Execute(string key, T item)
        {
            if (_keyActions.ContainsKey(key))
            {
                return _keyActions[key](item);
            }
            return false;
        }
    }
}
