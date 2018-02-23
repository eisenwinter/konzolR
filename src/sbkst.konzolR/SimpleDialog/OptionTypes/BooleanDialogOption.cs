using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace sbkst.konzolR.SimpleDialog.OptionTypes
{
    class BooleanDialogOption<T> : ReflectedDialogOptionBase<T, bool>
    {
        public BooleanDialogOption(Expression<Func<T, bool>> expression, string question, T item)
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
            while (!_yes.Any(a => a == arg.ToLower()) && !_no.Any(a => a == arg.ToLower()))
            {
                Console.WriteLine("Invalid Input this field should be either y or n");
                Console.Write(this.Question + " ");
                arg = Console.ReadLine();
            }

            SetValue(_yes.Any(a => a == arg.ToLower()));
        }
    }
}
