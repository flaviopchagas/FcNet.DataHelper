using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FcNet.DataHelper.Demo
{
    public static class ExpressionExtensions
    {
        public static string ToMSSqlString(this Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                    var add = expression as BinaryExpression;
                    return add.Left.ToMSSqlString() + " + " + add.Right.ToMSSqlString();
                case ExpressionType.Constant:
                    var constant = expression as ConstantExpression;
                    if (constant.Type == typeof(string))
                        return "N'" + constant.Value.ToString().Replace("'", "''") + "'";
                    return constant.Value.ToString();
                case ExpressionType.Equal:
                    var equal = expression as BinaryExpression;
                    return equal.Left.ToMSSqlString() + " = " +
                           equal.Right.ToMSSqlString();
                case ExpressionType.Lambda:
                    var l = expression as LambdaExpression;
                    return l.Body.ToMSSqlString();
                case ExpressionType.MemberAccess:
                    var memberaccess = expression as MemberExpression;
                    // todo: if column aliases are used, look up ColumnAttribute.Name
                    return "[" + memberaccess.Member.Name + "]";

                case ExpressionType.AndAlso:
                    var andAlso = expression as BinaryExpression;
                    return ToMSSqlString(andAlso.Left) + " AND " + ToMSSqlString(andAlso.Right);

                case ExpressionType.OrElse:
                    var orElse = expression as BinaryExpression;
                    return ToMSSqlString(orElse.Left) + " OR " + ToMSSqlString(orElse.Right);
                case ExpressionType.Call:
                    var call = expression as MethodCallExpression;

                    return call.ToString() + " OR " + call.Arguments[0];
            }

            return "";
            //throw new NotImplementedException(
            //  expression.GetType().ToString() + " " +
            //  expression.NodeType.ToString());
        }
    }
}
