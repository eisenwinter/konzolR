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
                if (_boundObject != null)
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


        public BoundTextboxControl(string id, T obj, Expression<Func<T, string>> field) : base(id)
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



        public override bool KeyReceived(ControlKeyReceived controlKey)
        {
#if DEBUG
            System.Diagnostics.Trace.WriteLine(String.Format("Control {0} [{1},{2}|{3},{4}] received key {5}", this.Id, this.Position.X, this.Position.Y, this.Size.Width, this.Size.Height, controlKey.Key));
#endif
           

            if(!HookStandardKeys(controlKey, () =>
            {
                if(CursorPosition.X > 0 && CursorPosition.X < Value.Length)
                {
                    var sb = new StringBuilder(this.Value);
                    sb.Remove(CursorPosition.X, 1);
                    this.Value = sb.ToString();
                    this._currentSize = Value.Length;
                }
            }, () =>
            {
                var sb = new StringBuilder(this.Value);
                sb.Remove(CursorPosition.X, 1);
                this.Value = sb.ToString();
                this._currentSize = Value.Length;

            }, () =>
            {
                this.Blur();
            }, () =>
            {

            }))
            {

                if (CursorPosition.X < Value.Length)
                {
                    var sb = new StringBuilder(this.Value);
                    sb[CursorPosition.X] = controlKey.Character;
                    this.Value = sb.ToString();
                    if(CursorPosition.X < this.Size.Width - 1)
                    {
                        CursorPosition.X++;
                    }
                }
                else
                {
                    this.Value = Value + controlKey.Character;
                    this._currentSize = Value.Length;
                    if (CursorPosition.X < this.Size.Width - 1)
                    {
                        CursorPosition.X++;
                    }
                }

            }

            return true;
        }
    }
}
