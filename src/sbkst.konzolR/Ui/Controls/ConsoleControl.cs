using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;
using sbkst.konzolR.Ui.Utility;
using sbkst.konzolR.Ui.Layout;
using System.ComponentModel;
namespace sbkst.konzolR.Ui.Controls
{
    public abstract class ConsoleControl : IRenderable
    {
        protected ConsoleControl(string id)
        {
            this.Id = id;
            this.Position = new Position();
            this.Size = new Size();
        }

        private bool _valid = true;
        public bool Valid { get
            {
                return _valid;
            }
            protected set
            {
                _valid = value;
            }
        }

        public virtual bool IsReadonly { get; protected set; }
        public string Id { get; private set; }
        public Position Position { get; protected set; }


        private Size _size;
        public Size Size { get
            {
                return _size;
            }
            protected set
            {
                _size = value;
            }
        }

        public abstract IRenderProvider GetProvider();

      
    }
}
