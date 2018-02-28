using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Exceptions
{
    public class UnregisterKeyHandlerException : Exception
    {
        public string AttemptedToRegister { get; private set; }

        public UnregisterKeyHandlerException(string message) : base(message)
        {

        }

        public UnregisterKeyHandlerException(string message, string attemptedtoreiger) : base(message)
        {
           this.AttemptedToRegister = attemptedtoreiger;
        }
    
    }
}
