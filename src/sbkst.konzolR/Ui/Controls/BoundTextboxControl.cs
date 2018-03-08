using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using sbkst.konzolR.Ui.Rendering;
using System.Reflection;
using sbkst.konzolR.Ui.Behavior;
using sbkst.konzolR.Ui.Utility;
using System.ComponentModel;
namespace sbkst.konzolR.Ui.Controls
{
    public class BoundTextboxControl<T> : ConsoleTextbox
    {
        T _boundObject;
        Func<T, string> _boundProperty;
        PropertyInfo _propertyInfo;

        public T BoundObject
        {
            get
            {
                return _boundObject;
            }
        }

        public override string Value
        {
            get
            {
                if (_boundObject != null)
                {
                    return _boundProperty(_boundObject);
                }
                return null;
            }
            set
            {
                _propertyInfo.SetValue(_boundObject, value, null);
                if(value != null)
                {
                    this._currentSize = Value.Length;
                }
            }
        }


        public BoundTextboxControl(string id, T obj, Expression<Func<T, string>> field) : base(id,"")
        {
            _boundObject = obj;
            _boundProperty = field.Compile();
            MemberExpression body = (field.Body.NodeType == ExpressionType.Convert) ? (MemberExpression)((UnaryExpression)field.Body).Operand : (MemberExpression)field.Body;
            string propname = body.Member.Name;
            _propertyInfo = obj.GetType().GetProperty(propname);
            if (!String.IsNullOrEmpty(_boundProperty(obj)))
            {
                this._currentSize = Value.Length;
            }
        }

        public override IRenderProvider GetProvider()
        {
            return new ControlRenderEngine(this, Value);
        }



      
    }
}
