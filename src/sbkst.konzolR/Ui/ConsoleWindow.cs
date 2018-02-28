using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using sbkst.konzolR.Ui.Rendering;
using sbkst.konzolR.Ui.Layout;
using sbkst.konzolR.Ui.Behavior;
using sbkst.konzolR.Ui.Input;
namespace sbkst.konzolR.Ui
{
    public enum ConsoleWindowBackgroundColor { Red, Green, Blue }
    public class ConsoleWindow : 
                IRenderable, 
                IObserve<WindowFocusChange>, 
                IObserve<ControlKeyReceived>,
                IKeyListener<ConsoleWindow>
    {
        private int _zindex = 0;
        private string _title;
        private string _id;
        private Size _size;
        ConsoleColor _color = ConsoleColor.Red;

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
        public delegate void RequestRedraw(ConsoleWindow window);
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

        private Position position;

        public Position Position
        {
            get { return position; }
            set { position = value; }
        }


        private Dictionary<string, Controls.ConsoleControl> _controls = new Dictionary<string, Controls.ConsoleControl>();

        public ConsoleWindow(string title, string id, Size size, Position pos)
        {
            _title = title;
            _id = id;
            this._size = size;
            this.position = pos;
        }

        public void ChangeBackgroundColor(ConsoleColor color)
        {
            _color = color;
            OnRequestRedraw?.Invoke(this);
        }

        public void AddControl(Controls.ConsoleControl control)
        {
            control.Size.Height = 1;
            control.Size.Width = this.Size.Width;
            control.Position.X = 0;
            control.Position.Y = (ushort)(2 + _controls.Count);
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
                control.Value.Position.X = 0;
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
        }

        public void Update(ControlKeyReceived input)
        {
            if (this._focused)
            {
                _eventHandlers.Value.Execute(input.GenerateDictionaryKey("f"), this);
            }
            _eventHandlers.Value.Execute(input.GenerateDictionaryKey("a"), this);
        }

        private Lazy<KeyEventHandler<ConsoleWindow>> _eventHandlers = new Lazy<KeyEventHandler<ConsoleWindow>>(() => new KeyEventHandler<ConsoleWindow>(),true);

        public IKeyEventHandler<ConsoleWindow> Keys
        {
            get
            {
                return _eventHandlers.Value;
            }

        }
    }
}
