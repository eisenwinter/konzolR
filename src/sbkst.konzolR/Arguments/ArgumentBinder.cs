using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Arguments
{
    public static class ArgumentBinder
    {
        /// <summary>
        /// create a new argument binder for the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IArgumentSetup<T> Create<T>() where T : class
        {
            return new CommandLineArgumentBinder<T>();
        }
    }
}
