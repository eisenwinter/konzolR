using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Input
{
    interface IKeyListener<T> where T : class
    {
        IKeyEventHandler<T> Keys { get; }
    }
}
