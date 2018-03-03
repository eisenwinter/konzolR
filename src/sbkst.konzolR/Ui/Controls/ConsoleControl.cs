using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;
using sbkst.konzolR.Ui.Utility;
using sbkst.konzolR.Ui.Layout;
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

        public virtual bool IsReadonly { get; protected set; }
        public string Id { get; private set; }
        public Position Position { get; protected set; }

        public Size Size { get; protected set; }

        public abstract IRenderProvider GetProvider();

      
    }
}
