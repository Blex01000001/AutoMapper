using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqExp = System.Linq.Expressions;

namespace AutoMapper.ExpressionsMapping
{
    public class ConstantExpression : BaseExpression
    {
        public override object GetValue(Expression expression, object sourceObj)
        {
            LinqExp.ConstantExpression constantExpression = expression as LinqExp.ConstantExpression;
            return constantExpression.Value;
        }
    }
}
