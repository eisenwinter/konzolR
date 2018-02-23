using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
namespace sbkst.konzolR.SimpleDialog.OptionTypes
{
    abstract class ReflectedDialogOptionBase<T, R> : DialogOptionBase<T>
    {
        public Expression<Func<T, R>> Expression { get; protected set; }
        protected void SetValue(R arg)
        {
            MemberExpression body = (Expression.Body.NodeType == ExpressionType.Convert) ? (MemberExpression)((UnaryExpression)Expression.Body).Operand : (MemberExpression)Expression.Body;
            string propname = body.Member.Name;
            PropertyInfo propertyInfo = _item.GetType().GetProperty(propname);
            if (propertyInfo.PropertyType == typeof(R))
            {
                propertyInfo.SetValue(_item, arg, null);
            }
        }
    }
}
