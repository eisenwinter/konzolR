using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace sbkst.konzolR.Arguments.Config
{
    class CommandLineRegexParameter : CommandLineParameter
    {
        public CommandLineRegexParameter(string cmd, string help, PropertyInfo boundTo, PropertyInfo payload, Regex regex ) : base(cmd, help, boundTo)
        {
            this.Payload = payload;
            this.Pattern = regex;
            this.ArgsExplained = " <MATCHING:" + regex + ">";
        }

        public Regex Pattern { get; private set; }
        public PropertyInfo Payload { get; private set; }

        public override bool Search(string[] args, object item)
        {
            if (base.Search(args, item))
            {
                int idx = args.ToList().IndexOf(Command);
                if ((idx + 1) < args.Length)
                {
                    string toTest = args[idx + 1];
                    if (Pattern.IsMatch(toTest))
                    {
                        BoundTo.SetValue(item, toTest, null);
                        return true;
                    }
                    this.BindingErros.Add(String.Format("Wrong argument for command {1} needs to match {0}", Pattern, this.Command));
                }
                else
                {
                    this.BindingErros.Add(String.Format("Missing argument {0} for command {1}", ArgsExplained.Trim(), this.Command));
                }
                

            }
            return false;
        }
    }
}
