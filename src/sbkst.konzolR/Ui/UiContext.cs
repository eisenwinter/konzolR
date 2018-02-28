using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using sbkst.konzolR.Ui.Behavior;
using sbkst.konzolR.Internals;
using sbkst.konzolR.Ui.Input;
namespace sbkst.konzolR.Ui
{
    public class UiContext :
        IObserve<ControlKeyReceived>,
        IKeyListener<UiContext>,
        IDisposable
    {
        private IntPtr _currentConsoleHandle = IntPtr.Zero;
        private IntPtr _screenBuffer = IntPtr.Zero;

        private ConsoleCanvas _canvas;
        private List<IObserve<WindowFocusChange>> _focusObservers = new List<IObserve<WindowFocusChange>>();

        private Input.InputHandler _inputHandler;

        private ConsoleInteropt.CONSOLE_CURSOR_INFO _nfo;

        
        public UiContext()
        {
            _currentConsoleHandle = ConsoleInteropt.GetConsoleWindow();
            if (_currentConsoleHandle == IntPtr.Zero)
            {
                throw new Exceptions.ConsoleHandleException("Couldnt retrive console handle, are you using a console application?");
            }

            _screenBuffer = ConsoleInteropt.CreateConsoleScreenBuffer(W32ConsoleConstants.GENERIC_WRITE, 0, IntPtr.Zero, W32ConsoleConstants.CONSOLE_TEXTMODE_BUFFER, IntPtr.Zero);
            if (_screenBuffer == IntPtr.Zero)
            {
                throw new Exceptions.ScreenBufferException("Couldnt create screen buffer");
            }
            _nfo = new ConsoleInteropt.CONSOLE_CURSOR_INFO
            {
                Visible = false,
                Size = 100
            };
            ConsoleInteropt.SetConsoleCursorInfo(_screenBuffer, ref _nfo);
            _inputHandler = new Input.InputHandler();
            _canvas = new ConsoleCanvas(_screenBuffer, (ushort)Console.BufferWidth, (ushort)Console.WindowHeight);
            _inputHandler.Start();
            _inputHandler.Register(this);

        }

        public void Initialize(ConsoleColor backgroundColor)
        {
            _canvas.Initiliaze(backgroundColor);
            ConsoleInteropt.SetConsoleActiveScreenBuffer(_screenBuffer);
        }

        public void AddWindow(ConsoleWindow window)
        {
            _focusObservers.Add(window);
            _inputHandler.Register(window);
            Focus(window.Id);
            _canvas[window.Id] = window;
            _canvas.Redraw();
        }


        public void Focus(string id)
        {
            _focusObservers.ForEach(window =>
             {
                 window.Update(new WindowFocusChange(id));
             });
        }

        public void FocusNextWindow()
        {
            if (_canvas.Windows.Any())
            {
                var windows = _canvas.Windows.ToList();
                var focused = windows.FirstOrDefault(f => f.HasFocus);
                if(focused != null)
                {
                    int idx = windows.IndexOf(focused) + 1;
                    if(idx >= windows.Count)
                    {
                        idx = 0;
                    }
                    Focus(windows[idx].Id);
                }
            }
        }

        public ConsoleWindow GetWindow(string id)
        {
            return _canvas[id];
        }

        public ConsoleWindow RemoveWindow(string id)
        {

            var window = _canvas.RemoveWindow(id);
            _focusObservers.Remove(window);
            _inputHandler.Unregister(window);
            if (window.HasFocus && _canvas.Windows.Any())
            {
                Focus(_canvas.Windows.First().Id);
            }
            return window;
        }

        public void HookInputLoop()
        {
            _inputHandler.Stop();
            _inputHandler.StartBlocking();
        }

        public void UnhookInputlook()
        {
            _inputHandler.Stop();

        }

        public void Dispose()
        {
            _inputHandler.Stop();
            _inputHandler.Unregister(this);
            ConsoleInteropt.CloseHandle(_screenBuffer);
        }

        private Lazy<KeyEventHandler<UiContext>> _eventHandlers = new Lazy<KeyEventHandler<UiContext>>(() => new KeyEventHandler<UiContext>(), true);

        public IKeyEventHandler<UiContext> Keys
        {
            get
            {
                return _eventHandlers.Value;
            }

        }
        public void Update(ControlKeyReceived input)
        {
            if (!this._canvas.Windows.Any())
            {
                _eventHandlers.Value.Execute(input.GenerateDictionaryKey("f"), this);
            }
            _eventHandlers.Value.Execute(input.GenerateDictionaryKey("a"), this);
        }

        public void MaximizeWindow(string id)
        {
            if (!_canvas[id].Maximized)
            {
                _canvas[id].ToggleMaximize(_canvas.ViewPort);
            }
        }

        public void RestoreWindowSize(string id)
        {
            if (_canvas[id].Maximized)
            {
                _canvas[id].ToggleMaximize(_canvas.ViewPort);
            }
        }
    }
}
