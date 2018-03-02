using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Behavior
{
    /// <summary>
    /// obererve for a designated event
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObserve<T>
    {
        void Update(T input);
    }
}
