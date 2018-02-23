using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
namespace sbkst.konzolR.SimpleDialog.OptionTypes
{
    class StringDialogOption<T> : ReflectedDialogOptionBase<T, string>
    {
        private bool _opt;
        private bool _regexFilter;
        private Regex _filter;

        public StringDialogOption(Expression<Func<T, string>> expression, string question, T item, bool isOptional)
        {
            this.Expression = expression;
            this.Question = question;
            _item = item;
            _opt = isOptional;
        }

        public StringDialogOption(Expression<Func<T, string>> expression, string question, T item, bool isOptional, Regex filter)
            : this(expression, question, item, isOptional)
        {
            _filter = filter;
            _regexFilter = true;
        }

        public override void Read()
        {
            var arg = Console.ReadLine();
            if (String.IsNullOrEmpty(arg) && !_opt)
            {
                while (String.IsNullOrEmpty(arg))
                {
                    Console.WriteLine("Input required!");
                    Console.Write(this.Question + " ");
                    arg = Console.ReadLine();
                }
            }

            while (_regexFilter && !_filter.IsMatch(arg) && (!_opt || !(_opt && String.IsNullOrEmpty(arg))))
            {
                Console.WriteLine("Invalid Input for this field");
                if (_regexFilter)
                {
                    Console.WriteLine("Input has to match the following pattern: {0}", _filter);
                }

                Console.Write(this.Question + " ");
                arg = Console.ReadLine();
            }

            SetValue(arg);
        }


    }
}
