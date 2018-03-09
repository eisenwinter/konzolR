using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace sbkst.konzolR.Ui.Layout
{
    /// <summary>
    /// defines a positive only size
    /// </summary>
    public class Size : INotifyPropertyChanged
    {
        public Size()
        {

        }

        public Size(Size size)
        {
            this.Width = size.Width;
            this.Height = size.Height;
        }

        public Size(ushort w, ushort h)
        {
            this.Width = w;
            this.Height = h;
        }


        private ushort _width;
        private ushort _height;

        /// <summary>
        /// width
        /// </summary>
        public ushort Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
            }
        }

        /// <summary>
        /// height
        /// </summary>
        public ushort Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Height"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
