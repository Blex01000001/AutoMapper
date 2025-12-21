using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LinqExp = System.Linq.Expressions;

namespace AutoMapper.ExpressionsMapping
{
    public class MethodCallExpression : BaseExpression
    {
        public override object GetValue(Expression expression, object sourceObj)
        {
            LinqExp.MethodCallExpression methodExp = expression as LinqExp.MethodCallExpression;
            MethodInfo parseMethod = methodExp.Method;
            object[] args = methodExp.Arguments.Select(x => GetExpressionValue(x, sourceObj)).ToArray();
            var res = parseMethod.Invoke(methodExp.Object, args);
            return res;
        }
    }
}
