using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Loading
{
    public static class LoadingHelper
    {
        /// <summary>
        /// retrive a progress bar for loading with percentage
        /// </summary>
        /// <returns></returns>
        public static ILoadingBar GetLoadingBar()
        {
            return new LoadingBar(LoadingBar.BarType.LoadingPercentage);
        }

        /// <summary>
        /// retrive a progress bar for waiting (nonsense values)
        /// </summary>
        /// <returns></returns>

        public static IWaitingBar GetWaitingBar()
        {
            return new LoadingBar(LoadingBar.BarType.Waiting);
        }
    }
}
