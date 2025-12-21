using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqExp = System.Linq.Expressions;

namespace AutoMapper.ExpressionsMapping
{
    public class ParameterExpression : BaseExpression
    {
        public override object GetValue(Expression expression, object sourceObj)
        {
            LinqExp.ParameterExpression parameterExpression = expression as LinqExp.ParameterExpression;


            //var value = sourceObj.GetType().GetProperty(parameterExpression.Name).GetValue(sourceObj);
            return sourceObj;

        }
    }
}
