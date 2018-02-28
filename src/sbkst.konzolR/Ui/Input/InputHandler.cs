using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Concurrent;
namespace sbkst.konzolR.Ui.Input
{
    public enum FunctionKeys
    {
        Tab = 9,
        Backspace = 8,
        Escape = 27,
        Cr = 13,
        LF = 10
    }
    //using the standard console wrappings for convience for now ...
    class InputHandler
    {
        Thread _inputThread;

        private bool _listening = false;
        List<Behavior.IObserve<Behavior.ControlKeyReceived>> _receivers = new List<Behavior.IObserve<Behavior.ControlKeyReceived>>();

        public InputHandler()
        {
            
         
        }

        public void Register(Behavior.IObserve<Behavior.ControlKeyReceived> observe)
        {
            _receivers.Add(observe);
        }

        public void Unregister(Behavior.IObserve<Behavior.ControlKeyReceived> observe)
        {
            _receivers.Remove(observe);
        }
        public void Start()
        {
            _inputThread = new Thread(new ThreadStart(CheckInputBuffer));
            _listening = true;
            _inputThread.Start();
        }

        public void StartBlocking()
        {
            _listening = true;
            CheckInputBuffer();
        }

        public void Stop()
        {
            _listening = false;
        }

        private void CheckInputBuffer()
        {
            while (_listening)
            {
                var key = Console.ReadKey(true);
                if (key.Modifiers == ConsoleModifiers.Control)
                {
                    foreach(var r in _receivers)
                    {
                        r.Update(new Behavior.ControlKeyReceived(key.Key,key.Modifiers));
                    }
                }
                else
                {
                    foreach (var r in _receivers)
                    {
                        r.Update(new Behavior.ControlKeyReceived(key.Key));
                    }
                }
            }
        }
    }
}
