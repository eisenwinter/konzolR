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
    
    //using the standard console wrappings for convience for now ...
    class InputHandler
    {
        Thread _inputThread;

        private bool _listening = false;
        List<Behavior.IObserve<Behavior.ControlKeyReceived>> _receivers = new List<Behavior.IObserve<Behavior.ControlKeyReceived>>();


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

        public TextReader GetInputStream()
        {
            return Console.In;
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
                    foreach(var r in _receivers.ToArray())
                    {
                        r.Update(new Behavior.ControlKeyReceived(key.Key,key.Modifiers,key.KeyChar));
                    }
                }
                else
                {
                    foreach (var r in _receivers.ToArray())
                    {
                        r.Update(new Behavior.ControlKeyReceived(key.Key,key.KeyChar));
                    }
                }
            }
        }
    }
}
