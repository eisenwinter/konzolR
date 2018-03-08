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

        private bool _cursorVisible = true;

        public Boolean CursorVisible
        {
            get
            {
                return _cursorVisible;
            }
            protected set
            {
                _cursorVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CursorVisible"));
            }
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

        public virtual void Blur()
        {
            this.HasFocus = false;
        }

        public virtual void Focus()
        {
            this.HasFocus = true;
        }

      
    }
}
