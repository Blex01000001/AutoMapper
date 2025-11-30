using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.ExpressionsMapping
{
    public class MemberType : BaseExpression
    {
        public override object GetValue(Expression expression, object sourceObj)
        {
            MemberExpression memExp = (MemberExpression)expression;
            var value = sourceObj.GetType().GetProperty(memExp.Member.Name).GetValue(sourceObj);
            return value;
        }
    }
}
