using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqExp = System.Linq.Expressions;

namespace AutoMapper.ExpressionsMapping
{
    public class ConditionalExpression : BaseExpression
    {
        public override object GetValue(Expression expression, object sourceObj)
        {
            LinqExp.ConditionalExpression conditionalExpression = expression as LinqExp.ConditionalExpression;

            Expression test = conditionalExpression.Test;
            bool t =  (bool)GetExpressionValue(test, sourceObj);

            return t ? GetExpressionValue(conditionalExpression.IfTrue, sourceObj) : GetExpressionValue(conditionalExpression.IfFalse, sourceObj);
        }
    }
}
