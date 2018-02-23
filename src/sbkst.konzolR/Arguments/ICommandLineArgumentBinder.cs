using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Arguments
{
    public interface ICommandLineArgumentBinder<T> where T : class
    {
        /// <summary>
        /// displays the help for the registred commands to the console
        /// </summary>
        void Help();

        /// <summary>
        /// binds the given argument array
        /// </summary>
        /// <param name="argv">args array</param>
        /// <returns></returns>
        T Bind(string[] argv);
    }
}
