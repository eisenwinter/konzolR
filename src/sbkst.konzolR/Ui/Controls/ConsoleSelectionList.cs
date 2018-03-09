using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.Ui.Behavior;
using sbkst.konzolR.Ui.Rendering;
using sbkst.konzolR.Ui.Containers;
using sbkst.konzolR.Ui.Utility;
namespace sbkst.konzolR.Ui.Controls
{
    public class ConsoleSelectionList<T> : ListeningConsoleControl
    {
        private int _yOffset = 0;
        IEnumerable<SelectValue<T>> _selectValues;

        private T _selectedItem;
        public T SelectedItem
        {
            get
            {
                return _selectedItem;
            }
        }

        private int IndexOfSelected
        {
            get
            {
                if (_selectedItem != null)
                {
                    return _selectValues.Select(s => s.Data).ToList().IndexOf(_selectedItem);
                }
                return 0;
            }
        }

        private string[] Viewbox
        {
            get
            {
                if (_selectValues.NotNullAndAny())
                {
                    return _selectValues.Select(s => s.Display).Skip(_yOffset).ToArray();
                }
                return new string[0];
            }
        }

        private ConsoleColor _backgroundColor = ConsoleColor.White;
        public ConsoleColor BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
            }
        }
        public ConsoleSelectionList(string id, IEnumerable<SelectValue<T>> selectValues) : base(id)
        {
            _selectValues = selectValues;
            this.Size.Height = (ushort)((selectValues.NotNullAndAny() && selectValues.Count() < 3) ? selectValues.Count() : 3);
            this.CursorVisible = false;
            if (selectValues.NotNullAndAny())
            {
                this._selectedItem = _selectValues.FirstOrDefault().Data;
            }
            else
            {
                this._selectedItem = default(T);
            }
        }

        public override IRenderProvider GetProvider()
        {
            //TODO: Viewbox INDEX
            return new ConsoleSelectionRenderEngine(this, Viewbox, IndexOfSelected, _backgroundColor);
        }

        public override bool KeyReceived(ControlKeyReceived controlKey)
        {
            //TODO: Finish
            if (controlKey.Key == ConsoleKey.LeftArrow)
            {
                this._selectedItem = _selectValues.Select(s => s.Data).Previous(_selectedItem, true);
                return true;
            }
            if (controlKey.Key == ConsoleKey.RightArrow)
            {
                this._selectedItem = _selectValues.Select(s => s.Data).Next(_selectedItem, true);
                return true;
            }
            return false;
        }
    }
}
