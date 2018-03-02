using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using sbkst.konzolR.Ui.Rendering;
using sbkst.konzolR.Ui.Layout;
using sbkst.konzolR.Ui.Utility;
using sbkst.konzolR.Ui.Behavior;
using sbkst.konzolR.Ui.Input;
using sbkst.konzolR.Ui.Defaults;
namespace sbkst.konzolR.Ui
{

    public class ConsoleWindow :
                IRenderable,
                IObserve<WindowFocusChange>,
                IObserve<ControlKeyReceived>,
                IKeyListener<ConsoleWindow>
    {
        private int _zindex = 0;
        private ushort _padding = 1;

        public ushort Padding
        {
            get
            {
                return _padding;
            }
            set
            {
                _padding = value;
            }
        }
        private string _title;
        private string _id;
        private Size _size;

        private Position _rollbackPosition;
        private Size _rollbackSize;

        private bool _isMaximized = false;
        public bool Maximized
        {
            get
            {
                return _isMaximized;
            }
        }


        IDefaultWindowHotkeys _hotkeys;
        public IDefaultWindowHotkeys Hotkeys
        {
            get
            {
                return _hotkeys;
            }
            set
            {
                if (_hotkeys != null)
                {
                    this.Keys.WithFocusOff(_hotkeys.WINDOW_NEXT_CONTROL.Key, _hotkeys.WINDOW_NEXT_CONTROL.Modifier);
                    this.Keys.WithFocusOff(_hotkeys.WINDOW_PREVIOUS_CONTROL.Key, _hotkeys.WINDOW_PREVIOUS_CONTROL.Modifier);
                }
                _hotkeys = value;
                if (value != null)
                {
                    this.Keys.WithFocusOn(
                        _hotkeys.WINDOW_PREVIOUS_CONTROL.Key,
                        _hotkeys.WINDOW_PREVIOUS_CONTROL.Modifier,
                        (window) =>
                    {
                        FocusPreviousControl();
                        return true;
                    });
                    this.Keys.WithFocusOn(
                         _hotkeys.WINDOW_NEXT_CONTROL.Key,
                         _hotkeys.WINDOW_NEXT_CONTROL.Modifier,
                         (window) =>
                         {
                             FocusNextControl();
                             return true;
                         });
                }

            }
        }

        IBehaviorObserver<CursorPositionChange> _cursor;
        public IBehaviorObserver<CursorPositionChange> Cursor
        {
            get
            {
                return _cursor;
            }
            set
            {
                _cursor = value;

            }
        }

        private string _currentlyFocusedId = string.Empty;


        private void FocusNextControl()
        {
            var ctrl = this.Controls.FirstOrDefault(a => a is Controls.IFocusableControl && (a.Id == _currentlyFocusedId || _currentlyFocusedId == string.Empty));
            if (ctrl != null)
            {
                SetFocusTo(this.Controls.Previous(ctrl, true));
            }
        }

        private void FocusPreviousControl()
        {
            var ctrl = this.Controls.FirstOrDefault(a => a is Controls.IFocusableControl && (a.Id == _currentlyFocusedId || _currentlyFocusedId == string.Empty));
            if (ctrl != null)
            {
                SetFocusTo(this.Controls.Next(ctrl, true));
            }
        }

        private bool _currentlyMaximizing = false;
        public void ToggleMaximize(Size viewPort)
        {
            if (!_currentlyMaximizing)
            {

                _currentlyMaximizing = true;

                if (!_isMaximized)
                {
                    this._rollbackPosition = new Position(this._position);
                    this._rollbackSize = new Size(this.Size);
                    this.Size.Width = (ushort)(viewPort.Width - 2);
                    this.Size.Height = (ushort)(viewPort.Height - 2);
                    this.Position.X = 1;
                    this.Position.Y = 1;
                    _isMaximized = true;
                }
                else
                {
                    this._position = new Position(this._rollbackPosition);
                    this._size = new Size(_rollbackSize);
                    this._rollbackPosition = null;
                    this._rollbackSize = null;
                    _isMaximized = false;
                }
                PerformLayout();
                RefreshCursorPosition();
                OnRequestRedraw?.Invoke(this, true);
                _currentlyMaximizing = false;
            }
        }

        ConsoleColor _color = ConsoleColor.Gray;

        private Boolean _focused;
        public Boolean HasFocus
        {
            get
            {
                return _focused;
            }
            private set
            {
                bool redraw = _focused != value;
                _focused = value;
                if (redraw)
                {
                    if (_focused)
                    {
                        Zindex += 100;
                    }
                    else
                    {
                        Zindex -= 100;
                    }
                }
            }
        }
        public delegate void RequestRedraw(IRenderable rectangle, bool fullRedraw = false);
        public event RequestRedraw OnRequestRedraw;

        public String Id
        {
            get
            {
                return _id;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        public ConsoleColor BackgroundColor
        {
            get
            {
                return _color;
            }
        }

        public int Zindex
        {
            get
            {
                return _zindex;
            }
            set
            {
                _zindex = value;
                OnRequestRedraw?.Invoke(this);
            }
        }

        public IEnumerable<Controls.ConsoleControl> Controls
        {
            get
            {
                foreach (var c in _controls)
                {
                    yield return c.Value;
                }
            }
        }

        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private Position _position;

        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private bool HasControlListening
        {
            get
            {
                return !String.IsNullOrEmpty(_currentlyFocusedId) && _controls[_currentlyFocusedId] is Controls.IListeningControl;
            }
        }

        private Controls.IListeningControl GetListeningControl()
        {
            if (HasControlListening)
            {
                return _controls[_currentlyFocusedId] as Controls.IListeningControl;
            }
            return null;
        }


        private Dictionary<string, Controls.ConsoleControl> _controls = new Dictionary<string, Controls.ConsoleControl>();

        public ConsoleWindow(string title, string id, Size size, Position pos)
        {
            _title = title;
            _id = id;
            this._size = size;
            this._position = pos;
        }

        public void ChangeBackgroundColor(ConsoleColor color)
        {
            _color = color;
            OnRequestRedraw?.Invoke(this);
        }

        private void ApplyPadding(Controls.ConsoleControl control)
        {
            if ((this.Size.Width - (Padding * 2)) > 0)
            {
                control.Size.Width = (ushort)(this.Size.Width - (Padding * 2));
                control.Position.X = this.Padding;
            }
            else //if we cant apply the padding we will just try to scale it all the way
            {
                control.Size.Width = this.Size.Width;
                control.Position.X = 0;
            }
        }

        public void AddControl(Controls.ConsoleControl control)
        {
            control.Size.Height = 1;
            control.Position.Y = (ushort)(2 + _controls.Count);
            ApplyPadding(control);
            _controls.Add(control.Id, control);
            OnRequestRedraw?.Invoke(this);
        }

        public Controls.ConsoleControl GetControl(string id)
        {
            return _controls[id];
        }

        public void PerformLayout()
        {
            int idx = 0;
            foreach (var control in _controls)
            {
                //basic auto-masonry vertical
                ApplyPadding(control.Value);
                control.Value.Position.Y = (ushort)(2 + idx);
                idx++;
            }
        }

        private IRenderProvider _renderProvider = null;

        public IRenderProvider GetProvider()
        {
            if (_renderProvider == null)
            {
                _renderProvider = new ConsoleWindowRenderEngine(this);
            }
            return _renderProvider;
        }

        public void Update(WindowFocusChange input)
        {
            this.HasFocus = input.Id == this.Id;
            if (HasFocus && this.Cursor != null)
            {
                RefreshCursorPosition();
            }

        }

        private void RefreshCursorPosition()
        {
            if (this.Controls.Any(a => a is Controls.IFocusableControl && (a.Id == _currentlyFocusedId || _currentlyFocusedId == string.Empty)))
            {
                var ctrl = this.Controls.First(a => a is Controls.IFocusableControl && (a.Id == _currentlyFocusedId || _currentlyFocusedId == string.Empty));
                SetFocusTo(ctrl);
            }
            else
            {
                Cursor.RequestChange(new CursorPositionChange(0, 0, false));
            }
        }

        private void SetFocusTo(Controls.ConsoleControl ctrl)
        {
            ushort x = (ushort)(ctrl.Position.X + this.Position.X);
            ushort y = (ushort)(ctrl.Position.Y + this.Position.Y);
            if (this.HasFocus) { 
                Cursor.RequestChange(new CursorPositionChange(x, y));
            }
            if (!String.IsNullOrEmpty(_currentlyFocusedId))
            {
                (_controls[_currentlyFocusedId] as Controls.IFocusableControl).Blur();
            }
             (ctrl as Controls.IFocusableControl).Focus();
            _currentlyFocusedId = ctrl.Id;
           
        }

        public void Update(ControlKeyReceived input)
        {
            bool isSuccessfullyExecuted = false;
            if (this._focused)
            {
                if(_eventHandlers.Value.Execute(input.GenerateDictionaryKey("f"), this))
                {
                    isSuccessfullyExecuted = true;
                }
            }
            if(_eventHandlers.Value.Execute(input.GenerateDictionaryKey("a"), this))
            {
                isSuccessfullyExecuted = true;
            }
            if (!isSuccessfullyExecuted && _focused && HasControlListening)
            {
                var ctrl = GetListeningControl();
                //if (ctrl.KeyReceived(input))
                //{
                //    //ToDo: correct redraw
                //    //FixMe: relative position to absolute
                //    OnRequestRedraw?.Invoke(ctrl as Controls.ConsoleControl);
                //}
            }
        }

        private Lazy<KeyEventHandler<ConsoleWindow>> _eventHandlers = new Lazy<KeyEventHandler<ConsoleWindow>>(() => new KeyEventHandler<ConsoleWindow>(), true);

        public IKeyEventHandler<ConsoleWindow> Keys
        {
            get
            {
                return _eventHandlers.Value;
            }

        }
    }
}
