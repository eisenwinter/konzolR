﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Controls
{
    /// <summary>
    /// inherit if the control can receive focus 
    /// </summary>
    public interface IFocusableControl
    {
        void Focus();
        void Blur();
    }
}
