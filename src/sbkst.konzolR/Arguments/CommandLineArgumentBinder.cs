using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;
namespace sbkst.konzolR.Arguments
{
    class CommandLineArgumentBinder<T> : ICommandLineArgumentBinder<T>, IArgumentSetup<T> where T : class
    {
       
        T _item;
        public CommandLineArgumentBinder()
        {
            _item = Activator.CreateInstance<T>();
        }
        public List<Config.CommandLineParameter> param = new List<Config.CommandLineParameter>();
        
        private PropertyInfo GetInfo<R>(Expression<Func<T, R>> field)
        {
            MemberExpression body = (field.Body.NodeType == ExpressionType.Convert) ? (MemberExpression)((UnaryExpression)field.Body).Operand : (MemberExpression)field.Body;
            return _item.GetType().GetProperty(body.Member.Name);
        }

        public IArgumentSetup<T> CreateArgumentWithPayloadFor(string command, string helptext, Expression<Func<T, bool>> field, Expression<Func<T, string>> payload, Regex payloadFilter)
        {
            param.Add(new Config.CommandLineRegexParameter(command, helptext, GetInfo(field), GetInfo(payload), payloadFilter));
            return this;
        }

        public IArgumentSetup<T> CreateArrayArgumentFor(string command, string helptext, Expression<Func<T, bool>> field, Expression<Func<T, string[]>> target, char seperator = ',')
        {
            param.Add(new Config.CommandLineArrayParameter(command, helptext, GetInfo(field), GetInfo(target),seperator));
            return this;
        }

        public IArgumentSetup<T> CreateFor(string command, string helptext, Expression<Func<T, bool>> field)
        {
            param.Add(new Config.CommandLineParameter(command,helptext, GetInfo(field)));
            return this;
        }

        public IArgumentSetup<T> CreatePathArgumentFor(string command, string helptext, Expression<Func<T, bool>> field, Expression<Func<T, string>> path)
        {
           
            param.Add(new Config.FileCommandLineParameter(command,helptext, GetInfo(field), GetInfo(path)));
            return this;
        }

        public ICommandLineArgumentBinder<T> Build()
        {
            return this;
        }

        public void Help()
        {
            foreach(var c in param)
            {
                Console.WriteLine("{0}{1} => {2}", c.Command, c.ArgsExplained, c.Help);
            }
        }

        public T Bind(string[] argv)
        {
            List<string> errors = new List<string>();
            foreach(var c in param)
            {
                if(!c.Search(argv, _item) && c.BindingErros.Any())
                {
                    errors.AddRange(c.BindingErros);
                }
            }
            if (errors.Any())
            {
                throw new ArgumentBinderException(String.Format("{0} errors occured during argument binding", errors.Count), errors);
            }
            return _item;
        }
    }
}
