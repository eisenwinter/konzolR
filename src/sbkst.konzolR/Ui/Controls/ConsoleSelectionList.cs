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
        private readonly int _yOffset = 0;
        private IEnumerable<SelectValue<T>> _selectValues;
        public T SelectedItem { get; private set; }

        private int IndexOfSelected
        {
            get
            {
                if (SelectedItem != null)
                {
                    return _selectValues.Select(s => s.Data).ToList().IndexOf(SelectedItem);
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
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.White;

        public ConsoleSelectionList(string id, IEnumerable<SelectValue<T>> selectValues) : base(id)
        {
            _selectValues = selectValues;
            this.Size.Height = (ushort)((selectValues.NotNullAndAny() && selectValues.Count() < 3) ? selectValues.Count() : 3);
            this.CursorVisible = false;
            if (selectValues.NotNullAndAny())
            {
                this.SelectedItem = _selectValues.FirstOrDefault().Data;
            }
            else
            {
                this.SelectedItem = default(T);
            }
         
        }

        public override void Focus()
        {
            base.Focus();
            this.Valid = false;
        }

        public override IRenderProvider GetProvider()
        {
            //TODO: Viewbox INDEX
            return new ConsoleSelectionRenderEngine(this, Viewbox, IndexOfSelected, BackgroundColor);
        }

        public override bool KeyReceived(ControlKeyReceived controlKey)
        {
            //TODO: Finish
            if (controlKey.Key == ConsoleKey.LeftArrow)
            {
                this.SelectedItem = _selectValues.Select(s => s.Data).Previous(SelectedItem, true);
                return true;
            }
            if (controlKey.Key == ConsoleKey.RightArrow || controlKey.Key == ConsoleKey.Spacebar)
            {
                this.SelectedItem = _selectValues.Select(s => s.Data).Next(SelectedItem, true);
                return true;
            }
            return false;
        }
    }
}
