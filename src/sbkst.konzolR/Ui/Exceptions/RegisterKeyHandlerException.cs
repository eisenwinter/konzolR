using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Exceptions
{
    public class RegisterKeyHandlerException : Exception
    {

        public string AttemptedToRegister { get; private set; }

        public RegisterKeyHandlerException(string message) : base(message)
        {

        }

        public RegisterKeyHandlerException(string message, string attemptedtoreiger) : base(message)
        {
            this.AttemptedToRegister = attemptedtoreiger;
        }

    }
}
