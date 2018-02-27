using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Behavior
{
    public class WindowFocusChange
    {
        public WindowFocusChange(string id)
        {
            this.Id = id;
        }
        public string Id { get; private set; }
    }
}
