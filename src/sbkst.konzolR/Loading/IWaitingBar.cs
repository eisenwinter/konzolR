using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Loading
{
    public interface IWaitingBar : IDisposable
    {
        /// <summary>
        /// starts the bar
        /// </summary>
        void Start();
        /// <summary>
        /// Stops the bar
        /// </summary>
        void Done();
    }
}
