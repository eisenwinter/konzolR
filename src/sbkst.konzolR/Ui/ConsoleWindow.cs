using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;
using sbkst.konzolR.Ui.Layout;
namespace sbkst.konzolR.Ui
{
    public enum ConsoleWindowBackgroundColor { Red, Green, Blue }
    public class ConsoleWindow  : IRenderable
    {
        private int _zindex = 0;
        private string _title;
        private string _id;

        private bool _hidden;

        private Size size;
        ConsoleColor _color = ConsoleColor.Red;

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
                foreach(var c in _controls)
                {
                    yield return c.Value;
                }
            }
        }

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        private Position position;

        public Position Position
        {
            get { return position; }
            set { position = value; }
        }


        private Dictionary<string,Controls.ConsoleControl> _controls = new Dictionary<string, Controls.ConsoleControl>();

        public ConsoleWindow(string title, string id, Size size, Position pos)
        {
            _title = title;
            _id = id;
            this.size = size;
            this.position = pos;
        }

        public void ChangeBackgroundColor(ConsoleColor color)
        {
            _color = color;
            OnRequestRedraw?.Invoke(this);
        }

        public void AddControl(Controls.ConsoleControl control)
        {
            _controls.Add(control.Id,control);
            OnRequestRedraw?.Invoke(this);
        }

        public Controls.ConsoleControl GetControl(string id)
        {
            return _controls[id];
        }

        private IRenderProvider _renderProvider = null;

        public IRenderProvider GetProvider()
        {
            if(_renderProvider == null)
            {
                _renderProvider = new ConsoleWindowRenderEngine(this);
            }
            return _renderProvider;
        }
    }
}
