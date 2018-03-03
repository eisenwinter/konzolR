using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using sbkst.konzolR.Ui.Behavior;
using sbkst.konzolR.Ui.Input;
using sbkst.konzolR.Ui.Defaults;
namespace sbkst.konzolR.Ui
{
    public class UiContext :
        IObserve<ControlKeyReceived>,
        IKeyListener<UiContext>,
        IDisposable
    {

        private IDefaultHotkeys _defaultHotkeys;
 
        private ConsoleCanvas _canvas;
        private List<IObserve<WindowFocusChange>> _focusObservers = new List<IObserve<WindowFocusChange>>();
       
        private InputHandler _inputHandler;
        private DefaultBehaviorObserver<CursorPositionChange> _cursorTracking; 

        public UiContext()
        {
            _inputHandler = new InputHandler();
            _canvas = new ConsoleCanvas((ushort)Console.BufferWidth, (ushort)Console.WindowHeight);
            _cursorTracking = new DefaultBehaviorObserver<CursorPositionChange>();
            _cursorTracking.Register(_canvas);
            _inputHandler.Start();
            _inputHandler.Register(this);

        }

        public void Initialize(ConsoleColor backgroundColor, IDefaultHotkeys overrideHotkeys = null)
        {
            _defaultHotkeys = overrideHotkeys ?? new DefaultHotkeys();
            HookupDefaultHotkeys(_defaultHotkeys);
            _canvas.Initiliaze(backgroundColor);
        }

        private void HookupDefaultHotkeys(IDefaultContextHotkeys hotkeys)
        {
            if(hotkeys.CONTEXT_FOCUS_NEXT_WINDOW != null)
            {
                this.Keys.On(hotkeys.CONTEXT_FOCUS_NEXT_WINDOW.Key, hotkeys.CONTEXT_FOCUS_NEXT_WINDOW.Modifier, (ctx) =>
                {
                    FocusNextWindow();
                    return true;
                });
            }
        }

        private void HookdownDefaultHotkeys(IDefaultContextHotkeys hotkeys)
        {
            if (hotkeys.CONTEXT_FOCUS_NEXT_WINDOW != null)
            {
                this.Keys.Off(hotkeys.CONTEXT_FOCUS_NEXT_WINDOW.Key, hotkeys.CONTEXT_FOCUS_NEXT_WINDOW.Modifier);
            }
        }

        public void AddWindow(ConsoleWindow window)
        {
            _focusObservers.Add(window);
            _inputHandler.Register(window);
            window.Hotkeys = this._defaultHotkeys;
            window.Cursor = this._cursorTracking;
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
            _canvas.Dispose();
            HookdownDefaultHotkeys(this._defaultHotkeys);
            _defaultHotkeys = null;
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
