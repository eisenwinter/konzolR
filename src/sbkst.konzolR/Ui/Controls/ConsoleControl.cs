﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Controls
{
    abstract class ConsoleControl
    {
        public virtual Boolean IsReadonly { get; protected set; }
    }
}
