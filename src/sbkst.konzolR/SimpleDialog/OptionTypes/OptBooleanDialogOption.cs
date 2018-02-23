using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace sbkst.konzolR.SimpleDialog.OptionTypes
{
    class OptBooleanDialogOption<T> : ReflectedDialogOptionBase<T, bool?>
    {
        public OptBooleanDialogOption(Expression<Func<T, bool?>> expression, string question, T item)
        {
            this.Expression = expression;
            this.Question = question;
            _item = item;
        }



        private string[] _yes = new string[] { "y", "yes", "1", "true", "t" };
        private string[] _no = new string[] { "n", "no", "0", "false", "f" };

        public override void Read()
        {
            var arg = Console.ReadLine();
            if (!String.IsNullOrEmpty(arg))
            {
                SetValue(_yes.Any(a => a == arg.ToLower()));
            }
        }
    }
}
