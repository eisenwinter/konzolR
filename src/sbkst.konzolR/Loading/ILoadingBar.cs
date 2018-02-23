using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Loading
{
    public interface ILoadingBar : IWaitingBar
    {
        /// <summary>
        /// sets the current percentage [0-100]
        /// </summary>
        /// <param name="percentage"></param>
        void SetPercentage(short percentage);
    }
}
