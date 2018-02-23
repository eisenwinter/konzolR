using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.TitleScreen
{
    public interface ITitleScreen : IDisposable
    {

        /// <summary>
        /// displays the title screen
        /// </summary>
        void Show();

        /// <summary>
        /// changes the text beneath the title screen
        /// </summary>
        /// <param name="message"></param>
        void ChangeText(string message);

        /// <summary>
        /// closes the title screen and clears the console
        /// </summary>
        void Close();
    }
}
