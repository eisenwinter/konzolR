using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Controls
{
    public abstract class ConsoleControl
    {
        protected ConsoleControl(string id)
        {
            this.Id = id;
        }

        public virtual bool IsReadonly { get; protected set; }
        public virtual bool Valid { get; protected set; }

        public string Id { get; private set; }
    }
}
