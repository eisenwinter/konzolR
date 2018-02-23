using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace sbkst.konzolR.Arguments.Config
{
    class CommandLineArrayParameter : CommandLineParameter
    {
        public CommandLineArrayParameter(string cmd, string help, PropertyInfo boundTo, PropertyInfo input, char seperator) : base(cmd, help, boundTo)
        {
            this.Input = input;
            this.Seperator = seperator;
            this.ArgsExplained += " (A [" + seperator + "B, " + seperator + "...])";
        }
        public char Seperator { get; private set; }
        public PropertyInfo Input { get; private set; }

        public override bool Search(string[] args, object item)
        {
            if (base.Search(args, item))
            {
                int idx = args.ToList().IndexOf(Command);
                if ((idx + 1) < args.Length)
                {
                    Input.SetValue(item, args[idx + 1].Split(new char[] { Seperator },StringSplitOptions.RemoveEmptyEntries));
                    return true;
                }
                this.BindingErros.Add(String.Format("Missing argument {0} for command {1}", ArgsExplained.Trim(), this.Command));

            }
            return false;
        }
    }
}
