using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace sbkst.konzolR.Ui.Controls
{
    public abstract class FocusableConsoleControl : ConsoleControl, IFocusableControl, INotifyPropertyChanged
    {
        protected FocusableConsoleControl(string id) : base(id)
        {
        }

        private bool _focus;
        public Boolean HasFocus
        {
            get
            {
                return _focus;
            }
            set
            {
                _focus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasFocus"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Blur()
        {
            this.HasFocus = false;
        }

        public void Focus()
        {
            this.HasFocus = true;
        }

      
    }
}
