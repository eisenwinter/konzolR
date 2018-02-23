using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.SimpleDialog.OptionTypes
{
    public abstract class DialogOptionBase
    {
        public string Question { get; protected set; }

        public abstract void Read();

        protected object _item;
    }

    public abstract class DialogOptionBase<T> : DialogOptionBase
    {
        protected new T _item;

    }
}
