using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Demo
{
    class InputArgs
    {
        public Boolean Force { get; set; }

        public Boolean OutputToFileSupplied { get; set; }

        public string FilePath { get; set; }

        public Boolean UsersSupplied { get; set; }

        public string[] UserNames { get; set; }


        public Boolean HexSupplied { get; set; }
        public string  Hex { get; set; }
    }
}
