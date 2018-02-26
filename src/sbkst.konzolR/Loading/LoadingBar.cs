using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace sbkst.konzolR.Loading
{
    class LoadingBar : ILoadingBar
    {
        private short _current = 0;
        private string _titleBuffer;

        private int startedLeft = 0;
        private int startedTop = 0;

        private const int _titleLength = 10;

        public enum BarType { LoadingPercentage, Waiting }
        BarType _type;

        public LoadingBar(BarType type)
        {
            startedLeft = Console.CursorLeft;
            startedTop = Console.CursorTop;
            _titleBuffer = Console.Title;
            _type = type;
        }

        public void Dispose()
        {
            Console.Title = _titleBuffer;
            if (_type == BarType.Waiting)
            {
                _timerDisposing = true;
            }

        }

        public void Done()
        {
            _current = 100;
            Redraw();
            if (_type == BarType.Waiting)
            {
                _timerDisposing = true;
            }

        }

        private void Redraw()
        {
            if (!Console.IsOutputRedirected)
            {
                int sight = Console.WindowHeight;
                //if it goes out of sight, glue it to the top
                if (Console.CursorTop - sight >= startedTop)
                {
                    startedTop = Console.CursorTop - sight + 1;
                }
                int barLength = Console.BufferWidth;
                if (barLength <= 0) barLength = 1;
                int currently = (int)Math.Floor((_current * (decimal)0.01) * barLength);
                string bar = String.Format("{0}{1}", new String(AsciiArtIndex.BAR_FILLED, currently), new String(AsciiArtIndex.BAR_EMPTY, barLength - currently));
                int restetLeft = Console.CursorLeft;
                int resetTop = Console.CursorTop;
                Console.SetCursorPosition(startedLeft, startedTop);
                Console.Write(bar);
                Console.SetCursorPosition(restetLeft, resetTop);
            }
            else
            {
                //if we are in output redirection we show the bar on the title
                int titleLength = (int)Math.Floor(_current * (decimal)0.01 * _titleLength);
                Console.Title = String.Format("{0}{1}", new String(AsciiArtIndex.BAR_FILLED, titleLength), new String(AsciiArtIndex.BAR_EMPTY, _titleLength - titleLength));
            }
        }

        public void SetPercentage(short percentage)
        {
            _current = Math.Max((short)0, Math.Min((short)100, percentage));
            Redraw();
        }

        private Timer _waitingTimer;
        private bool _timerDisposing;
        public void Start()
        {
            _current = 0;
            startedLeft = Console.CursorLeft;
            startedTop = Console.CursorTop;
            if (_type == BarType.Waiting)
            {
                _timerDisposing = false;
                //start auto filling percent
                _waitingTimer = new Timer(WaitingTick);
                _waitingTimer.Change(450, -1);

            }
        }

        public void WaitingTick(object s)
        {
            lock (_waitingTimer)
            {
                if (_timerDisposing) return;
                _current++;
                if (_current > 100) _current = 0;
                Redraw();
                _waitingTimer.Change(450, -1);
            }
        }
    }
}
