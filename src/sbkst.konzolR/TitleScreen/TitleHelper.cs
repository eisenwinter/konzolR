using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.TitleScreen
{
    public static class TitleHelper
    {
        /// <summary>
        /// create a new title screen for the applcation
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public static ITitleScreen CreateTitlescreenFor(string applicationName)
        {
            return new DefaultTitleScreen(applicationName);
        }
    }
}
