using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqExp = System.Linq.Expressions;

namespace AutoMapper.ExpressionsMapping
{
    public class UnaryExpression : BaseExpression
    {
        public override object GetValue(Expression expression, object sourceObj)
        {
            LinqExp.UnaryExpression unaryExpression = expression as LinqExp.UnaryExpression;
            return GetExpressionValue(unaryExpression.Operand, sourceObj);
        }
    }
}
