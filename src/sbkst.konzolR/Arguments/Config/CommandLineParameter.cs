using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace sbkst.konzolR.Arguments.Config
{
    class CommandLineParameter
    {
        
        public CommandLineParameter(string cmd, string help, PropertyInfo boundTo)
        {
            this.Command = cmd;
            this.Help = help;
            this.BoundTo = boundTo;
            this.ArgsExplained = "";
            this.BindingErros = new List<string>();
        }
        public string Command { get; private set; }

        public string ArgsExplained { get; protected set; }

        public string Help { get; private set; }

        public PropertyInfo BoundTo { get; private set; }

        public List<string> BindingErros { get; private set; }

        public virtual bool Search(string[] args, object item)
        {
            if(args.Any(a=> a == this.Command))
            {
                BoundTo.SetValue(item, true, null);
                return true;
            }
            return false;
        }

    }
}
