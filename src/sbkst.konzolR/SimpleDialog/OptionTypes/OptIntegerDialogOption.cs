using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace sbkst.konzolR.SimpleDialog.OptionTypes
{
    class OptIntegerDialogOption<T> : ReflectedDialogOptionBase<T, int?>
    {
        public OptIntegerDialogOption(Expression<Func<T, int?>> expression, string question, T item)
        {
            this.Expression = expression;
            this.Question = question;
            _item = item;
        }


        public override void Read()
        {
            var arg = Console.ReadLine();
            int d = 0;
            while (!String.IsNullOrEmpty(arg) && !Int32.TryParse(arg, out d))
            {
                Console.WriteLine("Invalid Input this field should be a numeric value");
                Console.Write(this.Question + " ");
                arg = Console.ReadLine();
            }
            if (!String.IsNullOrEmpty(arg))
            {
                SetValue(d);
            }
        }
    }
}
