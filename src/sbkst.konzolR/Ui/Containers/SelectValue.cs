using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Containers
{
    public class SelectValue<T>
    {
        public SelectValue()
        {

        }

        public SelectValue(T data, string display)
        {
            Data = data;
            Display = display;
        }

       public T Data { get; set; }

       public string Display { get; set; }
    }
}
