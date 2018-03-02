using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Rendering;
using System.ComponentModel;
namespace sbkst.konzolR.Ui.Controls
{
    public abstract class ListeningConsoleControl : FocusableConsoleControl, IListeningControl
    {
        public ListeningConsoleControl(string id) : base(id)
        {
            this.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == "HasFocus")
                {
                    if (this.HasFocus)
                    {
                        //hook input stream
                    }
                    else
                    {
                        //release input stream
                    }
                }
            };
        }
    }
}
