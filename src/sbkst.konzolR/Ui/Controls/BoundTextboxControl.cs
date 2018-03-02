using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using sbkst.konzolR.Ui.Rendering;
using System.Reflection;
using sbkst.konzolR.Ui.Behavior;
using System.ComponentModel;
namespace sbkst.konzolR.Ui.Controls
{
    public class BoundTextboxControl<T> : ListeningConsoleControl
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
            set
            {
                _propertyInfo.SetValue(_boundObject, value, null);
            }
        }
        

        public BoundTextboxControl(string id,T obj, Expression<Func<T,string>> field) : base(id)
        {
            _boundObject = obj;
            _boundProperty = field.Compile();
            MemberExpression body = (field.Body.NodeType == ExpressionType.Convert) ? (MemberExpression)((UnaryExpression)field.Body).Operand : (MemberExpression)field.Body;
            string propname = body.Member.Name;
            _propertyInfo = obj.GetType().GetProperty(propname);
           
        }

        public override IRenderProvider GetProvider()
        {
            return new ControlRenderEngine(this, Value);
        }

//        public bool KeyReceived(ControlKeyReceived keyReceived)
//        {
//#if DEBUG
//            System.Diagnostics.Trace.WriteLine(String.Format("Control {0} [{1},{2}|{3},{4}] received key {5}",this.Id, this.Position.X,this.Position.Y,this.Size.Width,this.Size.Height,keyReceived.Key));
//#endif
           
//            return false;
//        }
    }
}
