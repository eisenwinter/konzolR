using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.SimpleDialog.OptionTypes;
namespace sbkst.konzolR.SimpleDialog.Internals
{
    class SimpleConsoleDialog<T> : ISimpleDialog<T>
    {
        public SimpleConsoleDialog(string message)
        {
            this.Item = (T)Activator.CreateInstance(typeof(T));
            this.Message = message;
        }
        public string Message { get; set; }
        public T Item { get; set; }


        List<DialogOptionBase> options = new List<DialogOptionBase>();

        public void AddOption(DialogOptionBase o)
        {

            options.Add(o);
        }

        public T Run()
        {
            Console.WriteLine(Message);
            foreach (var o in options)
            {
                Console.Write(o.Question + " ");
                o.Read();

            }
            return Item;
        }
    }
}
