using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Behavior
{
    public interface IBehaviorObserver<T>
    {
        void Register(IObserve<T> observer);
        void Unregister(IObserve<T> observer);
        void RequestChange(T request);
    }
}
