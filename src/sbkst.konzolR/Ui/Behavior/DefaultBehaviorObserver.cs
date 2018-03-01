using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Behavior
{
    class DefaultBehaviorObserver<T> : IBehaviorObserver<T>
    {
        private List<IObserve<T>> _observating = new List<IObserve<T>>();

        public void Register(IObserve<T> observer)
        {
            _observating.Add(observer);
        }

        public void RequestChange(T request)
        {
            _observating.ForEach(item =>
            {
                item.Update(request);
            });
        }

        public void Unregister(IObserve<T> observer)
        {
            _observating.Remove(observer);
        }
    }
}
