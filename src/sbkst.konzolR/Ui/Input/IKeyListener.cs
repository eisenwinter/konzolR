using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Input
{
    /// <summary>
    /// defines the key listener object which is used to register new keys listeners
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IKeyListener<T> where T : class
    {
        IKeyEventHandler<T> Keys { get; }
    }
}
