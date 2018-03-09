using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using sbkst.konzolR.Ui.Utility;
namespace sbkst.konzolR.Ui.Layout
{
    /// <summary>
    /// defines a positive-only two dimensional position
    /// </summary>
    public class Position : INotifyPropertyChanged
    {
        private Position _absolutePosition;

        public Position()
        {

        }

        public Position(Position position)
        {
            _x = position.X;
            _y = position.Y;
        }

        public Position(Position position, Position relativeTo)
        {
            _x = position.X;
            _y = position.Y;
            this._absolutePosition = relativeTo;
        }

        public Position(ushort x, ushort y)
        {
            _x = x;
            _y = y;
        }

      
        private ushort _x;
        /// <summary>
        /// x coordinate
        /// </summary>
        public ushort X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("X"));
            }
        }

        private ushort _y;
        /// <summary>
        /// y coordinate
        /// </summary>
        public ushort Y { get
            {
                return _y;
            }
            set
            {
                _y = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Y"));
            }
        }

        /// <summary>
        /// checks if the position is a relative position or a absolute
        /// </summary>
        public Boolean IsRelative
        {
            get
            {
                return _absolutePosition != null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// gets the absolute position
        /// </summary>
        /// <returns></returns>
        public Position GetAbsolutePosition()
        {
            var pos = new Position(this);
            var tmp = _absolutePosition;
            while(tmp != null)
            {
                pos._x += tmp._x;
                pos._y += tmp._y;
                tmp = tmp._absolutePosition;
            }
            return pos;
        }

        public void SetRelativeTo(Position position)
        {
            _absolutePosition = position;
        }

        /// <summary>
        /// adds a position to another
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Position operator +(Position p, Position q)
        {
            return new Position((ushort)(p.X + q.X), (ushort)(p.Y + q.Y));
        }

        public static Position operator - (Position p, ushort i)
        {
            var pos = new Position
            {
                X = (ushort)((p.X - i).Clamp(0, Int16.MaxValue)),
                Y = (ushort)((p.Y - i).Clamp(0, Int16.MaxValue))
            };

                
            return pos;
        }

        public static Position operator +(Position p, ushort i)
        {
            var pos = new Position
            {
                X = (ushort)((p.X + i).Clamp(0, Int16.MaxValue)),
                Y = (ushort)((p.Y + i).Clamp(0, Int16.MaxValue))
            };
            return pos;
        }


    }
}
