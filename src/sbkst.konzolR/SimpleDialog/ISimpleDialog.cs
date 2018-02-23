using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbkst.konzolR.SimpleDialog.OptionTypes;
namespace sbkst.konzolR.SimpleDialog
{
    public interface ISimpleDialog<T>
    {
        /// <summary>
        /// bound object
        /// </summary>
        T Item
        {
            get;
        }

        /// <summary>
        /// Adds a option to the dialog
        /// caution: use this only if you had to implment your own dialog steps!
        /// </summary>
        /// <param name="o">dialog step</param>
        void AddOption(DialogOptionBase o);

        /// <summary>
        /// starts the dialog
        /// </summary>
        /// <returns>the bound object</returns>
        T Run();
    }
}
