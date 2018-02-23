using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace sbkst.konzolR.Arguments.Config
{
    class FileCommandLineParameter : CommandLineParameter
    {
        public FileCommandLineParameter(string cmd, string help, PropertyInfo boundTo, PropertyInfo path) : base(cmd, help, boundTo)
        {
            this.Path = path;
            this.ArgsExplained += " <PATH>";
        }

        public PropertyInfo Path { get; private set; }

        public override bool Search(string[] args, object item)
        {
            if (base.Search(args, item))
            {
                int idx = args.ToList().IndexOf(Command);
                if ((idx + 1) < args.Length)
                {
                    Path.SetValue(item,args[idx + 1].Trim(new char[] { '"' }));
                    return true;
                }
                this.BindingErros.Add(String.Format("Missing argument {0} for command {1}", ArgsExplained.Trim(), this.Command));
                
            }
            return false;
        }
    }
}
