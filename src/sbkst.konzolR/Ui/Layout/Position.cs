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

        public Position(Position position, Position absolutePosition)
        {
            _x = position.X;
            _y = position.Y;
            if (absolutePosition.IsRelative)
            {
                throw new Exceptions.NestedAbsolutePositionException("Cant set a relative position as absolute position");
            }
            this._absolutePosition = new Position(absolutePosition);
        }

        public Position(ushort x, ushort y)
        {
            _x = x;
            _y = y;
        }

        public Position(ushort x, ushort y, ushort absx, ushort absy)
        {
            _x = x;
            _y = y;
            this._absolutePosition = new Position(absx, absy);
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
            return IsRelative ? this._absolutePosition : this;
        }

        public void SetRelativeTo(Position position)
        {
            if (position.IsRelative)
            {
                _absolutePosition = position._absolutePosition + this;
            }
            else
            {
                _absolutePosition = position + this;
            }
        }

        /// <summary>
        /// adds a position to another
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Position operator +(Position p, Position q)
        {
            if (q.IsRelative || p.IsRelative)
            {
                var pos = new Position((ushort)(p.X + q.X), (ushort)(p.Y + q.Y)); ;
                pos._absolutePosition = ((q.IsRelative) ? q._absolutePosition : q) + ((p.IsRelative) ? p._absolutePosition : p);
                return pos;
            }
            else
            {
                return new Position((ushort)(p.X + q.X), (ushort)(p.Y + q.Y));
            }
        }

        public static Position operator - (Position p, ushort i)
        {
            var pos = new Position
            {
                X = (ushort)((p.X - i).Clamp(0, Int16.MaxValue)),
                Y = (ushort)((p.Y - i).Clamp(0, Int16.MaxValue))
            };

            if (pos.IsRelative)
            {
                pos._absolutePosition.X = (ushort)((p._absolutePosition.X - i).Clamp(0, Int16.MaxValue));
                pos._absolutePosition.Y = (ushort)((p._absolutePosition.Y - i).Clamp(0, Int16.MaxValue));
            }
            return pos;
        }

        public static Position operator +(Position p, ushort i)
        {
            var pos = new Position
            {
                X = (ushort)((p.X + i).Clamp(0, Int16.MaxValue)),
                Y = (ushort)((p.Y + i).Clamp(0, Int16.MaxValue))
            };

            if (pos.IsRelative)
            {
                pos._absolutePosition.X = (ushort)((p._absolutePosition.X + i).Clamp(0, Int16.MaxValue));
                pos._absolutePosition.Y = (ushort)((p._absolutePosition.Y + i).Clamp(0, Int16.MaxValue));
            }
            return pos;
        }


    }
}
