using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Utility;
using sbkst.konzolR.Ui.Layout;
namespace sbkst.konzolR.Ui
{
    class ConsoleCanvas 
    {
        struct CanvasTile
        {
            public char Character { get; set; }
            public ushort DwFlag { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is CanvasTile)
                {
                    return this.Character == ((CanvasTile)obj).Character && this.DwFlag == ((CanvasTile)obj).DwFlag;
                }
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {

                return base.GetHashCode();
            }
        }

        private Dictionary<string,ConsoleWindow> _windows = new Dictionary<string, ConsoleWindow>();

        public IEnumerable<ConsoleWindow> Windows
        {
            get
            {
                if (_windows == null)
                    yield return null;
                foreach(var w in _windows.Select(s => s.Value))
                {
                    yield return w;
                }
            }
        }

        public ConsoleWindow this[string s]
        {
            get
            {
                return _windows[s];
            }
            set
            {

                value.Zindex = _windows.Count; //before so we dont trigger the redraw yet
                value.OnRequestRedraw += (window,full) =>
                {
                    if (!full)
                    {
                        RedrawArea(window.Position, window.Size);
                    }
                    else
                    {
                        Redraw();
                    }
                   
                };
                _windows[s] = value;
            }
        }

        private void RedrawArea(Position pos, Size size)
        {
            ushort width = (ushort)(pos.X + size.Width);
            ushort height = (ushort)(pos.Y + size.Height);
            UpdateWithin(width, height, pos.X, pos.Y);
            UpdateScreenBuffer(); 
        }

        /// reserved
        //private ConsoleMenu _menu = null;
        //private ConsoleStatusStrip _statusStrip = null;

        private CanvasTile[] _tiles;
        private Stack<Position> _invalidTiles = new Stack<Position>();

        private Size _viewport;

        public Size ViewPort
        {
            get
            {
                return _viewport;
            }
        }

        private IntPtr _screenBuffer;
        ConsoleColor _backgroundColor;

        private CanvasTile GenerateTileAt(ushort x, ushort y)
        {
            var win = _windows.Select(s=>s.Value)
                .OrderByDescending(c => c.Zindex)
                .FirstOrDefault(BoundingBoxFilter.Filter(x,y));
            if (win != null)
            {
                var provider = win.GetProvider();
                var relative = win.Position.RelativePositionTo(new Position(x,y));
                var itm = provider.GetRelative(relative.X, relative.Y);
                return new CanvasTile
                {
                    Character = itm.Item1,
                    DwFlag = itm.Item2
                };
            }
            else
            {
                return new CanvasTile
                {
                    Character = ' ',
                    DwFlag = _backgroundColor.ColorToBackgroundDWORD()
                };
            }
        }

        public void Initiliaze(ConsoleColor backgroundColor)
        {
            _backgroundColor = backgroundColor;
#if DEBUG
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif
            Rebuild();
#if DEBUG
            sw.Stop();
            System.Diagnostics.Trace.WriteLine(String.Format("Building the tile array took {0} ms", sw.ElapsedMilliseconds));
            sw.Reset();
            long tc = 0;
            long ac = 0;
            sw.Start();
#endif
            for (short i = 0; i < _viewport.Width; i++)
            {
                for (short j = 0; j < _viewport.Height; j++)
                {
                    var tile = _tiles[Index((ushort)i, (ushort)j)];
                    var coord = new Internals.ConsoleInteropt.COORD
                    {
                        X = i,
                        Y = j
                    };
                    if (tile.DwFlag != 0)
                    {
                        Internals.ConsoleInteropt.FillConsoleOutputAttribute(_screenBuffer, tile.DwFlag, 1, coord, out uint attrwritten);
#if DEBUG
                        ac += attrwritten;
#endif
                    }

                    if (tile.Character != ' ')
                    {
                        Internals.ConsoleInteropt.FillConsoleOutputCharacter(_screenBuffer, tile.Character, 1, coord, out uint written);
#if DEBUG
                        tc += written;
#endif
                    }
                }
            }
#if DEBUG
            sw.Stop();
            System.Diagnostics.Trace.WriteLine(String.Format("Drawing to output buffer took {0} ms for {1} chars and {2} attributes", sw.ElapsedMilliseconds, tc, ac));
#endif

        }

        private void Rebuild()
        {
            for (ushort i = 0; i < _viewport.Width; i++)
            {
                for (ushort j = 0; j < _viewport.Height; j++)
                {
                    _tiles[Index(i, j)] = GenerateTileAt(i, j);
                }
            }
        }

        private void UpdateWithin(ushort width, ushort height, ushort x, ushort y)
        {
            for (ushort i = x; i < width; i++)
            {
                for (ushort j = y; j < height; j++)
                {
                    var newTile = GenerateTileAt(i, j);
                    if (!_tiles[Index(i, j)].Equals(newTile))
                    {
                        _tiles[Index(i, j)] = newTile;
                        _invalidTiles.Push(new Position((ushort)i, (ushort)j));
                    }
                }
            }
        }

        public void Redraw()
        {
#if DEBUG
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif
            UpdateWithin(_viewport.Width, _viewport.Height, 0, 0);
#if DEBUG
            sw.Stop();
            System.Diagnostics.Trace.WriteLine(String.Format("Updating tiles array took {0} ms", sw.ElapsedMilliseconds));
#endif
            UpdateScreenBuffer();

        }

        private void UpdateScreenBuffer()
        {
#if DEBUG
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif
            if (_invalidTiles.Any())
            {
                while (_invalidTiles.Any())
                {
                    var pos = _invalidTiles.Pop();
                    var tile = _tiles[Index(pos.X, pos.Y)];
                    var coord = new Internals.ConsoleInteropt.COORD
                    {
                        X = (short)pos.X,
                        Y = (short)pos.Y
                    };
                    Internals.ConsoleInteropt.FillConsoleOutputAttribute(_screenBuffer, tile.DwFlag, 1, coord, out uint attrwritten);
                    Internals.ConsoleInteropt.FillConsoleOutputCharacter(_screenBuffer, tile.Character, 1, coord, out uint written);
                }

            }
#if DEBUG
            sw.Stop();
            System.Diagnostics.Trace.WriteLine(String.Format("Redraw took {0} ms", sw.ElapsedMilliseconds));
#endif
        }

        public ConsoleCanvas(IntPtr screenBuffer, ushort width, ushort height)
        {
            _viewport = new Size(width, height);
            _tiles = new CanvasTile[(width * height)];
            _screenBuffer = screenBuffer;
        }

        private ushort Index(ushort x, ushort y)
        {
            return Convert.ToUInt16(x * _viewport.Height + y);
        }

        public ConsoleWindow RemoveWindow(string id)
        {
            var window = _windows[id];
            _windows.Remove(id);
            RedrawArea(window.Position, window.Size);
            return window;
        }
    }
}
