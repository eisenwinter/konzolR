using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using sbkst.konzolR.Ui.Rendering;
using System.Reflection;
namespace sbkst.konzolR.Ui.Controls
{
    public class BoundTextboxControl<T> : ConsoleControl, IFocusableControl
    {
        T _boundObject;
        Func<T, string> _boundProperty;

        public T BoundObject
        {
            get
            {
                return _boundObject;
            }
        }

        public string Value
        {
            get
            {
                if(_boundObject != null)
                {
                    return _boundProperty(_boundObject);
                }
                return null;
            }
        }
        

        public BoundTextboxControl(string id,T obj, Expression<Func<T,string>> field) : base(id)
        {
            _boundObject = obj;
            _boundProperty = field.Compile();
        }

        public override IRenderProvider GetProvider()
        {
            return new ControlRenderEngine(this, Value);
        }
    }
}
