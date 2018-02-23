using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Arguments
{
    public class ArgumentBinderException : Exception
    {
        public ArgumentBinderException(string message) : base(message)
        {

        }

        public ArgumentBinderException(string message, IEnumerable<string> bindingErrors) : base(message)
        {
            this.BindingErrors = bindingErrors;
        }

        public IEnumerable<String> BindingErrors { get; private set; }
    }
}
